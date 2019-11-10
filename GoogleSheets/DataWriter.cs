using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using System.Data;
namespace Bars
{
     class DataWriter 
     {/// <summary>
     /// This class is desiigned to write the data into a sheet
     /// </summary>
        public SheetsService service { get; set; }
        public string spreadSheetId { get; set; }
        public string sheetName { get; set; }
        public DataWriter(SheetsService service,string spreadSheetId, string sheetName) 
        {
            this.service = service;
            this.spreadSheetId = spreadSheetId;
            this.sheetName = sheetName;
        }
        /// <summary>
        /// This method is designed to write the data into a sheet
        /// </summary>
        /// <param name="dataTable">Datatabl</param>
        public void MyWriteData(DataTable dataTable)
        {
            var range = $"{sheetName}!A:D";
            var valueRange = new ValueRange() { Values = ConvertToIlist(dataTable) };
            var appendRequest = this.service.Spreadsheets.Values.Append(valueRange,this.spreadSheetId, range);
            appendRequest.ValueInputOption = SpreadsheetsResource.ValuesResource.AppendRequest.ValueInputOptionEnum.USERENTERED;
            var appendReponse = appendRequest.Execute();
        }
        /// <summary>
        /// This method is designed to convert DataTable to IList object
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public  IList<IList<object>> ConvertToIlist(DataTable data)
        {
            IList<IList<object>> resultList = new List<IList<object>>();

            foreach (DataRow dr in data.Rows)
            {
                resultList.Add(dr.ItemArray);
            }
            return resultList;
        }
    }
}
