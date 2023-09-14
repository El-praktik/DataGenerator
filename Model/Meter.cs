

namespace DataGenerator.Model
{
    public class Meter
    {
        private static int id = 1;

        public int MID { get; set; }
        public double PowerUsed { get; set; }
        public double PowerGenerated { get; set; }
        public int CID { get; set; }
        public DateTime Timestamp { get; set; }

        public Meter()
        {
            CID = 1;
            MID = id++;
        }

        public Meter(int meterId, double powerUsed, double powerGenerated, int communityId, DateTime timeStamp)
        {
            MID = meterId;
            PowerUsed = powerUsed;
            PowerGenerated = powerGenerated;
            CID = communityId;
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

        public void SetPowerGenerated(double powerGenerated)
        {
            Random random = new Random();
            PowerGenerated = powerGenerated + random.Next(1, 7) * 51.87;
        }
    }
}