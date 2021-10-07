using MySql.Data.MySqlClient;
using Recovery_Backend_Data.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Recovery_Backend_Data.Context
{
    public class QueryDatabaseContext
    {
        public MySqlCommand AddUserToDatabase(string Fname, string Lname, int height, int weight, string email, string password, DateTime birthdate, MySqlConnection connection)
        {
            MySqlCommand registerUser = new MySqlCommand("INSERT INTO `user`(`first_name`, `last_name`, `birthday`, `height`, `weight`, `email`, `password`) VALUES(@val1, @val2, @val3, @val4, @val5, @val6, @val7);", connection);
            registerUser.Parameters.AddWithValue("@val1", Fname);
            registerUser.Parameters.AddWithValue("@val2", Lname);
            registerUser.Parameters.AddWithValue("@val3", birthdate);
            registerUser.Parameters.AddWithValue("@val4", height);
            registerUser.Parameters.AddWithValue("@val5", weight);
            registerUser.Parameters.AddWithValue("@val6", email);
            registerUser.Parameters.AddWithValue("@val7", password);
            return registerUser; 
        }

        public MySqlCommand LoginUser(string email, string password, MySqlConnection connection)
        {
            MySqlCommand loginUser = new MySqlCommand("SELECT `unique_id` FROM user WHERE email=@val1 AND password=@val2", connection);
            loginUser.Parameters.AddWithValue("@val1", email);
            loginUser.Parameters.AddWithValue("@val2", password);
            return loginUser;
        }
    }
}
