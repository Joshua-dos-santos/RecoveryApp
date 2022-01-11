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

        public async Task <string> GetInjuryByID(int? unique_id)
        {
            var injury = await _context.injury.Where(i => i.Unique_ID == unique_id).FirstOrDefaultAsync();
            return injury.Part_of_Body + injury.Description;
        }
    }
}
