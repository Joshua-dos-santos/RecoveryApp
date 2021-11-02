using Recovery_Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recovery_Backend_Data.Data
{
    public class DietData
    {
        private readonly RecoveryDBContext _context;
        public DietData(RecoveryDBContext context)
        {
            _context = context;
        }

        public List<DietModel> GetDietList()
        {
            var diets = _context.diet.ToList();
            return diets;
        }
    }
}
