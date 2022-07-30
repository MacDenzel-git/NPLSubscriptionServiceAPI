using BusinessLogicLayer.Services.SubscriptionServiceContainer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NPLDataAccessLayer.DataTransferObjects;

namespace NPLSubscriptionServiceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriptionController : ControllerBase
    {
        private readonly ISubscriptionService _service;
        public SubscriptionController(ISubscriptionService service)
        {
            _service = service;
        }

        /// <summary>
        /// This is the API for creating Subscription
        /// </summary>
        /// <param name="Subscription"></param>
        /// <returns></returns>
        [HttpPost("Create")]
        public async Task<IActionResult> Create(SubscriptionDTO subscription)
        {
            var outputHandler = await _service.Create(subscription);
            if (outputHandler.IsErrorOccured)
            {
                return BadRequest(outputHandler);
            }
            return Ok(outputHandler);
        }

        /// <summary>
        /// This is the API for updating Subscription
        /// </summary>
        /// <param name="Subscription"></param>
        /// <returns></returns>
        /// 
        [HttpPut("Update")]
        public async Task<IActionResult> Update(SubscriptionDTO subscription)
        {
            var outputHandler = await _service.Update(subscription);
            if (outputHandler.IsErrorOccured)
            {
                return BadRequest(outputHandler);
            }
            return Ok(outputHandler);
        }

        /// <summary>
        /// This is the API that gets Subscription 
        /// </summary>
        /// <returns></returns>
        /// 

        [HttpGet("GetAllSubscriptions")]
        public async Task<IActionResult> GetAllSubscriptiones()
        {
            var output = await _service.GetAllSubscriptions();
            if (output != null)
            {
                return Ok(output);
            }
            return NoContent();
        }

        /// <summary>
        /// This is the API that deletes a Subscription
        /// </summary>
        /// <param name="SubscriptionId"></param>
        /// <returns></returns>
        /// 
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(int SubscriptionId)
        {
            var output = await _service.Delete(SubscriptionId);
            if (output.IsErrorOccured)
            {
                return BadRequest(output);
            }
            return Ok(output);
        }

     

        /// <summary>
        /// This is API that gets a Subscription
        /// </summary>
        /// <param name="SubscriptionId"></param>
        /// <returns></returns>
        /// 

        [HttpGet("GetSubscription")]
        public async Task<IActionResult> GetSubsctiption(int SubscriptionId)
        {
            var output = await _service.GetSubscription(SubscriptionId);
            if (output != null)
            {
                return Ok(output);
            }
            return NoContent();

        }

    }
}
