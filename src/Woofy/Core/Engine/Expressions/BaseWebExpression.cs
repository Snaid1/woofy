using System.Net;
using Woofy.Core.SystemProxies;
using Woofy.Flows.ApplicationLog;

namespace Woofy.Core.Engine.Expressions
{
    public abstract class BaseWebExpression : BaseExpression
    {
        protected readonly IWebClientProxy webClient;

        protected BaseWebExpression(IAppLog appLog, IWebClientProxy webClient) : base(appLog)
        {
            this.webClient = webClient;
        }

        protected bool ContentIsEmpty(Context context)
        {
            return string.IsNullOrEmpty(context.PageContent);
        }

        protected void InitializeContent(Context context)
        {
            Log(context, "starting at {0}", context.CurrentAddress);

            try
            {
                context.PageContent = webClient.DownloadString(context.CurrentAddress);
            }
            catch (WebException ex)
            {
                Warn(context, ex.Message);
            }
        }

        protected bool TryToEnsureThatContentIsInitialized(Context context)
        {
            if (ContentIsEmpty(context))
                InitializeContent(context);

            //in case the initialization fails
            if (ContentIsEmpty(context))
                return false;

            return true;
        }

        protected void ReportBadRegex(Context context, string regex, string link)
        {
            Warn(context, "found '{0}' using the regex '{1}', but it's not a valid link", link, regex);
        }

        protected void ReportContentEmpty(Context context)
        {
            Warn(context, "content is empty - skipping");
        }
    }
}