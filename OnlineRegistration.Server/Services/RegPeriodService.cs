using OnlineRegisteration.Shared.Models;
using OnlineRegistration.Client.Models;
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

        public async Task ChangeRegPeriod(RegPeriodModel regPeriod)
        {
            await _RegPeriodRepo.ChangeRegPeriod(regPeriod);
        }

        public async Task<RegPeriod> LastRegPeriod()
        {
            return await _RegPeriodRepo.LastRegPeriod();
        }
    }
}
