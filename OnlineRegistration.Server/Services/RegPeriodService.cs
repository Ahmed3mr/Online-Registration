using OnlineRegisteration.Shared.Models;
using OnlineRegistration.Server.ReposInterface;
using OnlineRegistration.Server.ServicesInterfaces;
using System.Collections.Generic;

namespace OnlineRegistration.Server.Services
{
    public class RegPeriodService : IRegPeriodService
    {
        private readonly IRegPeriodRepo _RegPeriodRepo;

        public RegPeriodService(IRegPeriodRepo RegPeriodRepo)
        {
            _RegPeriodRepo = RegPeriodRepo;
        }

        public async Task ChangeRegPeriod(DateTime SD, DateTime ED, int Adminid)
        {
            await _RegPeriodRepo.ChangeRegPeriod(SD, ED, Adminid);
        }

        public RegPeriod LastRegPeriod()
        {
            return _RegPeriodRepo.LastRegPeriod();
        }
    }
}
