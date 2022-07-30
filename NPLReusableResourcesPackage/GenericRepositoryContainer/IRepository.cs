using NPLReusableResourcesPackage.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NPLReusableResourcesPackage.GenericRepositoryContainer
{
    public interface IRepository<TEntity>
    {
        public Task<IEnumerable<TEntity>> GetAll();
        public Task<OutputHandler> Create(TEntity entity);

        //the below methods take an expression as a parameter to allow selection/filtering through lambda expressions
        public Task<OutputHandler> Delete(Expression<Func<TEntity, bool>> expression);
        public Task<TEntity> GetSingleItem(Expression<Func<TEntity, bool>> expression);
        public Task<OutputHandler> Update(TEntity entity);
    }
}
