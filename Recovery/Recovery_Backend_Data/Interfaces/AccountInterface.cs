﻿using Recovery_Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recovery_Backend_Data.Interfaces
{
    public interface AccountInterface
    {
        //public Task<IEnumerable<UserModel>> GetUser(string id);
        public UserModel GetUser(string email, string password);
        public UserModel Register(UserModel user);
    }
}