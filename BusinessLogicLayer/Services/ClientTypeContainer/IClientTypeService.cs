using NPLDataAccessLayer.DataTransferObjects;
using NPLReusableResourcesPackage.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services.ClientTypeContainer
{
    public interface IClientTypeService
    {
        Task<OutputHandler> Create(ClientTypeDTO clientType);
        Task<OutputHandler> Update(ClientTypeDTO clientType);
        Task<OutputHandler> Delete(int clientTypeId);
        Task<IEnumerable<ClientTypeDTO>> GetAllClientTypes();
        Task<ClientTypeDTO> GetClientType(int clientTypeId);
    }
}
