using Dapper;
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

namespace LSH.Infrastructure.Dapper
{
    public class DapperContext : IDisposable
    {
        //连接
        private IDbConnection _database;
        //事务
        private IDbTransaction _tran;

        public DapperContext(string connStr, DatabaseType type)
        {
            switch (type)
            {
                case DatabaseType.Mysql:
                    _database = new MySqlConnection(connStr);
                    break;
                case DatabaseType.Sqlserver:
                    _database = new SqlConnection(connStr);
                    break;
                case DatabaseType.Postgresql:
                    _database = new NpgsqlConnection(connStr);
                    break;
                case DatabaseType.Sqlite:
                    _database = new SQLiteConnection(connStr);
                    break;
                case DatabaseType.Oracle:
                    _database = new OracleConnection(connStr);
                    break;
                default:
                    break;
            }
        }


        public IDbTransaction TranBegin()
        {
            if (_tran != null)
                return _tran;

            _database.Open();
            _tran = _database.BeginTransaction();
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
            return _database.Query<T>(sql, paramObj);
        }

        public T Find<T>(string sql, params object[] paramArr)
        {
            return _database.QueryFirst<T>(sql, paramArr);
        }

        public int Execute(string sql, object paramObj = null)
        {
            return _database.Execute(sql, paramObj);
        }

        public T ExecuteScalar<T>(string sql, object paramObj = null)
        {
            return _database.ExecuteScalar<T>(sql, paramObj);
        }

        public void Dispose()
        {
            //销毁事务
            if (_tran != null)
            {
                _tran.Dispose();
                _tran = null;
            }
            //销毁连接
            if (_database != null && _database.State == ConnectionState.Open)
            {
                _database.Dispose();
                _database.Close();
                _database = null;

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
