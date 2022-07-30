using BusinessLogicLayer.Services.RegionContainer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NPLDataAccessLayer.DataTransferObjects;

namespace NPLSubscriptionServiceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionController : ControllerBase
    {
        private readonly IRegionService _service;
        public RegionController(IRegionService service)
        {
            _service = service;
        }

        /// <summary>
        /// This is the API for creating Region
        /// </summary>
        /// <param name="region"></param>
        /// <returns></returns>
        [HttpPost("Create")]
        public async Task<IActionResult> Create(RegionDTO region)
        {
            var outputHandler = await _service.Create(region);
            if (outputHandler.IsErrorOccured)
            {
                return BadRequest(outputHandler);
            }
            return Ok(outputHandler);
        }

        /// <summary>
        /// This is the API for updating Region
        /// </summary>
        /// <param name="region"></param>
        /// <returns></returns>
        /// 
        [HttpPut("Update")]
        public async Task<IActionResult> Update(RegionDTO region)
        {
            var outputHandler = await _service.Update(region);
            if (outputHandler.IsErrorOccured)
            {
                return BadRequest(outputHandler);
            }
            return Ok(outputHandler);
        }

        /// <summary>
        /// This is the API that gets Region 
        /// </summary>
        /// <returns></returns>
        /// 

        [HttpGet("GetAllRegions")]
        public async Task<IActionResult> GetAllRegions()
        {
            var output = await _service.GetAllRegions();
            if (output != null)
            {
                return Ok(output);
            }
            return NoContent();
        }

        /// <summary>
        /// This is the API that deletes a Region
        /// </summary>
        /// <param name="regionId"></param>
        /// <returns></returns>
        /// 
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(int regionId)
        {
            var output = await _service.Delete(regionId);
            if (output.IsErrorOccured)
            {
                return BadRequest(output);
            }
            return Ok(output);
        }

     

        /// <summary>
        /// This is API that gets a Region
        /// </summary>
        /// <param name="fileTypeId"></param>
        /// <returns></returns>
        /// 

        [HttpGet("GetRegion")]
        public async Task<IActionResult> GetRegion(int regionId)
        {
            var output = await _service.GetRegion(regionId);
            if (output != null)
            {
                return Ok(output);
            }
            return NoContent();

        }

    }
}
