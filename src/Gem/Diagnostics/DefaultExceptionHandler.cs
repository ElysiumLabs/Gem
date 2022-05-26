using Prism.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gem.Diagnostics
{
    public class DefaultExceptionHandler : ExceptionHandler
    {
        //private readonly ILoggerFacade logger;

        public DefaultExceptionHandler(
          //  ILoggerFacade logger
            )
        {
            //this.logger = logger;
        }
        public override void Handle(Exception exception)
        {
          //  logger.Log(exception.Message, Category.Exception, Priority.High);
            throw exception;
        }
    }
}
