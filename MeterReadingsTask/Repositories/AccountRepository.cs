using MeterReadingsTask.Data;
using MeterReadingsTask.Repositories.Interfaces;

namespace MeterReadingsTask.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly MeterReadingsTaskContext _context;

        public AccountRepository(MeterReadingsTaskContext context)
        {
            _context = context;
        }

        public async Task<bool> DoesAccountExist(int accountId)
        {
            return (await _context.Account.FindAsync(accountId) != null);
        }
    }

}
