using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TManagement.AppServices.Loockups;
using TManagement.Entities;

namespace TManagement.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoockupsController(ILoockupAppService loockupAppService) : ControllerBase
    {

        [HttpGet("getall/{type}")]
        public async Task<ActionResult<List<LoockupDto>>> GetLookups(LookupType type )
        {

            var  result =  loockupAppService.GetLoockupList(type);

            return Ok(result);  
        }

    }
}
