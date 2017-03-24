using System;
using System.Configuration;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Helpers;

namespace PayrollApp.Rest.Helpers
{
    /// <summary>
    /// Common Helper
    /// </summary>
    public partial class CommonHelper
    {
        static string _googleClientId = ConfigurationManager.AppSettings["GoogleClientID"];
        public static string GoogleClientId { get { return _googleClientId; } }

        static string _googleClientSecret = ConfigurationManager.AppSettings["GoogleClientSecret"];
        public static string GoogleClientSecret { get { return _googleClientSecret; } }

        static string _facebookAppId = ConfigurationManager.AppSettings["FacebookAppId"];
        public static string FacebookAppId { get { return _facebookAppId; } }

        static string _facebookAppSecret = ConfigurationManager.AppSettings["FacebookAppSecret"];
        public static string FacebookAppSecret { get { return _facebookAppSecret; } }

        static string _facebookAppToken = ConfigurationManager.AppSettings["FacebookAppToken"];
        public static string FacebookAppToken { get { return _facebookAppToken; } }

        static string _twitterConsumerKey = ConfigurationManager.AppSettings["TwitterConsumerKey"];
        public static string TwitterConsumerKey { get { return _twitterConsumerKey; } }

        static string _twitterConsumerSecret = ConfigurationManager.AppSettings["TwitterConsumerSecret"];
        public static string TwitterConsumerSecret { get { return _twitterConsumerSecret; } }

        static string _restDomain = ConfigurationManager.AppSettings["RestDomain"];
        public static string RestDomain { get { return _restDomain; } }

        /// <summary>
        /// Gets the sh a1 hash string.
        /// </summary>
        /// <param name="strToHash">The string to hash.</param>
        /// <returns></returns>
        public static string GetSha1HashCode(string strToHash)
        {
            string strResult = string.Empty;

            SHA1CryptoServiceProvider sha1Obj = new SHA1CryptoServiceProvider();
            byte[] bytesToHash = Encoding.ASCII.GetBytes(strToHash);
            bytesToHash = sha1Obj.ComputeHash(bytesToHash);

            foreach (Byte b in bytesToHash)
            {
                strResult += b.ToString("x2");
            }

            return strResult;
        }

        /// <summary>
        /// Creates the random password.
        /// </summary>
        /// <param name="passwordLength">Length of the password.</param>
        /// <returns></returns>
        public static string CreateRandomPassword(int passwordLength)
        {
            string allowedChars = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ0123456789!@$?_-";
            char[] chars = new char[passwordLength];
            Random rd = new Random();

            for (int i = 0; i < passwordLength; i++)
            {
                chars[i] = allowedChars[rd.Next(0, allowedChars.Length)];
            }

            return new string(chars);
        }

        /// <summary>
        /// Deletes the target file.
        /// </summary>
        /// <param name="path">The path.</param>
        public static void DeleteTargetFile(string path)
        {
            if (File.Exists(path))
            {
                File.SetAttributes(path, FileAttributes.Normal);
                File.Delete(path);
            }
        }

        /// <summary>
        /// Get current user ip address.
        /// </summary>
        /// <returns>The IP Address</returns>
        public static string GetUserIpAddress()
        {
            var context = System.Web.HttpContext.Current;
            string ip = String.Empty;

            if (context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)
                ip = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
            else if (!String.IsNullOrWhiteSpace(context.Request.UserHostAddress))
                ip = context.Request.UserHostAddress;

            if (ip == "::1")
                ip = "127.0.0.1";

            return ip;
        }


        /// <summary>
        /// Verifies that a uploading file is in valid Image format
        /// </summary>
        /// <author>
        /// Mayur Lohite
        /// </author>
        /// <param name="postedFile">File which is selected for upload</param>
        /// <param name="imageMinBytes">Minimum file size in byte</param>
        /// <param name="imageMaxBytes">Maximum file size in byte</param>
        /// <returns>true if the file is a valid image format and false if it's not</returns>
        public static bool IsValidImageFormat(HttpPostedFileBase postedFile, int imageMinBytes, long imageMaxBytes)
        {

            //-------------------------------------------
            //  Check the image extension
            //-------------------------------------------
            var extension = Path.GetExtension(postedFile.FileName);
            if (extension != null && (extension.ToLower() != ".jpg"
                                                                   && extension.ToLower() != ".png"
                                                                   && extension.ToLower() != ".gif"
                                                                   && extension.ToLower() != ".jpeg"))
            {
                return false;
            }

            //-------------------------------------------
            //  Check the image MIME types
            //-------------------------------------------
            if (postedFile.ContentType.ToLower() != "image/jpg" &&
                        postedFile.ContentType.ToLower() != "image/jpeg" &&
                        postedFile.ContentType.ToLower() != "image/pjpeg" &&
                        postedFile.ContentType.ToLower() != "image/gif" &&
                        postedFile.ContentType.ToLower() != "image/x-png" &&
                        postedFile.ContentType.ToLower() != "image/png")
            {
                return false;
            }



            //-------------------------------------------
            //  Attempt to read the file and check the first bytes
            //-------------------------------------------
            try
            {
                if (!postedFile.InputStream.CanRead)
                {
                    return false;
                }

                if (postedFile.ContentLength < imageMinBytes)
                {
                    return false;
                }

                byte[] buffer = new byte[512];
                postedFile.InputStream.Read(buffer, 0, 512);
                string content = System.Text.Encoding.UTF8.GetString(buffer);
                if (Regex.IsMatch(content, @"<script|<html|<head|<title|<body|<pre|<table|<a\s+href|<img|<plaintext|<cross\-domain\-policy",
                    RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Multiline))
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }

            //-------------------------------------------
            //  Try to instantiate new Bitmap, if .NET will throw exception
            //  we can assume that it's not a valid image
            //-------------------------------------------

            try
            {
                using (var bitmap = new System.Drawing.Bitmap(postedFile.InputStream))
                {
                }
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }


        /// <summary>
        /// Generate file name based on guid
        /// </summary>
        /// <param name="oriFileName">Original filname to get extension</param>
        /// <returns>return the autogenerated filename based on guid</returns>
        public static string GenerateFileNameGuid(string oriFileName, out string firstTwoChars, out string secondTwoChars)
        {
            string extension = Path.GetExtension(oriFileName);
            var fileName = Guid.NewGuid().ToString("N") + extension;
            firstTwoChars = fileName.Substring(3, 2);
            secondTwoChars = fileName.Substring(5, 2);

            fileName = firstTwoChars + "/" + secondTwoChars + "/" + fileName;
            return fileName;
        }

        public static string GetHash(string input)
        {
            HashAlgorithm hashAlgorithm = new SHA256CryptoServiceProvider();

            byte[] byteValue = Encoding.UTF8.GetBytes(input);

            byte[] byteHash = hashAlgorithm.ComputeHash(byteValue);

            return Convert.ToBase64String(byteHash);
        }

        public static string GenerateAntiForgeryToken()
        {
            string cookieToken, formToken;
            AntiForgery.GetTokens(null, out cookieToken, out formToken);
            return cookieToken + ":" + formToken;
        }


        public static string CreateXmlData(string PrevNodes, string Node, string Value)
        {
            string newXmlString = "<" + Node + ">" + Value + "</" + Node + ">";

            return PrevNodes += newXmlString;
        }


        public static string CreateXmlData(string PrevNodes, string Node, bool isOpeningNode)
        {
            string newXmlString = string.Empty;

            if (isOpeningNode)
                newXmlString = "<" + Node + ">";
            else
                newXmlString = "</" + Node + ">";

            return PrevNodes += newXmlString;
        }
    }
}