using System;
using System.Collections.Generic;
using System.Text;

namespace Gem.Diagnostics
{
    public abstract class ExceptionHandler
    {
        public abstract void Handle(Exception exception);
    }

    
}
