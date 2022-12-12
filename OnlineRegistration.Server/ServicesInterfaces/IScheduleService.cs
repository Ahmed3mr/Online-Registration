using OnlineRegisteration.Shared.Models;
using OnlineRegistration.Shared.Models;

namespace OnlineRegistration.Server.ServicesInterfaces
{
    public interface IScheduleService
    {
        Task<IEnumerable<Course>> AvailableCourses(int Regnum);
        Task<IEnumerable<Course>> untakencourses(int RegNum);
        Task<IEnumerable<Course>> TakenCourses(int RegNum);
        Task<IEnumerable<Course>> AllCourses();
        Task<IEnumerable<Classroom>> AvailableClassrooms(int CourseId);
        Task Register(int studentId, List<int> classroomIds, int regperiodId);
        Task<String> CreditHoursRegistrationCheck(int studentId, List<int> classroomIds, int regperiodId);
        Task<String> OverLappingRegistrationCheck(int studentId, List<int> classroomIds, int regperiodId);
        Task<String> CapacityRegistrationCheck(int studentId, List<int> classroomIds, int regperiodId);
        Task<RegPeriod> LastRegPeriod();
        Task<bool> RegPeriodCheck(DateTime nowtime);
        Task<IEnumerable<ScheduleSlot>> LastRegistered(int studentId,int regperiodId);
        Task<IEnumerable<Student>> LastRegisteredStudents();

    }
}
