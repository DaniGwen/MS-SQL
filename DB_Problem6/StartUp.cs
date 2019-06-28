using DbAdvancedExercise;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Problem6
{
    class StartUp
    {
        static void Main(string[] args)
        {
            string countryInput = Console.ReadLine();

            using (SqlConnection connection = new SqlConnection(Connection.DbConnection))
            {
                connection.Open();

                int? countryId = GetCountryId(connection, countryInput);

                if (countryId == null)
                {
                    Console.WriteLine("No such Country.");
                    return;
                }

                var listOfTowns = new List<string>();
                listOfTowns = GetTowns(connection, countryId, listOfTowns);

                int rowsAffected = UpdateDb(connection, listOfTowns, countryId);

                Console.WriteLine($"{rowsAffected} town names were affected.");

                var sb = new StringBuilder();

                foreach (var town in listOfTowns)
                {
                    sb.Append(town + ',');
                }
                Console.WriteLine($"[{sb.ToString().TrimEnd(',')}]");
            }
        }

        private static int UpdateDb(SqlConnection connection, List<string> listOfTowns, int? countryId)
        {
            string updateQuery = "UPDATE Towns SET [Name] = UPPER([Name]) WHERE CountryCode = @countryId";
            int rowsAffected = 0;

            using (SqlCommand command = new SqlCommand(updateQuery, connection))
            {
                command.Parameters.AddWithValue("@countryId", countryId);
                rowsAffected = command.ExecuteNonQuery();
            }

            if (rowsAffected == 0)
            {
                Console.WriteLine("No town names were affected.");
            }

            return rowsAffected;
        }

        private static List<string> GetTowns(SqlConnection connection, int? countryId, List<string> listOfTowns)
        {
            string takeTownsQuery = "SELECT Name FROM Towns WHERE CountryCode = @countryId";

            using (SqlCommand command = new SqlCommand(takeTownsQuery, connection))
            {
                command.Parameters.AddWithValue("@countryId", countryId);
                SqlDataReader reader = command.ExecuteReader();

                using (reader)
                {
                    while (reader.Read())
                    {
                        listOfTowns.Add((string)reader[0]);
                    }
                }
            }

            return listOfTowns;
        }

        private static int? GetCountryId(SqlConnection connection, string countryInput)
        {
            string countryIdQuery = "SELECT * FROM Countries WHERE Name = @countryInput";
            int? countryId = 0;

            using (SqlCommand command = new SqlCommand(countryIdQuery, connection))
            {
                command.Parameters.AddWithValue("@countryInput", countryInput);
                countryId = (int?)command.ExecuteScalar();
            }

            return countryId;
        }
    }
}
