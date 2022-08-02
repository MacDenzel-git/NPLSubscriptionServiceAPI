using BusinessLogicLayer.Services.ClientServiceContainer;
using BusinessLogicLayer.Services.SubscriptionServiceContainer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NPLDataAccessLayer.DataTransferObjects;

namespace NPLSubscriptionServiceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _service;
        public ClientController(IClientService service)
        {
            _service = service;
        }

        /// <summary>
        /// This is the API for creating Client
        /// </summary>
        /// <param name="Client"></param>
        /// <returns></returns>
        [HttpPost("Create")]
        public async Task<IActionResult> Create(ClientDTO client)
        {
            var outputHandler = await _service.Create(client);
            if (outputHandler.IsErrorOccured)
            {
                return BadRequest(outputHandler);
            }
            return Ok(outputHandler);
        }

        /// <summary>
        /// This is the API for updating Client
        /// </summary>
        /// <param name="Client"></param>
        /// <returns></returns>
        /// 
        [HttpPut("Update")]
        public async Task<IActionResult> Update(ClientDTO client)
        {
            var outputHandler = await _service.Update(client);
            if (outputHandler.IsErrorOccured)
            {
                return BadRequest(outputHandler);
            }
            return Ok(outputHandler);
        }

        /// <summary>
        /// This is the API that gets Client 
        /// </summary>
        /// <returns></returns>
        /// 

        [HttpGet("GetAllClients")]
        public async Task<IActionResult> GetAllClients()
        {
            var output = await _service.GetAllClients();
            if (output != null)
            {
                return Ok(output);
            }
            return NoContent();


        }
        
        [HttpGet("ClientsByRegion")]
        public async Task<IActionResult> ClientsByRegion(int regionId)
        {
            var output = await _service.ClientsByRegion(regionId);
            if (output != null)
            {
                return Ok(output);
            }
            return NoContent();
        }

        /// <summary>
        /// This is the API that deletes a Client
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns></returns>
        /// 
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(int clientId)
        {
            var output = await _service.Delete(clientId);
            if (output.IsErrorOccured)
            {
                return BadRequest(output);
            }
            return Ok(output);
        }

     

        /// <summary>
        /// This is API that gets a Client
        /// </summary>
        /// <param name="ClientId"></param>
        /// <returns></returns>
        /// 

        [HttpGet("GetClient")]
        public async Task<IActionResult> GetClient(int ClientId)
        {
            var output = await _service.GetClient(ClientId);
            if (output != null)
            {
                return Ok(output);
            }
            return NoContent();

        }

    }
}
