using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using System.Configuration;

namespace Bars
{
    public class DbInfo: IDbInfo
    {/// <summary>
    /// This class is designed to get the data of server from database
    /// </summary>
        private readonly string serverName;
        private readonly string connectionParams;

        public DbInfo(string serverName, string connectionParams)
        {
            this.serverName = serverName;
            this.connectionParams = connectionParams;
        }

       /// <summary>
       /// This method is designed to get the data about server from database
       /// </summary>
       /// <returns>The list of database objects</returns>
        public List<DbObject> GetDbData()
        {
            string getSizeRequest = "SELECT pg_database.datname  AS database_name, pg_database_size(pg_database.datname) AS database_size FROM pg_database WHERE pg_database.datname NOT Like 'template_';";
           
            
            NpgsqlConnection connection = new NpgsqlConnection(connectionParams);
            try
            {
                connection.Open();
                Console.WriteLine($"Подключение к серверу \"{this.serverName}\"");
            }
            catch (Exception exp)
            {
                Console.WriteLine($"Ошибка подключения к серверу {this.serverName} \n {exp.ToString()}");
                
            }

            NpgsqlCommand command = new NpgsqlCommand(getSizeRequest, connection);
            NpgsqlDataReader reader = command.ExecuteReader();
            List <DbObject> dbObjects = new List<DbObject>();
            while (reader.Read())
            {
                double size = (double.Parse(reader.GetValue(1).ToString())) / 1024 / 1024 / 1024;
                size = Math.Round(size, 4);
                DbObject dbObject = new DbObject(this.serverName,reader.GetString(0),size);
                dbObjects.Add(dbObject);
            }

            connection.Close();
            return dbObjects;

        }
        
    }
}
