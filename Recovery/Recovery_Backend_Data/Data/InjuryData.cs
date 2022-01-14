using Microsoft.EntityFrameworkCore;
using Recovery_Backend_Data.Interfaces;
using Recovery_Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recovery_Backend_Data.Data
{
    public class InjuryData : InjuryInterface
    {
        private readonly RecoveryDBContext _context;
        public InjuryData(RecoveryDBContext context)
        {
            _context = context;
        }

        public async Task<string> GetInjuryName(int? unique_id)
        {
            if (unique_id != null)
            {
                var injury = await _context.injury.Where(i => i.Unique_ID == unique_id).FirstOrDefaultAsync();
                return injury.Part_of_Body + " " + injury.Description;
            }
            return null;
        }

        public async Task<RegisterModel> UpdateUserInjury(InjuryModel injury, int userID)
        {
            RegisterModel user = await _context.usermodel.Where(m => m.Unique_ID == userID).FirstOrDefaultAsync();
            InjuryModel injuryNew = await _context.injury.Where(m => m.Description == injury.Description && m.Pain_Scale == injury.Pain_Scale && m.Part_of_Body == injury.Part_of_Body).FirstOrDefaultAsync();
            if (injuryNew == null)
            {
                var newInjury = new InjuryModel()
                {
                    Unique_ID = injury.Unique_ID,
                    Part_of_Body = injury.Part_of_Body,
                    Pain_Scale = injury.Pain_Scale,
                    Description = injury.Description
                };

                await _context.injury.AddAsync(newInjury);
                await _context.SaveChangesAsync();
                int id = _context.injury.Where(m => m.Description == injury.Description && m.Pain_Scale == injury.Pain_Scale && m.Part_of_Body == injury.Part_of_Body).FirstOrDefaultAsync().Result.Unique_ID;
                user.Injury = id;
                await _context.SaveChangesAsync();
                return user;
            }
            user.Injury = injuryNew.Unique_ID;
            await _context.SaveChangesAsync();
            return user;
        }
    }
}
