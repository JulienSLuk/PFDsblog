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
    public class ProductManagerDAL
    {
        private IConfiguration Configuration { get; }
        private SqlConnection conn;
        //Constructor
        public ProductManagerDAL()
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
                    }
                    ); ;
            }
            reader.Close();
            conn.Close();
            return productList;

        }

        ////add product
        //public int Add(ProductManager product)
        //{
        //    //add product into database based on inputs to add into database
        //    SqlCommand cmd = conn.CreateCommand();

        //    cmd.CommandText = @"INSERT INTO Product (ProductTitle, ProductImage, Price, EffectiveDate, Obsolete)
        //                        VALUES(@productName, @productImage, @productPrice, @productEffectiveDate, @productCondition)";

        //    cmd.Parameters.AddWithValue("@productName", product.productName);
        //    cmd.Parameters.AddWithValue("@productImage", "null-picture.jpg");
        //    cmd.Parameters.AddWithValue("@productPrice", product.productPrice);
        //    cmd.Parameters.AddWithValue("@productEffectiveDate", product.productEffectiveDate);           
        //    cmd.Parameters.AddWithValue("@productCondition", 1);

        //    conn.Open();

        //    int count = cmd.ExecuteNonQuery();
        //    conn.Close();

        //    return count;
        //}

        ////get product details to display project whevenever needed
        //public ProductManager GetProductDetails(int productID)
        //{
        //    ProductManager product = new ProductManager();
        //    SqlCommand cmd = conn.CreateCommand();

        //    cmd.CommandText = @"SELECT * FROM Product WHERE ProductID = @selectedProductID";

        //    cmd.Parameters.AddWithValue("@selectedProductID", productID);

        //    conn.Open();

        //    SqlDataReader reader = cmd.ExecuteReader();

        //    if (reader.HasRows)
        //    {
        //        while (reader.Read()){
        //            //product.productID = productID;
        //            product.productName = reader.GetString(1);
        //            product.productImage = !reader.IsDBNull(2) ? reader.GetString(2) : null;
        //            product.productPrice = Decimal.Round(reader.GetDecimal(3), 2);
        //            product.productEffectiveDate = reader.GetDateTime(4);
        //            //checking if object is obsolete or new based on 0/1 in database
        //            if(!reader.IsDBNull(5)){
        //                if (reader.GetString(5) == "0")
        //                {
        //                    product.productCondition = "Obsolete";
        //                }
        //                else if (reader.GetString(5) == "1")
        //                {
        //                    product.productCondition = "New";
        //                }
        //            }
        //        }
        //    }

        //    reader.Close();
        //    conn.Close();

        //    return product;
        //}

        ////update products into database
        //public int UpdateProduct(ProductManager product, int productID)
        //{
        //    SqlCommand cmd = conn.CreateCommand();

        //    cmd.CommandText = @"UPDATE Product SET ProductTitle = @productName, ProductImage = @productImage, Price = @productPrice,
        //                        EffectiveDate = @productEffectiveDate, Obsolete = @productCondition WHERE ProductID = @productID";

        //    cmd.Parameters.AddWithValue("@productName", product.productName);
            
        //    if (product.photoUpload == null)
        //    {
        //        cmd.Parameters.AddWithValue("@productImage", "null-picture.jpg");
        //    }
        //    else
        //    {
        //        cmd.Parameters.AddWithValue("@productImage", product.productImage);
        //    }
        //    cmd.Parameters.AddWithValue("@productPrice", product.productPrice);
        //    cmd.Parameters.AddWithValue("@productEffectiveDate", product.productEffectiveDate);
        //    //checking product condition, if new database should store 1 and if obsolete, database should store 0
        //    if (product.productCondition.ToUpper() == "NEW")
        //    {
        //        cmd.Parameters.AddWithValue("@productCondition", 1);
        //    }
        //    else if (product.productCondition.ToUpper() == "OBSOLETE")
        //    {
        //        cmd.Parameters.AddWithValue("@productCondition", 0);
        //    }
        //    cmd.Parameters.AddWithValue("@productID", productID);

        //    conn.Open();

        //    int count = cmd.ExecuteNonQuery();

        //    conn.Close();

        //    return count;
        //}


    }
}
