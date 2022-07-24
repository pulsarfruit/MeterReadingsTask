using MeterReadingsTask.Entities;

namespace MeterReadingsTask.Repositories.Interfaces
{
    public interface IMeterReadingRepository
    {
        Task<bool> CreateMeterReading(MeterReading meterReading);
        bool DoesMeterReadingExist(MeterReading meterReading);
    }
}
