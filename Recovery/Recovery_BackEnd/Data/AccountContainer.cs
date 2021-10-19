using Recovery_Backend_Data;
using Recovery_Backend_Data.Data;
using Recovery_Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recovery_BackEnd.Data
{
    public class AccountContainer
    {
        private readonly AccountData _accountData;

        public AccountContainer(RecoveryDBContext context)
        {
            _accountData = new AccountData(context);
        }
        public AccountContainer()
        {

        }

        public UserModel AddUser()
        {
            var newUser = new UserModel()
            {
                First_Name = "Joost",
                Last_Name = "Arens",
                Birthdate = "2004-05-09",
                Height = 174,
                Weight = 64,
                Email = "joost@arens",
                Password = "joost2004"
            };
            return _accountData.AddUser(newUser);
        }
    }
}
