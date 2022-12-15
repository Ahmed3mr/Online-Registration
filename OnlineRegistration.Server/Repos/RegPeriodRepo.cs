using Microsoft.EntityFrameworkCore;
using OnlineRegistration.Server.ReposInterface;
using OnlineRegisteration.Shared.Models;
using OnlineRegisteration.Server;
using OnlineRegistration.Client.Models;

namespace OnlineRegistration.Server.Repos
{
    public class RegPeriodRepo : IRegPeriodRepo
    {
        private readonly RegistrationDBContext _context;

        public RegPeriodRepo(RegistrationDBContext context)
        {
            _context = context;
        }


        public async Task<RegPeriod> LastRegPeriod()
        {
            var lastreg = await _context.RegPeriods.OrderBy(a => a.ID)
                .Select(a => new RegPeriod
                {
                    ID = a.ID,
                    StartDate = a.StartDate,
                    EndDate = a.EndDate
                })
                .LastOrDefaultAsync();

            return lastreg;
        }

        public async Task ChangeRegPeriod(RegPeriodModel regPeriod)
        {

            var ouradmin = await _context.Admins.Where(a=>a.Id== regPeriod.TheAdminId).FirstOrDefaultAsync();
            var newReg = new RegPeriod { StartDate= regPeriod.StartDate, EndDate = regPeriod.EndDate,TheAdmin = ouradmin };
            await _context.RegPeriods.AddAsync(newReg);

            await _context.SaveChangesAsync();
        }

       
    }
}
