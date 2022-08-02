using BusinessLogicLayer.Services.SelfSubscriptionApplicationContainer;
 using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NPLDataAccessLayer.DataTransferObjects;

namespace NPLSelfSubscriptionServiceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SelfSubscriptionController : ControllerBase
    {
        private readonly ISelfSubscriptionApplicationService _service;
        public SelfSubscriptionController(ISelfSubscriptionApplicationService service)
        {
            _service = service;
        }

        /// <summary>
        /// This is the API for creating SelfSubscription
        /// </summary>
        /// <param name="SelfSubscription"></param>
        /// <returns></returns>
        [HttpPost("CreateApplication")]
        public async Task<IActionResult> CreateApplication(SelfSubscriptionApplicationDTO selfSubscription)
        {
            var outputHandler = await _service.CreateApplication(selfSubscription);
            if (outputHandler.IsErrorOccured)
            {
                return BadRequest(outputHandler);
            }
            return Ok(outputHandler);
        }

        /// <summary>
        /// This is the API for updating SelfSubscription
        /// </summary>
        /// <param name="SelfSubscription"></param>
        /// <returns></returns>
        /// 
        [HttpPut("Update")]
        public async Task<IActionResult> ConvertApplicationToSubscription(SelfSubscriptionApplicationDTO selfSubscription)
        {
            var outputHandler = await _service.ConvertApplicationToSubscription(selfSubscription);
            if (outputHandler.IsErrorOccured)
            {
                return BadRequest(outputHandler);
            }
            return Ok(outputHandler);
        }

        /// <summary>
        /// This is the API that gets SelfSubscription 
        /// </summary>
        /// <returns></returns>
        /// 

        [HttpGet("GetAllSubscriptions")]
        public async Task<IActionResult> GetAllSubscriptions(bool DashboardRequest)
        {
            if (DashboardRequest)
            {
                var output = await _service.GetAllSubscriptionApplications();
                if (output != null)
                {
                    return Ok(output);
                }
            }
            return NoContent();
        }

        /// <summary>
        /// This is the API that deletes a SelfSubscription
        /// </summary>
        /// <param name="SelfSubscriptionId"></param>
        /// <returns></returns>
        /// 
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(int SelfSubscriptionId)
        {
            var output = await _service.Delete(SelfSubscriptionId);
            if (output.IsErrorOccured)
            {
                return BadRequest(output);
            }
            return Ok(output);
        }

     

        /// <summary>
        /// This is API that gets a SelfSubscription
        /// </summary>
        /// <param name="selfSubscriptionId"></param>
        /// <returns></returns>
        /// 

        [HttpGet("GetApplication")]
        public async Task<IActionResult> GetApplication(int selfSubscriptionId)
        {
            var output = await _service.GetApplication(selfSubscriptionId);
            if (output != null)
            {
                return Ok(output);
            }
            return NoContent();

        }

    }
}
