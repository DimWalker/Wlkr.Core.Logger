using log4net;
using System;
using System.Collections.Generic;
using System.Text;
using Wlkr.Common.Logger;

namespace Wlkr.Core.Logger
{
    public class Example
    {
        private static ILog logger = Log4netHelper.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public void IntentLogger()
        {
            logger.Info("IntentLogger will show classname: " + typeof(Example));
        }
        public void StaticLogger()
        {
            Log4netHelper.Info("StaticLogger will show classname: " + typeof(Log4netHelper));
        }
    }
}
