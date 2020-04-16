using Dapper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace LSH.Infrastructure.Dapper
{
    public class DapperContext : IDisposable
    {

        private IDbConnection _database;

        public DapperContext(string connStr, DatabaseType type)
        {
            switch (type)
            {
                case DatabaseType.Mysql:
                    _database = new MySqlConnection(connStr);
                    break;
                default:
                    break;
            }
        }

        public IEnumerable<T> Query<T>(string sql,params object[] paramArr)
        {
           return _database.Query<T>(sql, paramArr);
        }

        public T Find<T>(string sql,params object[] paramArr)
        {
            return _database.QueryFirst<T>(sql,paramArr);
        }

        public  int Excute(string sql, params object[] paramArr)
        {
            return _database.Execute(sql,paramArr);
        }

        public T ExecuteScalar<T>(string sql, params object[] paramArr)
        {
            return _database.ExecuteScalar<T>(sql, paramArr);
        }

        public void Dispose()
        {
            if (_database.State == ConnectionState.Closed)
                return;

            _database.Dispose();
            _database.Close();
           
        }
    }


    public enum DatabaseType
    {

          Mysql=1,


    }
}
