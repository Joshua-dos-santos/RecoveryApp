using Recovery_Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recovery_Backend_Data.Interfaces
{
    public interface PTInterface
    {
        public Task<PTModel> GetPTByID(int? key);
        public Task<PTModel> GetPTByKey(string key);
        public Task<PTModel> GetPTByLogin(string email, string password);
        public Task<List<UserListModel>> GetUsersByPT(int id);
        public Task<PTModel> RegisterPT(PTModel ptModel);
    }
}
