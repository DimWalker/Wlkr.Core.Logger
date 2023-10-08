using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;

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

    #region Sample 2

    ///// <summary>
    ///// Author     : yenange
    ///// Date       : 2014-02-21
    ///// Description: 日志辅助类
    ///// </summary>
    //public sealed class Log4netHelper
    //{
    //    #region [ 单例模式 ]
    //    private static readonly Log4netHelper _logger = new Log4netHelper();
    //    private static readonly log4net.ILog _Logger4net = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

    //    /// <summary>
    //    /// 无参私有构造函数
    //    /// </summary>
    //    private Log4netHelper()
    //    {
    //    }

    //    /// <summary>
    //    /// 得到单例
    //    /// </summary>
    //    public static Log4netHelper Singleton
    //    {
    //        get
    //        {
    //            return _logger;
    //        }
    //    }
    //    #endregion

    //    #region [ 参数 ]
    //    public bool IsDebugEnabled
    //    {
    //        get { return _Logger4net.IsDebugEnabled; }
    //    }
    //    public bool IsInfoEnabled
    //    {
    //        get { return _Logger4net.IsInfoEnabled; }
    //    }
    //    public bool IsWarnEnabled
    //    {
    //        get { return _Logger4net.IsWarnEnabled; }
    //    }
    //    public bool IsErrorEnabled
    //    {
    //        get { return _Logger4net.IsErrorEnabled; }
    //    }
    //    public bool IsFatalEnabled
    //    {
    //        get { return _Logger4net.IsFatalEnabled; }
    //    }
    //    #endregion

    //    #region [ 接口方法 ]

    //    #region [ Debug ]
    //    public void Debug(string message)
    //    {
    //        if (this.IsDebugEnabled)
    //        {
    //            this.Log(LogLevel.Debug, message);
    //        }
    //    }

    //    public void Debug(string message, Exception exception)
    //    {
    //        if (this.IsDebugEnabled)
    //        {
    //            this.Log(LogLevel.Debug, message, exception);
    //        }
    //    }

    //    public void DebugFormat(string format, params object[] args)
    //    {
    //        if (this.IsDebugEnabled)
    //        {
    //            this.Log(LogLevel.Debug, format, args);
    //        }
    //    }

    //    public void DebugFormat(string format, Exception exception, params object[] args)
    //    {
    //        if (this.IsDebugEnabled)
    //        {
    //            this.Log(LogLevel.Debug, string.Format(format, args), exception);
    //        }
    //    }
    //    #endregion

    //    #region [ Info ]
    //    public void Info(string message)
    //    {
    //        if (this.IsInfoEnabled)
    //        {
    //            this.Log(LogLevel.Info, message);
    //        }
    //    }

    //    public void Info(string message, Exception exception)
    //    {
    //        if (this.IsInfoEnabled)
    //        {
    //            this.Log(LogLevel.Info, message, exception);
    //        }
    //    }

    //    public void InfoFormat(string format, params object[] args)
    //    {
    //        if (this.IsInfoEnabled)
    //        {
    //            this.Log(LogLevel.Info, format, args);
    //        }
    //    }

    //    public void InfoFormat(string format, Exception exception, params object[] args)
    //    {
    //        if (this.IsInfoEnabled)
    //        {
    //            this.Log(LogLevel.Info, string.Format(format, args), exception);
    //        }
    //    }
    //    #endregion

    //    #region  [ Warn ]

    //    public void Warn(string message)
    //    {
    //        if (this.IsWarnEnabled)
    //        {
    //            this.Log(LogLevel.Warn, message);
    //        }
    //    }

    //    public void Warn(string message, Exception exception)
    //    {
    //        if (this.IsWarnEnabled)
    //        {
    //            this.Log(LogLevel.Warn, message, exception);
    //        }
    //    }

    //    public void WarnFormat(string format, params object[] args)
    //    {
    //        if (this.IsWarnEnabled)
    //        {
    //            this.Log(LogLevel.Warn, format, args);
    //        }
    //    }

    //    public void WarnFormat(string format, Exception exception, params object[] args)
    //    {
    //        if (this.IsWarnEnabled)
    //        {
    //            this.Log(LogLevel.Warn, string.Format(format, args), exception);
    //        }
    //    }
    //    #endregion

    //    #region  [ Error ]

    //    public void Error(string message)
    //    {
    //        if (this.IsErrorEnabled)
    //        {
    //            this.Log(LogLevel.Error, message);
    //        }
    //    }

    //    public void Error(string message, Exception exception)
    //    {
    //        if (this.IsErrorEnabled)
    //        {
    //            this.Log(LogLevel.Error, message, exception);
    //        }
    //    }

    //    public void ErrorFormat(string format, params object[] args)
    //    {
    //        if (this.IsErrorEnabled)
    //        {
    //            this.Log(LogLevel.Error, format, args);
    //        }
    //    }

    //    public void ErrorFormat(string format, Exception exception, params object[] args)
    //    {
    //        if (this.IsErrorEnabled)
    //        {
    //            this.Log(LogLevel.Error, string.Format(format, args), exception);
    //        }
    //    }
    //    #endregion

    //    #region  [ Fatal ]

    //    public void Fatal(string message)
    //    {
    //        if (this.IsFatalEnabled)
    //        {
    //            this.Log(LogLevel.Fatal, message);
    //        }
    //    }

    //    public void Fatal(string message, Exception exception)
    //    {
    //        if (this.IsFatalEnabled)
    //        {
    //            this.Log(LogLevel.Fatal, message, exception);
    //        }
    //    }

    //    public void FatalFormat(string format, params object[] args)
    //    {
    //        if (this.IsFatalEnabled)
    //        {
    //            this.Log(LogLevel.Fatal, format, args);
    //        }
    //    }

    //    public void FatalFormat(string format, Exception exception, params object[] args)
    //    {
    //        if (this.IsFatalEnabled)
    //        {
    //            this.Log(LogLevel.Fatal, string.Format(format, args), exception);
    //        }
    //    }
    //    #endregion
    //    #endregion

    //    #region [ 内部方法 ]
    //    /// <summary>
    //    /// 输出普通日志
    //    /// </summary>
    //    /// <param name="level"></param>
    //    /// <param name="format"></param>
    //    /// <param name="args"></param>
    //    private void Log(LogLevel level, string format, params object[] args)
    //    {
    //        switch (level)
    //        {
    //            case LogLevel.Debug:
    //                _Logger4net.DebugFormat(format, args);
    //                break;
    //            case LogLevel.Info:
    //                _Logger4net.InfoFormat(format, args);
    //                break;
    //            case LogLevel.Warn:
    //                _Logger4net.WarnFormat(format, args);
    //                break;
    //            case LogLevel.Error:
    //                _Logger4net.ErrorFormat(format, args);
    //                break;
    //            case LogLevel.Fatal:
    //                _Logger4net.FatalFormat(format, args);
    //                break;
    //        }
    //    }

    //    /// <summary>
    //    /// 格式化输出异常信息
    //    /// </summary>
    //    /// <param name="level"></param>
    //    /// <param name="message"></param>
    //    /// <param name="exception"></param>
    //    private void Log(LogLevel level, string message, Exception exception)
    //    {
    //        switch (level)
    //        {
    //            case LogLevel.Debug:
    //                _Logger4net.Debug(message, exception);
    //                break;
    //            case LogLevel.Info:
    //                _Logger4net.Info(message, exception);
    //                break;
    //            case LogLevel.Warn:
    //                _Logger4net.Warn(message, exception);
    //                break;
    //            case LogLevel.Error:
    //                _Logger4net.Error(message, exception);
    //                break;
    //            case LogLevel.Fatal:
    //                _Logger4net.Fatal(message, exception);
    //                break;
    //        }
    //    }
    //    #endregion
    //}//end of class

    //#region [ enum: LogLevel ]
    ///// <summary>
    ///// 日志级别
    ///// </summary>
    //public enum LogLevel
    //{
    //    Debug,
    //    Info,
    //    Warn,
    //    Error,
    //    Fatal
    //}
    //#endregion

    ////--------------------- 
    ////作者：吉普赛的歌 
    ////来源：CSDN 
    ////原文：https://blog.csdn.net/yenange/article/details/19625581 
    ////版权声明：本文为博主原创文章，转载请附上博文链接！

    #endregion
}

