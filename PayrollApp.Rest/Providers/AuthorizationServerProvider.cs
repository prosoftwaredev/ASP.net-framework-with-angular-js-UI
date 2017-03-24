using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using PayrollApp.Core.Data.System;
using PayrollApp.Rest.Clients;
using PayrollApp.Rest.Helpers;
using PayrollApp.Service.IServices;
using PayrollApp.Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PayrollApp.Rest.Providers
{
    public class AuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        private readonly IAccountService _accountService;
        private readonly ILastLoginService _lastLoginService;

        public AuthorizationServerProvider(IAccountService accountService, ILastLoginService lastLoginService)
        {
            _lastLoginService = lastLoginService;
            _accountService = accountService;
        }

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {

            string clientId;
            string clientSecret;
            Client client = null;

            if (!context.TryGetBasicCredentials(out clientId, out clientSecret))
            {
                context.TryGetFormCredentials(out clientId, out clientSecret);
            }

            if (context.ClientId == null)
            {
                //Remove the comments from the below line context.SetError, and invalidate context 
                //if you want to force sending clientId/secrects once obtain access tokens. 
                context.Validated();
                //context.SetError("invalid_clientId", "ClientId should be sent.");
                return Task.FromResult<object>(null);
            }

            Client clientRepo = new Client();
            List<Client> clientList = clientRepo.ClientsList;
            client = clientList.SingleOrDefault(x => x.Id == context.ClientId);

            if (client == null)
            {
                context.SetError("invalid_clientId", string.Format("Client '{0}' is not registered in the system.", context.ClientId));
                return Task.FromResult<object>(null);
            }

            if (client.ApplicationType == ApplicationTypes.Android)
            {
                if (string.IsNullOrWhiteSpace(clientSecret))
                {
                    context.SetError("invalid_clientId", "Client secret should be sent.");
                    return Task.FromResult<object>(null);
                }
                else
                {
                    if (client.Secret != CommonHelper.GetHash(clientSecret))
                    {
                        context.SetError("invalid_clientId", "Client secret is invalid.");
                        return Task.FromResult<object>(null);
                    }
                }
            }

            if (!client.Active)
            {
                context.SetError("invalid_clientId", "Client is inactive.");
                return Task.FromResult<object>(null);
            }

            context.OwinContext.Set<string>("as:clientAllowedOrigin", client.AllowedOrigin);
            context.OwinContext.Set<string>("as:clientRefreshTokenLifeTime", client.RefreshTokenLifeTime.ToString());

            context.Validated();
            return Task.FromResult<object>(null);
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            try
            {
                var allowedOrigin = context.OwinContext.Get<string>("as:clientAllowedOrigin");

                string scope = null;

                Dictionary<string, string> body = context.Request.GetBodyParameters();

                body.TryGetValue("scope", out scope);

                if (allowedOrigin == null) allowedOrigin = "*";

                context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { allowedOrigin });

                using (AccountService _accountService = new AccountService())
                {

                    User gotUser = new User();

                    gotUser = await _accountService.GetUserByEmail(context.UserName);

                    if (gotUser.UserID == 0)
                    {
                        context.SetError("This email not registered with us...");
                        return;
                    }
                    else
                        if (gotUser.UserID > 0)
                        {
                            if (gotUser.IsDelete == true)
                            {
                                context.SetError("Your account was deleted by admin !!!");
                                return;
                            }
                            else if (gotUser.IsEnable == false)
                            {
                                context.SetError("Your account is disable by admin !!!");
                                return;
                            }

                            User user = new User();

                            string pasword = await _accountService.GetPasswordByEmail(context.UserName);
                            string hashPasword;

                            if (!string.IsNullOrWhiteSpace(pasword))
                            {
                                hashPasword = CommonHelper.GetSha1HashCode(context.Password);

                                if (hashPasword == pasword)
                                {
                                    user = await _accountService.GetEnabledAndVerifyUserByEmailAndPassword(context.UserName, hashPasword);

                                    if (user.UserID == 0)
                                    {
                                        context.SetError("Email or password is incorrect...");
                                        return;
                                    }
                                    else if (user.IsDelete == true)
                                    {
                                        context.SetError("Your account was deleted by admin !!!");
                                        return;
                                    }
                                    else
                                    {
                                        string firstName = user.Firstname + " " + user.Lastname;
                                        string id = Convert.ToString(user.UserID);
                                        string roles = string.Empty;
                                        string img = user.Picture;

                                        var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                                        identity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));
                                        identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, Convert.ToString(user.UserID)));

                                        var userRoles = await _accountService.GetUserRolesByUserId(user.UserID);
                                        foreach (var role in userRoles)
                                        {
                                            identity.AddClaim(new Claim(ClaimTypes.Role, role.RoleID.ToString()));
                                            roles = roles + role.RoleID.ToString();
                                        }

                                        var last = await _lastLoginService.GetLastByUserID(user.UserID);

                                        var props = new AuthenticationProperties(new Dictionary<string, string>
                                    {
                                        { 
                                            "as:client_id", (context.ClientId == null) ? string.Empty : context.ClientId
                                        },
                                        { 
                                            "userName", context.UserName
                                        },
                                        {
                                            "firstName", firstName
                                        },
                                        {
                                            "id", id
                                        },
                                        {
                                            "roles", roles
                                        },
                                        {
                                            "img", img == null ? "NA" : img
                                        },
                                        {
                                            "lastLogin", last.LastLoginID > 0 ? last.DateTime.ToString() + " ago on IP: " + last.IPAddress.ToString() : ""
                                        }
                                    });

                                        if (body["scope"].ToString() == "true")
                                        {
                                            //props.IssuedUtc = DateTime.Now;
                                            props.ExpiresUtc = DateTime.UtcNow.Add(TimeSpan.FromDays(365));
                                        }
                                        else
                                        {
                                            //props.IssuedUtc = DateTime.Now;
                                            props.ExpiresUtc = DateTime.UtcNow.Add(TimeSpan.FromMinutes(1));
                                        }

                                        var ticket = new AuthenticationTicket(identity, props);
                                        bool validate = context.Validated(ticket);
                                        string ip = context.Request.RemoteIpAddress == "::1" ? "127.0.0.1" : context.Request.RemoteIpAddress;

                                        if (validate)
                                        {
                                            LastLogin lastLogin = new LastLogin
                                            {
                                                UserID = user.UserID,
                                                DateTime = DateTime.UtcNow,
                                                IPAddress = ip
                                            };
                                            await _lastLoginService.Create(lastLogin);
                                        }
                                    }
                                }
                                else
                                {
                                    context.SetError("Email or password is incorrect...");
                                    return;
                                }
                            }
                        }
                }
            }
            catch (Exception ex)
            {
                context.SetError(ex.Message + "\n\n\n" + ex.StackTrace);
            }
        }

        public override Task GrantRefreshToken(OAuthGrantRefreshTokenContext context)
        {
            var originalClient = context.Ticket.Properties.Dictionary["as:client_id"];
            var currentClient = context.ClientId;

            if (originalClient != currentClient)
            {
                context.SetError("invalid_clientId", "Refresh token is issued to a different clientId.");
                return Task.FromResult<object>(null);
            }

            // Change auth ticket for refresh token requests
            var newIdentity = new ClaimsIdentity(context.Ticket.Identity);

            var newClaim = newIdentity.Claims.FirstOrDefault(c => c.Type == "newClaim");
            if (newClaim != null)
            {
                newIdentity.RemoveClaim(newClaim);
            }
            newIdentity.AddClaim(new Claim("newClaim", "newValue"));

            var newTicket = new AuthenticationTicket(newIdentity, context.Ticket.Properties);
            context.Validated(newTicket);

            return Task.FromResult<object>(null);
        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            return Task.FromResult<object>(null);
        }
    }
}