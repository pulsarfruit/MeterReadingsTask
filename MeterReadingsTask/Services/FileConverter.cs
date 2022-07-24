using MeterReadingsTask.Entities;
using MeterReadingsTask.Services.Interfaces;

namespace MeterReadingsTask.Services
{
    public class FileConverter : IFileConverter
    {
        public List<MeterReading> ConvertFileToMeterReadings(IFormFile fileToConvert)
        {
            var readings = new List<MeterReading>();
            using (var reader = new StreamReader(fileToConvert.OpenReadStream()))
            {
                reader.ReadLine(); // Header

                while (reader.Peek() >= 0)
                {
                    var line = reader.ReadLine().Split(',');
                    readings.Add(
                        new MeterReading
                        {
                            AccountId = int.Parse(line[0]),
                            ReadingDateTime = DateTime.Parse(line[1]),
                            Value = line[2]
                        });
                }
            }

            return readings;
        }
    }
}
