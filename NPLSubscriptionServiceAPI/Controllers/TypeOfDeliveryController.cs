using BusinessLogicLayer.Services.SubscriptionServiceContainer;
using BusinessLogicLayer.Services.TypeOfDeliveryServiceContainer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NPLDataAccessLayer.DataTransferObjects;

namespace NPLSubscriptionServiceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TypeOfDeliveryController : ControllerBase
    {
        private readonly ITypeOfDeliveryService _service;
        public TypeOfDeliveryController(ITypeOfDeliveryService service)
        {
            _service = service;
        }

        /// <summary>
        /// This is the API for creating Subscription Status
        /// </summary>
        /// <param name="TypeOfDelivery"></param>
        /// <returns></returns>
        [HttpPost("Create")]
        public async Task<IActionResult> Create(TypeOfDeliveryDTO typeOfDelivery)
        {
            var outputHandler = await _service.Create(typeOfDelivery);
            if (outputHandler.IsErrorOccured)
            {
                return BadRequest(outputHandler);
            }
            return Ok(outputHandler);
        }

        /// <summary>
        /// This is the API for updating Subscription Status
        /// </summary>
        /// <param name="TypeOfDelivery"></param>
        /// <returns></returns>
        /// 
        [HttpPut("Update")]
        public async Task<IActionResult> Update(TypeOfDeliveryDTO typeOfDelivery)
        {
            var outputHandler = await _service.Update(typeOfDelivery);
            if (outputHandler.IsErrorOccured)
            {
                return BadRequest(outputHandler);
            }
            return Ok(outputHandler);
        }

        /// <summary>
        /// This is the API that gets Subscription Status 
        /// </summary>
        /// <returns></returns>
        /// 

        [HttpGet("GetAllTypeOfDeliveries")]
        public async Task<IActionResult> GetAllTypeOfDeliveries()
        {
            var output = await _service.GetAllTypeOfDeliveries();
            if (output != null)
            {
                return Ok(output);
            }
            return NoContent();
        }

        /// <summary>
        /// This is the API that deletes a Subscription Status
        /// </summary>
        /// <param name="TypeOfDeliveryId"></param>
        /// <returns></returns>
        /// 
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(int TypeOfDeliveryId)
        {
            var output = await _service.Delete(TypeOfDeliveryId);
            if (output.IsErrorOccured)
            {
                return BadRequest(output);
            }
            return Ok(output);
        }



        /// <summary>
        /// This is API that gets a Type Of Delivery
        /// </summary>
        /// <param name="fileTypeId"></param>
        /// <returns></returns>
        /// 

        [HttpGet("GetTypeOfDelivery")]
        public async Task<IActionResult> GetTypeOfDelivery(int TypeOfDeliveryId)
        {
            var output = await _service.GetTypeOfDelivery(TypeOfDeliveryId);
            if (output != null)
            {
                return Ok(output);
            }
            return NoContent();

        }

    }
}
