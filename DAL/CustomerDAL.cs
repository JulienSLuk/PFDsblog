using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Data.SqlClient;
using WEB2022_ZZFashion.Models;
using System.Linq;

namespace WEB2022_ZZFashion.DAL
{
	public class CustomerDAL
	{
		private IConfiguration Configuration { get; }
		private SqlConnection conn;
		public CustomerDAL()
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

		// method to get customer base on id
		public Customer GetCustomer(string id)
		{
			//Create a SqlCommand object from connection object
			SqlCommand cmd = conn.CreateCommand();
			//Specify the SELECT SQL statement
			cmd.CommandText = @"SELECT * FROM Customer WHERE MemberID = @currentMemberID";
			cmd.Parameters.AddWithValue("@currentMemberID", id);
			//Open a database connection
			conn.Open();
			//Execute the SELECT SQL through a DataReader
			SqlDataReader reader = cmd.ExecuteReader();
			//Create customer object
			Customer customer = new Customer();

			while (reader.Read())
			{
				customer = new Customer
				{
					MemberID = reader.GetString(0), //0: 1st column
					Name = reader.GetString(1), //1: 2nd column
												//Get the first character of a string
					Gender = reader.GetString(2)[0], //2: 3rd column
					DOB = reader.GetDateTime(3), //3: 4th column
					ResidentialAddr = !reader.IsDBNull(4) ? reader.GetString(4) : (string?)null, //5: 6th column
					Country = reader.GetString(5), //6: 7th column
					ContactNo = !reader.IsDBNull(6) ? reader.GetString(6) : (string?)null, //7: 8th column
					Email = !reader.IsDBNull(7) ? reader.GetString(7) : (string?)null, //8: 9th column
					Password = reader.GetString(8)
				};
			}

			//Close DataReader
			reader.Close();
			//Close the database connection
			conn.Close();
			return customer;

		}
		public List<Customer> GetAllCustomer()
		{
			//Create a SqlCommand object from connection object
			SqlCommand cmd = conn.CreateCommand();
			//Specify the SELECT SQL statement
			cmd.CommandText = @"SELECT MemberID
									  ,MName
									  ,MGender
									  ,MBirthDate
									  ,MAddress
									  ,MCountry
									  ,MTelNo
									  ,MEmailAddr
								FROM Customer ORDER BY MemberID";
			//Open a database connection
			conn.Open();
			//Execute the SELECT SQL through a DataReader
			SqlDataReader reader = cmd.ExecuteReader();
			//Read all records until the end, save data into a customer list
			List<Customer> customerList = new List<Customer>();
			while (reader.Read())
			{
				customerList.Add(
					new Customer
					{
						MemberID = reader.GetString(0), //0: 1st column
						Name = reader.GetString(1), //1: 2nd column
													//Get the first character of a string
						Gender = reader.GetString(2)[0], //2: 3rd column
						DOB = reader.GetDateTime(3), //3: 4th column
						ResidentialAddr = !reader.IsDBNull(4) ? reader.GetString(4) : (string?)null, //5: 6th column
						Country = reader.GetString(5), //6: 7th column
						ContactNo = !reader.IsDBNull(6) ? reader.GetString(6) : (string?)null, //7: 8th column
						Email = !reader.IsDBNull(7) ? reader.GetString(7) : (string?)null, //8: 9th column
						
					}
				);
			}

			//Close DataReader
			reader.Close();
			//Close the database connection
			conn.Close();
			return customerList;
		}

		public string getNewMemberID()
		{

			SqlCommand cmd = conn.CreateCommand();
			cmd.CommandText = @"SELECT MemberID
								FROM Customer
								ORDER BY MemberID DESC";
			conn.Open();
			//get member id
			string prevmemberid = cmd.ExecuteScalar().ToString();
			conn.Close();
			int memberidno = int.Parse(prevmemberid[1..]);
			memberidno += 1;
			string memberid = 'M' + String.Format("{0:00000000}", memberidno);
			return memberid;
		}

		public string Add(Customer customer)
		{

			//Create a SqlCommand object from connection object
			SqlCommand cmd = conn.CreateCommand();

			cmd.CommandText = @"INSERT INTO Customer (MemberID, MName, MGender, MBirthDate, MAddress, MCountry, MTelNo, MEmailAddr)
							VALUES(@memberid, @name, @gender, @dob, @residentialaddr, @country, @contactno, @email)";


			customer.MemberID = getNewMemberID();


			//Define the parameters used in SQL statement, value for each parameter
			//is retrieved from respective class's property.
			cmd.Parameters.AddWithValue("@memberid", customer.MemberID);
			cmd.Parameters.AddWithValue("@name", customer.Name);
			cmd.Parameters.AddWithValue("@gender", customer.Gender);
			cmd.Parameters.AddWithValue("@dob", customer.DOB);
			if (customer.ResidentialAddr == null)
			{
				cmd.Parameters.AddWithValue("@residentialaddr", DBNull.Value);
			}
			else
			{
				cmd.Parameters.AddWithValue("@residentialaddr", customer.ResidentialAddr);
			}
			cmd.Parameters.AddWithValue("@country", customer.Country);
			if (customer.ContactNo == null)
			{
				cmd.Parameters.AddWithValue("@contactno", DBNull.Value);
			}
			else
			{
				cmd.Parameters.AddWithValue("@contactno", customer.ContactNo);
			}
			if (customer.Email == null)
			{
				cmd.Parameters.AddWithValue("@email", DBNull.Value);
			}
			else
			{
				cmd.Parameters.AddWithValue("@email", customer.Email);
			}

			//A connection to database must be opened before any operations made.
			conn.Open();
			cmd.ExecuteNonQuery();
			//A connection should be closed after operations.
			conn.Close();
			//Return id when no error occurs.
			return customer.MemberID;
		}

		public List<SPCustomerViewModel> Search(String query)
		{
			List<SPCustomerViewModel> customerPreviewList = new List<SPCustomerViewModel>();
			///Create a SqlCommand object from connection object
			SqlCommand cmd0 = conn.CreateCommand();
			SqlCommand cmd1 = conn.CreateCommand();
			//Specify the SELECT SQL statement
			string searchmember = @$"SELECT Customer.MemberID, MName, NumOfPendingVoucher FROM Customer FULL OUTER JOIN (SELECT MemberID, COUNT(IssuingID) AS NumOfPendingVoucher FROM CashVoucher WHERE Status = 0 group by MemberID) as A ON Customer.MemberID = A.MemberID WHERE Customer.MemberID LIKE '%{query}%'";

			cmd0.CommandText = searchmember;

			//Specify the SELECT SQL statement
			string searchname = @$"SELECT Customer.MemberID, MName, NumOfPendingVoucher FROM Customer FULL OUTER JOIN (SELECT MemberID, COUNT(IssuingID) AS NumOfPendingVoucher FROM CashVoucher WHERE Status = 0 group by MemberID) as A ON Customer.MemberID = A.MemberID WHERE MName LIKE '%{query}%'";
			cmd1.CommandText = searchname;

			//Open a database connection
			conn.Open();
			//Execute the SELECT SQL through a DataReader
			SqlDataReader reader0 = cmd0.ExecuteReader();
			//Read all records until the end, save data into a customer list

			if (reader0.HasRows)
			{
				//Read the record from database
				while (reader0.Read())
				{
					SPCustomerViewModel customerPreview0 =
						new SPCustomerViewModel
						{
							MemberID = reader0.GetString(0), //0: 1st column
							Name = reader0.GetString(1), //1: 2nd column
							PendingVoucherNo = !reader0.IsDBNull(2) ? reader0.GetInt32(2) : 0

						};
					customerPreviewList.Add(customerPreview0);
				}
			}
			reader0.Close();

			SqlDataReader reader1 = cmd1.ExecuteReader();
			if (reader1.HasRows)
			{
				while (reader1.Read())
				{
					SPCustomerViewModel customerPreview1 = new SPCustomerViewModel
					{
						MemberID = reader1.GetString(0), //0: 1st column
						Name = reader1.GetString(1), //1: 2nd column
						PendingVoucherNo = !reader1.IsDBNull(2) ? reader1.GetInt32(2) : 0

					};
					customerPreviewList.Add(customerPreview1);
				}
			}

			//Close DataReader
			reader1.Close();

			conn.Close();

			return customerPreviewList.GroupBy(x => x.MemberID).Select(x => x.First()).ToList();
		}


		public string findName(string memberID)
		{
			string MName = "";

			//Create a SqlCommand object from connection object
			SqlCommand cmd = conn.CreateCommand();

			//Specify the SELECT SQL statement that
			//retrieves all attributes of a staff record.
			cmd.CommandText = @"SELECT MName FROM Customer
				WHERE MemberID = @selectedMemberID";

			cmd.Parameters.AddWithValue("@selectedMemberID", memberID);

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
						MName = reader.GetString(0);
					}
				}
				//Close data reader
				reader.Close();
				//Close database connection
				conn.Close();
				return MName;
			}
			catch (Exception ex)
			{
				return "Unknown";
			}
		}


		public bool CheckPassword(string memberID, string password)
		{
			//Create a SqlCommand object from connection object
			SqlCommand cmd = conn.CreateCommand();

			//Specify the SELECT SQL statement that
			//retrieves all attributes of a staff record.
			cmd.CommandText = @"SELECT MPassword FROM Customer
				WHERE MemberID = @selectedMemberID";

			cmd.Parameters.AddWithValue("@selectedMemberID", memberID);

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




		public bool IsEmailExist(string email, string customerID)
		{
			bool emailFound = false;
			//Create a SqlCommand object and specify the SQL statement
			//to get a staff record with the email address to be validated
			SqlCommand cmd = conn.CreateCommand();
			cmd.CommandText = @"SELECT MemberID FROM Customer
								WHERE MEmailAddr=@selectedEmail";
			cmd.Parameters.AddWithValue("@selectedEmail", email);
			//Open a database connection and execute the SQL statement
			conn.Open();
			SqlDataReader reader = cmd.ExecuteReader();
			if (reader.HasRows)
			{ //Records found
				while (reader.Read())
				{
					if (reader.GetString(0) != customerID)
						//The email address is used by another staff
						emailFound = true;
				}
			}
			else
			{ //No record
				emailFound = false; // The email address given does not exist
			}
			reader.Close();
			conn.Close();
			return emailFound;
		}

		public bool IsContactExist(string contactno, string customerID)
		{
			bool contactnoFound = false;
			//Create a SqlCommand object and specify the SQL statement
			//to get a staff record with the contactno to be validated
			SqlCommand cmd = conn.CreateCommand();
			cmd.CommandText = @"SELECT MemberID FROM Customer
								WHERE MTelNo=@selectedContact";
			cmd.Parameters.AddWithValue("@selectedContact", contactno);
			//Open a database connection and execute the SQL statement
			conn.Open();
			SqlDataReader reader = cmd.ExecuteReader();
			if (reader.HasRows)
			{ //Records found
				while (reader.Read())
				{
					if (reader.GetString(0) != customerID)
						//The contactno is used by another staff
						contactnoFound = true;
				}
			}
			else
			{ //No record
				contactnoFound = false; // The contactno given does not exist
			}
			reader.Close();
			conn.Close();
			return contactnoFound;
		}

		public void UpdatePW(Customer customer, string newPW)
		{
			//Create a SqlCommand object from connection object
			SqlCommand cmd = conn.CreateCommand();
			//Specify an UPDATE SQL statement
			cmd.CommandText = @"UPDATE Customer SET MPassword = @newPassword
				WHERE MemberID = @selectedMemberID";
			//Define the parameters used in SQL statement, value for each parameter
			//is retrieved from respective class's property.
			cmd.Parameters.AddWithValue("@newPassword", newPW);
			cmd.Parameters.AddWithValue("@selectedMemberID", customer.MemberID);

			//Open a database connection
			conn.Open();
			//ExecuteNonQuery is used for UPDATE and DELETE
			cmd.ExecuteNonQuery();
			//Close the database connection
			conn.Close();
		}

		public void UpdateEmail(string memberID, string newEmail)
		{
			//Create a SqlCommand object from connection object
			SqlCommand cmd = conn.CreateCommand();
			//Specify an UPDATE SQL statement
			cmd.CommandText = @"UPDATE Customer SET MEmailAddr = @newEmail
				WHERE MemberID = @selectedMemberID";
			//Define the parameters used in SQL statement, value for each parameter
			//is retrieved from respective class's property.
			cmd.Parameters.AddWithValue("@newEmail", newEmail);
			cmd.Parameters.AddWithValue("@selectedMemberID", memberID);

			//Open a database connection
			conn.Open();
			//ExecuteNonQuery is used for UPDATE and DELETE
			cmd.ExecuteNonQuery();
			//Close the database connection
			conn.Close();
		}

		public void UpdatePhone(string memberID, string newPhone)
		{
			//Create a SqlCommand object from connection object
			SqlCommand cmd = conn.CreateCommand();
			//Specify an UPDATE SQL statement
			cmd.CommandText = @"UPDATE Customer SET MTelNo = @newPhone
				WHERE MemberID = @selectedMemberID";
			//Define the parameters used in SQL statement, value for each parameter
			//is retrieved from respective class's property.
			cmd.Parameters.AddWithValue("@newPhone", newPhone);
			cmd.Parameters.AddWithValue("@selectedMemberID", memberID);

			//Open a database connection
			conn.Open();
			//ExecuteNonQuery is used for UPDATE and DELETE
			cmd.ExecuteNonQuery();
			//Close the database connection
			conn.Close();
		}

		public void UpdateAddr(string memberID, string newAddr)
		{
			//Create a SqlCommand object from connection object
			SqlCommand cmd = conn.CreateCommand();
			//Specify an UPDATE SQL statement
			cmd.CommandText = @"UPDATE Customer SET MAddress = @newAddr
				WHERE MemberID = @selectedMemberID";
			//Define the parameters used in SQL statement, value for each parameter
			//is retrieved from respective class's property.
			cmd.Parameters.AddWithValue("@newAddr", newAddr);
			cmd.Parameters.AddWithValue("@selectedMemberID", memberID);

			//Open a database connection
			conn.Open();
			//ExecuteNonQuery is used for UPDATE and DELETE
			cmd.ExecuteNonQuery();
			//Close the database connection
			conn.Close();
		}
	}
}
