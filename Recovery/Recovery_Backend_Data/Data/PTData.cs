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
    public class PTData : PTInterface
    {
        private readonly RecoveryDBContext _context;
        public PTData(RecoveryDBContext context)
        {
            _context = context;
        }

        public async Task<PTModel> GetPTByID(int? key)
        {
            var physical_therapist = await _context.physical_therapist.Where(m => m.Unique_ID == key).FirstOrDefaultAsync();
            return physical_therapist;
        }

        public async Task<PTModel> GetPTByLogin(string email, string password, string key)
        {
            var physical_therapist = await _context.physical_therapist.Where(m => m.Email == email && m.Password == password && m.PT_Key == key).FirstOrDefaultAsync();
            return physical_therapist;
        }

        public  async Task<List<RegisterModel>> GetUsersByPT(int id)
        {
            var users =await _context.usermodel.Where(m => m.Physical_Therapist == id).ToListAsync();
            return users;
        }

        public async Task<PTModel> RegisterPT(PTModel ptModel)
        {
            ptModel.PT_Key = Utilities.RandomString();
            var newPt = new PTModel()
            {
                Unique_ID = ptModel.Unique_ID,
                First_Name = ptModel.First_Name,
                Last_Name = ptModel.Last_Name,
                Birthdate = ptModel.Birthdate,
                Email = ptModel.Email,
                Password = ptModel.Password,
                PT_Key = ptModel.PT_Key
            };

            await _context.physical_therapist.AddAsync(newPt);
            await _context.SaveChangesAsync();
            return newPt;
        }
    }
}
