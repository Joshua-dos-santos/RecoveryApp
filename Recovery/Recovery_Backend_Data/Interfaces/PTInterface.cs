using Recovery_Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recovery_Backend_Data.Interfaces
{
    public interface IPTContext
    {
        public PTModel GetPTByID(int? key);
        public PTModel GetPTByLogin(string email, string password, string key);
        public List<UserModel> GetUsersByPT(int id);
        public PTModel RegisterPT(PTModel ptModel);
    }
}
