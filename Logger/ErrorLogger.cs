using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logger
{
    public class ErrorLogger
    {
        private readonly string _whereToLog;
        public ErrorLogger(string whereToLog)
        {
            this._whereToLog = whereToLog.ToUpper();
        }

        public void LogError(string message)
        {
            switch (_whereToLog)
            {
                case "TEXTFILE":
                    WriteTextFile(message);
                    break;
                case "EVENTLOG":
                    WriteEventLog(message);
                    break;
                default:
                    throw new Exception("Unable to log error");
            }
        }

        private void WriteTextFile(string message)
        {
            System.IO.File.WriteAllText(@"C:\Users\Public\LogFolder\Errors.txt", message);
        }

        private void WriteEventLog(string message)
        {
            string source = "DDD Course";
            string log = "Application";

            if (!EventLog.SourceExists(source))
            {
                EventLog.CreateEventSource(source, log);
            }
            EventLog.WriteEntry(source, message, EventLogEntryType.Error, 1);
        }
    }

}
