namespace MeterReadingsTask.Entities
{
    public class MeterReading
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public DateTime ReadingDateTime { get; set; }
        public string Value { get; set; }
    }
}
