using System;

namespace Logger.Refactor
{
    public class LoggerFactory
    {
        public static ILogger CreateLogger(string whereToLog)
        {
            switch (whereToLog)
            {
                case "TEXTFILE":
                    return new FileLogger();
                    break;
                case "EVENTLOG":
                    return new EventLogger();
                    break;
                default:
                    throw new Exception("not Supported Log !!!!!");
            }
        }
    }
}