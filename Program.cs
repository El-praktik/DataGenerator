using DataGenerator.Model;
using System.Data;
using System.Data.SqlClient;

// See https://aka.ms/new-console-template for more information

string connectionString = "CONNECTION STRING";
// string connectionString = "";
// string connectionString = "";

using SqlConnection connection = new SqlConnection(connectionString);
connection.Open();

string insertQuery = "INSERT INTO Measures (MID, PowerUsed, PowerGenerated, CID, TimestampFrom, TimestampTo) VALUES (@MeterId, @PowerUsed, @PowerGenerated, @CommunityId, @TimestampFrom, @TimestampTo)";

Random random = new Random();

// Create a list of ten meters
List<Meter> meters = new List<Meter>();
for (int i = 1; i <= 10; i++)
{
    meters.Add(new Meter { MID = i });
}

// Iterate through each meter
foreach (Meter meter in meters)
{
    for (int hour = 1; hour <= 24; hour++)
    {
        // Generate data for each hour
        meter.AddHourToTimestamp();
        meter.SetPowerUsed(20);
        meter.SetPowerGenerated(20);

        // Create and execute the SQL command for inserting data
        using SqlCommand command = new SqlCommand(insertQuery, connection);
        command.Parameters.Add("@MeterId", SqlDbType.Int).Value = meter.MID;
        command.Parameters.Add("@PowerUsed", SqlDbType.Float).Value = meter.PowerUsed;
        command.Parameters.Add("@PowerGenerated", SqlDbType.Float).Value = meter.PowerGenerated;
        command.Parameters.Add("@CommunityId", SqlDbType.Int).Value = meter.CID;
        command.Parameters.Add("@Timestamp", SqlDbType.DateTime).Value = meter.Timestamp;

        int rowsAffected = command.ExecuteNonQuery();
        Console.WriteLine($"{rowsAffected} row(s) inserted for MeterId: {meter.MID}, Hour: {hour}");
    }
}

connection.Close();