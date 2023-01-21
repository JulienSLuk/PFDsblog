using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WEB2022_ZZFashion.Models;

namespace WEB2022_ZZFashion.DAL
{
    public class ResponseDAL
    {
        private IConfiguration Configuration { get; }
        private SqlConnection conn;

		public ResponseDAL()
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

		// method to get list of response based on MemberID
		public List<Response> GetMemberResponse(string id)
        {
            //Create a SqlCommand object from connection object
            SqlCommand cmd = conn.CreateCommand();
            //Specify the SELECT SQL statement 
            cmd.CommandText = @"SELECT * FROM Response INNER JOIN Feedback ON Response.FeedbackID = Feedback.FeedbackID WHERE Feedback.MemberID = @selectedMemberID";
            // add parameters
            cmd.Parameters.AddWithValue("@selectedMemberID", id);
            //Open a database connection
            conn.Open();
            //Execute the SELECT SQL through a DataReader
            SqlDataReader reader = cmd.ExecuteReader();
            //Read all records until the end, save data into a feedback list
            List<Response> responseList = new List<Response>();
            while (reader.Read())
            {
                responseList.Add(
                    new Response
                    {
                        ResponseID = reader.GetInt32(0), //0: 1st column
                        FeedbackID = reader.GetInt32(1), //1: 2nd column
                                                        //Get the first character of a string
                        MemberID = !reader.IsDBNull(2) ? reader.GetString(2) : (string?)null, // 3rd column
                        StaffID = !reader.IsDBNull(3) ? reader.GetString(3) : (string?)null, // 4th column
                        DateTimePosted = reader.GetDateTime(4), //1: 5th column
                        Text = reader.GetString(5), //5: 6th column
                    }
                );
            }
            //Close DataReader
            reader.Close();
            //Close the database connection
            conn.Close();
            return responseList;
        }

        // method to get list of response based on feedbackID
        public List<Response> GetResponseByFeedbackID(int id)
        {
            //Create a SqlCommand object from connection object
            SqlCommand cmd = conn.CreateCommand();
            //Specify the SELECT SQL statement 
            cmd.CommandText = @"SELECT * FROM Response INNER JOIN Feedback ON Response.FeedbackID = Feedback.FeedbackID WHERE Feedback.FeedbackID = @selectedFeedbackID";
            // add parameters
            cmd.Parameters.AddWithValue("@selectedFeedbackID", id);
            //Open a database connection
            conn.Open();
            //Execute the SELECT SQL through a DataReader
            SqlDataReader reader = cmd.ExecuteReader();
            //Read all records until the end, save data into a feedback list
            List<Response> responseList = new List<Response>();
            while (reader.Read())
            {
                responseList.Add(
                    new Response
                    {
                        ResponseID = reader.GetInt32(0), //0: 1st column
                        FeedbackID = reader.GetInt32(1), //1: 2nd column
                                                         //Get the first character of a string
                        MemberID = !reader.IsDBNull(2) ? reader.GetString(2) : (string?)null, // 3rd column
                        StaffID = !reader.IsDBNull(3) ? reader.GetString(3) : (string?)null, // 4th column
                        DateTimePosted = reader.GetDateTime(4), //1: 5th column
                        Text = reader.GetString(5), //5: 6th column
                    }
                );
            }
            //Close DataReader
            reader.Close();
            //Close the database connection
            conn.Close();
            return responseList;
        }
        public int AddResponse(Response response)
        {
            //Create a SqlCommand object from connection object
            SqlCommand cmd = conn.CreateCommand();
            //Specify an INSERT SQL statement which will
            //return the auto-generated StaffID after insertion
            cmd.CommandText = @"INSERT INTO Response (FeedbackID, MemberID, StaffID, DateTimePosted, 
 [text]) 
OUTPUT INSERTED.ResponseID 
VALUES(@feedbackid, @memberid, @staffid, @datetimeposted, 
@text)";
            //Define the parameters used in SQL statement, value for each parameter
            //is retrieved from respective class's property.
            cmd.Parameters.AddWithValue("@feedbackid", response.FeedbackID);
            cmd.Parameters.AddWithValue("@staffid", response.StaffID);
            cmd.Parameters.AddWithValue("@datetimeposted", response.DateTimePosted);
            cmd.Parameters.AddWithValue("@text", response.Text);
            if (response.MemberID == null)
            {
                cmd.Parameters.AddWithValue("@memberid", DBNull.Value);
            }
            
            //A connection to database must be opened before any operations made.
            conn.Open();
            //ExecuteScalar is used to retrieve the auto-generated
            //StaffID after executing the INSERT SQL statement
            response.ResponseID = (int)cmd.ExecuteScalar();
            //A connection should be closed after operations.
            conn.Close();
            //Return id when no error occurs.
            return response.ResponseID;
        }
        public List<Response> GetStaffResponseByFeedbackID(int id)
        {
            //Create a SqlCommand object from connection object
            SqlCommand cmd = conn.CreateCommand();
            //Specify the SELECT SQL statement 
            cmd.CommandText = @"SELECT * FROM Response INNER JOIN Feedback ON Response.FeedbackID = Feedback.FeedbackID WHERE Feedback.FeedbackID = @selectedFeedbackID AND StaffID is not NULL";
            // add parameters
            cmd.Parameters.AddWithValue("@selectedFeedbackID", id);
            //Open a database connection
            conn.Open();
            //Execute the SELECT SQL through a DataReader
            SqlDataReader reader = cmd.ExecuteReader();
            //Read all records until the end, save data into a feedback list
            List<Response> responseList = new List<Response>();
            while (reader.Read())
            {
                responseList.Add(
                    new Response
                    {
                        ResponseID = reader.GetInt32(0), //0: 1st column
                        FeedbackID = reader.GetInt32(1), //1: 2nd column
                                                         //Get the first character of a string
                        MemberID = !reader.IsDBNull(2) ? reader.GetString(2) : (string?)null, // 3rd column
                        StaffID = !reader.IsDBNull(3) ? reader.GetString(3) : (string?)null, // 4th column
                        DateTimePosted = reader.GetDateTime(4), //1: 5th column
                        Text = reader.GetString(5), //5: 6th column
                    }
                );
            }
            //Close DataReader
            reader.Close();
            //Close the database connection
            conn.Close();
            return responseList;
        }
        public int DeleteResponse(int responseID)
        {
            //Instantiate a SqlCommand object, supply it with a DELETE SQL statement

            //to delete a staff record specified by a Staff ID
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = @"DELETE FROM Response
 WHERE ResponseID = @selectResponseID";
            cmd.Parameters.AddWithValue("@selectResponseID", responseID);
            //Open a database connection
            conn.Open();
            int rowAffected = 0;
            //Execute the DELETE SQL to remove the staff record
            rowAffected += cmd.ExecuteNonQuery();
            //Close database connection
            conn.Close();
            //Return number of row of staff record updated or deleted
            return rowAffected;
        }
        public int GetFeedbackIDByResponseID(int id)
        {
            //Create a SqlCommand object from connection object
            SqlCommand cmd = conn.CreateCommand();
            //Specify the SELECT SQL statement 
            cmd.CommandText = @"SELECT * FROM Response INNER JOIN Feedback ON Response.FeedbackID = Feedback.FeedbackID WHERE ResponseID= @selectedResponseID AND StaffID is not NULL";
            // add parameters
            cmd.Parameters.AddWithValue("@selectedResponseID", id);
            //Open a database connection
            conn.Open();
            //Execute the SELECT SQL through a DataReader
            SqlDataReader reader = cmd.ExecuteReader();
            //Read all records until the end, save data into a feedback list
            int feedbackID = 0;
            while (reader.Read())
            {
                feedbackID = reader.GetInt32(1);
            }
            //Close DataReader
            reader.Close();
            //Close the database connection
            conn.Close();
            return feedbackID;
        }
        //Get the details response
        public Response GetResponseDetails(int responseid)
        {
            Response response = new Response();
            //Create a SqlCommand object from connection object
            SqlCommand cmd = conn.CreateCommand();
            //Specify the SELECT SQL statement 
            cmd.CommandText = @"SELECT * FROM Response WHERE ResponseID = @selectedResponseID";
            // add parameters
            cmd.Parameters.AddWithValue("@selectedResponseID", responseid);
            //Open a database connection
            conn.Open();
            //Execute the SELECT SQL through a DataReader
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    response.ResponseID = reader.GetInt32(0);//0: 1st column
                    response.FeedbackID = reader.GetInt32(1); //1: 2nd column
                    response.MemberID = !reader.IsDBNull(2) ? reader.GetString(2) : (string?)null;// 3rd column
                    response.StaffID = !reader.IsDBNull(3) ? reader.GetString(3) : (string?)null; // 4th column
                    response.DateTimePosted = reader.GetDateTime(4); //1: 5th column
                    response.Text = reader.GetString(5); //5: 6th column
                }
            }
            //Close DataReader
            reader.Close();
            //Close the database connection
            conn.Close();
            return response;
        }
        public int Update(Response response)
        {
            //Create a SqlCommand object from connection object
            SqlCommand cmd = conn.CreateCommand();
            //Specify an UPDATE SQL statement
            cmd.CommandText = @"UPDATE Response SET 
 FeedbackID=@feedbackid,StaffID=@staffid,DateTimePosted=@datetimeposted,Text=@Text
WHERE ResponseID = @selectedResponseID";
            //Define the parameters used in SQL statement, value for each parameter
            //is retrieved from respective class's property.
            
            cmd.Parameters.AddWithValue("@feedbackid", response.FeedbackID);
            cmd.Parameters.AddWithValue("@staffid", response.StaffID);
            cmd.Parameters.AddWithValue("@datetimeposted", response.DateTimePosted);
            cmd.Parameters.AddWithValue("@text", response.Text);
            cmd.Parameters.AddWithValue("@selectedResponseID", response.ResponseID);
            //Open a database connection
            conn.Open();
            //ExecuteNonQuery is used for UPDATE and DELETE
            int count = cmd.ExecuteNonQuery();
            //Close the database connection
            conn.Close();
            return count;
        }
        
        public bool IsResponseExist(string text,int responseID)
        {
            bool respondFound = false;
            //Create a SqlCommand object and specify the SQL statement 
            //to get a staff record with the email address to be validated
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = @"SELECT ResponseID FROM Response 
 WHERE Text=@selectedText AND StaffID IS NOT NULL";
            cmd.Parameters.AddWithValue("@selectedText",text);
            //Open a database connection and execute the SQL statement
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            { //Records found
                while (reader.Read())
                {
                    if (reader.GetInt32(0) != responseID)
                       respondFound = true;
                        //If same text appears in another response
                        
                }
            }
            else
            { //No record
                respondFound = false; // The email address given does not exist
            }
            reader.Close();
            conn.Close();
            return respondFound;
        }
    }
}
