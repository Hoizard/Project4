using Project3;
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
            
        public void AddMortgages(string formattedTempString, string formattedPrinciple, string formattedYears, string formattedRate)
        {
            string insertCommand = "insert into Mortgages (Principle, InterestRate, DurationYears, MonthlyPayment) values  (@principleAmount, @rateValue, @otherYears, @resultPayment)";
            //string insertCommand = "insert into Mortgages (Principle, InterestRate, DurationYears, MonthlyPayment) values ('" + PrincipleAmount.Text + "', '" + DropDownList1.SelectedItem.Text + "', '" + OtherYears.Text + "', '" + ResultPayment.Text + "',) (@formattedString)";
            using (SqlConnection sqlConnection = new SqlConnection(conString))
            {
                SqlCommand sqlCommand = new SqlCommand(insertCommand, sqlConnection);
                sqlCommand.Parameters.Add("@principleAmount", SqlDbType.NVarChar).Value = formattedPrinciple;
                sqlCommand.Parameters.Add("@rateValue", SqlDbType.NVarChar).Value = formattedRate;
                sqlCommand.Parameters.Add("@otherYears", SqlDbType.NVarChar).Value = formattedYears;
                sqlCommand.Parameters.Add("@resultPayment", SqlDbType.NVarChar).Value = formattedTempString;

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

                int result = sqlCommand.ExecuteNonQuery();

                sqlConnection.Close();
            }
        }

        public DataTable ListAllMortgages()
        {
            List<string> list = new List<string>();

            string conString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Mortgages;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            string selectCommand = "select * from mortgages";
            DataTable dt;
            dt = new DataTable();

            using (SqlConnection sqlConnection = new SqlConnection(conString))
            {
                SqlCommand sqlCommand = new SqlCommand(selectCommand, sqlConnection);
                sqlConnection.Open();

                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader(CommandBehavior.CloseConnection);
                if (sqlDataReader.HasRows)
                {
                    dt.Load(sqlDataReader);
                    //while(sqlDataReader.Read())
                    //{
                    //    dt.Load(sqlDataReader);
                    //    // Principle ,InterestRate, DurationYears, MonthlyPayment
                    //    //list.Add((string)sqlDataReader["Principle"]);
                    //    //list.Add((string)sqlDataReader["DurationYears"]);
                    //    //list.Add((string)sqlDataReader["MonthlyPayment"];
      
                    //}
                    
                }
                sqlConnection.Close();
            }
            return dt;
        }
    }
}
