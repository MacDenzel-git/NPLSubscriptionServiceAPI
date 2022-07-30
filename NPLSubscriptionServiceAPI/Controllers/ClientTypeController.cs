using BusinessLogicLayer.Services.ClientTypeContainer;
using BusinessLogicLayer.Services.SubscriptionServiceContainer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NPLDataAccessLayer.DataTransferObjects;

namespace NPLSubscriptionServiceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientTypeController : ControllerBase
    {
        private readonly IClientTypeService _service;
        public ClientTypeController(IClientTypeService service)
        {
            _service = service;
        }

        /// <summary>
        /// This is the API for creating client Type
        /// </summary>
        /// <param name="ClientType"></param>
        /// <returns></returns>
        [HttpPost("Create")]
        public async Task<IActionResult> Create(ClientTypeDTO clientType)
        {
            var outputHandler = await _service.Create(clientType);
            if (outputHandler.IsErrorOccured)
            {
                return BadRequest(outputHandler);
            }
            return Ok(outputHandler);
        }

        /// <summary>
        /// This is the API for updating client Type
        /// </summary>
        /// <param name="ClientType"></param>
        /// <returns></returns>
        /// 
        [HttpPut("Update")]
        public async Task<IActionResult> Update(ClientTypeDTO clientType)
        {
            var outputHandler = await _service.Update(clientType);
            if (outputHandler.IsErrorOccured)
            {
                return BadRequest(outputHandler);
            }
            return Ok(outputHandler);
        }

        /// <summary>
        /// This is the API that gets client Type 
        /// </summary>
        /// <returns></returns>
        /// 

        [HttpGet("GetAllClientTypes")]
        public async Task<IActionResult> GetAllClientTypes()
        {
            var output = await _service.GetAllClientTypes();
            if (output != null)
            {
                return Ok(output);
            }
            return NoContent();
        }

        /// <summary>
        /// This is the API that deletes a client Type
        /// </summary>
        /// <param name="ClientTypeId"></param>
        /// <returns></returns>
        /// 
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(int ClientTypeId)
        {
            var output = await _service.Delete(ClientTypeId);
            if (output.IsErrorOccured)
            {
                return BadRequest(output);
            }
            return Ok(output);
        }

     

        /// <summary>
        /// This is API that gets a client Type
        /// </summary>
        /// <param name="fileTypeId"></param>
        /// <returns></returns>
        /// 

        [HttpGet("GetClientType")]
        public async Task<IActionResult> GetClientType(int ClientTypeId)
        {
            var output = await _service.GetClientType(ClientTypeId);
            if (output != null)
            {
                return Ok(output);
            }
            return NoContent();

        }

    }
}
