using MySql.Data.MySqlClient;
using Recovery_Backend_Data.DTO;
using Recovery_Backend_Data.Interfaces;
using System;

namespace Recovery_Backend_Data.Context
{
    public class UserDatabaseContext : IUserContext
    {
        UserDTO userDTO = new UserDTO();
        PTDTO ptDTO = new PTDTO();
        DietDTO dietDTO = new DietDTO();
        TrainingDTO trainingDTO = new TrainingDTO();
        InjuryDTO injuryDTO = new InjuryDTO();
        QueryDatabaseContext queryDatabaseContext = new QueryDatabaseContext();

        public UserDTO GetUserDetails(int unique_id)
        {
            throw new NotImplementedException();
        }

        public UserDTO LoginUser(string email, string password)
        {
            MySqlConnection databaseConnection = new MySqlConnection(DatabaseDTO.DbConnectionString);
            MySqlCommand loginUser = queryDatabaseContext.LoginUser(email, password, databaseConnection);
            try
            {
                databaseConnection.Open();
                loginUser.Prepare();
                var executeString = loginUser.ExecuteReader();
                while (executeString.Read())
                {
                    userDTO.Unique_ID = executeString.GetInt32(0);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("error: " + e.Message);
            }
            return userDTO;
        }

        public UserDTO RegisterUser(string Fname, string Lname, int height, int weight, string email, string password, DateTime birthdate)
        {
            UserDTO user = new UserDTO { First_Name = Fname, Last_Name = Lname, Birthdate = birthdate, Height = height, Weight = weight, Email = email, Password = password };
            MySqlConnection databaseConnection = new MySqlConnection(DatabaseDTO.DbConnectionString);
            MySqlCommand registerUser = queryDatabaseContext.AddUserToDatabase(Fname, Lname, height, weight, email, password, birthdate, databaseConnection);
            try
            {
                databaseConnection.Open();
                registerUser.Prepare();
                var executeString = registerUser.ExecuteReader();
                databaseConnection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
            return user;
        }
    }
}
