using System;
using System.Linq;
using System.Data.SqlClient;
using DbAdvancedExercise;

namespace AddMinion
{
    class StartUp
    {
        static void Main()
        {
            var minionInput = Console.ReadLine().Split(':', ' ');
            var villainInput = Console.ReadLine().Split(':', ' ');

            string minionName = minionInput[2];
            int age = int.Parse(minionInput[3]);
            string townName = minionInput[4];
            string villainName = villainInput[2];

            using (SqlConnection sqlConnection = new SqlConnection(Connection.DbConnection))
            {
                sqlConnection.Open();

                var townId = GetTownId(sqlConnection, townName);
                if (townId == null)
                {
                    AddTown(sqlConnection, townName);
                }
                townId = GetTownId(sqlConnection, townName);


                var villainId = GetVillainId(sqlConnection, villainName);
                if (villainId == null)
                {
                    AddVillain(sqlConnection, villainName);
                }
                villainId = GetVillainId(sqlConnection, villainName);


                var minionId = GetMinionId(sqlConnection, minionName, age, townId, villainName);
                if (minionId == null)
                {
                    AddMinion(sqlConnection, minionName, age, townId, villainName);
                }
                minionId = GetMinionId(sqlConnection, minionName, age, townId, villainName);


                AddMinionVillains(sqlConnection, villainId, minionId, minionName, villainName);
            }
        }

        private static int? GetVillainId(SqlConnection sqlConnection, string villainName)
        {
            string villainQuery = "SELECT Id FROM dbo.Villains WHERE Name = @villainName";
            int? villainId = 0;

            using (SqlCommand command = new SqlCommand(villainQuery, sqlConnection))
            {
                command.Parameters.AddWithValue("@villainName", villainName);

                villainId = (int?)command.ExecuteScalar();
            }

            return villainId;
        }

        private static int? GetMinionId(SqlConnection sqlConnection, string minionName, int age, int? townId, string villainName)
        {
            string minionQuery = "SELECT Id FROM dbo.Minions WHERE Name = @minionName";
            int? minionId = 0;

            using (SqlCommand command = new SqlCommand(minionQuery, sqlConnection))
            {
                command.Parameters.AddWithValue("@minionName", minionName);

                minionId = (int?)command.ExecuteScalar();
            }

            return minionId;
        }

        private static int? GetTownId(SqlConnection sqlConnection, string townName)
        {
            string townQuery = "SELECT Id FROM dbo.Towns WHERE Name = @town";
            int? townId = 0;

            using (SqlCommand sqlCommand = new SqlCommand(townQuery, sqlConnection))
            {
                sqlCommand.Parameters.AddWithValue("@town", townName);

                townId = (int?)sqlCommand.ExecuteScalar();
            }

            return townId;
        }

        private static void AddMinionVillains(SqlConnection sqlConnection, int? villainId, int? minionId, string minionName, string villainName)
        {
            string addMinionVillainsQuery = "INSERT INTO dbo.MinionsVillains (MinionId,VillainId)" +
                                             "VALUES (@minionId, @villainId)";

            using (SqlCommand command = new SqlCommand(addMinionVillainsQuery, sqlConnection))
            {
                command.Parameters.AddWithValue("@minionId", minionId);
                command.Parameters.AddWithValue("@villainId", villainId);
                command.ExecuteNonQuery();
            }

            Console.WriteLine("Successfully added {0} to be minion of {1}.", minionName, villainName);
        }

        private static void AddVillain(SqlConnection sqlConnection, string villainName)
        {
            string addVillain = $@"INSERT INTO dbo.Villains (Name,EvilnessFactorId)
                                   VALUES(@villainName, @evilness)";

            using (SqlCommand command = new SqlCommand(addVillain, sqlConnection))
            {
                command.Parameters.AddWithValue("@villainName", villainName);
                command.Parameters.AddWithValue("@evilness", 4);
                command.ExecuteNonQuery();

                Console.WriteLine("Villain {0} was added to the database.", villainName);
            }
        }

        private static void AddMinion(SqlConnection sqlConnection, string minionName, int? age, int? townId, string villainName)
        {
            string addMinion = "INSERT INTO dbo.Minions (Name,Age,TownId)" +
                                $"VALUES (@minionName, @age, @townId)";

            using (SqlCommand command = new SqlCommand(addMinion, sqlConnection))
            {
                command.Parameters.AddWithValue("@minionName", minionName);
                command.Parameters.AddWithValue("@age", age);
                command.Parameters.AddWithValue("@townId", townId);
                command.ExecuteNonQuery();
            }
        }

        private static void AddTown(SqlConnection connection, string town)
        {
            using (connection)
            {
                string addTownToDb = $@"INSERT INTO dbo.Towns (Name)
                                       VALUES(@townName)";

                using (SqlCommand command = new SqlCommand(addTownToDb, connection))
                {
                    command.Parameters.AddWithValue("@townName", town);
                    command.ExecuteNonQuery();
                    Console.WriteLine("Town {0} was added to the database.", town);
                }
            }
        }
    }
}
