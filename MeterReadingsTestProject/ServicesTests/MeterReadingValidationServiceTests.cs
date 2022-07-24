using MeterReadingsTask.Entities;
using MeterReadingsTask.Repositories.Interfaces;
using MeterReadingsTask.Services;
using NSubstitute;

namespace MeterReadingsTestProject.ServicesTests
{
    [TestFixture(1234, "22/04/2019 9:24", "12345", false, true)]
    [TestFixture(5678, "22/04/2019 9:24", "12345", false, false)]
    [TestFixture(1234, "22/04/2019 9:24", "12345", true, false)]
    [TestFixture(1234, "22/04/2019 9:24", "2345", false, false)]
    [TestFixture(1234, "22/04/2019 9:24", "VOID", false, false)]
    public class MeterReadingValidationServiceTests
    {
        private readonly MeterReadingValidationService SUT;
        private readonly IAccountRepository _accountRepository = Substitute.For<IAccountRepository>();
        private readonly IMeterReadingRepository _meterReadingRepository = Substitute.For<IMeterReadingRepository>();
        private readonly MeterReading _testMeterReading;
        private readonly bool _expectedResult;

        public MeterReadingValidationServiceTests(
            int accountId,
            string readingDateTime,
            string value,
            bool meterReadingExists,
            bool expectedResult
            )
        {
            SUT = new MeterReadingValidationService(
                _accountRepository,
                _meterReadingRepository);

            _accountRepository.DoesAccountExist(1234).Returns(true);
            _accountRepository.DoesAccountExist(5678).Returns(false);
            _meterReadingRepository.DoesMeterReadingExist(Arg.Any<MeterReading>()).Returns(meterReadingExists);

            _testMeterReading = new MeterReading
            {
                AccountId = accountId,
                ReadingDateTime = DateTime.Parse(readingDateTime),
                Value = value
            };
            _expectedResult = expectedResult;
        }

        [TestCase]
        public void WhenIsValidReadingIsCalled_ThenCorrectResultIsReturned()
        {
            Assert.That(_expectedResult, Is.EqualTo(SUT.IsValidReading(_testMeterReading).Result));
        }
    }
}