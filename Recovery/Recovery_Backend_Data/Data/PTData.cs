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

        public PTModel GetPT(int id)
        {
            var physical_therapist = _context.ptmodel.Where(m => m.Unique_ID == id).FirstOrDefault();
            return physical_therapist;
        }

        public async Task<List<UserModel>> GetUserByPT(int id)
        {
            var users = await _context.usermodel.Where(m => m.Physical_Therapist.Unique_ID == id).ToListAsync();
            return users;
        }
    }
}
