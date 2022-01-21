using Recovery_Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recovery_Backend_Data.Interfaces
{
    public interface InjuryInterface
    {
        public Task <string> GetInjuryName(int? unique_id);
        public Task<InjuryModel> GetInjury(int? unique_id);
    }
}
