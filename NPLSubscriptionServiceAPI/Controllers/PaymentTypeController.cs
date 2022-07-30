using BusinessLogicLayer.Services.PaymentTypeContainer;
using BusinessLogicLayer.Services.SubscriptionServiceContainer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NPLDataAccessLayer.DataTransferObjects;

namespace NPLSubscriptionServiceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentTypeController : ControllerBase
    {
        private readonly IPaymentTypeService _service;
        public PaymentTypeController(IPaymentTypeService service)
        {
            _service = service;
        }

        /// <summary>
        /// This is the API for creating Payment Type
        /// </summary>
        /// <param name="PaymentType"></param>
        /// <returns></returns>
        [HttpPost("Create")]
        public async Task<IActionResult> Create(PaymentTypeDTO paymentType)
        {
            var outputHandler = await _service.Create(paymentType);
            if (outputHandler.IsErrorOccured)
            {
                return BadRequest(outputHandler);
            }
            return Ok(outputHandler);
        }

        /// <summary>
        /// This is the API for updating Payment Type
        /// </summary>
        /// <param name="PaymentType"></param>
        /// <returns></returns>
        /// 
        [HttpPut("Update")]
        public async Task<IActionResult> Update(PaymentTypeDTO paymentType)
        {
            var outputHandler = await _service.Update(paymentType);
            if (outputHandler.IsErrorOccured)
            {
                return BadRequest(outputHandler);
            }
            return Ok(outputHandler);
        }

        /// <summary>
        /// This is the API that gets Payment Type 
        /// </summary>
        /// <returns></returns>
        /// 

        [HttpGet("GetAllPaymentTypes")]
        public async Task<IActionResult> GetAllPaymentTypes()
        {
            var output = await _service.GetAllPaymentTypes();
            if (output != null)
            {
                return Ok(output);
            }
            return NoContent();
        }

        /// <summary>
        /// This is the API that deletes a Payment Type
        /// </summary>
        /// <param name="PaymentTypeId"></param>
        /// <returns></returns>
        /// 
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(int PaymentTypeId)
        {
            var output = await _service.Delete(PaymentTypeId);
            if (output.IsErrorOccured)
            {
                return BadRequest(output);
            }
            return Ok(output);
        }

     

        /// <summary>
        /// This is API that gets a Payment Type
        /// </summary>
        /// <param name="fileTypeId"></param>
        /// <returns></returns>
        /// 

        [HttpGet("GetPaymentType")]
        public async Task<IActionResult> GetPaymentType(int PaymentTypeId)
        {
            var output = await _service.GetPaymentType(PaymentTypeId);
            if (output != null)
            {
                return Ok(output);
            }
            return NoContent();

        }

    }
}
