

namespace DataGenerator.Model
{
    public class Meter
    {
        public int MID { get; set; }
        public int AID { get; set; }
        public double PowerUsed { get; set; }
        public DateTime Timestamp { get; set; }
        public int ReadingID { get; set; }

        public Meter()
        {
        }

        public Meter(int apartID, double powerUsed, DateTime timeStamp)
        {
            AID = apartID++;
            PowerUsed = powerUsed;
            Timestamp = timeStamp;
        }

        public void AddHourToTimestamp()
        {
            Timestamp = Timestamp.AddHours(1);

        }

        public void AddDayToTimestamp()
        {
            Timestamp = Timestamp.AddDays(1);

        }

        public void SetPowerUsed(double powerUsed)
        {
            Random random = new Random();
            PowerUsed = powerUsed + random.Next(10, 20) * 10.87;
        }
    }
}