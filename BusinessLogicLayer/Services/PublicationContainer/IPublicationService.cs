using NPLDataAccessLayer.DataTransferObjects;
using NPLReusableResourcesPackage.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services.PublicationServiceContainer
{
    public interface IPublicationService
    {
        Task<OutputHandler> Create(PublicationDTO publication);
        Task<OutputHandler> Update(PublicationDTO publication);
        Task<OutputHandler> Delete(int publicationId);
        Task<IEnumerable<PublicationDTO>> GetAllPublications();
        Task<PublicationDTO> GetPublication(int publicationId);
    }
}
