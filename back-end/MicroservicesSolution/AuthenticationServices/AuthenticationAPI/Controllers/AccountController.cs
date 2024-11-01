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
        private readonly ILogger<AccountController> logger;
        public AccountController(
            IAccountRepository accountRepository,
            JwtTokenGenerator jwtTokenGenerator,
            IHashAlgorithmRepository hashAlgorithm,
            ILogger<AccountController> logger)
        {
            this.accountRepository = accountRepository;
            this.jwtTokenGenerator = jwtTokenGenerator;
            this.hashAlgorithm = hashAlgorithm;
            this.logger = logger;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(string email, string password)
        {
            try
            {
                var account = await accountRepository.FindAccount(email);
                if (account == null)
                {
                    logger.LogWarning(StatusCodes.Status400BadRequest + " This user is not exist");
                    return BadRequest("This user is not exist");
                }
                else if (hashAlgorithm.Hash256Algorithm(password) != account.Password)
                {
                    logger.LogWarning(StatusCodes.Status400BadRequest + " Wrong password");
                    return BadRequest("Wrong password");
                }
                var token = jwtTokenGenerator.GenerateToken(account);
                logger.LogInformation(StatusCodes.Status200OK + " Login successfully");
                return Ok(token);
            }
            catch
            {
                logger.LogError(StatusCodes.Status500InternalServerError + " Internal server error");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest registerRequest)
        {
            try
            {
                AccountDTO account = new AccountDTO
                {
                    Email = registerRequest.Email,
                    Password = hashAlgorithm.Hash256Algorithm(registerRequest.Password),
                    RoleId = registerRequest.RoleId,
                };
                await accountRepository.CreateAccount(account);
                logger.LogInformation(StatusCodes.Status200OK + " Create account successfully");
                return Ok("Create account successfully");
            }
            catch
            {
                logger.LogError(StatusCodes.Status500InternalServerError + " Interval server error");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
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
