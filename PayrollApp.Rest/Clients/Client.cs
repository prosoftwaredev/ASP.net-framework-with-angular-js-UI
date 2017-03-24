using PayrollApp.Rest.Helpers;
using System.Collections.Generic;

namespace PayrollApp.Rest.Clients
{
    public class Client
    {
        public string Id { get; set; }
        public string Secret { get; set; }
        public string Name { get; set; }
        public ApplicationTypes ApplicationType { get; set; }
        public bool Active { get; set; }
        public int RefreshTokenLifeTime { get; set; }
        public string AllowedOrigin { get; set; }

        public List<Client> ClientsList
        {
            get
            {
                List<Client> clientsList = new List<Client> 
                      { 
                       new Client { Id = "Wilkinson", 
                                              Secret= CommonHelper.GetHash("PayBackAngularApp@123"), 
                                              Name="AngularJSApplication", 
                                              ApplicationType =  ApplicationTypes.Angular, 
                                              Active = true, 
                                              RefreshTokenLifeTime = 60, 
                                              AllowedOrigin = "http://localhost:59535"
                                           },
                       new Client { Id = "2", 
                                             Secret=CommonHelper.GetHash("PayBackAndroidApp@123"), 
                                             Name="AndroidApplication", 
                                             ApplicationType =ApplicationTypes.Android, 
                                             Active = true, 
                                             RefreshTokenLifeTime = 14400, 
                                             AllowedOrigin = "*"
                                          }
                  };

                return clientsList;
            }
        }
    }

    public enum ApplicationTypes
    {
        Angular = 1,
        Android = 2
    };
}