using BusinessLogicLayer.Services.PaymentTypeContainer;
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

namespace BusinessLogicLayer.Services.SubscriptionServiceContainer
{
    public class PaymentTypeService : IPaymentTypeService 
    {

        private readonly GenericRepository<PaymentType> _service;
        public PaymentTypeService(GenericRepository<PaymentType> service)
        {
            _service = service;
        }
        public async Task<OutputHandler> Create(PaymentTypeDTO paymentType)
        {
            try
            {
                //check if record with same name already exist to avoid duplicates
                bool isExist = await _service.AnyAsync(x => x.Description == paymentType.Description);
                if (isExist)
                {
                    return new OutputHandler
                    {
                        IsErrorOccured = true,
                        Message = StandardMessages.GetDuplicateMessage(paymentType.Description) 

                    };
                }
 
                var mapped = new AutoMapper<PaymentTypeDTO, PaymentType>().MapToObject(paymentType);
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
            {
                await _service.Delete(x => x.PaymentTypeId == id);
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

        public async Task<PaymentTypeDTO> GetPaymentType(int id)
        {
            var output = await _service.GetSingleItem(x => x.PaymentTypeId == id);
            return new AutoMapper<PaymentType, PaymentTypeDTO>().MapToObject(output);
        }

        public async Task<IEnumerable<PaymentTypeDTO>> GetAllPaymentTypes()
        {
            var output = await _service.GetAll( );
            return new AutoMapper<PaymentType, PaymentTypeDTO>().MapToList(output);
        }

        public async Task<OutputHandler> Update(PaymentTypeDTO paymentType)
        {
            try
            {
                //check if record with same name already exist to avoid duplicates
                bool isExist = await _service.AnyAsync(x => x.AccountNumber == paymentType.AccountNumber && x.Description == paymentType.Description);
                if (isExist)
                {
                    return new OutputHandler
                    {
                        IsErrorOccured = true,
                        Message = $"An Account with these details already exist > Bank:{paymentType.AccountNumber} Number:{paymentType.AccountNumber}"

                    };
                }

                var mapped = new AutoMapper<PaymentTypeDTO, PaymentType>().MapToObject(paymentType);
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
