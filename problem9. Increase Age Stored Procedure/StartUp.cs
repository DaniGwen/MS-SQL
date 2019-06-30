using DbAdvancedExercise;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace problem9._Increase_Age_Stored_Procedure
{
    class StartUp
    {
        static void Main()
        {
            int minionId = int.Parse(Console.ReadLine());
            var minionDict = new Dictionary<string, int>();

            using (SqlConnection connection = new SqlConnection(Connection.DbConnection))
            {
                connection.Open();

                string query = "EXECUTE usp_GetOlder @minionId, @result";
                string result = "OUTPUT";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@minionId", minionId);
                    command.Parameters.AddWithValue("@result", result);
                    var rowsAffected = (string)command.ExecuteScalar();
                    Console.WriteLine(rowsAffected);
                }
            }
        }
    }
}
