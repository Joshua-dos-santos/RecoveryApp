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
    public class DietTest
    {
        RecoveryDBContext context;
        DietData data;
        DietModel diet = new DietModel
        {
            Unique_ID = 2,
            Meal = "test",
            Protein = 8.3m,
            Fats = 643.6m,
            Carbohydrates = 28.543m,
            Calories = 435.432m,
            Fibers = 32.43m
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
            Diet = 2,
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
                data = new DietData(context);
            }
            else
            {
                var options = new DbContextOptionsBuilder<RecoveryDBContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
                context = new RecoveryDBContext(options);
                SeedData(context);
                data = new DietData(context);
            }
        }


        public void SeedData(RecoveryDBContext context)
        {
            context.Add(new DietModel
            {
                Unique_ID = 2,
                Meal = "meal",
                Protein = 5.4m,
                Fats = 75.2m,
                Carbohydrates =29.93m,
                Calories = 145.4m,
                Fibers = 12.34m
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
                Diet = 2,
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
        public async Task StoreDiets()
        {
            DietModel dietNew = new DietModel
            {
                Unique_ID = 3,
                Meal = "test",
                Protein = 0,
                Fats = 0,
                Carbohydrates = 0,
                Calories = 0,
                Fibers = 0
            };
            List<DietModel> diets = new List<DietModel> { diet, dietNew };
            await data.StoreDiets(diets);
            DietModel dietCheck = await context.diet.Where(x => x.Unique_ID == diet.Unique_ID).FirstOrDefaultAsync();
            Assert.IsTrue(dietCheck.Unique_ID > 0);
        }

        [TestMethod]
        public async Task UpdateUserDiet()
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
            RegisterModel updatedUser = await data.UpdateUserDiet(diet, user.Unique_ID);
            Assert.AreEqual(diet.Unique_ID, updatedUser.Diet);
        }

        [TestMethod]
        public async Task GetDiet()
        {
            DietModel meal = await data.GetDiet(user.Diet);
            Assert.AreNotEqual(0, meal.Protein);
        }

        [TestMethod]
        public async Task GetDietName()
        {
            string meal = await data.GetDietName(user.Diet);
            Assert.AreEqual("meal", meal);
        }
    }
}
