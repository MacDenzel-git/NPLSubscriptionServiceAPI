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

namespace BusinessLogicLayer.Services.SubscriptionStatusServiceContainer
{
    public class SubscriptionStatusService : ISubscriptionStatusService 
    {

        private readonly GenericRepository<SubscriptionStatus> _service;
        public SubscriptionStatusService(GenericRepository<SubscriptionStatus> service)
        {
            _service = service;
        }
        public async Task<OutputHandler> Create(SubscriptionStatusDTO subscriptionStatus)
        {
            try
            {
                bool isExist = await _service.AnyAsync(x => x.Description == subscriptionStatus.Description);
                if (isExist)
                {
                    return new OutputHandler
                    {
                        IsErrorOccured = true,
                        Message = StandardMessages.GetDuplicateMessage(subscriptionStatus.Description) 

                    };
                }
 
                var mapped = new AutoMapper<SubscriptionStatusDTO, SubscriptionStatus>().MapToObject(subscriptionStatus);
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
                await _service.Delete(x => x.SubscriptionStatusId == id);
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

        public async Task<SubscriptionStatusDTO> GetSubscriptionStatus(int id)
        {
            var output = await _service.GetSingleItem(x => x.SubscriptionStatusId == id);
            return new AutoMapper<SubscriptionStatus, SubscriptionStatusDTO>().MapToObject(output);
        }

        public async Task<IEnumerable<SubscriptionStatusDTO>> GetAllSubscriptionStatuses()
        {
            var output = await _service.GetAll( );
            return new AutoMapper<SubscriptionStatus, SubscriptionStatusDTO>().MapToList(output);
        }

        public async Task<OutputHandler> Update(SubscriptionStatusDTO subscriptionStatus)
        {
            try
            {
                var mapped = new AutoMapper<SubscriptionStatusDTO, SubscriptionStatus>().MapToObject(subscriptionStatus);
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
