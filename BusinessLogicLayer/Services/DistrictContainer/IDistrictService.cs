using NPLDataAccessLayer.DataTransferObjects;
using NPLReusableResourcesPackage.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services.DistrictServiceContainer
{
    public interface IDistrictService
    {
        Task<OutputHandler> Create(DistrictDTO district);
        Task<OutputHandler> Update(DistrictDTO district);
        Task<OutputHandler> Delete(int regionId);
        Task<IEnumerable<DistrictDTO>> GetAllDistricts();
        Task<DistrictDTO> GetDistrict(int regionId);
    }
}
