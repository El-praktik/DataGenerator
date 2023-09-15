using DataGenerator.Model;
using System.Data;
using System.Data.SqlClient;

// See https://aka.ms/new-console-template for more information


// Simply uncomment whichever connection string you want to use
string connectionString = @"data source=BLADE;initial catalog=ElPraktik;trusted_connection=true;Encrypt=False;";
// string connectionString = "";    PROVIDE YOUR OWN CONNECTION STRING HERE
// string connectionString = "";    PROVIDE YOUR OWN CONNECTION STRING HERE

using SqlConnection connection = new SqlConnection(connectionString);
connection.Open();

string insertQuery = $"INSERT INTO Measure (AID, PowerUsed, Timestamp) VALUES (@AID, @PowerUsed, @Timestamp)";

Random random = new Random();

// Create a list of ten meters
List<Meter> meters = new List<Meter>();
DateTime Today = DateTime.Now;
DateTime meterTime = new DateTime(Today.Year, Today.Month, 1, 0,0,0);
DateTime DaysOfLastMonth = meterTime.AddMonths(1).AddDays(-1);
int totalDays = DaysOfLastMonth.Day;

//Console.WriteLine(meterTime);
//Console.ReadKey();

int apartID = 1;
for (int i = 1; i <= totalDays * 24; i++)
{
    meters.Add(new Meter { MID = i, AID = 2, Timestamp = meterTime.AddHours(i-1)});
}

// Iterate through each meter
foreach (Meter meter in meters)
{
    for (int hour = 1; hour <= 24; hour++)
    {
        // Generate data for each hour
        meter.AddHourToTimestamp();
        meter.SetPowerUsed(20);

        // Create and execute the SQL command for inserting data
        using SqlCommand command = new SqlCommand(insertQuery, connection);
        command.Parameters.Add(@"AID", SqlDbType.Int).Value = meter.AID;
        command.Parameters.Add("@PowerUsed", SqlDbType.Float).Value = meter.PowerUsed;
        command.Parameters.Add("@Timestamp", SqlDbType.DateTime).Value = meter.Timestamp;

        int rowsAffected = command.ExecuteNonQuery();
        Console.WriteLine($"{rowsAffected} row(s) inserted for MID: {meter.MID}, Hour: {hour}");
    }
}

connection.Close();

Console.ReadKey();