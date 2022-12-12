using OnlineRegisteration.Shared.Models;

namespace OnlineRegistration.Server.ServicesInterfaces
{
    public interface IRegPeriodService
    {

        public RegPeriod LastRegPeriod();
        Task ChangeRegPeriod(DateTime SD, DateTime ED, int Adminid);
    }
}
