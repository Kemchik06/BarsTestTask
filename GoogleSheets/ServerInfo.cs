using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Bars
{/// <summary>
/// This class designed to work with data of server
/// </summary>
    public class ServerInfo
    {
       private string serverName { get; set; }
        public ServerInfo(string serverName )
        {
            this.serverName = serverName;
        }
        public double GetServerSize(string serverName)
        {
            double allMemory = ConfigurationInfo.GetServerSizeFromConfig(this.serverName);
            return allMemory;

        }
      /// <summary>
      /// This method is designed to put the data of server to the Datatable
      /// </summary>
      /// <param name="dataTable">the datatable</param>
      /// <param name="allMemory">The size of the disk</param>
      /// <returns></returns>
        public DataTable GetServerInfo(DataTable dataTable, double allMemory)
        {
            DataRow [] rows = dataTable.Select();
            for (int i = 0; i < rows.Length; i++)
                allMemory -= double.Parse(rows[i]["DbSize"].ToString());//здесь тоже можно 

            DataRow row = dataTable.NewRow();
            row["ServerName"] = serverName;
            row["DbName"] = "Свободно";
            row["DbSize"] = allMemory.ToString();
            row["DateTime"] = DateTime.Now.ToString();
            dataTable.Rows.Add(row);
            dataTable.Rows.Add(new string[]
            {
                "_______",
                "_______",
                "_______",
                "_______"
            });

            return dataTable;
        }
        /// <summary>
        /// This method is designed to put the data of server to the Datatable
        /// </summary>
        /// <param name="dbObjects">The list of data from database</param>
        /// <returns>The Datatable</returns>
        public DataTable GetaData(List<DbObject> dbObjects)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("ServerName", typeof(string));
            dataTable.Columns.Add("DbName", typeof(string));
            dataTable.Columns.Add("DbSize", typeof(string));
            dataTable.Columns.Add("DateTime", typeof(string));
            foreach(DbObject dbObject in dbObjects)
            {
                DataRow row = dataTable.NewRow();
                row["ServerName"] = this.serverName;
                row["DbName"] = dbObject.dbName;
                row["DbSize"] = dbObject.dbSize;
                row["DateTime"] = DateTime.Now.ToString();
                dataTable.Rows.Add(row);
            }
            return dataTable;
            


        }
    }
}
