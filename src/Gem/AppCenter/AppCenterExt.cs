using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gem.AppCenter
{
    public class AppCenterExt
    {
        public static void Initialize(string android = null, string ios = null, string uwp = null)
        {
            var s = "";

            if (!string.IsNullOrEmpty(android))
            {
                s += "android=" + android + ";";
            }

            if (!string.IsNullOrEmpty(ios))
            {
                s += "ios=" + ios + ";";
            }

            if (!string.IsNullOrEmpty(uwp))
            {
                s += "uwp=" + uwp + ";";
            }

            Microsoft.AppCenter.AppCenter.Start(s, typeof(Analytics), typeof(Crashes));
        }
    }
}
