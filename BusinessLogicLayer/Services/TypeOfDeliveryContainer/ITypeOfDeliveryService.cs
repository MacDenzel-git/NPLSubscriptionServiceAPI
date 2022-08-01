using NPLDataAccessLayer.DataTransferObjects;
using NPLReusableResourcesPackage.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services.TypeOfDeliveryServiceContainer
{
    public interface ITypeOfDeliveryService
    {
        Task<OutputHandler> Create(TypeOfDeliveryDTO typeOfDelivery);
        Task<OutputHandler> Update(TypeOfDeliveryDTO typeOfDelivery);
        Task<OutputHandler> Delete(int typeOfDeliveryId);
        Task<IEnumerable<TypeOfDeliveryDTO>> GetAllTypeOfDeliveries();
        Task<TypeOfDeliveryDTO> GetTypeOfDelivery(int regionId);
    }
}
