using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bars;
using System.Data;

namespace Tests
{
    [TestClass]

    public class ServerInfoTest
    {

        [TestMethod]
       
        public void GetData_RealName_RerurnsLocalServer()
        {
            string expected = "LocalServer";
            IDbInfo dbInfo = new FakeDbInfo(expected);
            ServerInfo serverInfo = new ServerInfo(expected);
            DataTable dataTable = serverInfo.GetaData(dbInfo.GetDbData());
            string servername = dataTable.Rows[0]["ServerName"].ToString();
            Assert.AreEqual(expected, servername);

        }
        [TestMethod]
        public void GetData_GoodDbName_ReturnsPostgres()

        {
            string serverName = "LocalServer";
            string expected = "postgres";
            IDbInfo dbInfo = new FakeDbInfo(serverName);
            ServerInfo serverInfo = new ServerInfo(serverName);
            DataTable dataTable = serverInfo.GetaData(dbInfo.GetDbData());
            string dbName = dataTable.Rows[0]["DbName"].ToString();
            Assert.AreEqual(expected, dbName);

        }

        [TestMethod]
        public void GetData_GoodDbName_ReturnsDb()
        {
            string serverName = "LocalServer";
            string expected = "db";
            IDbInfo dbInfo = new FakeDbInfo(serverName);
            ServerInfo serverInfo = new ServerInfo(serverName);
            DataTable dataTable = serverInfo.GetaData(dbInfo.GetDbData());
            string dbName = dataTable.Rows[1]["DbName"].ToString();
            Assert.AreEqual(expected, dbName);
        }
        [TestMethod]
        public void GetData_RealServerName_ReturnsServer1()
        {
            string expected = "Server1";

            IDbInfo dbInfo = new FakeDbInfo(expected);
            ServerInfo serverInfo = new ServerInfo(expected);
            DataTable dataTable = serverInfo.GetaData(dbInfo.GetDbData());
            string serverName = dataTable.Rows[0]["ServerName"].ToString();
            Assert.AreEqual(expected, serverName);
        }
        [TestMethod]
       public void GetServerInfo_RealSizq__Returns79876()
        {
            string expected = "7,9876";
            string serverNAme = "LocalServer";
            double memory = 8.0;
            IDbInfo dbInfo = new FakeDbInfo(serverNAme);
            ServerInfo serverInfo = new ServerInfo(serverNAme);
            DataTable dataTable = serverInfo.GetaData(dbInfo.GetDbData());
            dataTable = serverInfo.GetServerInfo(dataTable,memory);
            string freespase = dataTable.Rows[dataTable.Rows.Count-2]["DbSize"].ToString();
            Assert.AreEqual(expected, freespase);
        }
        public void GetServeInfo_RealSize_Returns()
        {
            string expected = "11,9876";
            string serverNAme = "Server2";
            double memory = 12.0;
            IDbInfo dbInfo = new FakeDbInfo(serverNAme);
            ServerInfo serverInfo = new ServerInfo(serverNAme);
            DataTable dataTable = serverInfo.GetaData(dbInfo.GetDbData());
            dataTable = serverInfo.GetServerInfo(dataTable, memory);
            string freespase = dataTable.Rows[dataTable.Rows.Count - 2]["DbSize"].ToString();
            Assert.AreEqual(expected, freespase);
        }
    }
}