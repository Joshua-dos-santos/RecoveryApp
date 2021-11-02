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
        private readonly InjuryData _injuryData;
        public AccountData(RecoveryDBContext context)
        {
            _context = context;
            _PtData = new PTData(context);
            _injuryData = new InjuryData(context);
        }

        public UserModel GetUser(string email, string password)
        {
            var userID =  _context.usermodel.Where(m => m.Email == email && m.Password == password).FirstOrDefault();
            return userID;
        }

        public UserModel Register(UserModel user)
        {
            user.User_Key = Utilities.RandomString();
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
                Physical_Therapist = _PtData.GetPTByID(user.Physical_Therapist.Unique_ID),
                Injury = _injuryData.GetInjuryByID(user.Injury.Unique_ID),
                Diet = user.Diet,
                Training_Schedule = user.Training_Schedule
            };

            _context.usermodel.Add(newUser);
            _context.SaveChanges();
            return newUser;
        }

        

    }
}
