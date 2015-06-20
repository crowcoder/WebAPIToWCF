using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WcfContracts;
using WcfServiceProxy;

namespace WcfWebAPI.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        public List<Fish> Get()
        {
            try
            {
                //For development only, bypasses the rejection of the self-signed SSL certificate presented by IIS when using HTTPS
                System.Net.ServicePointManager.ServerCertificateValidationCallback =
    ((sender, certificate, chain, sslPolicyErrors) => true);

                WcfServiceProxy.WcfProxy client = new WcfProxy();
                client.ClientCredentials.Windows.ClientCredential.Domain = "<TheDomain>";
                client.ClientCredentials.Windows.ClientCredential.UserName = "<TheUserName>";
                client.ClientCredentials.Windows.ClientCredential.Password = "<ThePassword>";

                return client.GetFish();

                client.Close();

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }
    }
}
