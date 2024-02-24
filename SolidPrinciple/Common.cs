using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerService
{

    public interface ILogger
    {
        void Log(string error);
    }

    public class FileLogger: ILogger
    {
        public void Log(string error)
        {
            File.WriteAllText(@"C:\Error.txt", error);
        }
    }

    public class DatabaseLogger: ILogger
    {
        public void Log(string error)
        {
           //Log To DB
        }
    }

    public class Customer
    {

    }
}
