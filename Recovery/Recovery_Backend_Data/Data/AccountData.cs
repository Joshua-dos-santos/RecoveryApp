using Microsoft.EntityFrameworkCore;
using Recovery_Backend_Data.Interfaces;
using Recovery_Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Recovery_Backend_Data.Data
{
    public class AccountData : IAccountContext
    {
        private readonly RecoveryDBContext _context;
        private readonly PTData _PtData;
        public AccountData(RecoveryDBContext context)
        {
            _context = context;
            _PtData = new PTData(context);
        }

        //public async Task<IEnumerable<UserModel>> GetUser(string id)
        //{
        //    var users = await _context.usermodel.Where(m => m.Unique_ID == Convert.ToInt32(id)).ToListAsync();
        //    return users;
        //}

        public UserModel GetUser(string email, string password)
        {
            var userID =  _context.usermodel.Where(m => m.Email == email && m.Password == password).FirstOrDefault();
            return userID;
        }

        public UserModel Register(UserModel user)
        {
            user.User_Key = Utilities.RandomString();
            PTModel physical_Therapist = _PtData.GetPTByID(user.Physical_Therapist.Unique_ID);
            var newUser = new UserModel()
            {
                Unique_ID = user.Unique_ID,
                First_Name = user.First_Name,
                Last_Name = user.Last_Name,
                Birthdate = user.Birthdate,
                Email = user.Email,
                Password = Utilities.HashPassword(user.Password),
                User_Key = user.User_Key,
                Height = user.Height,
                Weight = user.Weight,
                Physical_Therapist = physical_Therapist,
                Injury = user.Injury,
                Diet = user.Diet,
                Training_Schedule = user.Training_Schedule
            };

             _context.usermodel.Add(newUser);
            _context.SaveChanges();
            return newUser;
        }

        

    }
}
