using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class Student
    {


        public int IdStudent { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string IndexNumber { get; set; }

        public void AddStudent()
        {
            string connetionString = null;
            string sql = null;

            // All the info required to reach your db. See connectionstrings.com
            connetionString = "Data Source = localhost,1433; Initial Catalog = master; User ID = sa; Password = Mssql1234!";

            // Prepare a proper parameterized query 
            sql = "insert into Students (IdStudent, FirstName, LastName, IndexNumber ) values (@IdStudent, @FirstName, @LastName, @IndexNumber)";

            // Create the connection (and be sure to dispose it at the end)
            using (SqlConnection cnn = new SqlConnection(connetionString))
            {
                cnn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, cnn))
                {
                    cmd.Parameters.Add("@IdStudent", SqlDbType.Int).Value = IdStudent;
                    cmd.Parameters.Add("@FirstName", SqlDbType.NVarChar).Value = FirstName;
                    cmd.Parameters.Add("@LastName", SqlDbType.NVarChar).Value = LastName;
                    cmd.Parameters.Add("@IndexNumber", SqlDbType.NVarChar).Value = IndexNumber;
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
