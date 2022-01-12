using Recovery_Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recovery_Backend_Data.Interfaces
{
    public interface AccountInterface
    {
        public Task<RegisterModel> GetUserByLogin(string email, string password);
        public Task<RegisterModel> GetUserByID(int key);
        public Task<RegisterModel> Register(RegisterModel user);
        public Task<bool> DeleteUser(int user);
    }
}
