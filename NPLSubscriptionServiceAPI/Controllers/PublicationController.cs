using BusinessLogicLayer.Services.SubscriptionServiceContainer;
using BusinessLogicLayer.Services.PublicationServiceContainer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NPLDataAccessLayer.DataTransferObjects;

namespace NPLSubscriptionServiceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublicationController : ControllerBase
    {
        private readonly IPublicationService _service;
        public PublicationController(IPublicationService service)
        {
            _service = service;
        }

        /// <summary>
        /// This is the API for creating Subscription Status
        /// </summary>
        /// <param name="Publication"></param>
        /// <returns></returns>
        [HttpPost("Create")]
        public async Task<IActionResult> Create(PublicationDTO publication)
        {
            var outputHandler = await _service.Create(publication);
            if (outputHandler.IsErrorOccured)
            {
                return BadRequest(outputHandler);
            }
            return Ok(outputHandler);
        }

        /// <summary>
        /// This is the API for updating Subscription Status
        /// </summary>
        /// <param name="Publication"></param>
        /// <returns></returns>
        /// 
        [HttpPut("Update")]
        public async Task<IActionResult> Update(PublicationDTO publication)
        {
            var outputHandler = await _service.Update(publication);
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

        [HttpGet("GetAllPublications")]
        public async Task<IActionResult> GetAllPublications()
        {
            var output = await _service.GetAllPublications();
            if (output != null)
            {
                return Ok(output);
            }
            return NoContent();
        }

        /// <summary>
        /// This is the API that deletes a Subscription Status
        /// </summary>
        /// <param name="PublicationId"></param>
        /// <returns></returns>
        /// 
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(int PublicationId)
        {
            var output = await _service.Delete(PublicationId);
            if (output.IsErrorOccured)
            {
                return BadRequest(output);
            }
            return Ok(output);
        }



        /// <summary>
        /// This is API that gets a Publication
        /// </summary>
        /// <param name="fileTypeId"></param>
        /// <returns></returns>
        /// 

        [HttpGet("GetPublication")]
        public async Task<IActionResult> GetPublication(int PublicationId)
        {
            var output = await _service.GetPublication(PublicationId);
            if (output != null)
            {
                return Ok(output);
            }
            return NoContent();

        }

    }
}
