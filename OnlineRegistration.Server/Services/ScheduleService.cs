using OnlineRegisteration.Shared.Models;
using OnlineRegistration.Client.Models;
using OnlineRegistration.Server.ReposInterface;
using OnlineRegistration.Server.ServicesInterfaces;
using OnlineRegistration.Shared.Models;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace OnlineRegistration.Server.Services
{
    public class ScheduleService : IScheduleService
    {
        private readonly IScheduleRepo _ScheduleRepo;

        public ScheduleService(IScheduleRepo scheduleRepo)
        {
            _ScheduleRepo = scheduleRepo;
        }
        public async Task<IEnumerable<Course>> AllCourses()
        {
            var s = await _ScheduleRepo.AllCourses();
            return s;
        }
        public async Task<IEnumerable<Course>> TakenCourses(int RegNum)
        {
            return await _ScheduleRepo.TakenCourses(RegNum);
        }
        public async Task<IEnumerable<Course>> untakencourses(int regnum)
        {
            var takencourses = await TakenCourses(regnum);
            var allcourses = await AllCourses();
            var tc = takencourses.Select(m => m.CourseId).ToList();
            var ac = allcourses.Select(m => m.CourseId).ToList();
            var utc = ac.Except(tc);
            List<Course> utcourses = new();
            foreach (var u in utc)
            {
                var x = allcourses.Where(a => a.CourseId == u)
                    .Select(a => new Course
            {
                CourseId = a.CourseId,
                Name = a.Name,
                CreditHours = a.CreditHours,
                CourseDependenceId = a.CourseDependenceId,

            })
                    .FirstOrDefault();
                utcourses.Add(x);
            }
            return utcourses;
        }


        public async Task<IEnumerable<Course>> AvailableCourses(int Regnum)
        {
            var takencourses = await TakenCourses(Regnum);

            List<int> test = new List<int>();
            foreach (var c in takencourses)
            {
                test.Add(c.CourseId);
            }
            var untakencourse = await untakencourses(Regnum);
            List<Course> AVC = new();
            foreach (var UTC in untakencourse)
            {
                if (UTC.CourseDependenceId == null || test.Contains(UTC.CourseDependenceId.Value))
                {
                    AVC.Add(UTC);
                }
            }
            return AVC;
        }

        public async Task<IEnumerable<ClassroomModel>> AvailableClassrooms(int CourseId)
        {
            return await _ScheduleRepo.AvailableClassrooms(CourseId);
        }

        public async Task Register(int studentId, List<int> classroomIds, int regperiodId)
        {

               await _ScheduleRepo.Register(studentId, classroomIds, regperiodId);

        }

        public async Task<String> CreditHoursRegistrationCheck (int studentId, List<int> classroomIds, int regperiodId)
        {
            int classroomsCreditHours = 0;
            string result = "";

            foreach (var classroomId in classroomIds)
            {
                var clas = await _ScheduleRepo.GetClassroom(classroomId);

                classroomsCreditHours += clas.TheCourse.CreditHours;


            }
            var registeredclassrooms = await _ScheduleRepo.LastRegistered(studentId, regperiodId);
            foreach (var classes in registeredclassrooms)
            {
                var clas = await _ScheduleRepo.GetClassroom(classes.ClassroomId);
                classroomsCreditHours += clas.TheCourse.CreditHours;

            }
            if (classroomsCreditHours <= 18) ;
            else
                result = ("You cannot register more than 18 credithours.");

            return result;

        }

        public async Task<String> OverLappingRegistrationCheck(int studentId, List<int> classroomIds, int regperiodId)
        {
            List<int> classroomsSlotNumber = new List<int>();
            string result = "";

            foreach (var classroomId in classroomIds)
            {
                var clas = await _ScheduleRepo.GetClassroom(classroomId);

                classroomsSlotNumber.Add(clas.SlotName);


            }
            var registeredclassrooms = await _ScheduleRepo.LastRegistered(studentId, regperiodId);
            foreach (var classes in registeredclassrooms)
            {
                var clas = await _ScheduleRepo.GetClassroom(classes.ClassroomId);
                classroomsSlotNumber.Add(clas.SlotName);

            }
            if (classroomsSlotNumber.Count() == classroomsSlotNumber.Distinct().ToList().Count()) ;

            else
                result = ("Resolve overlapping in classrooms before registeration!");

            return result;

        }

        public async Task<String> CapacityRegistrationCheck(int studentId, List<int> classroomIds, int regperiodId)
        {
            string result = "";
            foreach (var classroomId in classroomIds)
            {
                var clas = await _ScheduleRepo.GetClassroom(classroomId);
                if (clas.Capacity > await _ScheduleRepo.GetNumOfRegisteredStudents(classroomId, regperiodId)) ;


                else
                {

                    result = ("Capacity is full in this course :" + clas.TheCourse.Name + "/n You can check another class for the same course");
                }

            }
            return result;
        }


        public async Task<RegPeriod> LastRegPeriod()
        {
            return await  _ScheduleRepo.LastRegPeriod();
        }

        public async Task<bool> RegPeriodCheck(DateTime nowtime)
        {
           var LastRegPeriod =await _ScheduleRepo.LastRegPeriod();
            if (nowtime > LastRegPeriod.StartDate && nowtime < LastRegPeriod.EndDate) ;
            else
            {
                return false;
                //throw new Exception("registration is now offline please comeback in registration time");
            }
            return true;
        }

        public async Task<IEnumerable<ScheduleSlot>> LastRegistered(int studentId,int regperiodId)
        {


            return await _ScheduleRepo.LastRegistered(studentId, regperiodId);
           
        }

        public async Task<IEnumerable<Student>> LastRegisteredStudents()
        {

            return await _ScheduleRepo.LastRegisteredStudents();
        }

    }
}
