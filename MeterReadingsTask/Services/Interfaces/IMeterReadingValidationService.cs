using MeterReadingsTask.Entities;

namespace MeterReadingsTask.Services.Interfaces
{
    public interface IMeterReadingValidationService
    {
        Task<bool> IsValidReading(MeterReading readingToValidate);
    }
}
