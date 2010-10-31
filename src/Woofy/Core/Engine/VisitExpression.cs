using System;
using System.Collections.Generic;
using Woofy.Core.Infrastructure;
using Woofy.Core.SystemProxies;
using Woofy.Flows.ApplicationLog;

namespace Woofy.Core.Engine
{
    public class VisitExpression : BaseExpression
    {
        private readonly IPageParser parser;
        private readonly IWebClientProxy webClient;
        private readonly IApplicationController applicationController;

        public VisitExpression(IPageParser parser, IWebClientProxy webClient, IAppLog appLog, IApplicationController applicationController)
            : base(appLog)
        {
            this.parser = parser;
            this.applicationController = applicationController;
            this.webClient = webClient;
        }

        public override IEnumerable<object> Invoke(object argument, Context context)
        {
            if (string.IsNullOrEmpty(context.PageContent))
            {
                ReportVisitingPage(context, context.CurrentAddress);
                context.PageContent = webClient.DownloadString(context.CurrentAddress);
                yield return context.CurrentAddress;
            }

            var regex = (string)argument;
            do
            {
                var links = parser.RetrieveLinksFromPage(context.PageContent, regex, context.CurrentAddress);
                ReportLinksFound(context, links);
                if (links.Length == 0)
                    yield break;

                var link = links[0];
                ReportVisitingPage(context, link);

                context.CurrentAddress = link;
                context.PageContent = webClient.DownloadString(link);
                yield return link;
            }
            while (true);
        }

        private void ReportLinksFound(Context context, Uri[] links)
        {
            Log(context, "Found {0} links", links.Length);
        }

        private void ReportVisitingPage(Context context, Uri page)
        {
            Log(context, "{0}", page);
            applicationController.Raise(new CurrentPageChanged(context.ComicId, page));
        }

        protected override string ExpressionName
        {
            get { return "visit"; }
        }
    }
}