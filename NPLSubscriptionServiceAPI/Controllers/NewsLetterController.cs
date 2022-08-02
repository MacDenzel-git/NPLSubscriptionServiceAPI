using BusinessLogicLayer.Services.NewsLetterContainer;
using BusinessLogicLayer.Services.SubscriptionServiceContainer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NPLDataAccessLayer.DataTransferObjects;

namespace NPLSubscriptionServiceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsLetterController : ControllerBase
    {
        private readonly INewsLetterService _service;
        public NewsLetterController(INewsLetterService service)
        {
            _service = service;
        }

        /// <summary>
        /// This is the API for creating newsLetter
        /// </summary>
        /// <param name="NewsLetter"></param>
        /// <returns></returns>
        [HttpPost("Create")]
        public async Task<IActionResult> Create(NewsLetterDTO newsLetter)
        {
            var outputHandler = await _service.Create(newsLetter);
            if (outputHandler.IsErrorOccured)
            {
                return BadRequest(outputHandler);
            }
            return Ok(outputHandler);
        }

        /// <summary>
        /// This is the API for updating newsLetter
        /// </summary>
        /// <param name="NewsLetter"></param>
        /// <returns></returns>
        /// 
        [HttpPut("Update")]
        public async Task<IActionResult> Update(NewsLetterDTO newsLetter)
        {
            var outputHandler = await _service.Update(newsLetter);
            if (outputHandler.IsErrorOccured)
            {
                return BadRequest(outputHandler);
            }
            return Ok(outputHandler);
        }

        /// <summary>
        /// This is the API that gets newsLetter 
        /// </summary>
        /// <returns></returns>
        /// 

        [HttpGet("GetAllNewsLetters")]
        public async Task<IActionResult> GetAllNewsLetters()
        {
            var output = await _service.GetAllNewsLetters();
            if (output != null)
            {
                return Ok(output);
            }
            return NoContent();
        }

        /// <summary>
        /// This is the API that deletes a newsLetter
        /// </summary>
        /// <param name="NewsLetterId"></param>
        /// <returns></returns>
        /// 
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(int NewsLetterId)
        {
            var output = await _service.Delete(NewsLetterId);
            if (output.IsErrorOccured)
            {
                return BadRequest(output);
            }
            return Ok(output);
        }

     

        /// <summary>
        /// This is API that gets a newsLetter
        /// </summary>
        /// <param name="fileTypeId"></param>
        /// <returns></returns>
        /// 

        [HttpGet("GetNewsLetter")]
        public async Task<IActionResult> GetNewsLetter(int NewsLetterId)
        {
            var output = await _service.GetNewsLetter(NewsLetterId);
            if (output != null)
            {
                return Ok(output);
            }
            return NoContent();

        }


        [HttpGet("SendNewsLetter")]
        public async Task<IActionResult> SendNewsLetter(int NewsLetterId)
        {
            var output = await _service.SendSoftCopyNewsLetter(NewsLetterId);
            if (output != null)
            {
                return Ok(output);
            }
            return NoContent();

        }

    }
}
