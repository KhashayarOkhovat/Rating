using Logger.Refactor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logger
{
    class Program
    {
        static void Main(string[] args)
        {
           
        }

        static void FactoryStrategySample()
        {
            var loggerType = LoggerFactory.CreateLogger("TEXTFILE");
            var logger = new LoggerContext();
            logger.SetLogger(loggerType);
            logger.Log("Logg ");
        }

        static void StrategySample()
        {
            var logger = new LoggerContext();
            logger.SetLogger(new FileLogger());
            logger.Log("Logg ");
        }
    }
}
