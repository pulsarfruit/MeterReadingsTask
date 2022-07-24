using MeterReadingsTask.Entities;

namespace MeterReadingsTask.Services.Interfaces
{
    public interface IFileConverter
    {
        List<MeterReading> ConvertFileToMeterReadings(IFormFile fileToConvert);
    }
}
