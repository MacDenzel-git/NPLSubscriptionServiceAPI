using NPLDataAccessLayer.DataTransferObjects;
using NPLReusableResourcesPackage.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services.SelfSubscriptionApplicationContainer
{
    public interface ISelfSubscriptionApplicationService
    {
        public Task<OutputHandler> CreateApplication(SelfSubscriptionApplicationDTO selfSubscription);
        public Task<OutputHandler> ConvertApplicationToSubscription(SelfSubscriptionApplicationDTO applicationDTO);
        public Task<OutputHandler> Delete(int applicationId); //SoftDelete
        public Task<SelfSubscriptionApplicationDTO> GetApplication(int applicationId);
        public Task<IEnumerable<SelfSubscriptionApplicationDTO>> GetAllSubscriptionApplications();
    }
}
