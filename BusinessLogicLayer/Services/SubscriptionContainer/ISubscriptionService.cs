using NPLDataAccessLayer.DataTransferObjects;
using NPLReusableResourcesPackage.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services.SubscriptionServiceContainer
{
    public interface ISubscriptionService
    {
        Task<OutputHandler> Create(SubscriptionDTO subscription);
        Task<OutputHandler> Update(SubscriptionDTO subscription);
        Task<OutputHandler> Delete(int subscriptionId);
        Task<IEnumerable<SubscriptionDTO>> GetAllSubscriptions();
        Task<SubscriptionDTO> GetSubscription(int subscriptionId);

    }
}
