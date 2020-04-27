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
        public IDbConnection CurConn { get; private set; }
        //事务
        public IDbTransaction CurTran { get; private set; }

        public DapperContext(string connStr, DatabaseType type = DatabaseType.Mysql)
        {
            switch (type)
            {
                case DatabaseType.Mysql:
                    CurConn = new MySqlConnection(connStr);
                    break;
                case DatabaseType.Sqlserver:
                    CurConn = new SqlConnection(connStr);
                    break;
                case DatabaseType.Postgresql:
                    CurConn = new NpgsqlConnection(connStr);
                    break;
                case DatabaseType.Sqlite:
                    CurConn = new SQLiteConnection(connStr);
                    break;
                case DatabaseType.Oracle:
                    CurConn = new OracleConnection(connStr);
                    break;
                default:
                    break;
            }
        }

        #region +Tran
        public IDbTransaction BeginTran()
        {
            if (CurTran != null)
                return CurTran;

            CurConn.Open();
            CurTran = CurConn.BeginTransaction();
            return CurTran;
        }

        public void CommitTran()
        {
            if (CurTran != null)
            {
                CurTran.Commit();
            }
        }

        public void RollbackTran()
        {
            if (CurTran != null)
            {
                CurTran.Rollback();
            }
        }
        #endregion

        #region +Query
        public IEnumerable<T> Query<T>(string sql, object paramObj = null, IDbTransaction CurTran = null)
        {
            return CurConn.Query<T>(sql, paramObj, CurTran);
        }

        public Task<IEnumerable<T>> QueryAsync<T>(string sql, object paramObj = null, IDbTransaction CurTran = null)
        {
            return CurConn.QueryAsync<T>(sql, paramObj, CurTran);
        }

        #endregion

        #region +Execute
        public int Execute(string sql, object paramObj = null, IDbTransaction CurTran = null)
        {
            return CurConn.Execute(sql, paramObj, CurTran);
        }
        public Task<int> ExecuteAsync(string sql, object paramObj = null, IDbTransaction CurTran = null)
        {
            return CurConn.ExecuteAsync(sql, paramObj, CurTran);
        }
        #endregion

        #region +Scalar
        public T Scalar<T>(string sql, object paramObj = null, IDbTransaction CurTran = null)
        {
            return CurConn.ExecuteScalar<T>(sql, paramObj, CurTran);
        }
        public Task<T> ScalarAsync<T>(string sql, object paramObj = null, IDbTransaction CurTran = null)
        {
            return CurConn.ExecuteScalarAsync<T>(sql, paramObj, CurTran);
        }

        #endregion

        #region +Find
        public T Find<T>(string sql, object paramObj = null, IDbTransaction CurTran = null)
        {
            return CurConn.QueryFirstOrDefault<T>(sql, paramObj, CurTran);
        }

        public Task<T> FindAsync<T>(string sql, object paramObj = null, IDbTransaction CurTran = null)
        {
            return CurConn.QueryFirstOrDefaultAsync<T>(sql, paramObj, CurTran);
        }
        public T Find<T>(object id) where T : class, new()
        {
            return CurConn.Get<T>(id);
        }
        public Task<T> FindAsync<T>(object id) where T : class, new()
        {

            return CurConn.GetAsync<T>(id);
        }
        #endregion

        #region +FindAll
        public IEnumerable<T> FindAll<T>() where T : class, new()
        {
            return CurConn.GetAll<T>();
        }

        public Task<IEnumerable<T>> FindAllAsync<T>() where T : class, new()
        {
            return CurConn.GetAllAsync<T>();
        }
        #endregion

        #region +Add
        public long Add<T>(T model) where T : class, new()
        {

            return CurConn.Insert<T>(model);
        }

        public Task<int> AddAsync<T>(T model) where T : class, new()
        {

            return CurConn.InsertAsync<T>(model);
        }
        #endregion

        #region +Modify
        public Task<bool> ModifyAsync<T>(T model) where T : class, new()
        {
            return CurConn.UpdateAsync<T>(model);
        }

        public bool Modify<T>(T model) where T : class, new()
        {
            return CurConn.Update<T>(model);
        }
        #endregion

        #region +Remove
        public bool Remove<T>(T model) where T : class, new()
        {
            return CurConn.Delete<T>(model);
        }

        public Task<bool> RemoveAsync<T>(T model) where T : class, new()
        {
            return CurConn.DeleteAsync<T>(model);
        }
        #endregion

        #region +Clear
        public Task<bool> ClearAsync<T>() where T : class, new()
        {
            return CurConn.DeleteAllAsync<T>();
        }

        public bool Clear<T>() where T : class, new()
        {
            return CurConn.DeleteAll<T>();
        }
        #endregion


        #region +Muilt

        public Tuple<IEnumerable<T>, int> Page<T>(string countSql, string dataSql, object paramObj = null)
        {
            using (var muilt = CurConn.QueryMultiple(string.Format("{0};{1}", countSql, dataSql), paramObj))
            {
                int count = muilt.Read<int>().FirstOrDefault();
                if (count <= 0) return new Tuple<IEnumerable<T>, int>(new List<T>(), 0);

                var data = muilt.Read<T>();
                return new Tuple<IEnumerable<T>, int>(data, count);
            }
        }

        public Tuple<IEnumerable<A>, IEnumerable<B>> Muilt<A, B>(string muilSql, object paramObj = null)
        {
            using (var muilt = CurConn.QueryMultiple(muilSql, paramObj))
            {
                var one = muilt.Read<A>();
                var two = muilt.Read<B>();
                return Tuple.Create<IEnumerable<A>, IEnumerable<B>>(one, two);
            }
        }
        public Tuple<IEnumerable<A>, IEnumerable<B>, IEnumerable<C>> Muilt<A, B, C>(string muilSql, object paramObj = null)
        {
            using (var muilt = CurConn.QueryMultiple(muilSql, paramObj))
            {
                var one = muilt.Read<A>();
                var two = muilt.Read<B>();
                var three = muilt.Read<C>();
                return Tuple.Create<IEnumerable<A>, IEnumerable<B>, IEnumerable<C>>(one, two, three);
            }
        }
        public Tuple<IEnumerable<A>, IEnumerable<B>, IEnumerable<C>, IEnumerable<D>> Muilt<A, B, C,D>(string muilSql, object paramObj = null)
        {
            using (var muilt = CurConn.QueryMultiple(muilSql, paramObj))
            {
                var one = muilt.Read<A>();
                var two = muilt.Read<B>();
                var three = muilt.Read<C>();
                var four = muilt.Read<D>();
                return Tuple.Create<IEnumerable<A>, IEnumerable<B>, IEnumerable<C>, IEnumerable<D>>(one, two, three,four);
            }
        }

        #endregion


        public void Dispose()
        {
            //销毁事务
            if (CurTran != null)
            {
                CurTran.Dispose();
                CurTran = null;
            }
            //销毁连接
            if (CurConn != null && CurConn.State == ConnectionState.Open)
            {
                CurConn.Dispose();
                CurConn.Close();
                CurConn = null;

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
