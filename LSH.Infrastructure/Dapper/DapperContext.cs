using Dapper;
using Dapper.Contrib.Extensions;
using MySql.Data.MySqlClient;
using Npgsql;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Text;
using System.Threading.Tasks;

namespace LSH.Infrastructure.Dapper
{
    public class DapperContext : IDisposable
    {
        //连接
        public IDbConnection Database { get; private set; }
        //事务
        private IDbTransaction _tran;

        public DapperContext(string connStr, DatabaseType type=DatabaseType.Mysql)
        {
            switch (type)
            {
                case DatabaseType.Mysql:
                    Database = new MySqlConnection(connStr);
                    break;
                case DatabaseType.Sqlserver:
                    Database = new SqlConnection(connStr);
                    break;
                case DatabaseType.Postgresql:
                    Database = new NpgsqlConnection(connStr);
                    break;
                case DatabaseType.Sqlite:
                    Database = new SQLiteConnection(connStr);
                    break;
                case DatabaseType.Oracle:
                    Database = new OracleConnection(connStr);
                    break;
                default:
                    break;
            }
        }
 
        public IDbTransaction TranBegin()
        {
            if (_tran != null)
                return _tran;

            Database.Open();
            _tran = Database.BeginTransaction();
            return _tran;
        }

        public void TranCommit()
        {
            if (_tran != null)
            {
                _tran.Commit();
            }
        }

        public void TranRollback()
        {
            if (_tran != null)
            {
                _tran.Rollback();
            }
        }

        public IEnumerable<T> Query<T>(string sql, object paramObj = null)
        {
            return Database.Query<T>(sql, paramObj);
        }

        public Task<IEnumerable<T>> QueryAsync<T>(string sql, object paramObj = null)
        {
            return Database.QueryAsync<T>(sql, paramObj);
        }

        public T Find<T>(string sql, object paramObj=null)
        {
            return Database.QueryFirstOrDefault<T>(sql, paramObj);
        }

        public Task<T> FindAsync<T>(string sql, object paramObj = null)
        {
            return Database.QueryFirstOrDefaultAsync<T>(sql, paramObj);
        }

        public int Execute(string sql, object paramObj = null)
        {
            return Database.Execute(sql, paramObj);
        }
        public Task<int> ExecuteAsync(string sql, object paramObj = null)
        {
            return Database.ExecuteAsync(sql, paramObj);
        }

        public T Scalar<T>(string sql, object paramObj = null)
        {
            return Database.ExecuteScalar<T>(sql, paramObj);
        }


     
        //public T QueryMuilt(string sql, object paramObj = null)
        //{
        //    using (var reader= Database.QueryMultiple(sql, paramObj))
        //    {
            
        //        reader.Read();
        //    }
            
        //}


        public void Dispose()
        {
            //销毁事务
            if (_tran != null)
            {
                _tran.Dispose();
                _tran = null;
            }
            //销毁连接
            if (Database != null && Database.State == ConnectionState.Open)
            {
                Database.Dispose();
                Database.Close();
                Database = null;

            }

        }
    }


    public enum DatabaseType
    {
        [Description("Mysql")]
        Mysql = 1,
        [Description("Sqlserver")]
        Sqlserver = 2,
        [Description("Postgresql")]
        Postgresql = 3,
        [Description("Sqlite")]
        Sqlite = 4,
        [Description("Oracle")]
        Oracle = 5

    }
}
