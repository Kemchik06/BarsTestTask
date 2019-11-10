using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bars
{/// <summary>
/// This class is designed to store the data from database
/// </summary>
    public class DbObject
    {
        public string serverName { get; set; }
        public string dbName { get; set; }
        public string dbSize { get; set; }
        public DbObject(string serverName, string dbName,double dbSize)
        {
            this.serverName = serverName;
            this.dbName = dbName;
            this.dbSize = Math.Round(dbSize, 4).ToString();
        }
    }
}
