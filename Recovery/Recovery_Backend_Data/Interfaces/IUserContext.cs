using Recovery_Backend_Data.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Recovery_Backend_Data.Interfaces
{
    public interface IUserContext
    {
        UserDTO RegisterUser(string Fname, string Lname, string email, string password, string birthdate);
        UserDTO LoginUser(string email, string password);
        UserDTO GetUserDetails(string unique_id);
    }
}
