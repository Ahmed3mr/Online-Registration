using Microsoft.EntityFrameworkCore;
using OnlineRegistration.Server.ReposInterface;
using OnlineRegisteration.Shared.Models;
using OnlineRegisteration.Server;
using System.Linq.Expressions;
using OnlineRegistration.Shared.Models;
using OnlineRegistration.Client.Models;

namespace OnlineRegistration.Server.Repos
{
    public class ScheduleRepo : IScheduleRepo
    {
        private readonly RegistrationDBContext _context;

        public ScheduleRepo(RegistrationDBContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Course>> AllCourses()
        {
            List<Course> courseList = new();
            courseList = await _context.Courses.Include(x=> x.TheDepartment).Select(a => new Course
            {
                CourseId = a.CourseId,
                Name = a.Name,
                CreditHours = a.CreditHours,
                TheDepartment = a.TheDepartment,
                CourseDependenceId = a.CourseDependenceId,

            }).ToListAsync();

            return courseList;


            //return await _context.Courses.Include( a => a.TheDepartment ).ToListAsync();
        }

        public  async Task<IEnumerable<Course>> TakenCourses(int RegNum)
        {


            var ClassroomIds = await  _context.Schedules.Where(m => m.StudentId == RegNum).Select(s => s.ClassroomId).ToListAsync();

            List<Course> TakenCourses = new();
            foreach (var id in ClassroomIds)
            {

                var TakenCourse = _context.Classrooms.Where(c => c.Id == id).Select(a => a.TheCourse).FirstOrDefault();
                TakenCourses.Add(TakenCourse);
            }
            return TakenCourses;

        }

        public async Task<IEnumerable<ClassroomModel>> AvailableClassrooms(int CourseId)
        {
            var availableClassrooms =await _context.Classrooms.Include(c=>c.TheLecturer).Include(c =>c.TheCourse)
                .Where(c => c.CourseID== CourseId).Select(a => new ClassroomModel
                {
                    Id = a.Id,
                    SlotName= a.SlotName,
                    LecturerName =a.TheLecturer.Name
                })
                .ToListAsync();
            return availableClassrooms;
        }

        public async Task<Classroom> GetClassroom(int ClassroomId)
        {
            var getclass = await _context.Classrooms.Include(m=>m.TheCourse).Where(m => m.Id == ClassroomId).FirstOrDefaultAsync();
            return getclass;
        }

        public async Task Register(int studentId, List<int> classroomIds, int regperiodId)
        {
            foreach (int crid in classroomIds)
            {
                var schedulepart = new Schedule { StudentId = studentId, ClassroomId = crid, RegPeriodId = regperiodId };
                 await _context.Schedules.AddAsync(schedulepart);
            }
            await _context.SaveChangesAsync();
        }

        public async Task<int> GetNumOfRegisteredStudents(int classroomId ,int regperiodID)
        {
            return ( await _context.Schedules.Where(a => a.ClassroomId == classroomId).Where(a => a.RegPeriodId == regperiodID).ToListAsync() ).Count();
            
        }

        public async Task<RegPeriod> LastRegPeriod()
        {
            var lastreg = await _context.RegPeriods.OrderBy(a=>a.ID)
                .Select(a => new RegPeriod
                { 
                  ID = a.ID,
                  StartDate = a.StartDate,
                  EndDate = a.EndDate
                })
                .LastOrDefaultAsync();

            return lastreg;
        }

        public async Task<IEnumerable<ScheduleSlot>> LastRegistered(int studentId, int regperiodId)
        {

            var lastregistered = await _context.Schedules
                
                .Where(c=>c.StudentId== studentId )
                .Where(c=>c.RegPeriodId == regperiodId)
                .Include(c => c.TheClassroom)
                .Include(c => c.TheClassroom.TheCourse)
                .Include(c => c.TheClassroom.TheLecturer)
                .Select(a => new ScheduleSlot
                {
                    Id = a.Id,
                    
                    CourseName = a.TheClassroom.TheCourse.Name,
                    SlotNumber = a.TheClassroom.SlotName,
                    LecturerName = a.TheClassroom.TheLecturer.Name,
                    ClassroomId = a.ClassroomId
                   

                })
                .ToListAsync();
            return lastregistered;
            
            

        }

        public async Task<IEnumerable<Student>> LastRegisteredStudents()
        {
            var lastreg = _context.RegPeriods.OrderBy(a=>a.ID).Select(a =>a.ID).LastOrDefault();

            var registeredstudents = await _context.Schedules.Where(a => a.RegPeriodId == lastreg).Select(a => a.StudentId).ToListAsync();
            registeredstudents =  registeredstudents.Distinct().ToList();
            List<Student> students = new List<Student>();

            foreach (var registeredstudent in registeredstudents)
            {
                var studentNames =await _context.Students.Where(a => a.RegId == registeredstudent).Select(a => new Student
                {
                    RegId = a.RegId,
                    Name = a.Name
                }).FirstOrDefaultAsync();
                students.Add(studentNames);

            }
            return students;
            

        }



    }
}

