namespace MeterReadingsTask.Repositories.Interfaces
{
    public interface IAccountRepository
    {
        Task<bool> DoesAccountExist(int accountId);
    }
}
