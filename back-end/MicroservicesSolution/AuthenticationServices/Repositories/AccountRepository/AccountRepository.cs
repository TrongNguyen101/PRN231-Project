using BusinessObject.DataTransfer;
using BusinessObject.Models;
using DataAccess.AccountDataAccess;
using Repositories.HashAlgorithmRepository;

namespace Repositories.AccountRepository
{
    public class AccountRepository : IAccountRepository
    {
        private AccountDTO MapToAccountDto(Account account)
        {
            return new AccountDTO
            {
                Id = account.AcountId,
                Email = account.Email,
                Password = account.Password,
                RoleId = account.RoleId,
                Role = new RoleDTO
                {
                    RoleName = account.Role.RoleName
                }
            };
        }

        private ProfileDTO MapToProfileDto(Profile profile)
        {
            return new ProfileDTO
            {
                Code = profile.Code,
                FirtName = profile.FirtName,
                MiddleName = profile.MiddleName,
                LastName = profile.LastName,
                GenderId = profile.GenderId,
                Birthday = profile.Birthday,
                MajorId = profile.MajorId,
            };
        }

        public async Task<AccountDTO?> FindAccount(string email)
        {
            var account = await AccountDAO.GetInstance().FindAccountByEmailAsync(email);
            if (account == null)
            {
                return null;
            }
            return MapToAccountDto(account);
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

        public async Task<ProfileDTO> GetProfile(int id)
        {
            var account = await AccountDAO.GetInstance().FindProfileByAccountAsync(id);
            return MapToProfileDto(account);
        }
    }
}
