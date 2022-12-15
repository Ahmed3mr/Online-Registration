using OnlineRegisteration.Shared.Models;
using OnlineRegistration.Client.Models;

namespace OnlineRegistration.Server.ServicesInterfaces
{
    public interface IRegPeriodService
    {

        Task<RegPeriod> LastRegPeriod();
        Task ChangeRegPeriod(RegPeriodModel regPeriod);
    }
}
