using NPLDataAccessLayer.DataTransferObjects;
using NPLDataAccessLayer.GenericRepositoryContainer;
using NPLDataAccessLayer.Models;
using NPLReusableResourcesPackage.AutoMapperContainer;
using NPLReusableResourcesPackage.ErrorHandlingContainer;
using NPLReusableResourcesPackage.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services.PublicationServiceContainer
{
    public class PublicationService : IPublicationService 
    {

        private readonly GenericRepository<Publication> _service;
        public PublicationService(GenericRepository<Publication> service)
        {
            _service = service;
        }
        public async Task<OutputHandler> Create(PublicationDTO publication)
        {
            try
            {
                bool isExist = await _service.AnyAsync(x => x.PublicationTitle == publication.PublicationTitle);
                if (isExist)
                {
                    return new OutputHandler
                    {
                        IsErrorOccured = true,
                        Message = StandardMessages.GetDuplicateMessage(publication.PublicationTitle) 

                    };
                }
 
                var mapped = new AutoMapper<PublicationDTO, Publication>().MapToObject(publication);
               var result = await _service.Create(mapped);
                //await _service.SaveChanges();
                return result;
            }
            catch (Exception ex)
            {
                return StandardMessages.getExceptionMessage(ex);
            }

        }

        public async Task<OutputHandler> Delete(int id)
        {

            try
            {
                await _service.Delete(x => x.PublicationId == id);
                //await _service.SaveChanges();
                return new OutputHandler
                {
                    IsErrorOccured = false,
                    Message = StandardMessages.GetSuccessfulMessage()
                };
            }
            catch (Exception ex)
            {
                return StandardMessages.getExceptionMessage(ex);
            }
        }

        public async Task<PublicationDTO> GetPublication(int id)
        {
            var output = await _service.GetSingleItem(x => x.PublicationId == id);
            return new AutoMapper<Publication, PublicationDTO>().MapToObject(output);
        }

        public async Task<IEnumerable<PublicationDTO>> GetAllPublications()
        {
            var output = await _service.GetAll( );
            return new AutoMapper<Publication, PublicationDTO>().MapToList(output);
        }

        public async Task<OutputHandler> Update(PublicationDTO publication)
        {
            try
            {
                var mapped = new AutoMapper<PublicationDTO, Publication>().MapToObject(publication);
                var result = await _service.Update(mapped);
                return result;
              //await _service.SaveChanges();
                 
            }
            catch (Exception ex)
            {
                return StandardMessages.getExceptionMessage(ex);
            }
        }

        
    }
}
