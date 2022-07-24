using MeterReadingsTask.Constants;
using MeterReadingsTask.Services.Interfaces;

namespace MeterReadingsTask.Services
{
    public class UploadedFileValidator: IUploadedFileValidator
    {
        public string ValidateUploadedFile(IFormFile uploadedFile)
        {
            if (uploadedFile == null || uploadedFile.Length == 0)
            {
                return MeterReadingConstants.MissingOrZeroLengthFile;
            }

            if (!uploadedFile.FileName.EndsWith(MeterReadingConstants.ExpectedFileType))
            {
                return MeterReadingConstants.UnsupportedFileType;
            }

            return string.Empty;
        }
    }
}
