using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bars
{
    public class FakeDbInfo: IDbInfo
    {
        public string serverName { get; set; }
        public FakeDbInfo(string serverName)
        {
            this.serverName = serverName;
        }
        public List<DbObject> GetDbData()
        {
            List<DbObject> dbObjects = new List<DbObject>();
            DbObject dbObject1 = new DbObject(serverName, "postgres", 0.00600641220808029);
            DbObject dbObject = new DbObject(serverName,"db", 0.00637262314558029);
            dbObjects.Add(dbObject1);
            dbObjects.Add(dbObject);
            return dbObjects;
        }
    }
}
