using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Recovery_Backend_Data;
using Recovery_Backend_Data.Data;
using Recovery_Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recovery_UnitTest
{
    [TestClass]
    public class ExerciseTest
    {

        RecoveryDBContext context;
        ExerciseData data;
        ExerciseModel exercise = new ExerciseModel
        {
            Unique_ID = 2,
            Name = "test",
            BodyPart = "waist",
            Equipment = "body weight",
            Target = "abs",
            GifUrl = "test",
        };
        RegisterModel user = new RegisterModel
        {
            Unique_ID = 1002,
            First_Name = "test",
            Last_Name = "test",
            Birthdate = "2002-09-28",
            Height = 175,
            Weight = 60,
            Email = "t@t",
            Password = "t",
            User_Key = "genehytfd",
            Physical_Therapist = 1,
            Diet = 715415,
            Injury = 6,
            Exercise = 1
        };


        [TestInitialize]
        public void TestInit()
        {
            if (context != null)
            {
                Dispose();
                var options = new DbContextOptionsBuilder<RecoveryDBContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
                context = new RecoveryDBContext(options);
                SeedData(context);
                data = new ExerciseData(context);
            }
            else
            {
                var options = new DbContextOptionsBuilder<RecoveryDBContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
                context = new RecoveryDBContext(options);
                SeedData(context);
                data = new ExerciseData(context);
            }
        }


        public void SeedData(RecoveryDBContext context)
        {
            context.Add(new ExerciseModel
            {
                Unique_ID = 1,
                Name = "air bike",
                BodyPart = "waist",
                Equipment = "body weight",
                Target = "abs",
                GifUrl = "test",
            });
            context.SaveChanges();
            context.Add(new RegisterModel
            {
                Unique_ID = 1002,
                First_Name = "test",
                Last_Name = "test",
                Birthdate = "2002-09-28",
                Height = 175,
                Weight = 60,
                Email = "t@t",
                Password = "t",
                User_Key = "genehytfd",
                Physical_Therapist = 1,
                Diet = 715415,
                Injury = 6,
                Exercise = 20
            });
            context.SaveChanges();
        }

        public void Dispose()
        {
            context.Database.EnsureDeleted();
        }

        [TestMethod]
        public async Task StoreExercises()
        {
            ExerciseModel exercisenew = new ExerciseModel
            {
                Unique_ID = 3,
                Name = "test",
                BodyPart = "waist",
                Equipment = "body weight",
                Target = "abs",
                GifUrl = "test",
            };
            List<ExerciseModel> exercises = new List<ExerciseModel> { exercise, exercisenew};
            await data.StoreExercises(exercises);
            ExerciseModel exerciseCheck = await context.exercise.Where(x => x.Unique_ID == exercise.Unique_ID).FirstOrDefaultAsync();
            Assert.IsTrue(exerciseCheck.Unique_ID > 0);
        }
        
        [TestMethod]
        public async Task UpdateUserExercise()
        {
            RegisterModel user = new RegisterModel
            {
                Unique_ID = 1002,
                First_Name = "joshua",
                Last_Name = "mota",
                Birthdate = "2002-09-28",
                Height = 175,
                Weight = 60,
                Email = "joshua_mota2002@hotmail.nl",
                Password = "test",
                User_Key = "genehytfd",
                Physical_Therapist = 1,
                Diet = null,
                Injury = 6,
                Exercise = 6
            };
            RegisterModel updatedUser = await data.UpdateUserExercise(exercise, user.Unique_ID);
            Assert.AreEqual(exercise.Unique_ID, updatedUser.Exercise);
        }

        [TestMethod]
        public async Task GetExercise()
        {
            ExerciseModel exercise = await data.GetExercise(user.Exercise);
            Assert.AreNotEqual("quads", exercise.Target);
        }

        [TestMethod]
        public async Task GetExerciseName()
        {
            string exercise = await data.GetExerciseName(user.Exercise);
            Assert.AreEqual("air bike", exercise);
        }
    }
}
