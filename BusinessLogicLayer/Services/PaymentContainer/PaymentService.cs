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

namespace BusinessLogicLayer.Services.PaymentServiceContainer
{
    public class PaymentService : IPaymentService
    {

        private readonly GenericRepository<Payment> _service;
        public PaymentService(GenericRepository<Payment> service)
        {
            _service = service;
        }
        public async Task<OutputHandler> Create(PaymentDTO payment)
        {
            try
            {
                //The same transaction cannot be entered twice
                bool isExist = await _service.AnyAsync(x => x.TransactionId == payment.TransactionId);
                if (isExist)
                {
                    return new OutputHandler
                    {
                        IsErrorOccured = true,
                        Message = $"{StandardMessages.GetDuplicateMessage(payment.TransactionId)}, it has already been used"

                    };
                }

                var mapped = new AutoMapper<PaymentDTO, Payment>().MapToObject(payment);
                mapped.IsUsed = false;
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
                await _service.Delete(x => x.PaymentId == id);
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

        public async Task<PaymentDTO> GetPayment(int id)
        {
            var output = await _service.GetSingleItem(x => x.PaymentId == id);
            return new AutoMapper<Payment, PaymentDTO>().MapToObject(output);
        }

        public async Task<IEnumerable<PaymentDTO>> GetAllPayments()
        {
            var output = await _service.GetAll();
            return new AutoMapper<Payment, PaymentDTO>().MapToList(output);
        }

        #region Commented update Code in case it's needed
        //public async Task<OutputHandler> Update(PaymentDTO payment)
        //{
        //    //
        //    try
        //    { 
        //        var mapped = new AutoMapper<PaymentDTO, Payment>().MapToObject(payment);
        //        var result = await _service.Update(mapped);
        //        return result;

        //    }
        //    catch (Exception ex)
        //    {
        //        return StandardMessages.getExceptionMessage(ex);
        //    }
        //}
        #endregion
        public async Task<OutputHandler> ForceToExpire(int paymentId)
        {
             //change status of payment to used
            try
            {
                var paymentDetails = await _service.GetSingleItem(x => x.PaymentId == paymentId);
                paymentDetails.IsUsed = true;
                var result = await _service.Update(paymentDetails);
                return result;

            }
            catch (Exception ex)
            {
                return StandardMessages.getExceptionMessage(ex);
            }
        }

    }
}
