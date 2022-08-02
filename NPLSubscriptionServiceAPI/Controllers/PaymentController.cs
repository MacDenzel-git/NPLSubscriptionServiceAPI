using BusinessLogicLayer.Services.PaymentServiceContainer;
using BusinessLogicLayer.Services.SubscriptionServiceContainer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NPLDataAccessLayer.DataTransferObjects;

namespace NPLSubscriptionServiceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _service;
        public PaymentController(IPaymentService service)
        {
            _service = service;
        }

        /// <summary>
        /// This is the API for creating Payment
        /// </summary>
        /// <param name="Payment"></param>
        /// <returns></returns>
        [HttpPost("Create")]
        public async Task<IActionResult> Create(PaymentDTO payment)
        {
            var outputHandler = await _service.Create(payment);
            if (outputHandler.IsErrorOccured)
            {
                return BadRequest(outputHandler);
            }
            return Ok(outputHandler);
        }

        /// <summary>
        /// This is the API for updating Payment
        /// </summary>
        /// <param name="PaymentId"></param>
        /// <returns></returns>
        /// 
        [HttpPut("ForcePaymentToExpire")]
        public async Task<IActionResult> ForceToExpire(int paymentId)
        {
            var outputHandler = await _service.ForceToExpire(paymentId);
            if (outputHandler.IsErrorOccured)
            {
                return BadRequest(outputHandler);
            }
            return Ok(outputHandler);
        }

        /// <summary>
        /// This is the API that gets Payment 
        /// </summary>
        /// <returns></returns>
        /// 

        [HttpGet("GetAllPayments")]
        public async Task<IActionResult> GetAllPayments()
        {
            var output = await _service.GetAllPayments();
            if (output != null)
            {
                return Ok(output);
            }
            return NoContent();
        } 
        
        [HttpGet("GetAllPaymentsForSubscription")]
        public async Task<IActionResult> GetAllPaymentsForSubscription()
        {
            var output = await _service.GetAllPaymentsForSubscription();
            if (output != null)
            {
                return Ok(output);
            }
            return NoContent();
        }


        [HttpGet("PaymentsByMerchant")]
        public async Task<IActionResult> PaymentsByMerchant(int paymentTypeId)
        {
            var output = await _service.PaymentsByMerchant(paymentTypeId);
            if (output != null)
            {
                return Ok(output);
            }
            return NoContent();
        }

        /// <summary>
        /// This is the API that deletes a Payment
        /// </summary>
        /// <param name="PaymentId"></param>
        /// <returns></returns>
        /// 
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(int PaymentId)
        {
            var output = await _service.Delete(PaymentId);
            if (output.IsErrorOccured)
            {
                return BadRequest(output);
            }
            return Ok(output);
        }

     

        /// <summary>
        /// This is API that gets a Payment
        /// </summary>
        /// <param name="PaymentId"></param>
        /// <returns></returns>
        /// 

        [HttpGet("GetPayment")]
        public async Task<IActionResult> GetGetPayment(int PaymentId)
        {
            var output = await _service.GetPayment(PaymentId);
            if (output != null)
            {
                return Ok(output);
            }
            return NoContent();

        }

    }
}
