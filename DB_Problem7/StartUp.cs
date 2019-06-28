using DbAdvancedExercise;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Problem8
{
    class StartUp
    {
        static void Main(string[] args)
        {
            var inputIds = Console.ReadLine().Split().Select(int.Parse).ToArray();
            var minionDict = new Dictionary<string, int>();

            using (SqlConnection connection = new SqlConnection(Connection.DbConnection))
            {
                connection.Open();
                string queryName = @"UPDATE Minions SET [Name]=UPPER(LEFT([Name],1))
                                    +LOWER(SUBSTRING([Name],2,LEN([Name])))
                                    WHERE Id = @IdName";

                string queryAge = @"UPDATE Minions SET [Age]=Age + 1
                                    WHERE Id = @IdAge";

                string queryMinions = "SELECT Name, Age FROM Minions WHERE Id = @id";

                UpdateNames(connection, queryName, inputIds);
                UpdateAge(connection, queryAge, inputIds);
                minionDict = AddMinionsToDict(connection, queryMinions, minionDict, inputIds);

                foreach (var minion in minionDict)
                {
                    Console.WriteLine($"{minion.Key}  {minion.Value}");
                }

            }
        }

        private static Dictionary<string, int> AddMinionsToDict(SqlConnection connection, string queryMinions, Dictionary<string, int> minionDict, int[] inputIds)
        {
            foreach (var id in inputIds)
            {
                using (SqlCommand command = new SqlCommand(queryMinions, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    SqlDataReader reader = command.ExecuteReader();
                    using (reader)
                    {
                        while (reader.Read())
                        {
                            string name = (string)reader[0];
                            int age = (int)reader[1];

                            if (!minionDict.ContainsKey(name))
                            {
                                minionDict.Add(name, age);
                            }
                            else
                            {
                                throw new Exception("Minion already exist");
                            }
                        }
                    }
                }
            }


            return minionDict;
        }

        private static void UpdateNames(SqlConnection connection, string queryName, int[] inputIds)
        {
            foreach (var id in inputIds)
            {
                using (SqlCommand command = new SqlCommand(queryName, connection))
                {
                    command.Parameters.AddWithValue("@IdName", id);
                    command.ExecuteNonQuery();
                }
            }

        }

        private static void UpdateAge(SqlConnection connection, string queryAge, int[] inputIds)
        {
            foreach (var id in inputIds)
            {
                using (SqlCommand command = new SqlCommand(queryAge, connection))
                {
                    command.Parameters.AddWithValue("@IdAge", id);
                    command.ExecuteNonQuery();
                }
            }

        }
    }
}
