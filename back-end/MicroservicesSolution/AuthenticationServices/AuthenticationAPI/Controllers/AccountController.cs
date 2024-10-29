using AuthenticationAPI.JWTProvider;
using BusinessObject.DataContext;
using BusinessObject.DataTransfer;
using BusinessObject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repositories.AccountRepository;
using Repositories.HashAlgorithmRepository;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AuthenticationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository accountRepository;
        private readonly JwtTokenGenerator jwtTokenGenerator;
        private readonly IHashAlgorithmRepository hashAlgorithm;
        public AccountController(
            IAccountRepository accountRepository,
            JwtTokenGenerator jwtTokenGenerator,
            IHashAlgorithmRepository hashAlgorithm)
        {
            this.accountRepository = accountRepository;
            this.jwtTokenGenerator = jwtTokenGenerator;
            this.hashAlgorithm = hashAlgorithm;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(string email, string password)
        {
            var account = await accountRepository.FindAccount(email);
            if (account == null)
            {
                return BadRequest("This user is not exist");
            }
            else if (hashAlgorithm.Hash256Algorithm(password) != account.Password)
            {
                return BadRequest("Wrong password");
            }
            var token = jwtTokenGenerator.GenerateToken(account);
            return Ok(token);
        }
        
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest registerRequest)
        {
            AccountDTO account = new AccountDTO
            {
                Email = registerRequest.Email,
                Password = hashAlgorithm.Hash256Algorithm(registerRequest.Password),
                RoleId = registerRequest.RoleId,
            };
            await accountRepository.CreateAccount(account);
            return Ok("Create account successfully");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAccounts(int id)
        {
            Account account;
            using (var context = new AuthenticationContext())
            {
                account = await context.Accounts.Where(x => x.AcountId == id).Include(x => x.Role).FirstOrDefaultAsync();
            }
            JsonSerializerOptions options = new()
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles,
                WriteIndented = true
            };
            return Ok(JsonSerializer.Serialize(account, options));
        }
    }
}
