using MeterReadingsTask.Models;

namespace MeterReadingsTask.Services.Interfaces
{
    public interface IMeterReadingService
    {
        Task<UploadMeterReadingsFileResponse> ProcessMeterReadings(IFormFile fileToProcess);
    }
}
