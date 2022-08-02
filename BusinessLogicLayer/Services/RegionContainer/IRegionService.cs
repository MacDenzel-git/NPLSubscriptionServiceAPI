using NPLDataAccessLayer.DataTransferObjects;
using NPLReusableResourcesPackage.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services.RegionContainer
{
    public interface IRegionService
    {
        Task<OutputHandler> Create(RegionDTO region);
        Task<OutputHandler> Update(RegionDTO region);
        Task<OutputHandler> Delete(int regionId);
        Task<IEnumerable<RegionDTO>> GetAllRegions();
        Task<RegionDTO> GetRegion(int regionId);
        
    }
}
