using Microsoft.EntityFrameworkCore;
using NPLReusableResourcesPackage.ErrorHandlingContainer;
using NPLReusableResourcesPackage.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
 using System.Data.SqlClient;

namespace NPLReusableResourcesPackage.GenericRepositoryContainer
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly DbContext _context;

        public Repository(DbContext context)
        {
            _context = context;
        }


        /// <summary>
        /// This methods helps you create a record in the database
        /// </summary>
        /// <param name="entity">takes a instance of the model</param>
        /// <returns></returns>
        public async Task<OutputHandler> Create(TEntity entity)
        {
            try
            {
                _context.Add(entity);
                await _context.SaveChangesAsync();
                return new OutputHandler
                {
                    IsErrorOccured = false,
                    Message = StandardMessages.GetSuccessfulMessage()
                };
            }
            catch (Exception ex)
            {
                return StandardMessages.getExceptionMessage(ex);

            }
        }

        /// <summary>
        /// This method deletes an item in the message
        /// </summary>
        /// <param name="expression">takes an expression as a parameter</param>
        /// <returns></returns>
        public async Task<OutputHandler> Delete(Expression<Func<TEntity, bool>> expression)
        {
            try
            {
                var entity = await _context.Set<TEntity>().FirstOrDefaultAsync(expression);
                if (entity==null)
                {
                    return new OutputHandler
                    {
                        IsErrorOccured = false,
                        Message = StandardMessages.GetGeneralErrorMessage()
                    };

                }
                _context.Remove(entity);
                await _context.SaveChangesAsync();
                return new OutputHandler
                {
                    IsErrorOccured = false,
                    Message = StandardMessages.GetSuccessfulMessage()
                };
            }
            catch (Exception ex)
            {
                return StandardMessages.getExceptionMessage(ex);

            }
        }

        /// <summary>
        /// This method gets all items in the database for the model specified
        /// </summary>
        /// <returns>All Items in the Database for the table/model specified</returns>
        public async Task<IEnumerable<TEntity>> GetAll()
        {

            var result = await _context.Set<TEntity>().ToListAsync();
            return result;
        }

        /// <summary>
        /// this method returns a single row of information from the database, based on the filter
        /// </summary>
        /// <param name="expression"></param>
        /// <returns>single entity of that row</returns>
        public async Task<TEntity> GetSingleItem(Expression<Func<TEntity, bool>> expression)
        {
            return await _context.Set<TEntity>().FirstOrDefaultAsync(expression);

        }

        /// <summary>
        /// This method is used to update a row in the database
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>Return output handler</returns>
        public async Task<OutputHandler> Update(TEntity entity)
        {
            try
            {
                _context.Entry(entity).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return new OutputHandler
                {
                    IsErrorOccured = false,
                    Message = StandardMessages.GetSuccessfulMessage()
                };
            }
            catch (Exception ex)
            {
                return StandardMessages.getExceptionMessage(ex);
            }

        }

        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> expression)
        {
            var result = await _context.Set<TEntity>().AnyAsync(expression);
            return result;
        }

        public async Task<TEntity> GetFilteredItemWithChildEntity(Expression<Func<TEntity, bool>> expression, string entity)
        {

            return await _context.Set<TEntity>().Include(entity).FirstOrDefaultAsync(expression);

        }

        public virtual async Task<IEnumerable<TModel>> FromSprocAsync<TModel>(string sproc, IDictionary<string, object> parameters = null) where TModel : new()
        {
             IEnumerable<TModel> data = Enumerable.Empty<TModel>();
            //await _context.Database.OpenConnectionAsync();
            //var command = _context.Database.GetDbConnection().CreateCommand();
            var command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = sproc;

                if (parameters != null)
                {
                    foreach (var parameter in parameters)
                    {
                        var dbParameter = command.CreateParameter();
                        dbParameter.ParameterName = parameter.Key;
                        dbParameter.Value = parameter.Value;
                        command.Parameters.Add(dbParameter);
                    }
                }
                using (var reader = await command.ExecuteReaderAsync())
                {
                    data = await reader.MapToListAsync<TModel>();
                }
                return data;
        }
    }
}
