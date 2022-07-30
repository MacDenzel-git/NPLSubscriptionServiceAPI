﻿using Microsoft.EntityFrameworkCore;
using NPLReusableResourcesPackage.ErrorHandlingContainer;
using NPLReusableResourcesPackage.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

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
    }
}
