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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSH.Infrastructure.Dapper
{
    public sealed class DapperContext : IDisposable
    {
        //连接
        public IDbConnection Database { get; private set; }
        //事务
        private IDbTransaction _tran;

        public DapperContext(string connStr, DatabaseType type = DatabaseType.Mysql)
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

        #region +Tran
        public IDbTransaction BeginTran()
        {
            if (_tran != null)
                return _tran;

            Database.Open();
            _tran = Database.BeginTransaction();
            return _tran;
        }

        public void CommitTran()
        {
            if (_tran != null)
            {
                _tran.Commit();
            }
        }

        public void RollbackTran()
        {
            if (_tran != null)
            {
                _tran.Rollback();
            }
        }
        #endregion

        #region +Query
        public IEnumerable<T> Query<T>(string sql, object paramObj = null, IDbTransaction tran = null)
        {
            return Database.Query<T>(sql, paramObj, tran);
        }

        public Task<IEnumerable<T>> QueryAsync<T>(string sql, object paramObj = null, IDbTransaction tran = null)
        {
            return Database.QueryAsync<T>(sql, paramObj, tran);
        }

        #endregion

        #region +Execute
        public int Execute(string sql, object paramObj = null, IDbTransaction tran = null)
        {
            return Database.Execute(sql, paramObj, tran);
        }
        public Task<int> ExecuteAsync(string sql, object paramObj = null, IDbTransaction tran = null)
        {
            return Database.ExecuteAsync(sql, paramObj, tran);
        }
        #endregion

        #region +Scalar
        public T Scalar<T>(string sql, object paramObj = null, IDbTransaction tran = null)
        {
            return Database.ExecuteScalar<T>(sql, paramObj, tran);
        }
        public Task<T> ScalarAsync<T>(string sql, object paramObj = null, IDbTransaction tran = null)
        {
            return Database.ExecuteScalarAsync<T>(sql, paramObj, tran);
        }

        #endregion

        #region +Find
        public T Find<T>(string sql, object paramObj = null, IDbTransaction tran = null)
        {
            return Database.QueryFirstOrDefault<T>(sql, paramObj, tran);
        }

        public Task<T> FindAsync<T>(string sql, object paramObj = null, IDbTransaction tran = null)
        {
            return Database.QueryFirstOrDefaultAsync<T>(sql, paramObj, tran);
        }
        public T Find<T>(object id) where T : class, new()
        {
            return Database.Get<T>(id);
        }
        public Task<T> FindAsync<T>(object id) where T : class, new()
        {

            return Database.GetAsync<T>(id);
        }
        #endregion

        #region +FindAll
        public IEnumerable<T> FindAll<T>() where T : class, new()
        {
            return Database.GetAll<T>();
        }

        public Task<IEnumerable<T>> FindAllAsync<T>() where T : class, new()
        {
            return Database.GetAllAsync<T>();
        }
        #endregion

        #region +Add
        public long Add<T>(T model) where T : class, new()
        {

            return Database.Insert<T>(model);
        }

        public Task<int> AddAsync<T>(T model) where T : class, new()
        {

            return Database.InsertAsync<T>(model);
        }
        #endregion

        #region +Modify
        public Task<bool> ModifyAsync<T>(T model) where T : class, new()
        {
            return Database.UpdateAsync<T>(model);
        }

        public bool Modify<T>(T model) where T : class, new()
        {
            return Database.Update<T>(model);
        }
        #endregion

        #region +Remove
        public bool Remove<T>(T model) where T : class, new()
        {
            return Database.Delete<T>(model);
        }

        public Task<bool> RemoveAsync<T>(T model) where T : class, new()
        {
            return Database.DeleteAsync<T>(model);
        }
        #endregion

        #region +Clear
        public Task<bool> ClearAsync<T>() where T : class, new()
        {
            return Database.DeleteAllAsync<T>();
        }

        public bool Clear<T>() where T : class, new()
        {
            return Database.DeleteAll<T>();
        }
        #endregion


        #region +Muilt
        public Tuple<IEnumerable<T>, int> Page<T>(string countSql, string dataSql, object paramObj = null)
        {
            using (var muilt = Database.QueryMultiple(string.Format("{0};{1}", countSql, dataSql), paramObj))
            {
                int count = muilt.Read<int>().FirstOrDefault();
                if (count <= 0) return new Tuple<IEnumerable<T>, int>(new List<T>(), 0);

                var data = muilt.Read<T>();
                return new Tuple<IEnumerable<T>, int>(data, count);
            }
        }

        public Tuple<A, B> Muilt<A, B>(string muilSql, object paramObj = null)
        {
            using (var muilt = Database.QueryMultiple(muilSql, paramObj))
            {
                var one = muilt.Read<A>();
                var two = muilt.Read<B>();
                return Tuple.Create<A, B>(one.FirstOrDefault(), two.FirstOrDefault());
            }
        }
        public Tuple<A, B, C> Muilt<A, B, C>(string muilSql, object paramObj = null)
        {
            using (var muilt = Database.QueryMultiple(muilSql, paramObj))
            {
                var one = muilt.Read<A>();
                var two = muilt.Read<B>();
                var three = muilt.Read<C>();
                return Tuple.Create<A, B, C>(one.FirstOrDefault(), two.FirstOrDefault(), three.FirstOrDefault());
            }
        }
        #endregion


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
