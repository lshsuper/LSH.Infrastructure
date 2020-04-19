using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Dapper.Contrib;
using Dapper.Contrib.Extensions;
using MySql.Data.MySqlClient;

namespace LSH.Infrastructure.Dapper
{
    public class DapperReponsitory: IReponsitory
    {

        protected DapperFactory _dbFactory;

        public DapperReponsitory(DapperFactory dbFactory)
        {
            _dbFactory = dbFactory;
        }

    }
}
