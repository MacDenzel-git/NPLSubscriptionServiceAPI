using NPLDataAccessLayer.Models;
using NPLReusableResourcesPackage.GenericRepositoryContainer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPLDataAccessLayer.GenericRepositoryContainer
{
    public class GenericRepository<TEntity> : Repository<TEntity> where TEntity : class
    {
        private readonly NPLSubsctiptionServiceDBContext _dBContext;
        public GenericRepository(NPLSubsctiptionServiceDBContext dbContext) : base(dbContext)
        {
            _dBContext = dbContext;
        }
    }
}
