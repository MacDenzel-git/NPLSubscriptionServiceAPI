using BusinessLogicLayer.Services.SubscriptionServiceContainer;
using BusinessLogicLayer.Services.SubscriptionTypeServiceContainer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NPLDataAccessLayer.DataTransferObjects;

namespace NPLSubscriptionServiceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriptionTypeController : ControllerBase
    {
        private readonly ISubscriptionTypeService _service;
        public SubscriptionTypeController(ISubscriptionTypeService service)
        {
            _service = service;
        }

        /// <summary>
        /// This is the API for creating Subscription Type
        /// </summary>
        /// <param name="SubscriptionType"></param>
        /// <returns></returns>
        [HttpPost("Create")]
        public async Task<IActionResult> Create(SubscriptionTypeDTO subscriptionType)
        {
            var outputHandler = await _service.Create(subscriptionType);
            if (outputHandler.IsErrorOccured)
            {
                return BadRequest(outputHandler);
            }
            return Ok(outputHandler);
        }

        /// <summary>
        /// This is the API for updating Subscription Type
        /// </summary>
        /// <param name="SubscriptionType"></param>
        /// <returns></returns>
        /// 
        [HttpPut("Update")]
        public async Task<IActionResult> Update(SubscriptionTypeDTO subscriptionType)
        {
            var outputHandler = await _service.Update(subscriptionType);
            if (outputHandler.IsErrorOccured)
            {
                return BadRequest(outputHandler);
            }
            return Ok(outputHandler);
        }

        /// <summary>
        /// This is the API that gets Subscription Type 
        /// </summary>
        /// <returns></returns>
        /// 

        [HttpGet("GetAllSubscriptionTypes")]
        public async Task<IActionResult> GetAllSubscriptionTypes()
        {
            var output = await _service.GetAllSubscriptionTypes();
            if (output != null)
            {
                return Ok(output);
            }
            return NoContent();
        }

        /// <summary>
        /// This is the API that deletes a Subscription Type
        /// </summary>
        /// <param name="SubscriptionTypeId"></param>
        /// <returns></returns>
        /// 
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(int SubscriptionTypeId)
        {
            var output = await _service.Delete(SubscriptionTypeId);
            if (output.IsErrorOccured)
            {
                return BadRequest(output);
            }
            return Ok(output);
        }

     

        /// <summary>
        /// This is API that gets a Subscription Type
        /// </summary>
        /// <param name="fileTypeId"></param>
        /// <returns></returns>
        /// 

        [HttpGet("GetSubscriptionType")]
        public async Task<IActionResult> GetSubsctiptionType(int SubscriptionTypeId)
        {
            var output = await _service.GetSubscriptionType(SubscriptionTypeId);
            if (output != null)
            {
                return Ok(output);
            }
            return NoContent();

        }

    }
}
