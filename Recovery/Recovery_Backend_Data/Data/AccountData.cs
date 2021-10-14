using Microsoft.EntityFrameworkCore;
using Recovery_Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recovery_Backend_Data.Data
{
    public class AccountData
    {
        private readonly RecoveryDBContext _context;
        public AccountData(RecoveryDBContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<UserModel>> GetUser(string id)
        {
            var users = await _context.usermodel.Where(m => m.Unique_ID == Convert.ToInt32(id)).ToListAsync();
            return users;
        }

        public async Task<string> GetUserID(string email)
        {
            var userID = await _context.usermodel.Where(m => m.Email == email).Select(m => m.Unique_ID).ToListAsync();
            return userID[0].ToString();
        }

        public string AddUser(UserModel user)
        {
            var newUser = new UserModel()
            {
                First_Name = user.First_Name,
                Last_Name = user.Last_Name,
                Birthdate = user.Birthdate,
                Email = user.Email,
                Password = user.Password,
                Height = user.Height,
                Weight = user.Weight,
                Physical_Therapist = user.Physical_Therapist,
                Injury = user.Injury,
                Diet = user.Diet,
                Training_Schedule = user.Training_Schedule
            };

             _context.usermodel.Add(newUser);
            _context.SaveChanges();
            return "succes";
        }

    }
}
