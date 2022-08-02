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

namespace BusinessLogicLayer.Services.SelfSubscriptionApplicationContainer
{
    public class SelfSubscriptionApplicationService: ISelfSubscriptionApplicationService
    {
        private readonly GenericRepository<SelfSubscriptionApplication> _service;
        private readonly NPLSubsctiptionServiceDBContext _context;
        public SelfSubscriptionApplicationService(NPLSubsctiptionServiceDBContext context,GenericRepository<SelfSubscriptionApplication> service)
        {
            _context = context;
            _service = service;
        }
        public async Task<OutputHandler> CreateApplication(SelfSubscriptionApplicationDTO selfSubscriptionApplication)
        {
            try
            {
                var mapped = new AutoMapper<SelfSubscriptionApplicationDTO, SelfSubscriptionApplication>().MapToObject(selfSubscriptionApplication);
                var result = await _service.Create(mapped);
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
            {   var application = await _service.GetSingleItem(x => x.SubscriptionApplicationId == id); 
                application.IsDeleted = true;//change to Isdeleted For Soft Delete
                await _service.Update(application);
                
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

        public async Task<SelfSubscriptionApplicationDTO> GetApplication(int id)
        {
            var output = await _service.GetSingleItem(x => x.SubscriptionApplicationId == id);
            return new AutoMapper<SelfSubscriptionApplication, SelfSubscriptionApplicationDTO>().MapToObject(output);
        }

        public async Task<IEnumerable<SelfSubscriptionApplicationDTO>> GetAllSubscriptionApplications()
        {
            var output = (from s in _context.SelfSubscriptionApplications
                          join pub in _context.Publications on s.PublicationId equals pub.PublicationId
                           join st in _context.SubscriptionTypes on s.SubscriptionTypeId equals st.SubscriptionTypeId
                           join d in _context.TypeOfDeliveries on s.TypeOfDeliveryId equals d.TypeOfDeliveryId
                           join pt in _context.PaymentTypes on s.PaymentTypeId equals pt.PaymentTypeId
                           select new SelfSubscriptionApplicationDTO
                          {
                              SubscriptionApplicationId = s.SubscriptionApplicationId ,
                              FullName = s.FullName,
                              PublicationTitle = pub.PublicationTitle,
                              SubscriptionTypeDescription = st.SubscriptionName,
                               NumberOfCopies = s.NumberOfCopies,
                              SubscriptionStartDate = s.SubscriptionStartDate,
                              IsConfirmed = s.IsConfirmed

                          }).ToList();
            return output;
            
        }

        public async Task<OutputHandler> ConvertApplicationToSubscription(SelfSubscriptionApplicationDTO selfSubscriptionApplication)
        {
            try
            {
                //Create Client
                

                //Create Payment 

                //Create Subscription

                var mapped = new AutoMapper<SelfSubscriptionApplicationDTO, SelfSubscriptionApplication>().MapToObject(selfSubscriptionApplication);
                var result = await _service.Update(mapped);
                return result;
               

            }
            catch (Exception ex)
            {
                return StandardMessages.getExceptionMessage(ex);
            }
        }

    }
}
