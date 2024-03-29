﻿using Microsoft.EntityFrameworkCore;
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
        public DietData(RecoveryDBContext context)
        {
            _context = context;
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

        public async Task<DietModel> GetDiet(int? id)
        {
            if(id == null)
            {
                return null;
            }
            var diet = await _context.diet.Where(m => m.Unique_ID == id).FirstOrDefaultAsync();
            return diet;
        }

        public async Task<string> GetDietName(int? id)
        {
            if (id == null)
            {
                return null;
            }
            var diet = await _context.diet.Where(m => m.Unique_ID == id).FirstOrDefaultAsync();
            return diet.Meal;
        }

        public async Task<DietModel> StoreDiets(List<DietModel> diets)
        {
            var meals = from m in _context.diet
                          select m;
            foreach (var meal in meals)
            {
                _context.diet.Remove(meal);
            }
            _context.SaveChanges();
            for (int i = 0; i <= diets.Count()-1; i++)
            {
                var newDiet = new DietModel()
                {
                    Unique_ID = diets[i].Unique_ID,
                    Meal = diets[i].Meal,
                    Protein = diets[i].Protein,
                    Fats = diets[i].Fats,
                    Carbohydrates = diets[i].Carbohydrates,
                    Calories = diets[i].Calories,
                    Fibers = diets[i].Fibers
                };
                await _context.diet.AddAsync(newDiet);
                await _context.SaveChangesAsync();
            }
            return diets[1];
        }
    }
}
