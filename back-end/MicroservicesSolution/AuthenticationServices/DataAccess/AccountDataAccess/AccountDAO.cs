using System.Drawing;
using BusinessObject.DataContext;
using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.AccountDataAccess
{
    public class AccountDAO
    {
        private AuthenticationContext context;
        public AccountDAO() => context = new AuthenticationContext();
        #region Singleton pattern
        private static volatile AccountDAO? Instance;
        private static readonly object LockObject = new object();
        public static AccountDAO GetInstance()
        {
            if (Instance == null)
            {
                lock (LockObject)
                {
                    if (Instance == null)
                    {
                        Instance = new AccountDAO();
                    }
                }
            }
            return Instance;
        }
        #endregion

        #region Find account by email
        public async Task<Account?> FindAccountByEmailAsync(string email)
        {
            try
            {
                using (var dbcontext = new AuthenticationContext())
                {
                    Account account = await context.Accounts.Where(x => x.Email == email).Include(x => x.Role).FirstOrDefaultAsync();
                    return account;
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region Add Account
        public async Task AddAccountAsync(Account account)
        {
            try
            {
                await context.Accounts.AddAsync(account);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }
        #endregion

        #region Find profile
        public async Task<Profile?> FindProfileByAccountAsync(int id)
        {
            var profile = new Profile();
            try
            {
                using (var dbcontext = new AuthenticationContext())
                {
                    profile = await context.Profiles.Where(x => x.AccountId == id).FirstOrDefaultAsync();
                }
            }
            catch (Exception ex)
            {
                
                throw new Exception(ex.Message);
            }
            return profile;
        }
        #endregion
    }
}
