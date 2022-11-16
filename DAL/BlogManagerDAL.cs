﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Data.SqlClient;
using WEB2022Apr_P01_T3.Models;

namespace WEB2022Apr_P01_T3.DAL
{
    public class BlogManagerDAL
    {
        private IConfiguration Configuration { get; }
        private SqlConnection conn;
        //Constructor
        public BlogManagerDAL()
        {
            //Read ConnectionString from appsettings.json file
            var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json");
            Configuration = builder.Build();
            string strConn = Configuration.GetConnectionString(
            "SBLOGConnectionString");
            //Instantiate a SqlConnection object with the
            //Connection String read.
            conn = new SqlConnection(strConn);
        }

        //get all products method
        public List<ProductManager> GetAllProducts()
        {

            //sql command to select all products based on productid
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = @"SELECT * FROM Blog ORDER BY BlogID";  
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();


            List<ProductManager> productList = new List<ProductManager>();
            
            //add products gathered from database into productlist
            while (reader.Read())
            {
                productList.Add(
                    new ProductManager
                    {
                        blogID = reader.GetInt32(0),
                        blogName = reader.GetString(1),
                        blogImage = !reader.IsDBNull(2) ? reader.GetString(2) : null,
                        blogDesc = reader.GetString(3),
                        //blogCat = reader.GetString(3),
                    }
                    ); ;
            }
            reader.Close();
            conn.Close();
            return productList;

        }

        //add product
        public void Add(ProductManager product)
        {
            //add product into database based on inputs to add into database
            SqlCommand cmd = conn.CreateCommand();

            cmd.CommandText = @"INSERT INTO Blog (BlogTitle, BlogImage, BlogDesc)
                                VALUES(@blogName, @blogImage, @blogDesc)";

            cmd.Parameters.AddWithValue("@blogName", product.blogName);
            cmd.Parameters.AddWithValue("@blogImage", product.blogImage);
            cmd.Parameters.AddWithValue("@blogDesc", product.blogDesc);

            conn.Open();

            cmd.ExecuteNonQuery();
            conn.Close();

        }


    }
}
