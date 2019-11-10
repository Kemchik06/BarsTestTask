using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using System.Configuration;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Sheets.v4;
using Google.Apis.Services;
using System.Threading;
using Google.Apis.Util.Store;
using System.IO;
using Google.Apis.Sheets.v4.Data;
using System.Data;

namespace Bars
{
    class Program
    {
     
        public static void Main(string[] args)
        {
            TimerCallback tm = new TimerCallback(new RealizationTask().PerfomingTask);
            Timer timer = new Timer(tm, null, 0, 20000);
            Console.ReadKey();
        }
    }
}
