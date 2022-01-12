using Microsoft.EntityFrameworkCore;
using Recovery_Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recovery_Backend_Data.Data
{
    public class ExerciseData
    {
        private readonly RecoveryDBContext _context;
        public ExerciseData(RecoveryDBContext context)
        {
            _context = context;
        }
        public async Task<ExerciseModel> StoreExercises(List<ExerciseModel> exercises)
        {
            var exercise = from m in _context.exercise
                        select m;
            foreach (var training in exercise)
            {
                _context.exercise.Remove(training);
            }
            _context.SaveChanges();
            for (int i = 0; i <= exercises.Count(); i++)
            {
                var newExercise = new ExerciseModel()
                {
                    Unique_ID = exercises[i].Unique_ID,
                    Name = exercises[i].Name,
                    BodyPart = exercises[i].BodyPart,
                    Equipment = exercises[i].Equipment,
                    Target = exercises[i].Target,
                    GifUrl = exercises[i].GifUrl,
                };
                await _context.exercise.AddAsync(newExercise);
                await _context.SaveChangesAsync();
            }
            return exercises[1];
        }
        public async Task<RegisterModel> UpdateUserExercise(ExerciseModel exercise, int userID)
        {
            RegisterModel user = await _context.usermodel.Where(m => m.Unique_ID == userID).FirstOrDefaultAsync();
            user.Exercise = exercise.Unique_ID;
            await _context.SaveChangesAsync();

            return user;
        }
        public async Task<ExerciseModel> GetExercise(int? id)
        {
            if(id == null)
            {
                return null;
            }
            var exercise = await _context.exercise.Where(m => m.Unique_ID == id).FirstOrDefaultAsync();
            return exercise;
        }

        public async Task<string> GetExerciseName(int? id)
        {
            if (id == null)
            {
                return null;
            }
            var exercise = await _context.exercise.Where(m => m.Unique_ID == id).FirstOrDefaultAsync();
            return exercise.Name;
        }
    }
}
