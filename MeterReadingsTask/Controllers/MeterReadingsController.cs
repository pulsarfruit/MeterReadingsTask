using Microsoft.AspNetCore.Mvc;
using MeterReadingsTask.Services.Interfaces;

namespace MeterReadingsTask.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MeterReadingController : ControllerBase
    {
        private readonly IMeterReadingService _meterReadingService;
        private readonly IUploadedFileValidator _uploadedFileValidator;

        public MeterReadingController(
            IMeterReadingService meterReadingService,
            IUploadedFileValidator uploadedFileValidator)
        {
            _meterReadingService = meterReadingService;
            _uploadedFileValidator = uploadedFileValidator;
        }

        [HttpPost("meter-reading-uploads")]
        public async Task<IActionResult> Post(IFormFile uploadedFile)
        {
            var fileError = _uploadedFileValidator.ValidateUploadedFile(uploadedFile);

            if (fileError != string.Empty)
            {
                return BadRequest(fileError);
            }

            var response = await _meterReadingService.ProcessMeterReadings(uploadedFile);

            return Ok(response);

        }
    }
}
