﻿using Project3;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MortgageLibrary
{
    public class DatabaseIOHelper : IIOHelper
    {
        const string conString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Mortgages;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            
        public void AddMortgages(string formattedTempString)
        {
            string insertCommand = "insert into Mortgages values (@formattedString)";
            using (SqlConnection sqlConnection = new SqlConnection(conString))
            {
                SqlCommand sqlCommand = new SqlCommand(insertCommand, sqlConnection);
                sqlCommand.Parameters.Add("@formattedString", SqlDbType.NVarChar).Value = formattedTempString;

                sqlConnection.Open();

                int result = sqlCommand.ExecuteNonQuery();

                if (result != 1) throw new Exception("Unable to add Data to Database");

                sqlConnection.Close();
            }
            
            
        }

        public void ClearAllMortgages()
        {
            string conString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Mortgages;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            string deleteCommand = "delete from mortgages";

            using (SqlConnection sqlConnection = new SqlConnection(conString))
            {
                SqlCommand sqlCommand = new SqlCommand(deleteCommand, sqlConnection);
                sqlConnection.Open();

                //int result = sqlCommand.ExecuteNonQuery();

                sqlConnection.Close();
            }
        }

        public List<string> ListAllMortgages()
        {
            List<string> list = new List<string>();

            string conString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Mortgages;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            string selectCommand = "select * from mortgages";

            using (SqlConnection sqlConnection = new SqlConnection(conString))
            {
                SqlCommand sqlCommand = new SqlCommand(selectCommand, sqlConnection);
                sqlConnection.Open();

                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader(CommandBehavior.CloseConnection);
                if (sqlDataReader.HasRows)
                {
                    while(sqlDataReader.Read())
                    {
                        list.Add((string)sqlDataReader["Principle"]);
                    }
                    
                }
                sqlConnection.Close();
            }
            return list;
        }
    }
}
