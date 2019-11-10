using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Bars
{
    
    public class GoogleApiSpreadSheet
    {/// <summary>
     /// This class is designed to work with google sheets API
     /// </summary>
        private SheetsService service { get; set; }
        private string sheetName { get; set; }
        public string spreadSheetId { get; set; }
        /// <summary>
        ///  This method is designed to create a new SpreadSheet document.
        /// </summary>
        /// <param name="service">The Sheet Service</param>
        /// <param name="spreadSheetId">SpreadSheet document</param>
        public GoogleApiSpreadSheet(SheetsService service, string spreadSheetId)
        {
            this.service = service;
            this.spreadSheetId = spreadSheetId;
        }
        public void GetNewSpredSheetId()
        {
            Spreadsheet spreadsheet = new Spreadsheet
            {
                Properties = new SpreadsheetProperties
                {
                    Title = "My SpreadSheet"
                }
            };
            var createSpreadSheet = this.service.Spreadsheets.Create(spreadsheet).Execute();
            spreadSheetId = createSpreadSheet.SpreadsheetId;
        }
        /// <summary>
        /// This method is deigned to create new Sheet in the docement
        /// </summary>
        /// <param name="sheetName">The name of nee Sheet/param>
        public void CreateSheet(string sheetName)
        {
            AddSheetRequest addSheetRequest = new AddSheetRequest
            {
                Properties = new SheetProperties
                {
                    Title = sheetName
                }
            };
            BatchUpdateSpreadsheetRequest batchUpdateSpreadsheetRequest = new BatchUpdateSpreadsheetRequest();//создаем запрос , который обновляет любое изменеие в таблице
            batchUpdateSpreadsheetRequest.Requests = new List<Request>();

            batchUpdateSpreadsheetRequest.Requests.Add(new Request
            {
                AddSheet = addSheetRequest
            });//кладем наш запрос на создание таблицs в список запросов

            SpreadsheetsResource.BatchUpdateRequest batchUpdateRequest = this.service.Spreadsheets.BatchUpdate(batchUpdateSpreadsheetRequest, this.spreadSheetId);
            BatchUpdateSpreadsheetResponse response = batchUpdateRequest.Execute();

            Console.WriteLine(DateTime.Now.ToString() + "Создан новый лист для {0} ", sheetName);
        }
        /// <summary>
        /// Данный метод предназначен для получения списка таблиц в созданом документе(SpreadSheet)
        /// </summary>
       /// <param name="spreadSheetId">The Id of SpreadSheet/param>
        /// <returns>The list of sheets in SpreadSheet ocement</returns>
        public List<string> GetSheetsList(string spreadSheetId)
        {
            List<string> sheetsList = new List<string>();

            var getRequest = service.Spreadsheets.Get(spreadSheetId);//получаем SpreadSheet по ID
            Spreadsheet spreadSheet = getRequest.Execute();
            foreach (var sheet in spreadSheet.Sheets)
                sheetsList.Add(sheet.Properties.Title);

            return sheetsList;
        }
        /// <summary>
        /// This method is designed to add new sheet in the document
        /// </summary>
        /// <param name="serverName">The name of new sheet</param>
        /// <returns></returns>
        public DataTable AddSheet(string serverName)
        {
            DataTable headOftable = new DataTable();
            headOftable.Columns.Add("ServerName");
            headOftable.Columns.Add("DbName");
            headOftable.Columns.Add("Size");
            headOftable.Columns.Add("Date");

            this.CreateSheet(serverName);
            headOftable.Rows.Add(new string[]
            {
                "Сервер",
                "База данных",
                "Размер в ГБ",
                "Дата обновления"
            });


            return headOftable;

        }


    }
}
