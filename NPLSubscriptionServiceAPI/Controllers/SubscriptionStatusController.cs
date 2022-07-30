using BusinessLogicLayer.Services.SubscriptionServiceContainer;
using BusinessLogicLayer.Services.SubscriptionStatusServiceContainer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NPLDataAccessLayer.DataTransferObjects;

namespace NPLSubscriptionServiceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriptionStatusController : ControllerBase
    {
        private readonly ISubscriptionStatusService _service;
        public SubscriptionStatusController(ISubscriptionStatusService service)
        {
            _service = service;
        }

        /// <summary>
        /// This is the API for creating Subscription Status
        /// </summary>
        /// <param name="SubscriptionStatus"></param>
        /// <returns></returns>
        [HttpPost("Create")]
        public async Task<IActionResult> Create(SubscriptionStatusDTO subscriptionStatus)
        {
            var outputHandler = await _service.Create(subscriptionStatus);
            if (outputHandler.IsErrorOccured)
            {
                return BadRequest(outputHandler);
            }
            return Ok(outputHandler);
        }

        /// <summary>
        /// This is the API for updating Subscription Status
        /// </summary>
        /// <param name="SubscriptionStatus"></param>
        /// <returns></returns>
        /// 
        [HttpPut("Update")]
        public async Task<IActionResult> Update(SubscriptionStatusDTO subscriptionStatus)
        {
            var outputHandler = await _service.Update(subscriptionStatus);
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

        [HttpGet("GetAllSubscriptionStatuses")]
        public async Task<IActionResult> GetAllSubscriptionStatuses()
        {
            var output = await _service.GetAllSubscriptionStatuses();
            if (output != null)
            {
                return Ok(output);
            }
            return NoContent();
        }

        /// <summary>
        /// This is the API that deletes a Subscription Status
        /// </summary>
        /// <param name="SubscriptionStatusId"></param>
        /// <returns></returns>
        /// 
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(int SubscriptionStatusId)
        {
            var output = await _service.Delete(SubscriptionStatusId);
            if (output.IsErrorOccured)
            {
                return BadRequest(output);
            }
            return Ok(output);
        }

     

        /// <summary>
        /// This is API that gets a Subscription Status
        /// </summary>
        /// <param name="fileTypeId"></param>
        /// <returns></returns>
        /// 

        [HttpGet("GetSubscriptionStatus")]
        public async Task<IActionResult> GetSubsctiptionStatus(int SubscriptionStatusId)
        {
            var output = await _service.GetSubscriptionStatus(SubscriptionStatusId);
            if (output != null)
            {
                return Ok(output);
            }
            return NoContent();

        }

    }
}
