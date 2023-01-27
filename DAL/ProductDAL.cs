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
	public class ProductDAL
	{
		private IConfiguration Configuration { get; }
		private SqlConnection conn;
		public ProductDAL()
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
		public List<Product> GetAllProduct()
		{
			SqlCommand cmd = conn.CreateCommand();
			cmd.CommandText = @"SELECT * From Product";
			conn.Open();
			SqlDataReader reader = cmd.ExecuteReader();
			List<Product> productList = new List<Product>();
            while (reader.Read())
            {
				productList.Add(
					new Product
					{
						ID = reader.GetInt32(0),
						Title = reader.GetString(1),
						Image = !reader.IsDBNull(2) ? reader.GetString(2) : (string?)null,
						Desc = reader.GetString(3),
						Cat = reader.GetString(4),
						ObsoleteStatus = reader.GetString(5)
					}
					);
            }
			reader.Close();
			return productList;
		}

		public List<Product> GetTechProducts()
		{

			//sql command to select all products based on productid
			SqlCommand cmd = conn.CreateCommand();
			cmd.CommandText = @"SELECT * FROM Product WHERE ProductCat = 'Tech' ORDER BY ProductID";
			conn.Open();
			SqlDataReader reader = cmd.ExecuteReader();
			List<Product> productList = new List<Product>();
			while (reader.Read())
			{
				productList.Add(
					new Product
					{
						ID = reader.GetInt32(0),
						Title = reader.GetString(1),
						Image = !reader.IsDBNull(2) ? reader.GetString(2) : (string?)null,
						Desc = reader.GetString(3),
						Cat = reader.GetString(4),
						ObsoleteStatus = reader.GetString(5)
					}
					);
			}
			reader.Close();
			return productList;

		}

		public List<Product> GetFinSchProducts()
		{

			//sql command to select all products based on productid
			SqlCommand cmd = conn.CreateCommand();
			cmd.CommandText = @"SELECT * FROM Product WHERE ProductCat = 'Financial Scheme' ORDER BY ProductID";
			conn.Open();
			SqlDataReader reader = cmd.ExecuteReader();
			List<Product> productList = new List<Product>();
			while (reader.Read())
			{
				productList.Add(
					new Product
					{
						ID = reader.GetInt32(0),
						Title = reader.GetString(1),
						Image = !reader.IsDBNull(2) ? reader.GetString(2) : (string?)null,
						Desc = reader.GetString(3),
						Cat = reader.GetString(4),
						ObsoleteStatus = reader.GetString(5)
					}
					);
			}
			reader.Close();
			return productList; ;

		}

		public List<Product> GetNewProducts()
        {
			SqlCommand cmd = conn.CreateCommand();
			cmd.CommandText = @"SELECT * From Product WHERE Obsolete = 1";
			conn.Open();
			SqlDataReader reader = cmd.ExecuteReader();
			List<Product> productList = new List<Product>();
            while (reader.Read())
            {
				productList.Add(new Product
				{
					ID = reader.GetInt32(0),
					Title = reader.GetString(1),
					Image = reader.GetString(2),
					Desc = reader.GetString(3),
					Cat = reader.GetString(4),
					ObsoleteStatus = reader.GetString(5)
				}
				);
            }
			reader.Close();
			return productList;
		}
		public Product GetDetails(int productID)
        {
			Product product = new Product();
			SqlCommand cmd = conn.CreateCommand();
			cmd.CommandText = @"SELECT * FROM Product WHERE ProductID = @selectedProductID";
			cmd.Parameters.AddWithValue("@selectedProductID", productID);
			conn.Open();
			SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
					product.ID = productID;
					product.Title = reader.GetString(1);
					product.Image = reader.GetString(2);
					product.Desc = reader.GetString(3);
					product.Cat = reader.GetString(4);
					product.ObsoleteStatus = reader.GetString(5);
                }
            }
			reader.Close();
			conn.Close();
			return product;
        }
		public List<Product> GetProductByProductID(int id)
        {
			SqlCommand cmd = conn.CreateCommand();
			cmd.CommandText = @"SELECT * FROM Product WHERE ProductID = @selectedProductID";
			cmd.Parameters.AddWithValue("@selectedProductID", id);
			conn.Open();
			SqlDataReader reader = cmd.ExecuteReader();
			List<Product> productList = new List<Product>();
            while (reader.Read())
            {
				productList.Add(
					new Product
					{
						ID = reader.GetInt32(0),
						Title = reader.GetString(1),
						Image = reader.GetString(2),
						Desc = reader.GetString(3),
						Cat = reader.GetString(4),
						ObsoleteStatus = reader.GetString(5)
					}
				);
            }
			reader.Close();
			conn.Close();
			return productList;
        }
		public int AddProduct(Product product)
        {
			SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = @"INSERT INTO Product(ProductTitle,ProductImage,ProductDesc,ProductCat,Obsolete) OUTPUT INSERTED.ProductID VALUES(@productTitle,@productImage,@productDesc,@productCat,@obsolete)";

			cmd.Parameters.AddWithValue("@productTitle", product.Title);
			cmd.Parameters.AddWithValue("@productDesc", product.Desc);
			cmd.Parameters.AddWithValue("@productCat", product.Cat);
			cmd.Parameters.AddWithValue("@obsolete", product.ObsoleteStatus);
			if(product.Image == null)
            {
				cmd.Parameters.AddWithValue("@productImage", DBNull.Value);
            }
			conn.Open();
			product.ID = (int)cmd.ExecuteScalar();
			conn.Close();
			return product.ID;
        }
        public int Update(Product product)
        {
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = @"UPDATE Product SET ProductDesc=@desc,Obsolete=@obsolete WHERE ProductID = @selectedProductID";
			cmd.Parameters.AddWithValue("@desc",product.Desc);
			cmd.Parameters.AddWithValue("@obsolete", product.ObsoleteStatus);
			cmd.Parameters.AddWithValue("@selectedProductID",product.ID);
			conn.Open();
			int count = (int)(cmd.ExecuteNonQuery());
			conn.Close();
			return count;
        }
		public int Delete(int id)
        {
			SqlCommand cmd = conn.CreateCommand();
			cmd.CommandText = @"DELETE FROM Product WHERE ProductID = @selectedProductID";
			cmd.Parameters.AddWithValue("@selectedProductID", id);
			conn.Open();
			int rowAffected = 0;
			rowAffected += cmd.ExecuteNonQuery();
			conn.Close();
			return rowAffected;
        }

		
	}
}
