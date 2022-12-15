using OnlineRegisteration.Shared.Models;
using OnlineRegistration.Client.Models;

namespace OnlineRegistration.Server.ReposInterface
{
    public interface IRegPeriodRepo
    {


        Task<RegPeriod> LastRegPeriod();
        Task ChangeRegPeriod(RegPeriodModel regPeriod);
    }
}
