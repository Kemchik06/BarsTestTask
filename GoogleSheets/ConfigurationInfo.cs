using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Bars
{/// <summary>
/// This class is designed to work with cofiguration file
/// </summary>
    public class ConfigurationInfo
    {
        /// <summary>
        /// This method is designed to get a disk size of the server
        /// </summary>
        /// <param name="serverName">The name of the server</param>
        /// <returns>The size of disk</returns>
        public static double GetServerSizeFromConfig(string serverName)
        {
            double size = double.Parse(ConfigurationManager.AppSettings[serverName]);
            return size;
        }
        /// <summary>
        /// This method is designed to get the list of servers
        /// </summary>
        /// <returns>The list of servers </returns>
        public static List<string> GetServersList()
        {
            List<string> serversList = new List<string>();
            for (int i = 0; i < ConfigurationManager.ConnectionStrings.Count; i++)
                if (ConfigurationManager.ConnectionStrings[i].ProviderName == "Npgsql")
                    serversList.Add(ConfigurationManager.ConnectionStrings[i].Name);

            return serversList;
        }
        /// <summary>
        /// This method is designed to get the credentials from the configuration file
        /// </summary>
        /// <returns>Credentials</returns>
        public static string Getcredentials()
        {
            string credentials = ConfigurationManager.AppSettings["credentials"];
            return credentials;
        }
        /// <summary>
        /// This method is designed to get the connection string
        /// </summary>
        /// <param name="servername">The name of server</param>
        /// <returns></returns>
        public static string GetConnStr(string servername)
        {
            string connectionString = ConfigurationManager.ConnectionStrings[servername].ConnectionString;
            return connectionString;
        }
        /// <summary>
        /// This method is designed to update the information of appSettings section of the configuration file
        /// </summary>
        /// <param name="key">The key</param>
        /// <param name="value">The value</param>
        public static void UpdateConfig(string key, string value)
        {
            Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            configuration.AppSettings.Settings.Remove(key);
            configuration.AppSettings.Settings.Add(key, value);
            configuration.Save(ConfigurationSaveMode.Full);
            ConfigurationManager.RefreshSection("appSettings");
        }
        /// <summary>
        /// This method is designed to get the id of spreadsheet
        /// </summary>
        /// <returns>The Id of spreadsheet</returns>
        public static string GetSpreadSheetId()
        {
            return ConfigurationManager.AppSettings["SheetId"];
        }
    }
}
