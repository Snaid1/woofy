using System;
using System.Collections.Generic;
using Woofy.Core.Infrastructure;
using Woofy.Core.SystemProxies;
using Woofy.Flows.ApplicationLog;

namespace Woofy.Core.Engine.Expressions
{
    public class GoToExpression : BaseExpression
    {
        private readonly IAppController appController;
        private readonly IWebClientProxy webClient;

        public GoToExpression(IAppLog appLog, IWebClientProxy webClient, IAppController appController) : base(appLog)
        {
            this.appController = appController;
            this.webClient = webClient;
        }

        public override IEnumerable<object> Invoke(object argument, Context context)
        {
            var link = new Uri((string)argument);
            ReportVisitingPage(link, context);

            context.CurrentAddress = link;
            context.PageContent = webClient.DownloadString(link);
            
            return null;
        }

        private void ReportVisitingPage(Uri page, Context context)
        {
            Log(context, "{0}", page);
            appController.Raise(new CurrentPageChanged(context.ComicId, page));
        }

        protected override string ExpressionName
        {
            get { return Expressions.GoTo; }
        }
    }
}