using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Sheets.v4;
using Google.Apis.Services;

namespace Bars
{/// <summary>
/// This class is designed to perfom the task
/// </summary>
    public  class RealizationTask
    {
       public UserCredential credential { get; set; }
       public string spreadSheetId { get; set; }
        public RealizationTask()
        {
            credential = Authentication.AuthorizeUser();
            spreadSheetId = ConfigurationInfo.GetSpreadSheetId();
            
        }
        /// <summary>
        /// This method designed to perfom the task
        /// </summary>
        public void PerfomingTask(object obj)
        {
            SheetsService sheetsService = new SheetsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "MyApp"
            });
            GoogleApiSpreadSheet spreadSheet = new GoogleApiSpreadSheet(sheetsService, spreadSheetId);

            List<string> serverlist = ConfigurationInfo.GetServersList();
            foreach (var server in serverlist)
            {
                string serverName = server.ToString();

                string connectionString = ConfigurationInfo.GetConnStr(server);
                DbInfo dbInfo = new DbInfo(server, connectionString);

                if (spreadSheetId == null)
                {
                    spreadSheet.GetNewSpredSheetId();
                    spreadSheetId = spreadSheet.spreadSheetId;
                    ConfigurationInfo.UpdateConfig("SheetId", spreadSheetId);
                }

                DataWriter writer = new DataWriter(sheetsService, spreadSheetId, serverName);
                ServerInfo serverInfo = new ServerInfo(server);
                if (!spreadSheet.GetSheetsList(spreadSheetId).Contains(serverName))
                {
                    writer.MyWriteData(spreadSheet.AddSheet(server));

                }
                double allmemory = serverInfo.GetServerSize(serverName);
                writer.MyWriteData(serverInfo.GetServerInfo(serverInfo.GetaData(dbInfo.GetDbData()), allmemory));

            }
        }

    }
}
