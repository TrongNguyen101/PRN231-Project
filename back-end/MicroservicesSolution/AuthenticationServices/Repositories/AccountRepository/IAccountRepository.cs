using BusinessObject.DataTransfer;

namespace Repositories.AccountRepository
{
    public interface IAccountRepository
    {
        Task<AccountDTO> FindAccount(string email);
        Task CreateAccount(AccountDTO account);
    }
}
