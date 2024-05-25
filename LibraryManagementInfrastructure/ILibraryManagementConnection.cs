using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementInfrastructure
{
   
        public interface ILibraryManagementConnection
        {
            public IDbConnection CreateConnection();
        }
    
}
