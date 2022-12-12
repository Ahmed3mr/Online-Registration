using OnlineRegisteration.Shared.Models;

namespace OnlineRegistration.Server.ReposInterface
{
    public interface IRegPeriodRepo
    {


        public RegPeriod LastRegPeriod();
        Task ChangeRegPeriod(DateTime SD, DateTime ED, int Adminid);
    }
}
