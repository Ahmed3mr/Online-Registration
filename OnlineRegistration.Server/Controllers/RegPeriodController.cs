using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineRegisteration.Shared.Models;
using OnlineRegistration.Client.Models;
using OnlineRegistration.Server.ReposInterface;
using OnlineRegistration.Server.ServicesInterfaces;

namespace OnlineRegistration.Server.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("[controller]")]

    public class RegPeriodController : Controller
    {
        private readonly IRegPeriodService RegPeriodService;

        public RegPeriodController(IRegPeriodService scheduleService)
        {
            this.RegPeriodService = scheduleService;
        }


        [HttpGet("LastRegPeriod")]
        public async Task<IActionResult> LastRegPeriod()
        {
            var result = await RegPeriodService.LastRegPeriod();
            return Ok(result);
        }

        [HttpPost("UpdateRegPeriod")]
        public async Task<IActionResult> ChangeRegPeriod([FromBody] RegPeriodModel regPeriod)
        { 
            await RegPeriodService.ChangeRegPeriod(regPeriod);
            return Ok();
        }

    }










}
