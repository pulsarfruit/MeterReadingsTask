using MeterReadingsTask.Entities;
using MeterReadingsTask.Repositories.Interfaces;
using MeterReadingsTask.Services.Interfaces;

namespace MeterReadingsTask.Services
{
    public class MeterReadingValidationService : IMeterReadingValidationService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IMeterReadingRepository _meterReadingRepository;

        public MeterReadingValidationService(
            IAccountRepository accountRepository, IMeterReadingRepository meterReadingRepository)
        {
            _accountRepository = accountRepository;
            _meterReadingRepository = meterReadingRepository;
        }

        public async Task<bool> IsValidReading(MeterReading readingToValidate)
        {
            return (
                await IsAccountValid(readingToValidate.AccountId) &&
                IsValueInCorrectFormat(readingToValidate.Value) &&
                ReadingDoesNotExist(readingToValidate));
        }

        private async Task<bool> IsAccountValid(int accountId)
        {
            return (await _accountRepository.DoesAccountExist(accountId));
        }

        private static bool IsValueInCorrectFormat(string reading)
        {
            var canParse = int.TryParse(reading, out var value);
            if (canParse && value >0 && reading.Length == 5)
            {
                return true;
            }
            return false;
        }
       private bool ReadingDoesNotExist(MeterReading meterReading)
        {
            return (!_meterReadingRepository.DoesMeterReadingExist(meterReading));
        }
}

    }
