using Gem.Diagnostics;
using Prism.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Text;

namespace Gem
{
    public class GemLogger : ILogger
    {
        public void Log(string message, IDictionary<string, string> properties)
        {
            string format = "[{0}] {1}: {2} - {3}";

            string messageToLog = string.Format(CultureInfo.InvariantCulture, format, DateTime.Now.ToString(),message);

            Debug.WriteLine(messageToLog);
        }

        public void Report(Exception ex, IDictionary<string, string> properties)
        {
            throw new NotImplementedException();
        }

        public void TrackEvent(string name, IDictionary<string, string> properties)
        {
            throw new NotImplementedException();
        }
    }
}
