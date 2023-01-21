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

        public List<ProductManager> GetTechProducts()
        {

            //sql command to select all products based on productid
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = @"SELECT * FROM Blog WHERE BlogCat = 'Tech' ORDER BY BlogID";
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
                        blogCat = reader.GetString(4),
                    }
                    ); ;
            }
            reader.Close();
            conn.Close();
            return productList;

        }

        public List<ProductManager> GetFinSchProducts()
        {

            //sql command to select all products based on productid
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = @"SELECT * FROM Blog WHERE BlogCat = 'Financial Scheme' ORDER BY BlogID";
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
                        blogCat = reader.GetString(4),
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

        public int UpdateProduct(ProductManager product, int productID)
        {
            SqlCommand cmd = conn.CreateCommand();

            cmd.CommandText = @"UPDATE Blog SET ProductTitle = @productName, ProductImage = @productImage, Price = @productPrice,
                                EffectiveDate = @productEffectiveDate, Obsolete = @productCondition WHERE ProductID = @productID";

            cmd.Parameters.AddWithValue("@productName", product.blogName);

            if (product.photoUpload == null)
            {
                cmd.Parameters.AddWithValue("@productImage", "null-picture.jpg");
            }
            else
            {
                cmd.Parameters.AddWithValue("@productImage", product.blogImage);
            }

            //checking product condition, if new database should store 1 and if obsolete, database should store 0

            cmd.Parameters.AddWithValue("@productID", productID);

            conn.Open();

            int count = cmd.ExecuteNonQuery();

            conn.Close();

            return count;
        }


    }
}
