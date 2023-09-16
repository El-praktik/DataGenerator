using DataGenerator.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

string connectionString = @"data source=BLADE;initial catalog=ElPraktik;trusted_connection=true;Encrypt=False;";
using SqlConnection connection = new SqlConnection(connectionString);
connection.Open();
string insertQuery = $"INSERT INTO Measure (AID, MID, PowerUsed, Timestamp) VALUES (@AID, @MID, @PowerUsed, @Timestamp)";

DateTime today = DateTime.Now;
DateTime meterTime = new DateTime(today.Year, today.Month, 1, 0, 0, 0);
DateTime daysOfLastMonth = meterTime.AddMonths(1).AddDays(-1);
int totalDays = daysOfLastMonth.Day;
int calculatedMonths = totalDays * 12;
int totalApartments = 18;

Stopwatch stopwatch = new Stopwatch();
stopwatch.Start();

int mid = 0; // Initialize MID outside the loop

for (int apartmentId = 1; apartmentId <= totalApartments; apartmentId++)
{
    int currentApartmentId = apartmentId;

    // Initialize MID for each apartment
    mid++;

    List<Meter> meters = new List<Meter>();
    for (int j = 1; j <= totalDays * 24; j++)
    {
        meters.Add(new Meter { AID = currentApartmentId, MID = mid, Timestamp = meterTime.AddHours(j - 1) });
    }

    foreach (Meter meter in meters)
    {
        meter.AddHourToTimestamp();
        meter.SetPowerUsed(20);

        using (SqlCommand command = new SqlCommand(insertQuery, connection))
        {
            command.Parameters.Add("@MID", SqlDbType.Int).Value = meter.MID;
            command.Parameters.Add("@AID", SqlDbType.Int).Value = meter.AID;
            command.Parameters.Add("@PowerUsed", SqlDbType.Float).Value = meter.PowerUsed;
            command.Parameters.Add("@Timestamp", SqlDbType.DateTime).Value = meter.Timestamp;

            int rowsAffected = command.ExecuteNonQuery();
            Console.WriteLine($"{rowsAffected} row(s) inserted for MID: {meter.MID}, AID: {meter.AID}, hour {meter.Timestamp}");
        }
    }
}

stopwatch.Stop();
connection.Close();

Console.WriteLine($"Time taken: {stopwatch.ElapsedMilliseconds} ms");
Console.WriteLine($"Time taken: {stopwatch.ElapsedMilliseconds / 1000} s");
Console.WriteLine($"Time taken: {stopwatch.ElapsedMilliseconds / 1000 / 60} min");
Console.ReadKey();


//Random random = new Random();

//List<Meter> meters = new List<Meter>();

//int apartID = 1;
//for (int i = 1; i <= totalDays * 24; i++)
//{
//    meters.Add(new Meter {, AID = , Timestamp = meterTime.AddHours(i - 1) });
//}

//// Iterate through each meter
//foreach (Meter meter in meters)
//{
//    for (int hour = 1; hour <= 24; hour++)
//    {
//        // Generate data for each hour
//        meter.AddHourToTimestamp();
//        meter.SetPowerUsed(20);

//        // Create and execute the SQL command for inserting data
//        using SqlCommand command = new SqlCommand(insertQuery, connection);
//        command.Parameters.Add(@"AID", SqlDbType.Int).Value = meter.AID;
//        command.Parameters.Add("@PowerUsed", SqlDbType.Float).Value = meter.PowerUsed;
//        command.Parameters.Add("@Timestamp", SqlDbType.DateTime).Value = meter.Timestamp;

//        int rowsAffected = command.ExecuteNonQuery();
//        Console.WriteLine($"{rowsAffected} row(s) inserted for MID: {meter.MID}, Hour: {hour}");
//    }
//}

//connection.Close();

//Console.ReadKey();