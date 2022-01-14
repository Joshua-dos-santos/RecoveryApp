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
        private readonly InjuryData _injuryData;
        private readonly DietData _dietData;
        private readonly ExerciseData _exerciseData;
        public PTData(RecoveryDBContext context)
        {
            _context = context;
            _injuryData = new InjuryData(context);
            _dietData = new DietData(context);
            _exerciseData = new ExerciseData(context);
        }

        public async Task<PTModel> GetPTByID(int? key)
        {
            var physical_therapist = await _context.physical_therapist.Where(m => m.Unique_ID == key).FirstOrDefaultAsync();
            return physical_therapist;
        }
        public async Task<PTModel> GetPTByKey(string key)
        {
            var physical_therapist = await _context.physical_therapist.Where(m => m.PT_Key == key).FirstOrDefaultAsync();
            return physical_therapist;
        }

        public async Task<PTModel> GetPTByLogin(string email, string password)
        {
            var physical_therapist = await _context.physical_therapist.Where(m => m.Email == email && m.Password == password).FirstOrDefaultAsync();
            return physical_therapist;
        }

        public  async Task<List<UserListModel>> GetUsersByPT(int id)
        {
            var users = await _context.usermodel.Where(m => m.Physical_Therapist == id).ToListAsync();
            var currentUsers = new List<UserListModel>();
            foreach(var user in users)
            {
                UserListModel UpdatedUser = new UserListModel
                {
                    Unique_ID = user.Unique_ID,
                    Name = user.First_Name +" "+ user.Last_Name,
                    Birthdate = user.Birthdate,
                    Height = user.Height +"cm",
                    Weight = user.Weight+"kg",
                    Email = user.Email,
                    User_Key = user.User_Key,
                    Injury = await _injuryData.GetInjuryName(user.Injury),
                    Diet = await _dietData.GetDietName(user.Diet),
                    Exercise = await _exerciseData.GetExerciseName(user.Exercise)
                };
                currentUsers.Add(UpdatedUser);
            }
            return currentUsers;
        }

        public async Task<PTModel> RegisterPT(PTModel ptModel)
        {
            ptModel.PT_Key = Utilities.KeyGeneratorPT(ptModel);
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
