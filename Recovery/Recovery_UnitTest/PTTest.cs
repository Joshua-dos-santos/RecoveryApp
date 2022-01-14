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
    public class PTTest
    {
        RecoveryDBContext context;
        PTData data;
        PTModel pt = new PTModel
        {
            Unique_ID = 1,
            First_Name = "t",
            Last_Name = "t",
            Birthdate = "2002-09-28",
            Email = "t@t",
            Password = "t",
            PT_Key = "gfhnghhnnfxgfgxfffb"
        };
        RegisterModel user = new RegisterModel
        {
            Unique_ID = 1,
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
                data = new PTData(context);
            }
            else
            {
                var options = new DbContextOptionsBuilder<RecoveryDBContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
                context = new RecoveryDBContext(options);
                SeedData(context);
                data = new PTData(context);
            }
        }

        public void SeedData(RecoveryDBContext context)
        {
            context.Add(new PTModel
            {
                Unique_ID = 1,
                First_Name = "test",
                Last_Name = "test",
                Birthdate = "2002-09-28",
                Email = "test@test",
                Password = "test",
                PT_Key = "gfhnghhnnfxgfgxfffb"
            });
            context.SaveChanges();
            context.Add(new RegisterModel
            {
                Unique_ID = 1,
                First_Name = "joshua",
                Last_Name = "mota",
                Birthdate = "2002-09-28",
                Height = 175,
                Weight = 60,
                Email = "test@test",
                Password = "test",
                User_Key = "ghfdhj",
                Physical_Therapist = 1,
                Diet = null,
                Injury = null,
                Exercise = null
            });
            context.SaveChanges();
        }

        public void Dispose()
        {
            context.Database.EnsureDeleted();
        }

        [TestMethod]
        public async Task GetPTByID()
        {
            PTModel ptCheck = await data.GetPTByID(pt.Unique_ID);
            Assert.AreNotEqual(pt.First_Name, ptCheck.First_Name);
        }

        [TestMethod]
        public async Task GetPTByKey()
        {
            PTModel ptCheck = await data.GetPTByKey(pt.PT_Key);
            Assert.IsTrue(ptCheck.Unique_ID != 0);
        }

        [TestMethod]
        public async Task GetPTByLogin()
        {
            PTModel ptCheck = await data.GetPTByLogin(pt.Email, pt.Password);
            Assert.IsNull(ptCheck);
        }

        [TestMethod]
        public async Task GetUsersByPT()
        {
            List<UserListModel> users = new List<UserListModel>();
            users = await data.GetUsersByPT(pt.Unique_ID);
            Assert.IsTrue(users.Count > 0);
        }

        [TestMethod]
        public async Task RegisterPT()
        {
            PTModel pt = new PTModel
            {
                Unique_ID = 7,
                First_Name = "t",
                Last_Name = "tester",
                Birthdate = "2002-08-28",
                Email = "test@tester",
                Password = "tester",
            };
            PTModel newUser = await data.RegisterPT(pt);
            Assert.IsTrue(newUser.Unique_ID != 0);
        }
    }
}
