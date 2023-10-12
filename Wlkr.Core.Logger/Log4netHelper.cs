using log4net;
using log4net.Config;
using System;
using System.IO;
using System.Reflection;
using System.Text;

namespace Wlkr.Common.Logger
{
    #region Sample 1
    public static class Log4netHelper
    {
        public static ILog GetLogger(Type t)
        {
            return LogManager.GetLogger(t);
        }
        private static ILog GetLogger(string name)
        {
            return LogManager.GetLogger(name);
        }

        private static ILog log;

        static Log4netHelper()
        {
            //XmlConfigurator.ConfigureAndWatch(new FileInfo(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile));
            //log = LogManager.GetLogger(typeof(Log4netHelper));

            string cnfg = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "log4net.config");
            if (!File.Exists(cnfg))
            {
                FileStream fs = new FileStream(cnfg, FileMode.CreateNew);
                StreamWriter sw = new StreamWriter(fs, Encoding.UTF8);
                sw.Write(@"<?xml version=""1.0"" encoding=""utf-8"" ?>
<configuration>
	<configSections>
		<section name=""log4net""
		type=""log4net.Config.Log4NetConfigurationSectionHandler, log4net"" />
	</configSections>
	<section name=""log4net"" type=""log4net.Config.Log4NetConfigurationSectionHandler, log4net"" />
	<log4net>
		<!-- OFF, FATAL, ERROR, WARN, INFO, DEBUG, ALL -->
		<!-- Set root logger level to ERROR and its appenders -->
		<root>
			<level value=""ALL"" />
			<appender-ref ref=""SysAppender"" />
			<!--控制台控制显示日志-->
			<appender-ref ref=""consoleApp"" />
		</root>
		<!-- Print only messages of level DEBUG or above in the packages -->
		<logger name=""WebLogger"">
			<level value=""DEBUG"" />
		</logger>
		<appender name=""SysAppender"" type=""log4net.Appender.RollingFileAppender,log4net"">
			<param name=""File"" value=""Logs/"" />
			<param name=""AppendToFile"" value=""true"" />
			<param name=""RollingStyle"" value=""Date"" />
			<param name=""DatePattern"" value=""&quot;Logs_&quot;yyyyMMdd&quot;.txt&quot;"" />
			<param name=""StaticLogFileName"" value=""false"" />
			<layout type=""log4net.Layout.PatternLayout,log4net"">
				<param name=""ConversionPattern"" value=""%d [%t] %-5p %c - %m%n"" />
			</layout>
		</appender>
		<appender name=""consoleApp"" type=""log4net.Appender.ConsoleAppender,log4net"">
			<layout type=""log4net.Layout.PatternLayout,log4net"">
				<param name=""ConversionPattern"" value=""%d [%t] %-5p %c - %m%n"" />
			</layout>
		</appender>
	</log4net>
</configuration>");
                sw.Close();
                fs.Close();
            }
            XmlConfigurator.ConfigureAndWatch(new FileInfo(cnfg));
            log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);//LogManager.GetLogger(typeof(Log4netHelper));
        }

        public static void Debug(object message)
        {
            log.Debug(message);
        }

        public static void DebugFormatted(string format, params object[] args)
        {
            log.DebugFormat(format, args);
        }

        public static void Info(object message)
        {
            log.Info(message);
        }

        public static void InfoFormatted(string format, params object[] args)
        {
            log.InfoFormat(format, args);
        }

        public static void Warn(object message)
        {
            log.Warn(message);
        }

        public static void Warn(object message, Exception exception)
        {
            log.Warn(message, exception);
        }

        public static void WarnFormatted(string format, params object[] args)
        {
            log.WarnFormat(format, args);
        }

        public static void Error(object message)
        {
            log.Error(message);
        }

        public static void Error(object message, Exception exception)
        {
            log.Error(message, exception);
        }

        public static void ErrorFormatted(string format, params object[] args)
        {
            log.ErrorFormat(format, args);
        }

        public static void Fatal(object message)
        {
            log.Fatal(message);
        }

        public static void Fatal(object message, Exception exception)
        {
            log.Fatal(message, exception);
        }

        public static void FatalFormatted(string format, params object[] args)
        {
            log.FatalFormat(format, args);
        }
    }
    #endregion
}

