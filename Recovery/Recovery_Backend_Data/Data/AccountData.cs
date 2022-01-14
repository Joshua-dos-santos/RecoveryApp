using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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
    public class AccountData : AccountInterface
    {
        private readonly RecoveryDBContext _context;

        public AccountData(RecoveryDBContext context)
        {
            _context = context;
        }

        public async Task<RegisterModel> GetUserByLogin(string email, string password)
        {
            var user = await _context.usermodel.Where(m => m.Email == email && m.Password == password).FirstOrDefaultAsync();
            return user;
        }

        public async Task<RegisterModel> GetUserByID(int key)
        {
            var user = await _context.usermodel.Where(m => m.Unique_ID == key).FirstOrDefaultAsync();
            return user;
        }

        public async Task<RegisterModel> Register(RegisterModel user)
        {
            user.User_Key = Utilities.RandomString(user);
            var newUser = new RegisterModel()
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
                Physical_Therapist = user.Physical_Therapist,
                Injury = user.Injury,
                Diet = user.Diet,
                Exercise = user.Exercise
            };

            await _context.usermodel.AddAsync(newUser);
            await _context.SaveChangesAsync();
            return newUser;
        }

        public async Task<bool> DeleteUser(int user)
        {
            RegisterModel deletedUser = await GetUserByID(user);
            if (deletedUser != null)
            {
                _context.usermodel.Remove(deletedUser);
                await _context.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<RegisterModel> UpdateUser(RegisterModel user)
        {
            RegisterModel userModel = await _context.usermodel.Where(x => x.Unique_ID == user.Unique_ID).FirstOrDefaultAsync();
            _context.usermodel.Update(userModel).CurrentValues.SetValues(user);
            user.Unique_ID = 0;
            await _context.SaveChangesAsync();
            userModel = await _context.usermodel.Where(x => x.Unique_ID == user.Unique_ID).FirstOrDefaultAsync();
            return userModel;
        }

        public virtual async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
