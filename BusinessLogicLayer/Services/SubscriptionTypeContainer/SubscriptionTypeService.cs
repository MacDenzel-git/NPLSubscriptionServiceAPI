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

namespace BusinessLogicLayer.Services.SubscriptionTypeServiceContainer
{
    public class SubscriptionTypeService : ISubscriptionTypeService
    {

        private readonly GenericRepository<SubscriptionType> _service;
        public SubscriptionTypeService(GenericRepository<SubscriptionType> service)
        {
            _service = service;
        }
        public async Task<OutputHandler> Create(SubscriptionTypeDTO subscriptionType)
        {
            try
            {
                //check if record with same name already exist to avoid duplicates
                bool isExist = await _service.AnyAsync(x => x.SubscriptionName == subscriptionType.SubscriptionName);
                if (isExist)
                {
                    return new OutputHandler
                    {
                        IsErrorOccured = true,
                        Message = StandardMessages.GetDuplicateMessage(subscriptionType.SubscriptionName)

                    };
                }

                var mapped = new AutoMapper<SubscriptionTypeDTO, SubscriptionType>().MapToObject(subscriptionType);
                var result = await _service.Create(mapped);
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
                await _service.Delete(x => x.SubscriptionTypeId == id);
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

        public async Task<SubscriptionTypeDTO> GetSubscriptionType(int id)
        {
            var output = await _service.GetSingleItem(x => x.SubscriptionTypeId == id);
            return new AutoMapper<SubscriptionType, SubscriptionTypeDTO>().MapToObject(output);
        }

        public async Task<IEnumerable<SubscriptionTypeDTO>> GetAllSubscriptionTypes()
        {
            var output = await _service.GetAll();
            return new AutoMapper<SubscriptionType, SubscriptionTypeDTO>().MapToList(output);
        }

        public async Task<OutputHandler> Update(SubscriptionTypeDTO subscriptionType)
        {
            try
            {
                //check if record with same name already exist to avoid duplicates
                bool isExist = await _service.AnyAsync(x => x.SubscriptionName == subscriptionType.SubscriptionName);
                if (isExist)
                {
                    return new OutputHandler
                    {
                        IsErrorOccured = true,
                        Message = StandardMessages.GetDuplicateMessage(subscriptionType.SubscriptionName)

                    };
                }
                
                var mapped = new AutoMapper<SubscriptionTypeDTO, SubscriptionType>().MapToObject(subscriptionType);
                var result = await _service.Update(mapped);
                return result;
 
            }
            catch (Exception ex)
            {
                return StandardMessages.getExceptionMessage(ex);
            }
        }


    }
}
