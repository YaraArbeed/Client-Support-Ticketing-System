using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Tickiting.Utility.Logging
{
    public static class Logger
    {
       
        #region Members
        private static CultureInfo culture = new CultureInfo("en-US");
        private static string logFileName = "Log_";

        private static object _lockObject = new object();

        public static string LogFileName
        {
            get { return logFileName; }
            set { logFileName = value; }
        }
        #endregion

        #region Init Methods

        public static void InitSettings(string logfile)
        {

            logFileName = logfile;
        }
       
        
        #endregion

        #region Helper Methods
        private static string GetVersion()
        {
            return System.Reflection.Assembly.GetCallingAssembly().GetName().Version.ToString(4);
        }

        private static string GetCurrentDateTimeString()
        {
            return DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff", culture);
        }

        private static string GetExceptionErrorString(Exception e)
        {
            try
            {
                StackTrace l_stackTrace = new StackTrace(e, true);
                var frame = l_stackTrace.GetFrame((l_stackTrace.FrameCount - 1));
                string l_fileName = frame?.GetFileName();
                l_fileName = Path.GetFileNameWithoutExtension(l_fileName);
                int l_lineNumber = frame == null ? 0 : frame.GetFileLineNumber();
                MethodBase l_methodBase = frame?.GetMethod();
                string l_innerError = e.InnerException != null ? ", " + e.InnerException.Message : "";
                return string.Format("{3}{4}{0}, Method name '{1}'{2}{5}Stack Trace:{6}",
                    !string.IsNullOrEmpty(l_fileName) ? string.Format(", Error in '{0}'", l_fileName) : "",
                    l_methodBase?.Name,
                    l_lineNumber > 0 ? string.Format(", at line number '{0}'", l_lineNumber) : "",
                    e.Message,
                    l_innerError,
                    Environment.NewLine,
                    e.StackTrace);
            }
            catch
            {
                return e.Message;
            }
        }
        private static void CheckDirectory(string dir)
        {
            try
            {
                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);
            }
            catch { }
        }
        private static void WriteLog(string txt, LogType type)
        {
            string LogDirectoryPath = "c:\\Log\\";
            try
            {
                lock (_lockObject)
                {

                    string currentDateTime = GetCurrentDateTimeString();
                    string fileName = AdjustFileName(logFileName);
                    CheckDirectory(Path.Combine(LogDirectoryPath));
                    using (var sw = new StreamWriter(Path.Combine(LogDirectoryPath, fileName), true))
                    {
                        sw.WriteLine(string.Format("{0}\t{1}\t{2}\t{3}", GetCurrentDateTimeString(), GetVersion(), type.ToString(), txt));
                    }
                    
                }
            }
            catch
            {

            }
        }
        private static string AdjustFileName(string fileName)
        {
            return fileName + "_" + DateTime.Now.ToString("yyyyMMdd", culture) + ".txt";
        }
        #endregion

        #region Log Methods
        public static void WriteInfo(string txt)
        {
            
                WriteLog(txt, LogType.INFO);
        }
        public static void WriteInfo(string caption, string txt)
        {
            WriteInfo(string.Format("{0}: {1}", caption, txt));
        }
        public static void WriteDebug(string txt)
        {
            
                WriteLog(txt, LogType.DEBUG);
        }
        public static void WriteDebug(string caption, string txt)
        {
            WriteDebug(string.Format("{0}: {1}", caption, txt));
        }
        public static void WriteDebug(Exception ex)
        {
            WriteDebug(GetExceptionErrorString(ex));
        }
        public static void WriteDebug(string caption, Exception ex)
        {
            WriteDebug(string.Format("{0}: {1}", caption, GetExceptionErrorString(ex)));
        }
        public static void WriteWarn(string txt)
        {
           
                WriteLog(txt, LogType.WARN);
        }
        public static void WriteWarn(string caption, string txt)
        {
            WriteWarn(string.Format("{0}: {1}", caption, txt));
        }
        public static void WriteWarn(Exception ex)
        {
            WriteWarn(GetExceptionErrorString(ex));
        }
        public static void WriteWarn(string caption, Exception ex)
        {
            WriteWarn(string.Format("{0}: {1}", caption, GetExceptionErrorString(ex)));
        }
        public static void WriteWarn(MethodBase mb, string txt)
        {
           
                WriteLog(string.Format("{0}: {1}", mb.Name, txt), LogType.WARN);
        }
        public static void WriteError(string txt)
        {
            
                WriteLog(txt, LogType.ERROR);
        }
        public static void WriteError(string caption, string txt)
        {
            WriteError(string.Format("{0}: {1}", caption, txt));
        }
        public static void WriteError(Exception ex)
        {
            WriteError(GetExceptionErrorString(ex));
        }
        public static void WriteError(string caption, Exception ex)
        {
            WriteError(string.Format("{0}: {1}", caption, GetExceptionErrorString(ex)));
        }
        public static void WriteError(MethodBase mb, Exception ex)
        {
            
                WriteLog(string.Format("{0}: {1}", mb.Name, GetExceptionErrorString(ex)), LogType.ERROR);
        }
        public static void WriteError(MethodBase mb, string txt)
        {
           
                WriteLog(string.Format("{0}: {1}", mb.Name, txt), LogType.ERROR);
        }
        public static void WriteException(Exception ex)
        {
          
                WriteLog(GetExceptionErrorString(ex), LogType.EXCEPTION);
        }
        public static void WriteException(string caption, Exception ex)
        {
           
                WriteLog(string.Format("{0}: {1}", caption, GetExceptionErrorString(ex)), LogType.EXCEPTION);
        }
        public static void WriteException(MethodBase mb, Exception ex)
        {
            
                WriteLog(string.Format("{0}: {1}", mb.Name, GetExceptionErrorString(ex)), LogType.EXCEPTION);
        }
        public static void WriteMonitoring(string txt)
        {
           
                WriteLog(txt, LogType.MONITORING);
        }
        public static void WriteMonitoring(string caption, string txt)
        {
            WriteMonitoring(string.Format("{0}: {1}", caption, txt));
        }
        #endregion
    }

    public enum LogType
    {
        INFO = 0,
        DEBUG = 1,
        WARN = 2,
        ERROR = 3,
        EXCEPTION = 4,
        MONITORING = 5,
    }
}