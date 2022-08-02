using NPLDataAccessLayer.DataTransferObjects;
using NPLReusableResourcesPackage.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services.ClientServiceContainer
{
    public interface IClientService
    {
        Task<OutputHandler> Create(ClientDTO client);
        Task<OutputHandler> Update(ClientDTO client);
        Task<OutputHandler> Delete(int clientId);
        Task<IEnumerable<ClientDTO>> GetAllClients();
        Task<IEnumerable<ClientDTO>> ClientsByRegion(int regionId);
        Task<ClientDTO> GetClient(int clientId);
    }
}
