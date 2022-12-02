using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Data.SqlClient;
using WEB2022Apr_P01_T3.Models;
using Microsoft.AspNetCore.Http;

namespace WEB2022Apr_P01_T3.DAL
{
    public class FeedbackDAL
    {
        public List<FeedbackViewModel> feedbackList = new List<FeedbackViewModel>();

        private IConfiguration Configuration { get; }
        private SqlConnection conn;

        public FeedbackDAL()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            Configuration = builder.Build();
            string strConn = Configuration.GetConnectionString("SBLOGConnectionString");
            conn = new SqlConnection(strConn);
        }


        public void GetAllFeedback()
        {
            feedbackList.Clear();

            SqlCommand cmd = conn.CreateCommand();

            // Select all feedback and order them by DateTimePosted in descending order and followed by ordering them by FeedbackID in descending order
            cmd.CommandText = @"SELECT * FROM Feedback ORDER BY DateTimePosted DESC, FeedbackID DESC";
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    FeedbackViewModel f = new FeedbackViewModel();
                    f.FeedbackID = reader.GetInt32(0);
                    f.Email = reader.GetString(1);
                    f.DateTimePosted = reader.GetDateTime(2);
                    f.Title = reader.GetString(3);
                    f.Text = !reader.IsDBNull(4) ?
                             reader.GetString(4) : (string?)null;
                    f.ImageFileName = !reader.IsDBNull(5) ?
                                      reader.GetString(5) : (string?)null;
                    feedbackList.Add(f);
                }
            }
            conn.Close();
        }

        // get all responses and add them into the respective feedback's ResponseList
        public void GetAllResponses()
        {
            foreach (FeedbackViewModel f in feedbackList)
            {
                f.ResponseList.Clear();
            }

            SqlCommand cmd = conn.CreateCommand();  
            cmd.CommandText = @"SELECT * FROM Response";
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Response r = new Response();
                    r.ResponseID = reader.GetInt32(0);
                    r.FeedbackID = reader.GetInt32(1);
                    //r.Email = !reader.IsDBNull(2) ?
                    //             reader.GetString(2) : (string?)null;
                    //r.StaffID = !reader.IsDBNull(3) ?
                    //             reader.GetString(3) : (string?)null;
                    r.DateTimePosted = reader.GetDateTime(2);
                    r.Text = reader.GetString(3);

                    foreach (FeedbackViewModel f in feedbackList)
                    {
                        if (f.FeedbackID == r.FeedbackID)
                        {
                            f.ResponseList.Add(r);
                            break;
                        }
                    }
                }
            }
            conn.Close();
        }

        List<Response> viewresponseList = new List<Response>();

        // get all responses and add them into the respective feedback's ResponseList
        public List<Response> GetResponse(Feedback feedback)
        {
            int selectedFeedback = feedback.FeedbackID;

            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = @"SELECT * FROM Response WHERE FeedbackID = @selectedfeedbackID";
            cmd.Parameters.AddWithValue("@selectedfeedbackID", selectedFeedback);
            conn.Open();
            //Execute the SELECT SQL through a DataReader
            SqlDataReader reader = cmd.ExecuteReader();

            //Read all records until the end, save data into a staff list
            while (reader.Read())
            {
                viewresponseList.Add(
                    new Response
                    {
                        ResponseID = reader.GetInt32(0),
                        FeedbackID = reader.GetInt32(1),
                        DateTimePosted = reader.GetDateTime(2),
                        Text = !reader.IsDBNull(3) ?
                             reader.GetString(3) : (string?)null,

                    }
                );
            }


            //Close DataReader
            reader.Close();
            //Close the database connection
            conn.Close();
            return viewresponseList;
        }

        // refresh all feedbacks and responses
        public List<FeedbackViewModel> GetAllFeedbackAndResponses()
        {
            GetAllFeedback();
            GetAllResponses();
            return feedbackList;
        }


        

        //customer feedback create to database
        public void Add(FeedbackViewModel feedback)
        {

            SqlCommand cmd = conn.CreateCommand();

            if (feedback.Text != null && feedback.ImageFileName != null)
            {
                cmd.CommandText = @"INSERT INTO Feedback (Email, Title, [Text], ImageFileName)
                                VALUES(@email, @title, @text, @fileName)";
                cmd.Parameters.AddWithValue("text", feedback.Text);
                cmd.Parameters.AddWithValue("fileName", feedback.ImageFileName);
            }
            else if (feedback.Text == null && feedback.ImageFileName != null)
            {
                cmd.CommandText = @"INSERT INTO Feedback (Email, Title, ImageFileName)
                                VALUES(@email, @title, @fileName)";
                cmd.Parameters.AddWithValue("fileName", feedback.ImageFileName);
            }
            else if (feedback.Text != null && feedback.ImageFileName == null)
            {
                cmd.CommandText = @"INSERT INTO Feedback (Email, Title, [Text])
                                VALUES(@email, @title, @text)";
                cmd.Parameters.AddWithValue("text", feedback.Text);
            }

            cmd.Parameters.AddWithValue("title", feedback.Title);
            cmd.Parameters.AddWithValue("Email", feedback.Email);

            conn.Open();

            cmd.ExecuteNonQuery();

            conn.Close();
        }
    }
}