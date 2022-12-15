using OnlineRegisteration.Shared.Models;
using OnlineRegistration.Client.Models;
using OnlineRegistration.Shared.Models;

namespace OnlineRegistration.Server.ReposInterface
{
    public interface IScheduleRepo
    {
        Task<IEnumerable<Course>> AllCourses();

        Task<IEnumerable<Course>> TakenCourses(int RegNum);

        Task<IEnumerable<ClassroomModel>> AvailableClassrooms(int CourseId);

        Task<Classroom> GetClassroom(int ClassroomId);

        Task Register(int studentId, List<int> classroomIds, int regperiodId);

        Task<int> GetNumOfRegisteredStudents(int classroomId, int regperiodID);

        Task<RegPeriod> LastRegPeriod();

        Task<IEnumerable<ScheduleSlot>> LastRegistered(int studentId, int regperiodId);
        Task<IEnumerable<Student>> LastRegisteredStudents();
    }
}
