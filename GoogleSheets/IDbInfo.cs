using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Bars
{
   public interface IDbInfo
    {
         
        List<DbObject> GetDbData();
        
        

    }
}
