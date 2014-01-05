using System;
using System.Net;

namespace Woofy.Core.SystemProxies
{
    public class CookieAwareWebClient : WebClient
    {
        private readonly CookieContainer container = new CookieContainer();

        protected override WebRequest GetWebRequest(Uri address)
        {
            var request = base.GetWebRequest(address);
            if (request is HttpWebRequest)
            {
                (request as HttpWebRequest).CookieContainer = container;
            }
            return request;
        }
    }
}