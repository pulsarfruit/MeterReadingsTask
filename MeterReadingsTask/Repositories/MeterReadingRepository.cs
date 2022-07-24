using MeterReadingsTask.Data;
using MeterReadingsTask.Entities;
using MeterReadingsTask.Repositories.Interfaces;

namespace MeterReadingsTask.Repositories
{
    public class MeterReadingRepository : IMeterReadingRepository
    {
        private readonly MeterReadingsTaskContext _context;

        public MeterReadingRepository(MeterReadingsTaskContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateMeterReading(MeterReading meterReading)
        {
            await _context.MeterReading.AddAsync(meterReading);
            return (await _context.SaveChangesAsync() == 1);
        }

        public bool DoesMeterReadingExist(MeterReading meterReading)
        {
            return (_context.MeterReading?.Any(e => (
            (e.Value == meterReading.Value) &&
            (e.ReadingDateTime == meterReading.ReadingDateTime) &&
            (e.AccountId == meterReading.AccountId))
            )).GetValueOrDefault();
        }
    }
}
