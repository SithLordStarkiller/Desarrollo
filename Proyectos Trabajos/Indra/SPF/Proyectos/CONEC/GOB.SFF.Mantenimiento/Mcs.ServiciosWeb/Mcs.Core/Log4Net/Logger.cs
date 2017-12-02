using System;
using System.Linq;
using System.Text;
using  System.Reflection;
using System.Diagnostics;
using log4net;
using Mcs.Core.Enumerators;

namespace Mcs.Core.Log4Net
{
    public static class Logger
    {
        private static readonly ILog _log = LogManager.GetLogger("");

        static Logger()
        {
            //XmlConfigurator.Configure();
        }

        public static void RegistraExcepcion(string message, Exception ex)
        {
            _log.Error(message, ex);
        }

        public static void WriteLog(Exception ex)
        {
            WriteLog(string.Empty, ex);
        }

        public static void WriteLog(string userId, Exception ex)
        {
            WriteLog(userId, string.Empty, string.Empty, ex);
        }

        public static void WriteLog(string userId, string aplicacion, string servicio, Exception ex)
        {
            WriteLog(LogType.Error, ex.Message, userId, aplicacion, servicio);
        }

        public static void WriteLog(LogType type, string message)
        {

            WriteLog(type, message, string.Empty);
        }

        public static void WriteLog(LogType type, string message, string userId)
        {
            WriteLog(type, message, userId, string.Empty, string.Empty);
        }
        public static void WriteLog(LogType type, string message, string userId, string aplicacion, string servicio)
        {
            string[] methods = GetMethodName("WriteLog");
            var sb = new StringBuilder();

            if (!string.IsNullOrEmpty(userId)) sb.Append($"[User ID: {userId}]");
            if (!string.IsNullOrEmpty(aplicacion)) sb.Append($"[Aplicación: {aplicacion}]");
            if (!string.IsNullOrEmpty(servicio)) sb.Append($"[Servicio: {servicio}]");
            if (!string.IsNullOrEmpty(message)) sb.Append(
                $" Mensaje: [{(methods[1] + "." ?? "") + methods[0]}] {message}");

            switch (type)
            {
                case LogType.Information:
                    _log.Info(sb.ToString());
                    break;
                case LogType.Warning:
                    _log.Warn(sb.ToString());
                    break;
                case LogType.Debug:
                    _log.Debug(sb.ToString());
                    break;
                case LogType.Error:
                    _log.Error(sb.ToString());
                    break;
            }
        }
        private static string[] GetMethodName(string methodName)
        {
            var stackFrame = FindStackFrame(methodName);
            var methodBase = GetCallingMethodBase(stackFrame);
            var callingMethod = methodBase.Name;
            var callingClass = methodBase.ReflectedType.Name;
            var lineNumber = stackFrame.GetFileLineNumber();
            string[] valores = { callingMethod, callingClass, lineNumber.ToString() };
            return valores;
        }

        private static MethodBase GetCallingMethodBase(StackFrame stackFrame)
        {
            return stackFrame == null ? MethodBase.GetCurrentMethod() : stackFrame.GetMethod();
        }

        private static StackFrame FindStackFrame(string methodName)
        {
            var stackTrace = new StackTrace(true);
            //for(int i=0; i<stackTrace.FrameCount;i++)
            for (var i = 0; i < stackTrace.GetFrames().Count(); i++)
            {
                if (stackTrace.GetFrame(i).GetMethod().Name.Equals(methodName))
                {
                    if (!stackTrace.GetFrame(i + 1).GetMethod().Name.Equals(methodName))
                    {
                        return new StackFrame(i + 1, true);
                    }
                }
            }
            return null;
        }
    }
}
