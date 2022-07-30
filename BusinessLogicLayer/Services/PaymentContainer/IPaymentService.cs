using NPLDataAccessLayer.DataTransferObjects;
using NPLReusableResourcesPackage.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services.PaymentServiceContainer
{
    public interface IPaymentService
    {
        Task<OutputHandler> Create(PaymentDTO payment);
        //Task<OutputHandler> Update(PaymentDTO payment);
        Task<OutputHandler> Delete(int paymentId);
        Task<IEnumerable<PaymentDTO>> GetAllPayments();
        Task<PaymentDTO> GetPayment(int paymentId);
        Task<OutputHandler> ForceToExpire(int paymentId);
    }
}
