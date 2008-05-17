﻿using System;
using System.Collections.Generic;
using System.Text;

using Woofy.Entities;
using System.Net;
using Woofy.Other;
using System.IO;
using System.Text.RegularExpressions;
using System.Web;

namespace Woofy.Services
{
    public class PageParseService
    {
        private WebClientWrapper _webClient;

        #region Constructor
        public PageParseService(WebClientWrapper webClient)
        {
            _webClient = webClient;
        }

        public PageParseService()
            : this(new WebClientWrapper())
        {
        }
        #endregion

        public virtual Uri RetrieveFaviconAddressFromPage(Uri address)
        {            
            string pageContent = "";
            try 
            { 
                pageContent = _webClient.DownloadString(address); 
            }
            catch (WebException) 
            {
                return null; 
            }

            Uri[] links = RetrieveLinksFromPageByRegex(ApplicationSettings.FaviconRegex, pageContent, address);

            if (links.Length > 0)
                return links[0];

            Uri defaultFaviconAddress = new Uri(address.Scheme + Uri.SchemeDelimiter + address.Authority + "/favicon.ico");
            Stream defaultFaviconStream = null;

            try
            {
                defaultFaviconStream = _webClient.OpenRead(defaultFaviconAddress);
            }
            catch (WebException)
            {
            }
            finally
            {
                if (defaultFaviconStream != null)
                    defaultFaviconStream.Dispose();
            }

            if (defaultFaviconStream != null)
                return defaultFaviconAddress;

            return null;
        }        

        public virtual Uri[] RetrieveLinksFromPageByRegex(string regex, string pageContent, Uri currentUri)
        {
            List<Uri> links = new List<Uri>();
            MatchCollection matches = Regex.Matches(pageContent, regex, ApplicationSettings.RegexOptions);

            foreach (Match match in matches)
            {
                string capturedContent;
                if (match.Groups[ApplicationSettings.ContentGroup].Success)
                    capturedContent = match.Groups[ApplicationSettings.ContentGroup].Value;
                else
                    capturedContent = match.Value;

                //just in case someone html-encoded the link; happened with Gone With The Blastwave;
                capturedContent = HttpUtility.HtmlDecode(capturedContent);

                Uri newUri;
                if (Uri.TryCreate(capturedContent, UriKind.Absolute, out newUri))
                    links.Add(newUri);
                else 
                    links.Add(new Uri(currentUri, capturedContent));
            }

            return links.ToArray();
        }

        public virtual Uri GetLatestPageOrStartAddress(Uri startAddress, string latestPageRegex)
        {
            if (string.IsNullOrEmpty(latestPageRegex))
                return startAddress;

            string pageContent = _webClient.DownloadString(startAddress);
            
            Uri[] addresses = RetrieveLinksFromPageByRegex(latestPageRegex, pageContent, startAddress);
            if (addresses.Length == 0)
                return startAddress;

            return addresses[0];
        }        
    }
}