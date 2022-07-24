using MeterReadingsTask.Models;
using MeterReadingsTask.Repositories.Interfaces;
using MeterReadingsTask.Services.Interfaces;

namespace MeterReadingsTask.Services
{
    public class MeterReadingService : IMeterReadingService
    {
        private readonly IFileConverter _fileConverter;
        private readonly IMeterReadingValidationService _meterReadingValidationService;
        private readonly IMeterReadingRepository _meterReadingRepository;

        public MeterReadingService(
            IFileConverter fileConverter,
            IMeterReadingValidationService meterReadingValidationService,
            IMeterReadingRepository meterReadingRepository)
        {
            _fileConverter = fileConverter;
            _meterReadingValidationService = meterReadingValidationService;
            _meterReadingRepository = meterReadingRepository;
        }

        public async Task<UploadMeterReadingsFileResponse> ProcessMeterReadings(IFormFile fileToProcess)
        {
            var meterReadings = _fileConverter.ConvertFileToMeterReadings(fileToProcess);
            var failedReadings = 0;
            var successfulReadings = 0;

            foreach (var meterReading in meterReadings)
            {
                if (await _meterReadingValidationService.IsValidReading(meterReading))
                {
                    await _meterReadingRepository.CreateMeterReading(meterReading);
                    successfulReadings++;
                }
                else
                {
                    failedReadings++;
                }
            }
            return new UploadMeterReadingsFileResponse
            {
                SuccessfulReadings = successfulReadings,
                FailedReadings = failedReadings
            };
        }
    }

}
