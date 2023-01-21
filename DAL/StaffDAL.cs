using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Data.SqlClient;
using WEB2022_ZZFashion.Models;

namespace WEB2022_ZZFashion.DAL
{
	public class StaffDAL
	{
		private IConfiguration Configuration { get; }
		private SqlConnection conn;

		public StaffDAL()
		{
			//Read ConnectionString from appsettings.json file
			var builder = new ConfigurationBuilder()
			.SetBasePath(Directory.GetCurrentDirectory())
			.AddJsonFile("appsettings.json");
			Configuration = builder.Build();
			string strConn = Configuration.GetConnectionString(
			"ZZFashionConnectionString");
			//Instantiate a SqlConnection object with the
			//Connection String read.
			conn = new SqlConnection(strConn);
		}

		public bool CheckPassword(string staffID, string password)
		{
			//Create a SqlCommand object from connection object
			SqlCommand cmd = conn.CreateCommand();

			//Specify the SELECT SQL statement that
			//retrieves all attributes of a staff record.
			cmd.CommandText = @"SELECT SPassword FROM Staff
				WHERE StaffID = @selectedStaffID";

			cmd.Parameters.AddWithValue("@selectedStaffID", staffID);

			//Open a database connection
			conn.Open();
			//Execute SELCT SQL through a DataReader
			SqlDataReader reader = cmd.ExecuteReader();


			if (reader.HasRows)
			{
				//Read the record from database
				while (reader.Read())
				{
					if (password == reader.GetString(0))
					{
						//Close data reader
						reader.Close();
						//Close database connection
						conn.Close();
						return true;
					}
				}
			}
			//Close data reader
			reader.Close();
			//Close database connection
			conn.Close();
			return false;
		}

		public string getRole(string staffID)
		{
			//Create a SqlCommand object from connection object
			SqlCommand cmd = conn.CreateCommand();

			//Specify the SELECT SQL statement that
			//retrieves all attributes of a staff record.
			cmd.CommandText = @"SELECT SAppt FROM Staff
				WHERE StaffID = @selectedStaffID";

			cmd.Parameters.AddWithValue("@selectedStaffID", staffID);

			//Open a database connection
			conn.Open();
			//Execute SELCT SQL through a DataReader
			SqlDataReader reader = cmd.ExecuteReader();

			reader.Read();
			string role = reader.GetString(0);
			reader.Close();
			//Close database connection
			conn.Close();
			return role;
		}
		public string findName(string StaffID)
		{
			string SName = "";

			//Create a SqlCommand object from connection object
			SqlCommand cmd = conn.CreateCommand();

			//Specify the SELECT SQL statement that
			//retrieves all attributes of a staff record.
			cmd.CommandText = @"SELECT SName FROM Staff
				WHERE StaffID = @selectedMemberID";

			cmd.Parameters.AddWithValue("@selectedMemberID", StaffID);

			//Open a database connection
			conn.Open();
			//Execute SELCT SQL through a DataReader

			try
			{
				SqlDataReader reader = cmd.ExecuteReader();


				if (reader.HasRows)
				{
					//Read the record from database
					while (reader.Read())
					{
						SName = reader.GetString(0);
					}
				}
				//Close data reader
				reader.Close();
				//Close database connection
				conn.Close();
				return SName;
			}
			catch (Exception ex)
			{
				return "Unknown";
			}
		}


	}
}
