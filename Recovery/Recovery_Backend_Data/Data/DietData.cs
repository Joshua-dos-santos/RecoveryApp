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
    public class DietData : IDietInterface
    {
        private readonly RecoveryDBContext _context;
        private readonly AccountData _accountData;
        public DietData(RecoveryDBContext context)
        {
            _context = context;
            _accountData = new AccountData(context);
        }

        public async Task<List<DietModel>> GetDietList()
        {
            var diets =await _context.diet.ToListAsync();
            return diets;
        }

        public async Task<RegisterModel> UpdateUserDiet(DietModel diet, int userID)
        {
            RegisterModel user = await _context.usermodel.Where(m => m.Unique_ID == userID).FirstOrDefaultAsync();
            user.Diet = diet.Unique_ID;
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<DietModel> GetDiet(int id)
        {
            var diet = await _context.diet.Where(m => m.Unique_ID == id).FirstOrDefaultAsync();
            return diet;
        }

        public async Task<DietModel> StoreDiets(DietModel diets)
        {
                var newDiet = new DietModel()
                {
                    Unique_ID = diets.Unique_ID,
                    Meal = diets.Meal,
                    Protein = diets.Protein,
                    Fats = diets.Fats,
                    Carbohydrates = diets.Carbohydrates,
                    Calories = diets.Calories,
                    Fibers = diets.Fibers
                };
                await _context.diet.AddAsync(newDiet);
                await _context.SaveChangesAsync();
            return diets;
        }
    }
}
