using Microsoft.EntityFrameworkCore;
using Recovery_Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recovery_Backend_Data.Data
{
    public class PTData
    {
        private readonly RecoveryDBContext _context;
        public PTData(RecoveryDBContext context)
        {
            _context = context;
        }

        public async Task<List<PTModel>> GetPT(int id)
        {
            var physical_therapist = await _context.ptmodel.Where(m => m.Unique_ID == id).ToListAsync();
            return physical_therapist;
        }
    }
}
