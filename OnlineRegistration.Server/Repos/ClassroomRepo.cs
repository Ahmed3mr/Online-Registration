using OnlineRegisteration.Server;
using OnlineRegistration.Server.ReposInterface;

namespace OnlineRegistration.Server.Repos
{
    public class ClassroomRepo : IClassroomRepo
    {
        private readonly RegistrationDBContext _context;

        public ClassroomRepo(RegistrationDBContext context)
        {
            _context = context;
        }


    }
}
