using System;
using System.Collections.Generic;
using System.Text;

namespace Woofy.Core
{
    public enum DownloadOutcome
    {
        None = 0,        
        NoStripMatchesRuleBroken,
        MultipleStripMatchesRuleBroken,
        Cancelled,
        Error,
        Successful
    }
}