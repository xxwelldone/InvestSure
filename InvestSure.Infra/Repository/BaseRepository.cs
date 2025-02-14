

using Dapper;
using InvestSure.Domain.Interfaces;
using InvestSure.Infra.Data;
using Microsoft.Extensions.Configuration;


namespace InvestSure.Infra.Repository

{
    public class BaseRepository<T> : IBaseRepository<T>
    {

        protected DBSession Session;
        protected string _tableName = typeof(T).Name;

        public BaseRepository(DBSession session)
        {
            Session = session;
        }
        public async Task<IEnumerable<T>> GetAllAsync()
        {

            string sql = $@"SELECT * from public.{_tableName}";
            IEnumerable<T> results = await Session.DbConnection.QueryAsync<T>(sql);

            return results;

        }
        public async Task<T> GetByIdAsync(Guid id)
        {



            string sql = $@" SELECT * from public.{_tableName} WHERE id = @id";
            T result = await Session.DbConnection.QueryFirstOrDefaultAsync<T>(sql, new { id });
            return result;

        }
        public async Task<Guid> CreateAsync(T entity)
        {
            try
            {
                Session.BeginTransaction();

                IEnumerable<string> properties = typeof(T).GetProperties()
                    .Where(x => x.Name != "Id" && x.Name != "Accounts")
                    .Select(x => x.Name);


                string columns = string.Join(", ", properties);  
                string values = string.Join(", ", properties.Select(x => "@" + x));  

                string sql = $@"INSERT INTO public.{_tableName} ({columns}) VALUES ({values}) RETURNING id";

                Guid id = await Session.DbConnection.QueryFirstOrDefaultAsync<Guid>(sql, entity); 

                Session.Commit();

                return id;  
            }
            catch (Exception ex)
            {
                Session.Rollback();
                throw new Exception(ex.Message);
            }
        }

        public async Task Update(T entity)
        {
            try
            {
                Session.BeginTransaction();

                IEnumerable<string> properties = typeof(T).GetProperties().Where(x => x.Name != "id").Select(x => x.Name);
                string sets = string.Join(", ", properties.Select(x => $"{x} = @{x}"));
                string sql = $@"UPDATE public.{_tableName} SET {sets} WHERE id = @id";
                Session.DbConnection.Execute(sql, entity);
                Session.Commit();


            }
            catch (Exception ex)
            {
                Session.Rollback();
                throw new Exception(ex.Message);
            }

        }

    }
}
