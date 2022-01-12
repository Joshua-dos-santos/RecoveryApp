using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Recovery_Backend_Data;
using Recovery_Backend_Data.Data;
using Recovery_Models.Models;
using System;
using System.Threading.Tasks;

namespace Recovery_UnitTest
{
    [TestClass]
    public class AccountTest
    {
        RecoveryDBContext context;
        AccountData data;
        RegisterModel user = new RegisterModel
        {
            Unique_ID = 1002,
            First_Name = "joshua",
            Last_Name = "mota",
            Birthdate = "2002-09-28",
            Height = 175,
            Weight = 60,
            Email = "test@test",
            Password = "test",
            User_Key = "ghfdhj",
            Physical_Therapist = 1,
            Diet = 715415,
            Injury = 6,
            Exercise = 20
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
                data = new AccountData(context);
            }
            else
            {
                var options = new DbContextOptionsBuilder<RecoveryDBContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
                context = new RecoveryDBContext(options);
                SeedData(context);
                data = new AccountData(context);
            }
        }


        public void SeedData(RecoveryDBContext context)
        {
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
        public async Task GetUserByLogin()
        {
            RegisterModel loginCheck = await data.GetUserByLogin(user.Email, user.Password);
            Assert.IsNull(loginCheck);
            RegisterModel loginTest = await data.GetUserByLogin("t@t", "t");
            Assert.IsNotNull(loginTest);
        }

        [TestMethod]
        public async Task GetUserByID()
        {
            RegisterModel userCheck = await data.GetUserByID(user.Unique_ID);
            Assert.AreEqual(user.Injury, userCheck.Injury);
            RegisterModel userTest = await data.GetUserByID(4);
            Assert.IsNull(userTest);
        }
        [TestMethod]
        public async Task Register()
        {
            RegisterModel user = new RegisterModel 
            {
                Unique_ID = 7,
                First_Name = "t",
                Last_Name = "tester",
                Birthdate = "2002-08-28",
                Height = 175,
                Weight = 56,
                Email = "test@tester",
                Password = "tester",
                User_Key = "kjcdbfhad",
                Physical_Therapist = 2,
                Diet = 715415,
                Injury = 6,
                Exercise = 20
            };
            RegisterModel newUser = await data.Register(user);
            Assert.AreEqual(175, newUser.Height);
        }
        [TestMethod]
        public async Task DeleteUser()
        {
            bool deleted = await data.DeleteUser(4);
            Assert.IsFalse(deleted);
        }
    }
}
