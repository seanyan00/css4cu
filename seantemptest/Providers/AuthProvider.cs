using System;
using System.Configuration;
using System.Text;
using System.Net;
using System.IO;
using Newtonsoft.Json;

using seantemptest.Models;

namespace seantemptest.Providers
{
    public static class AuthProvider
    {
        public static AuthToken GetAuthToken()
        {
            var ret = new AuthToken();
            var authKey = ConfigurationManager.AppSettings["Auth_ConsumerKey"].ToString();
            var authSecret = ConfigurationManager.AppSettings["Auth_ConsumerSecret"].ToString();
            var authUrl = ConfigurationManager.AppSettings["Auth_Url"].ToString();

            var basicToken = "Basic " +  Convert.ToBase64String(Encoding.UTF8.GetBytes(Uri.EscapeDataString(authKey) 
                                    + ":" 
                                    + Uri.EscapeDataString(authSecret)));

            var req = (HttpWebRequest)WebRequest.Create(authUrl);

            req.Headers.Add("Authorization", basicToken);
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded;charset=UTF-8";
            req.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            using (var sm = req.GetRequestStream())
            {
                byte[] cont = ASCIIEncoding.ASCII.GetBytes("grant_type=client_credentials");
                sm.Write(cont, 0, cont.Length);
            }
            req.Headers.Add("Accept-Encoding", "gzip");
                     
            using ( var rep = req.GetResponse())
            {
                using (var reader = new StreamReader(rep.GetResponseStream()))
                {
                    var temp = reader.ReadToEnd();
                    ret = JsonConvert.DeserializeObject<AuthToken>(temp);
                }
            }
            return ret;
        }
    }
}