using Gem.Diagnostics;
using Prism.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Text;

namespace Gem
{
    public class GemLogger : ILoggerFacade
    {
        public void Log(string message, Category category, Priority priority)
        {
            string format = "[{0}] {1}: {2} - {3}";

            string messageToLog = string.Format(CultureInfo.InvariantCulture, format, DateTime.Now.ToString(),
                                                category.ToString().ToUpper(), message, priority);

            Debug.WriteLine(messageToLog);
        }
    }
}
