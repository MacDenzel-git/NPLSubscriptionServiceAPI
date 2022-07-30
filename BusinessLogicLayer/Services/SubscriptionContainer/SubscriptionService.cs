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

namespace BusinessLogicLayer.Services.SubscriptionServiceContainer
{
    public class SubscriptionService : ISubscriptionService 
    {

        private readonly GenericRepository<Subscription> _service;
        public SubscriptionService(GenericRepository<Subscription> service)
        {
            _service = service;
        }
        public async Task<OutputHandler> Create(SubscriptionDTO subscription)
        {
            try
            {
                //check if the client already has a running subscription
                Subscription output = await _service.GetSingleItem(x => x.ClientId == subscription.ClientId && x.ExpiryDate < DateTime.UtcNow.AddHours(2));
                if (output != null)
                {
                    return new OutputHandler
                    {
                        IsErrorOccured = true,
                        Message = $"This Client already has an active subscription scheduled to expire on {output.ExpiryDate.ToShortDateString()}"
                    };
                }

               var mapped = new AutoMapper<SubscriptionDTO, Subscription>().MapToObject(subscription);
               var result = await _service.Create(mapped);
                 return result;
            }
            catch (Exception ex)
            {
                return StandardMessages.getExceptionMessage(ex);
            }

        }

        public async Task<OutputHandler> Delete(int subscriptionId)
        {

            try
            {
                await _service.Delete(x => x.SubscriptionId == subscriptionId);
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

        public async Task<SubscriptionDTO> GetSubscription(int subscriptionId)
        {
            var output = await _service.GetSingleItem(x => x.SubscriptionId == subscriptionId);
            return new AutoMapper<Subscription, SubscriptionDTO>().MapToObject(output);
        }

        public async Task<IEnumerable<SubscriptionDTO>> GetAllSubscriptions()
        {
            var output = await _service.GetAll( );
            return new AutoMapper<Subscription, SubscriptionDTO>().MapToList(output);
        }

        public async Task<OutputHandler> Update(SubscriptionDTO subscription)
        {
            try
            {
                //check if the client already has a running subscription
                Subscription output = await _service.GetSingleItem(x => x.ClientId == subscription.ClientId && x.ExpiryDate < DateTime.UtcNow.AddHours(2));
                if (output != null)
                {
                    return new OutputHandler
                    {
                        IsErrorOccured = true,
                        Message = $"This Client already has an active subscription scheduled to expire on {output.ExpiryDate.ToShortDateString()}"
                    };
                }
                var mapped = new AutoMapper<SubscriptionDTO, Subscription>().MapToObject(subscription);
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
