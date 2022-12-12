using Microsoft.EntityFrameworkCore;
using OnlineRegistration.Server.ReposInterface;
using OnlineRegisteration.Shared.Models;
using OnlineRegisteration.Server;

namespace OnlineRegistration.Server.Repos
{
    public class RegPeriodRepo : IRegPeriodRepo
    {
        private readonly RegistrationDBContext _context;

        public RegPeriodRepo(RegistrationDBContext context)
        {
            _context = context;
        }


        public RegPeriod LastRegPeriod()
        {
            var lastreg = _context.RegPeriods.OrderBy(a => a.ID)
                .Select(a => new RegPeriod
                {
                    ID = a.ID,
                    StartDate = a.StartDate,
                    EndDate = a.EndDate
                })
                .Last();

            return lastreg;
        }

        public async void ChangeRegPeriod(DateTime SD,DateTime ED,int Adminid)
        {

            var ouradmin = await _context.Admins.Where(a=>a.Id== Adminid).FirstOrDefaultAsync();
            var newReg = new RegPeriod { StartDate= SD, EndDate = ED,TheAdmin = ouradmin };
            await _context.RegPeriods.AddAsync(newReg);

            await _context.SaveChangesAsync();
        }

        Task IRegPeriodRepo.ChangeRegPeriod(DateTime SD, DateTime ED, int Adminid)
        {
            throw new NotImplementedException();
        }
    }
}
