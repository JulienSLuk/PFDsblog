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
            string strConn = Configuration.GetConnectionString("ZZFashionConnectionString");
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
                    f.MemberID = reader.GetString(1);
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
                    r.MemberID = !reader.IsDBNull(2) ?
                                 reader.GetString(2) : (string?)null;
                    r.StaffID = !reader.IsDBNull(3) ?
                                 reader.GetString(3) : (string?)null;
                    r.DateTimePosted = reader.GetDateTime(4);
                    r.Text = reader.GetString(5);

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

        // refresh all feedbacks and responses
        public List<FeedbackViewModel> GetAllFeedbackAndResponses()
        {
            GetAllFeedback();
            GetAllResponses();
            return feedbackList;
        }

        // add response
        public void AddResponse(Response response, String role, string userId)
        {
            SqlCommand cmd = conn.CreateCommand();

            //using id else statement due to the need of different parameters for different role, MemberID for customer, and StaffID for marketing manager
            if (role == "Customer")
            {
                cmd.CommandText = @"INSERT INTO Response (FeedbackID, MemberID, [Text])
                                VALUES(@feedbackId, @memberId, @text)";
                cmd.Parameters.AddWithValue("memberId", userId);
            }
            else if (role == "Marketing Manager")
            {
                cmd.CommandText = @"INSERT INTO Response (FeedbackID, StaffID, [Text])
                                VALUES(@feedbackId, @staffId, @text)";
                cmd.Parameters.AddWithValue("staffId", userId);
            }

            cmd.Parameters.AddWithValue("@feedbackId", response.FeedbackID);
            cmd.Parameters.AddWithValue("@text", response.Text);

            conn.Open();

            cmd.ExecuteNonQuery();

            conn.Close();

        }

        

        //customer feedback create to database
        public void Add(FeedbackViewModel feedback)
        {

            SqlCommand cmd = conn.CreateCommand();

            if (feedback.Text != null && feedback.ImageFileName != null)
            {
                cmd.CommandText = @"INSERT INTO Feedback (MemberID, Title, [Text], ImageFileName)
                                VALUES(@selectedMemberID, @title, @text, @fileName)";
                cmd.Parameters.AddWithValue("text", feedback.Text);
                cmd.Parameters.AddWithValue("fileName", feedback.ImageFileName);
            }
            else if (feedback.Text == null && feedback.ImageFileName != null)
            {
                cmd.CommandText = @"INSERT INTO Feedback (MemberID, Title, ImageFileName)
                                VALUES(@selectedMemberID, @title, @fileName)";
                cmd.Parameters.AddWithValue("fileName", feedback.ImageFileName);
            }
            else if (feedback.Text != null && feedback.ImageFileName == null)
            {
                cmd.CommandText = @"INSERT INTO Feedback (MemberID, Title, [Text])
                                VALUES(@selectedMemberID, @title, @text)";
                cmd.Parameters.AddWithValue("text", feedback.Text);
            }

            cmd.Parameters.AddWithValue("title", feedback.Title);
            cmd.Parameters.AddWithValue("selectedMemberID", feedback.MemberID);

            conn.Open();

            cmd.ExecuteNonQuery();

            conn.Close();
        }
    }
}