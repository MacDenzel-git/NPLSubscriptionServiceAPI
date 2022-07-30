using BusinessLogicLayer.Services.PromotionServiceContainer;
using BusinessLogicLayer.Services.SubscriptionServiceContainer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NPLDataAccessLayer.DataTransferObjects;

namespace NPLSubscriptionServiceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PromotionController : ControllerBase
    {
        private readonly IPromotionService _service;
        public PromotionController(IPromotionService service)
        {
            _service = service;
        }

        /// <summary>
        /// This is the API for creating Promotion
        /// </summary>
        /// <param name="Promotion"></param>
        /// <returns></returns>
        [HttpPost("Create")]
        public async Task<IActionResult> Create(PromotionDTO promotion)
        {
            var outputHandler = await _service.Create(promotion);
            if (outputHandler.IsErrorOccured)
            {
                return BadRequest(outputHandler);
            }
            return Ok(outputHandler);
        }

        /// <summary>
        /// This is the API for updating Promotion
        /// </summary>
        /// <param name="Promotion"></param>
        /// <returns></returns>
        /// 
        [HttpPut("Update")]
        public async Task<IActionResult> Update(PromotionDTO promotion)
        {
            var outputHandler = await _service.Update(promotion);
            if (outputHandler.IsErrorOccured)
            {
                return BadRequest(outputHandler);
            }
            return Ok(outputHandler);
        }

        /// <summary>
        /// This is the API that gets Promotion 
        /// </summary>
        /// <returns></returns>
        /// 

        [HttpGet("GetAllPromotions")]
        public async Task<IActionResult> GetAllPromotions()
        {
            var output = await _service.GetAllPromotions();
            if (output != null)
            {
                return Ok(output);
            }
            return NoContent();
        }

        /// <summary>
        /// This is the API that deletes a Promotion
        /// </summary>
        /// <param name="PromotionId"></param>
        /// <returns></returns>
        /// 
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(int PromotionId)
        {
            var output = await _service.Delete(PromotionId);
            if (output.IsErrorOccured)
            {
                return BadRequest(output);
            }
            return Ok(output);
        }

     

        /// <summary>
        /// This is API that gets a Promotion
        /// </summary>
        /// <param name="fileTypeId"></param>
        /// <returns></returns>
        /// 

        [HttpGet("GetPromotion")]
        public async Task<IActionResult> GetSubsctiptionStatus(int PromotionId)
        {
            var output = await _service.GetPromotion(PromotionId);
            if (output != null)
            {
                return Ok(output);
            }
            return NoContent();

        }

    }
}
