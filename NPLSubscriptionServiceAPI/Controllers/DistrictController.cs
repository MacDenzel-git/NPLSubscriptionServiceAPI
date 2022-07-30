using BusinessLogicLayer.Services.DistrictServiceContainer;
using BusinessLogicLayer.Services.SubscriptionServiceContainer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NPLDataAccessLayer.DataTransferObjects;

namespace NPLSubscriptionServiceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DistrictController : ControllerBase
    {
        private readonly IDistrictService _service;
        public DistrictController(IDistrictService service)
        {
            _service = service;
        }

        /// <summary>
        /// This is the API for creating District
        /// </summary>
        /// <param name="District"></param>
        /// <returns></returns>
        [HttpPost("Create")]
        public async Task<IActionResult> Create(DistrictDTO district)
        {
            var outputHandler = await _service.Create(district);
            if (outputHandler.IsErrorOccured)
            {
                return BadRequest(outputHandler);
            }
            return Ok(outputHandler);
        }

        /// <summary>
        /// This is the API for updating District
        /// </summary>
        /// <param name="District"></param>
        /// <returns></returns>
        /// 
        [HttpPut("Update")]
        public async Task<IActionResult> Update(DistrictDTO district)
        {
            var outputHandler = await _service.Update(district);
            if (outputHandler.IsErrorOccured)
            {
                return BadRequest(outputHandler);
            }
            return Ok(outputHandler);
        }

        /// <summary>
        /// This is the API that gets District 
        /// </summary>
        /// <returns></returns>
        /// 

        [HttpGet("GetAllDistricts")]
        public async Task<IActionResult> GetAllDistrictes()
        {
            var output = await _service.GetAllDistricts();
            if (output != null)
            {
                return Ok(output);
            }
            return NoContent();
        }

        /// <summary>
        /// This is the API that deletes a District
        /// </summary>
        /// <param name="DistrictId"></param>
        /// <returns></returns>
        /// 
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(int DistrictId)
        {
            var output = await _service.Delete(DistrictId);
            if (output.IsErrorOccured)
            {
                return BadRequest(output);
            }
            return Ok(output);
        }

     

        /// <summary>
        /// This is API that gets a District
        /// </summary>
        /// <param name="fileTypeId"></param>
        /// <returns></returns>
        /// 

        [HttpGet("GetDistrict")]
        public async Task<IActionResult> GetSubsctiptionStatus(int DistrictId)
        {
            var output = await _service.GetDistrict(DistrictId);
            if (output != null)
            {
                return Ok(output);
            }
            return NoContent();

        }

    }
}
