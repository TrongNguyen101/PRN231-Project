using BusinessObject.DataTransfer;
using BusinessObject.Models;
using DataAccess.AccountDataAccess;
using Repositories.HashAlgorithmRepository;

namespace Repositories.AccountRepository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly IHashAlgorithmRepository hashAlgorithm;
        private AccountDTO MapToDto(Account account)
        {
            return new AccountDTO
            {
                Email = account.Email,
                Password = account.Password,
                RoleId = account.RoleId,
            };
        }

        public async Task<AccountDTO?> FindAccount(string email)
        {
            var account = await AccountDAO.GetInstance().FindAccountByEmailAsync(email);
            if (account == null)
            {
                return null;
            }
            return MapToDto(account);
        }

        public async Task CreateAccount(AccountDTO accountDTO)
        {
            Account account = new Account
            {
                Email = accountDTO.Email,
                Password = accountDTO.Password,
                RoleId = accountDTO.RoleId,
            };
            await AccountDAO.GetInstance().AddAccountAsync(account);
        }
    }
}
