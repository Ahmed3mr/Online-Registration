using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Any;
using OnlineRegisteration.Shared.Models;
using OnlineRegistration.Client.Models;
using OnlineRegistration.Server.ReposInterface;
using OnlineRegistration.Server.ServicesInterfaces;

namespace OnlineRegistration.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ScheduleController : Controller
    {
        private readonly IScheduleService scheduleService;

        public ScheduleController(IScheduleService scheduleService)
        {
            this.scheduleService = scheduleService;
        }

        [HttpGet("GetAllCourses")]
        public async Task<IActionResult> GetAllCourses()
        {
            var result = await scheduleService.AllCourses();
            return Ok(result);
        }

        [HttpGet("TakenCourses")]
        public async Task<IActionResult> TakenCourses(int RegNum)
        {
            var result = await scheduleService.TakenCourses(RegNum);
            return Ok(result);
        }

        [HttpGet("UnTakenCourses")]
        public async Task<IActionResult> UnTakenCourses(int RegNum)
        {
            var result = await scheduleService.untakencourses(RegNum);
            return Ok(result);
        }

        [HttpGet("AvailableCourses")]
        public async Task<IActionResult> AvailableCourses(int RegNum)
        {
            var result = await scheduleService.AvailableCourses(RegNum);
            return Ok(result);
        }

        [HttpGet("AvailableClassrooms")]
        public async Task<IActionResult> AvailableClassrooms(int CourseId)
        {
            var result = await scheduleService.AvailableClassrooms(CourseId);
            return Ok(result);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody]RegisterModel reg )
        {
                
          
                var OLCheck = await scheduleService.OverLappingRegistrationCheck(reg.studentId, reg.classroomIds, reg.regperiodId);
                var CHCheck = await scheduleService.CreditHoursRegistrationCheck(reg.studentId, reg.classroomIds, reg.regperiodId);
                var CCheck = await scheduleService.CapacityRegistrationCheck(reg.studentId, reg.classroomIds, reg.regperiodId);

            if (OLCheck == "" && CHCheck == "" && CCheck == "")
            {

                await scheduleService.Register(reg.studentId, reg.classroomIds, reg.regperiodId);
                return new ObjectResult("200 ok");
            }
            else
            {
                return new ObjectResult(OLCheck+"/n"+CHCheck+"/n"+CCheck);
            }
               
           
        }

        [HttpGet("LastRegPeriod")]
        public async Task<IActionResult> LastRegPeriod()
        {
            var result = await scheduleService.LastRegPeriod();
            return Ok(result);
        }

        [HttpGet("LastRegistered")]
        public async Task<IActionResult> LastRegistered(int studentId)
        {
            var regperiod =await scheduleService.LastRegPeriod();
            var result = await scheduleService.LastRegistered(studentId,regperiod.ID);
            return Ok(result);
        }

        [HttpGet("RegisteredStudents")]
        public async Task<IActionResult> LastRegisteredStudents()
        {
            var result = await scheduleService.LastRegisteredStudents();
            return Ok(result);
        }

        [HttpGet("RegPeriodCheck")]
        public async Task<IActionResult> RegPeriodCheck(DateTime nowtime)
        {
            
                var result = await scheduleService.RegPeriodCheck(nowtime);
                return Ok(result);
            
        }

    }
}
