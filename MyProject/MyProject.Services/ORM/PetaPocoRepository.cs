using System.Collections.Generic;
using MyProject.Services.MvcPager;

namespace MyProject.Services.ORM
{
    public class PetaPocoRepository<T> where T : new()
    {
        public string ConnectionStringName { get; set; }

        private Database Database
        {
            get { return new Database(ConnectionStringName); }
        }

        public object Add(T entity) { return Database.Insert(entity); }

        public int Update(T entity) { return Database.Update(entity); }
        public int Update(Sql sql) { return Database.Update(sql); }
        public int Update(string sql, params object[] args) { return Database.Update(sql, args); }
         
        public int Delete(Sql sql) { return Database.Delete<T>(sql); }

        public T FirstOrDefault(string sql, params object[] args) { return Database.FirstOrDefault<T>(sql, args); }
        public T FirstOrDefault(Sql sql) { return Database.FirstOrDefault<T>(sql); }

        public TDto FirstOrDefault<TDto>(string sql, params  object[] args) { return Database.FirstOrDefault<TDto>(sql, args); }
        public TDto FirstOrDefault<TDto>(Sql sql) { return Database.FirstOrDefault<TDto>(sql); }

        public IEnumerable<T> Query(string sql, params object[] args) { return Database.Query<T>(sql, args); }
        public IEnumerable<T> Query(Sql sql) { return Database.Query<T>(sql); }

        public IEnumerable<TDto> Query<TDto>(string sql, params object[] args) { return Database.Query<TDto>(sql, args); }
        public IEnumerable<TDto> Query<TDto>(Sql sql) { return Database.Query<TDto>(sql); }

        public PagedList<T> PagedList(int pageIndex, int pageSize, string sql, params object[] args)
        {
            var pageData = Database.Page<T>(pageIndex, pageSize, sql, args);
            return new PagedList<T>(pageData.Items, pageIndex, pageSize, pageData.TotalItems);
        }

        public PagedList<T> PagedList(int pageIndex, int pageSize, Sql sql)
        {
            var pageData = Database.Page<T>(pageIndex, pageSize, sql);
            return new PagedList<T>(pageData.Items, pageIndex, pageSize, pageData.TotalItems);
        }

        public PagedList<TDto> PagedList<TDto>(int pageIndex, int pageSize, string sql, params object[] args)
        {
            var pageData = Database.Page<TDto>(pageIndex, pageSize, sql, args);
            return new PagedList<TDto>(pageData.Items, pageIndex, pageSize, pageData.TotalItems);
        }

        public PagedList<TDto> PagedList<TDto>(int pageIndex, int pageSize, Sql sql)
        {
            var pageData = Database.Page<TDto>(pageIndex, pageSize, sql);
            return new PagedList<TDto>(pageData.Items, pageIndex, pageSize, pageData.TotalItems);
        }

        public int Execute(string sql, params object[] args) { return Database.Execute(sql, args); }
        public int Execute(Sql sql) { return Database.Execute(sql); }
    }
}