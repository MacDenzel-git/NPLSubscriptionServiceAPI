using NPLDataAccessLayer.DataTransferObjects;
using NPLReusableResourcesPackage.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services.PaymentTypeContainer
{
    public interface IPaymentTypeService
    {
        Task<OutputHandler> Create(PaymentTypeDTO paymentType);
        Task<OutputHandler> Update(PaymentTypeDTO paymentType);
        Task<OutputHandler> Delete(int paymentId);
        Task<IEnumerable<PaymentTypeDTO>> GetAllPaymentTypes();
        Task<IEnumerable<PaymentTypeDTO>> GetPaymentsByMerchant(int paymentTypeId);

        Task<PaymentTypeDTO> GetPaymentType(int id);
    }
}
