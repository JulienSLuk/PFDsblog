using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Data.SqlClient;
using WEB2022_ZZFashion.Models;
using System.Linq;

namespace WEB2022_ZZFashion.DAL
{
	public class CashVoucherDAL
	{
		private IConfiguration Configuration { get; }
		private SqlConnection conn;
		public CashVoucherDAL()
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

		public List<SPCashVoucherViewModel> pVoucher(string memberID)
		{
			List<SPCashVoucherViewModel> sPCashVouchers = new List<SPCashVoucherViewModel>();

			SqlCommand cmd = conn.CreateCommand();

			cmd.CommandText = @"SELECT IssuingID
									  ,MonthIssuedFor
									  ,YearIssuedFor
									  ,Amount
								  FROM Customer INNER JOIN CashVoucher ON Customer.MemberID = CashVoucher.MemberID
								  WHERE Customer.MemberID = @selectedMemberID AND Status = 0";

			cmd.Parameters.AddWithValue("@selectedMemberID", memberID);

			conn.Open();

			try {

				SqlDataReader reader = cmd.ExecuteReader();


				if (reader.HasRows)
				{
					//Read the record from database
					while (reader.Read())
					{
						SPCashVoucherViewModel spcashVoucher = new SPCashVoucherViewModel
						{
							IssuingID = reader.GetInt32(0),
							Month = reader.GetInt32(1),
							Year = reader.GetInt32(2),
							Amount = reader.GetDecimal(3)
						};
						sPCashVouchers.Add(spcashVoucher);
					}
				}

				reader.Close();

				
			}
			catch (Exception ex) {
				sPCashVouchers = new List<SPCashVoucherViewModel>();
			}
			
			conn.Close();

			return sPCashVouchers;
		}

		//check if serial number can be used (not duplicate)
		public bool checkSN(string voucherSN)
		{
			bool checker = true;
			SqlCommand cmd = conn.CreateCommand();

			cmd.CommandText = @"SELECT IssuingID
								  FROM CashVoucher
								  WHERE VoucherSN = @selectedvoucherSN";

			cmd.Parameters.AddWithValue("@selectedvoucherSN", voucherSN);

			conn.Open();

			SqlDataReader reader = cmd.ExecuteReader();
			if (reader.HasRows)
			{
				checker = false;
			}
			else
			{
				checker = true;
			}

			reader.Close();
			conn.Close();
			return checker;
		}

		public void updatePendingVoucher(int IssuingID, string voucherSN)
		{
			SqlCommand cmd = conn.CreateCommand();
			string update = @$"UPDATE CashVoucher SET Status = 1, VoucherSN = '{voucherSN}' WHERE IssuingID = '{IssuingID}'";
			cmd.CommandText = update;
			conn.Open();
			cmd.ExecuteNonQuery();
			conn.Close();
		}

		//check if voucher is redeemable
		public bool checkStatus(string voucherSN)
		{
			//Create a SqlCommand object from connection object
			SqlCommand cmd = conn.CreateCommand();

			//Specify the SELECT SQL statement that
			//retrieves all attributes of a staff record.
			cmd.CommandText = @"SELECT IssuingID FROM CashVoucher
				WHERE VoucherSN = @selectedvoucherSN and Status = 1 and DATEDIFF(year, GETDATE(), DateTimeIssued) < 1";

			cmd.Parameters.AddWithValue("@selectedvoucherSN", voucherSN);


			//Open a database connection
			conn.Open();
			//Execute SELCT SQL through a DataReader
			SqlDataReader reader = cmd.ExecuteReader();


			if (reader.HasRows)
			{
				reader.Close();
				//Close database connection
				conn.Close();
				return true;
			}
			//Close data reader
			reader.Close();
			//Close database connection
			conn.Close();
			return false;
		}

		public void updateRedeemVoucher(string voucherSN)
		{
			SqlCommand cmd = conn.CreateCommand();
			string update = @$"UPDATE CashVoucher SET Status = 2, DateTimeRedeemed = GETDATE() WHERE VoucherSN = '{voucherSN}'";
			cmd.CommandText = update;
			conn.Open();
			cmd.ExecuteNonQuery();
			conn.Close();
		}


	}
}
