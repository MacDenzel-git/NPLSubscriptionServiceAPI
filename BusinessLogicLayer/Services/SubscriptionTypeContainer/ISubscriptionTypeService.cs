using NPLDataAccessLayer.DataTransferObjects;
using NPLReusableResourcesPackage.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services.SubscriptionTypeServiceContainer
{
    public interface ISubscriptionTypeService
    {
        Task<OutputHandler> Create(SubscriptionTypeDTO subscriptionType);
        Task<OutputHandler> Update(SubscriptionTypeDTO subscriptionType);
        Task<OutputHandler> Delete(int subcriptionTypeId);
        Task<IEnumerable<SubscriptionTypeDTO>> GetAllSubscriptionTypes();
        Task<SubscriptionTypeDTO> GetSubscriptionType(int subcriptionTypeId);
    }
}
