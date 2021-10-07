using Recovery_Backend_Data.Context;
using Recovery_Backend_Data.DTO;
using Recovery_Backend_Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Recovery_Backend_Data.Repositories
{
    public class UserRepository
    {
        private IUserContext _context;

        public UserRepository(UserDatabaseContext context)
        {
            this._context = context;
        }

        public UserDTO RegisterUser(string Fname, string Lname, int height, int weight, string email, string password, DateTime birthdate)
        {
            return _context.RegisterUser(Fname, Lname, height, weight, email, password, birthdate);
        }

        public UserDTO LoginUser(string email, string password)
        {
            return _context.LoginUser(email, password);
        }

        public UserDTO GetUserDetails(int Unique_id)
        {
            return _context.GetUserDetails(Unique_id);
        }


    }
}
