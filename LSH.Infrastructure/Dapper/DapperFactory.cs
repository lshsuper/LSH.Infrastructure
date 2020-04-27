using System;
using System.Collections.Generic;
using System.Text;

namespace LSH.Infrastructure.Dapper
{
    public sealed class DapperFactory
    {

        public DapperContext Create(string connStr, DatabaseType type = DatabaseType.Mysql)
        {
            return new DapperContext(connStr, type);
        }


        public DapperContext Create()
        {
            //配置
            return new DapperContext("", DatabaseType.Mysql);
        }



    }
}
