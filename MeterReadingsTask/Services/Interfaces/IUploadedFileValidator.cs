namespace MeterReadingsTask.Services.Interfaces
{
    public interface IUploadedFileValidator
    {
        string ValidateUploadedFile(IFormFile uploadedFile);
    }
}
