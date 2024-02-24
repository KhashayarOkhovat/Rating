//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Logger.Refactor
//{
//    class Program
//    {
//        static void Main()
//        {
//            ILogger logger = new LoggerFactory("TEXTFILE").CreateLogger();
//            var loggerSg = new LoggerSterategy();
//            loggerSg.SetLogger(new FileLogger());
//            loggerSg.Log("");
//        }


//    }

//    public class LoggerSterategy
//    {
//        ILogger _logger;
//        public LoggerSterategy()
//        {
//        }

//        public void SetLogger(ILogger logger)
//        {
//            _logger = logger;

//        }

//        public void Log(string text)
//        {
//            _logger.Log(text);
//        }
//    }

//    public class LoggerFactory
//    {
//        string whereToLog;
//        public LoggerFactory(string whereToLog)
//        {
//            this.whereToLog = whereToLog;
//        }
//        public ILogger CreateLogger()
//        {
//            switch (whereToLog)
//            {
//                case "TEXTFILE":
//                    return new FileLogger();
//                case "EVENTLOG":
//                    return new EventLogLogger();
//                default:
//                    throw new NotSupportedException();

//            }

//            return null;
//        }
//    }


//    public interface ILogger
//    {
//        void Log(string text);
//    }

//    public class FileLogger : ILogger
//    {
//        public void Log(string text)
//        {
//            throw new NotImplementedException();
//        }
//    }

//    public class EventLogLogger : ILogger
//    {
//        public void Log(string text)
//        {
//            throw new NotImplementedException();
//        }
//    }

//}
