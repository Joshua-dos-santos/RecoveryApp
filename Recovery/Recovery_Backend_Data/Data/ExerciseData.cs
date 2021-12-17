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
        private readonly AccountData _accountData;
        public ExerciseData(RecoveryDBContext context)
        {
            _context = context;
            _accountData = new AccountData(context);
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
    }
}
