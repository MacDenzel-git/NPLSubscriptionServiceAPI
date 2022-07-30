using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPLReusableResourcesPackage.AutoMapperContainer
{
 
        public class AutoMapper<TSource, TDestination>
        {
            public TDestination MapToObject(TSource source)
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<TSource, TDestination>();
                });
                return config.CreateMapper().Map<TSource, TDestination>(source);
            }

            public IEnumerable<TDestination> MapToList(IEnumerable<TSource> sourceList)
            {
                try
                {
                    var config = new MapperConfiguration(cfg =>
                    {
                         cfg.CreateMap<TSource, TDestination>();
                    });

                    return config.CreateMapper().Map<IEnumerable<TSource>, IEnumerable<TDestination>>(sourceList);
                }
                catch (Exception ex)
                {
                  throw new Exception(ex.Message);
                }

            }
        }
    }
 