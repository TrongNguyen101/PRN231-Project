﻿using AuthenticationAPI.JWTProvider;
using BusinessObject.DataContext;
using BusinessObject.DataTransfer;
using BusinessObject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repositories.AccountRepository;
using Repositories.HashAlgorithmRepository;

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
                return BadRequest("Invalid account");
            }
            else if (hashAlgorithm.Hash256Algorithm(password) != account.Password)
            {
                return BadRequest("wrong password");
            }
            var token = jwtTokenGenerator.GenerateToken(account);
            return Ok("Login successfully: " + token);
        }
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest registerRequest)
        {
            AccountDTO account = new AccountDTO
            {
                Email = registerRequest.Email,
                Password = hashAlgorithm.Hash256Algorithm(registerRequest.Password),
                RoleId = registerRequest.RoleId
            };
            await accountRepository.CreateAccount(account);
            return Created();
        }

        [HttpGet]
        public async Task<IActionResult> GetAccounts()
        {
            List<Account> list;
            using (var context = new AuthenticationContext())
            {
                list = await context.Accounts.ToListAsync();
            }
            return Ok(list);
        }
    }
}
