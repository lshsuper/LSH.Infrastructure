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
    public class DapperReponsitory
    {

        protected DapperFactory _dbFactory;

        public DapperReponsitory(DapperFactory dbFactory)
        {
            _dbFactory = dbFactory;
        }

        #region +Find
        public T Find<T>(object id) where T : class, new()
        {

            using (var ctx = _dbFactory.Create())
            {


                return ctx.Database.Get<T>(id);
            }
        }
        public Task<T> FindAsync<T>(object id) where T : class, new()
        {

            using (var ctx = _dbFactory.Create())
            {


                return ctx.Database.GetAsync<T>(id);
            }
        } 
        #endregion

        #region +FindAll
        public IEnumerable<T> FindAll<T>() where T : class, new()
        {
            using (var ctx = _dbFactory.Create())
            {
                return ctx.Database.GetAll<T>();
            }
        }

        public Task<IEnumerable<T>> FindAllAsync<T>() where T : class, new()
        {
            using (var ctx = _dbFactory.Create())
            {
                return ctx.Database.GetAllAsync<T>();
            }
        } 
        #endregion

        #region +Add
        public long Add<T>(T model) where T : class, new()
        {

            using (var ctx = _dbFactory.Create())
            {
                return ctx.Database.Insert<T>(model);
            }
        }

        public Task<int> AddAsync<T>(T model) where T : class, new()
        {

            using (var ctx = _dbFactory.Create())
            {
                return ctx.Database.InsertAsync<T>(model);
            }
        } 
        #endregion

        #region +Modify
        public Task<bool> ModifyAsync<T>(T model) where T : class, new()
        {
            using (var ctx = _dbFactory.Create())
            {
                return ctx.Database.UpdateAsync<T>(model);
            }
        }

        public bool Modify<T>(T model) where T : class, new()
        {
            using (var ctx = _dbFactory.Create())
            {
                return ctx.Database.Update<T>(model);
            }
        } 
        #endregion

        #region +Remove
        public bool Remove<T>(T model) where T : class, new()
        {
            using (var ctx = _dbFactory.Create())
            {
                return ctx.Database.Delete<T>(model);
            }
        }

        public Task<bool> RemoveAsync<T>(T model) where T : class, new()
        {
            using (var ctx = _dbFactory.Create())
            {
                return ctx.Database.DeleteAsync<T>(model);
            }
        } 
        #endregion

        #region +Clear
        public Task<bool> ClearAsync<T>() where T : class, new()
        {
            using (var ctx = _dbFactory.Create())
            {
                return ctx.Database.DeleteAllAsync<T>();
            }
        }

        public bool Clear<T>() where T : class, new()
        {
            using (var ctx = _dbFactory.Create())
            {
                return ctx.Database.DeleteAll<T>();
            }
        } 
        #endregion

    }
}
