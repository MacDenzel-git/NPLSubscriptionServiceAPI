using NPLDataAccessLayer.DataTransferObjects;
using NPLReusableResourcesPackage.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services.SubscriptionStatusServiceContainer
{
    public interface ISubscriptionStatusService
    {
        Task<OutputHandler> Create(SubscriptionStatusDTO subscriptionStatus);
        Task<OutputHandler> Update(SubscriptionStatusDTO subscriptionStatus);
        Task<OutputHandler> Delete(int regionId);
        Task<IEnumerable<SubscriptionStatusDTO>> GetAllSubscriptionStatuses();
        Task<SubscriptionStatusDTO> GetSubscriptionStatus(int regionId);
    }
}
