using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Data.SqlClient;
using WEB2022Apr_P01_T3.Models;

namespace WEB2022Apr_P01_T3.DAL
{
    public class AdminDAL
    {
        private IConfiguration Configuration { get; }
        private SqlConnection conn;

        public AdminDAL() 
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            Configuration = builder.Build();
            string strConn = Configuration.GetConnectionString("SBLOGConnectionString");
            conn = new SqlConnection(strConn);
        }

        public bool checkUsernamePresent(string username)
        {
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = @"SELECT PlayerUserName FROM Player";
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            while(reader.Read())
            {
                if(reader.GetString(0) == username)
                {
                    reader.Close();
                    conn.Close();
                    return true;
                }
            }
            reader.Close();
            conn.Close();
            return false;
        }

        public void createAccount(string username, string password)
        {
            SqlCommand cmd = conn.CreateCommand();

            cmd.CommandText = @"INSERT INTO Player (PlayerUserName, PlayerPassword) VALUES(@username, @password)";
            cmd.Parameters.AddWithValue("username", username);
            cmd.Parameters.AddWithValue("password", password);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }


        List<Admin> adminList = new List<Admin>();
        public List<Admin> GetAllAdmin()
        {
            //Create a SqlCommand object from connection object
            SqlCommand cmd = conn.CreateCommand();
            //Specify the SELECT SQL statement
            cmd.CommandText = @"SELECT * FROM Admin ORDER BY AdminUserName";
            conn.Open();
            //Execute the SELECT SQL through a DataReader
            SqlDataReader reader = cmd.ExecuteReader();

            //Read all records until the end, save data into a staff list
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    adminList.Add(
                        new Admin
                        {
                            Username = reader.GetString(0), //0: 1st column
                            Password = reader.GetString(1),
                        }
                    );
                }
            }


            //Close DataReader
            reader.Close();
            //Close the database connection
            conn.Close();
            return adminList;
        }

        public bool CredentialsMatch(string username, string password)
        {
            if (adminList.Count == 0)
            {
                GetAllAdmin();
                foreach (Admin a in adminList)
                {
                    if (a.Username == username)
                    {
                        if (a.Password == password)
                        {
                            return true;
                        }

                    }
                }
            }
            return false;


        }
    }
}
