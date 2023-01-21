using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Data.SqlClient;
using WEB2022_ZZFashion.Models;

namespace WEB2022_ZZFashion.DAL
{
    public class FeedbackDAL
    {
        private IConfiguration Configuration { get; }
        private SqlConnection conn;
        //Constructor
        public FeedbackDAL()
        {
            //Read ConnectionString from appsettings.json file
            var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json");
            Configuration = builder.Build();
            string strConn = Configuration.GetConnectionString(
            "SbloGConnectionString");
            //Instantiate a SqlConnection object with the 
            //Connection String read. 
            conn = new SqlConnection(strConn);
            
        }
        public List<Feedback> GetAllFeedback()
        {
            //Create a SqlCommand object from connection object
            SqlCommand cmd = conn.CreateCommand();
            //Specify the SELECT SQL statement 
            cmd.CommandText = @"SELECT * FROM Feedback ORDER BY DateTimePosted DESC";
            //Open a database connection
            conn.Open();
            //Execute the SELECT SQL through a DataReader
            SqlDataReader reader = cmd.ExecuteReader();
            //Read all records until the end, save data into a feedback list
            List<Feedback> feedbackList = new List<Feedback>();
            while (reader.Read())
            {
                feedbackList.Add(
                new Feedback
                {
                   FeedbackID = reader.GetInt32(0), //0: 1st column
                   MemberID = reader.GetString(1), //1: 2nd column
                                                   //Get the first character of a string
                   DateTimePosted = reader.GetDateTime(2), //1: 3rd column
                   Title = reader.GetString(3), //3: 4th column
                   Text = reader.GetString(4), //5: 6th column
                    ImageFileName = !reader.IsDBNull(5) ? reader.GetString(5) : (string?)null //6: 7th column
                }
                );
            }
            //Close DataReader
            reader.Close();
            //Close the database connection
            conn.Close();
            return feedbackList;
        }

        // method to get all feedbacks based on Member ID
        public List<Feedback> GetMemberFeedbacks(string id)
        {
            //Create a SqlCommand object from connection object
            SqlCommand cmd = conn.CreateCommand();
            //Specify the SELECT SQL statement 
            cmd.CommandText = @"SELECT * FROM Feedback WHERE MemberID = @selectedMemberID ORDER BY FeedbackID DESC";
            // add parameters
            cmd.Parameters.AddWithValue("@selectedMemberID", id);
            //Open a database connection
            conn.Open();
            //Execute the SELECT SQL through a DataReader
            SqlDataReader reader = cmd.ExecuteReader();
            //Read all records until the end, save data into a feedback list
            List<Feedback> feedbackList = new List<Feedback>();
            while (reader.Read())
            {
                feedbackList.Add(
                    new Feedback
                    {
                        FeedbackID = reader.GetInt32(0), //0: 1st column
                        MemberID = reader.GetString(1), //1: 2nd column
                                                        //Get the first character of a string
                        DateTimePosted = reader.GetDateTime(2), //1: 3rd column
                        Title = reader.GetString(3), //3: 4th column
                        Text = reader.GetString(4), //5: 6th column
                        ImageFileName = !reader.IsDBNull(5) ? reader.GetString(5) : (string?)null //6: 7th column
                    }
                );
            }
            //Close DataReader
            reader.Close();
            //Close the database connection
            conn.Close();
            return feedbackList;
        }
        //Get feedback details
        public Feedback GetFeedback(int feedbackid)
        {
            Feedback feedback = new Feedback();
            //Create a SqlCommand object from connection object
            SqlCommand cmd = conn.CreateCommand();
            //Specify the SELECT SQL statement 
            cmd.CommandText = @"SELECT * FROM Feedback WHERE FeedbackID = @selectedFeedbackID";
            // add parameters
            cmd.Parameters.AddWithValue("@selectedFeedbackID",feedbackid);
            //Open a database connection
            conn.Open();
            //Execute the SELECT SQL through a DataReader
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    feedback.FeedbackID = reader.GetInt32(0); //0: 1st column
                    feedback.MemberID = reader.GetString(1); //1: 2nd column
                    feedback.DateTimePosted = reader.GetDateTime(2); //1: 3rd column
                    feedback.Title = reader.GetString(3); //3: 4th column
                    feedback.Text = reader.GetString(4); //5: 6th column
                    feedback.ImageFileName = !reader.IsDBNull(5) ? reader.GetString(5) : (string?)null; //6: 7th column
                }
            }
            //Close DataReader
            reader.Close();
            //Close the database connection
            conn.Close();
            return feedback;
        }

        // method to get feedback by feedbackID
        public Feedback GetFeebackByFeedbackID(int id)
        {
            //Create a SqlCommand object from connection object
            SqlCommand cmd = conn.CreateCommand();
            //Specify the SELECT SQL statement 
            cmd.CommandText = @"SELECT * FROM Feedback WHERE FeedbackID = @selectedFeedbackID";
            // add parameters
            cmd.Parameters.AddWithValue("@selectedFeedbackID", id);
            //Open a database connection
            conn.Open();
            //Execute the SELECT SQL through a DataReader
            SqlDataReader reader = cmd.ExecuteReader();
            //Read all records until the end, save data into a feedback list
            Feedback feedback = new Feedback();
            while (reader.Read())
            {
                feedback = new Feedback
                {
                    FeedbackID = reader.GetInt32(0), //0: 1st column
                    MemberID = reader.GetString(1), //1: 2nd column
                                                             //Get the first character of a string
                    DateTimePosted = reader.GetDateTime(2), //1: 3rd column
                    Title = reader.GetString(3), //3: 4th column
                    Text = reader.GetString(4), //5: 6th column
                    ImageFileName = !reader.IsDBNull(5) ? reader.GetString(5) : (string?)null //6: 7th column
                };
                
            }
            //Close DataReader
            reader.Close();
            //Close the database connection
                conn.Close();
            return feedback;
        }

        // method to add feedback
        public void AddFeedback(Feedback feedback)
        {
            //Create a SqlCommand object from connection object
            SqlCommand cmd = conn.CreateCommand();

            cmd.CommandText = @"INSERT INTO Feedback (MemberID, DateTimePosted, Title, Text, ImageFileName)
							VALUES(@memberid, @datetimeposted, @title, @text, @imagefilename)";


            //Define the parameters used in SQL statement, value for each parameter
            //is retrieved from respective class's property.
            cmd.Parameters.AddWithValue("@memberid", feedback.MemberID);
            cmd.Parameters.AddWithValue("@datetimeposted", DateTime.Now);
            cmd.Parameters.AddWithValue("@title", feedback.Title);
            cmd.Parameters.AddWithValue("@text", feedback.Text);
            

            if (feedback.ImageFileName == null || feedback.ImageFileName == "")
            {
                cmd.Parameters.AddWithValue("@imagefilename", DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@imagefilename", feedback.ImageFileName);
            }

            //A connection to database must be opened before any operations made.
            conn.Open();
            cmd.ExecuteNonQuery();
            //A connection should be closed after operations.
            conn.Close();
        }

    }
}
