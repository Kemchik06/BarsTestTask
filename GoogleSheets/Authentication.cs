using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Sheets.v4;
using Google.Apis.Services;
using System.Threading;
using Google.Apis.Util.Store;
using System.IO;
using Google.Apis.Sheets.v4.Data;
using System.Configuration;

namespace Bars
{
    class Authentication
    {
        /// <summary>
        /// This method is designed to authorize the user
        /// </summary>
        /// <returns>Credentials of user </returns>
        static public UserCredential AuthorizeUser()
        {
            UserCredential credential;
            string[] Scopes = { SheetsService.Scope.Spreadsheets };

            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(ConfigurationInfo.Getcredentials()))) 
            {
                string credPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                credPath = Path.Combine(credPath, ".credentials/sheets.googleapis.com-dotnet-quickstart.json");
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
            }
            Console.WriteLine(credential);
            return credential;
        }
    }
}
