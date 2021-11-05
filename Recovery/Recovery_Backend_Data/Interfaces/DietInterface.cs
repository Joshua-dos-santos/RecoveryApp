﻿using Recovery_Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recovery_Backend_Data.Interfaces
{
    public interface DietInterface
    {
        public Task<List<DietModel>> GetDietList();
        public Task<UserModel> UpdateUserDiet(DietModel diet);
    }
}