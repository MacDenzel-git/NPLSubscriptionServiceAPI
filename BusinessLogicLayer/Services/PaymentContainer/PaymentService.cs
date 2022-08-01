using Microsoft.EntityFrameworkCore;
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
        private readonly NPLSubsctiptionServiceDBContext _context;
        public PaymentService(GenericRepository<Payment> service, NPLSubsctiptionServiceDBContext  context)
        {
            _context = context;
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
            IEnumerable<PaymentDTO> output = await (from payments in _context.Payments
                         join c in _context.Clients on payments.ClientId equals c.ClientId 
                         join pt in _context.PaymentTypes on payments.PaymentTypeId equals pt.PaymentTypeId 
                         select new PaymentDTO
                         {
                             ClientId = c.ClientId,
                             ClientName = c.ClientName,
                             PaymentTypeDescription = pt.Description,
                             AccountNumber = pt.AccountNumber,
                             TransactionId = payments.TransactionId,
                             PaymentId = payments.PaymentId
                         }).ToListAsync();

            return output;
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
