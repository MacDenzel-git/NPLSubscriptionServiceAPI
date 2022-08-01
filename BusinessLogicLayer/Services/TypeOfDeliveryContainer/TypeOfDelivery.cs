using NPLDataAccessLayer.DataTransferObjects;
using NPLDataAccessLayer.GenericRepositoryContainer;
using NPLDataAccessLayer.Models;
using NPLReusableResourcesPackage.AutoMapperContainer;
using NPLReusableResourcesPackage.ErrorHandlingContainer;
using NPLReusableResourcesPackage.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services.TypeOfDeliveryServiceContainer
{
    public class TypeOfDeliveryService : ITypeOfDeliveryService 
    {

        private readonly GenericRepository<TypeOfDelivery> _service;
        public TypeOfDeliveryService(GenericRepository<TypeOfDelivery> service)
        {
            _service = service;
        }
        public async Task<OutputHandler> Create(TypeOfDeliveryDTO subscriptionStatus)
        {
            try
            {
                bool isExist = await _service.AnyAsync(x => x.TypeOfDeliveryDescription == subscriptionStatus.TypeOfDeliveryDescription);
                if (isExist)
                {
                    return new OutputHandler
                    {
                        IsErrorOccured = true,
                        Message = StandardMessages.GetDuplicateMessage(subscriptionStatus.TypeOfDeliveryDescription) 

                    };
                }
 
                var mapped = new AutoMapper<TypeOfDeliveryDTO, TypeOfDelivery>().MapToObject(subscriptionStatus);
               var result = await _service.Create(mapped);
                //await _service.SaveChanges();
                return result;
            }
            catch (Exception ex)
            {
                return StandardMessages.getExceptionMessage(ex);
            }

        }

        public async Task<OutputHandler> Delete(int id)
        {

            try
            {
                await _service.Delete(x => x.TypeOfDeliveryId == id);
                //await _service.SaveChanges();
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

        public async Task<TypeOfDeliveryDTO> GetTypeOfDelivery(int id)
        {
            var output = await _service.GetSingleItem(x => x.TypeOfDeliveryId == id);
            return new AutoMapper<TypeOfDelivery, TypeOfDeliveryDTO>().MapToObject(output);
        }

        public async Task<IEnumerable<TypeOfDeliveryDTO>> GetAllTypeOfDeliveries()
        {
            var output = await _service.GetAll( );
            return new AutoMapper<TypeOfDelivery, TypeOfDeliveryDTO>().MapToList(output);
        }

        public async Task<OutputHandler> Update(TypeOfDeliveryDTO subscriptionStatus)
        {
            try
            {
                var mapped = new AutoMapper<TypeOfDeliveryDTO, TypeOfDelivery>().MapToObject(subscriptionStatus);
                var result = await _service.Update(mapped);
                return result;
              //await _service.SaveChanges();
                 
            }
            catch (Exception ex)
            {
                return StandardMessages.getExceptionMessage(ex);
            }
        }

        
    }
}
