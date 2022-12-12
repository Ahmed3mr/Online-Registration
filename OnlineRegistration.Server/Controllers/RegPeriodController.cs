using Microsoft.AspNetCore.Mvc;
using OnlineRegisteration.Shared.Models;
using OnlineRegistration.Server.ReposInterface;
using OnlineRegistration.Server.ServicesInterfaces;

namespace OnlineRegistration.Server.Controllers
{
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
        public RegPeriod LastRegPeriod()
        {
            var result = RegPeriodService.LastRegPeriod();
            return result;
        }

        [HttpPost("UpdateRegPeriod")]
        public async Task<IActionResult> ChangeRegPeriod(DateTime SD, DateTime ED, int Adminid)
        { 
            await RegPeriodService.ChangeRegPeriod(SD, ED, Adminid);
            return Ok();
        }

    }










}
