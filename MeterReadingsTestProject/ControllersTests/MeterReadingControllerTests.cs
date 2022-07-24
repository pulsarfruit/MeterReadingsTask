using MeterReadingsTask.Controllers;
using MeterReadingsTask.Models;
using MeterReadingsTask.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;

namespace MeterReadingsTestProject.ServicesTests
{
    public class MeterReadingControllerTests
    {
        private readonly MeterReadingController SUT;
        private readonly IMeterReadingService _meterReadingService = Substitute.For<IMeterReadingService>();
        private readonly IUploadedFileValidator _uploadedFileValidator = Substitute.For<IUploadedFileValidator>();
        private readonly IFormFile _formFile = Substitute.For<IFormFile>();

        public MeterReadingControllerTests()
        {
            SUT = new MeterReadingController(_meterReadingService, _uploadedFileValidator);
        }

        [TestCase]
        public void WhenPostIsCalledAndFileValidationFails_ThenCorrectErrorMessageIsReturned()
        {
            var errorString = "Test Error Message";
            _uploadedFileValidator.ValidateUploadedFile(Arg.Any<IFormFile>()).Returns(errorString);

            var response = SUT.Post(_formFile).Result as BadRequestObjectResult;
            Assert.That(errorString, Is.EqualTo(response.Value));
        }

        [TestCase]
        public void WhenPostIsCalledAndFileValidationPasses_ThenCorrectResponseIsReturned()
        {
            var successResponse = new UploadMeterReadingsFileResponse
            {
                SuccessfulReadings = 1,
                FailedReadings = 2
            };

            _uploadedFileValidator.ValidateUploadedFile(Arg.Any<IFormFile>()).Returns(string.Empty);
            _meterReadingService.ProcessMeterReadings(Arg.Any<IFormFile>()).Returns(successResponse);
            var response = SUT.Post(_formFile).Result as OkObjectResult;
            Assert.That(successResponse, Is.EqualTo(response.Value));
        }
    }
}