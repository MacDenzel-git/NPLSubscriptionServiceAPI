using BusinessLogicLayer.Services.ReportsContainer;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NPLSubscriptionServiceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly IReportService _service;
        public ReportsController(IReportService service)
        {
            _service = service;
        }
 

        /// <summary>
        /// This is the API that gets Reports 
        /// </summary>
        /// <returns></returns>
        /// 

        [HttpGet("ListActiveSubscribedUsers")]
        public async Task<IActionResult> ListActiveSubscribedUsers()
        {
            var output = await _service.ListActiveSubscribedUsers();
            if (output != null)
            {
                return Ok(output);
            }
            return NoContent();
        }

        /// <summary>
        /// This is the API that gets Reports 
        /// </summary>
        /// <returns></returns>
        /// 

        [HttpGet("ListExpiredSubscribedUsers")]
        public async Task<IActionResult> ListExpiredSubscribedUsers()
        {
            var output = await _service.ListExpiredSubscribedUsers();
            if (output != null)
            {
                return Ok(output);
            }
            return NoContent();
        }



        /// <summary>
        /// This is the API that gets Reports 
        /// </summary>
        /// <returns></returns>
        /// 

        [HttpGet("ListClientsByRegion")]
        public async Task<IActionResult> ListClientsByRegion(int regionId)
        {
            var output = await _service.ListClientsByRegion(regionId);
            if (output != null)
            {
                return Ok(output);
            }
            return NoContent();
        }
        /// <summary>
        /// This is the API that gets Reports 
        /// </summary>
        /// <returns></returns>
        /// 

        [HttpGet("SetupReportDashboard")]
        public async Task<IActionResult> SetupReportDashboard( )
        {
            var output = await _service.SetupReportDashboard( );
            if (output != null)
            {
                return Ok(output);
            }
            return NoContent();
        }
        /// <summary>
        /// This is the API that gets ActiveUsersGroupedByPublications 
        /// </summary>
        /// <returns></returns>
        /// 

        [HttpGet("ActiveUsersGroupedByPublications")]
        public async Task<IActionResult> ActiveUsersGroupedByPublications()
        {
            var output = await _service.ActiveUsersGroupedByPublications();
            if (output != null)
            {
                return Ok(output);
            }
            return NoContent();
        }
        /// <summary>
        /// This is the API that gets a GetReceipt 
        /// </summary>
        /// <returns></returns>
        /// 

        [HttpGet("GetReceipt")]
        public async Task<IActionResult> GetReceipt(int paymentId)
        {
            var output = await _service.GetReceipt(paymentId);
            if (output != null)
            {
                return Ok(output);
            }
            return NoContent();
        }


    }
}
