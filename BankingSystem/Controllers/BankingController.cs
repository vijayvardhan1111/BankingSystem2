using Microsoft.AspNetCore.Mvc;
using System;

namespace BankingSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BankingController : ControllerBase
    {
        private readonly IBankingService _bankingService;

        public BankingController(IBankingService bankingService)
        {
            _bankingService = bankingService;
        }

        [HttpPost("CreateUser/{name}")]
        public IActionResult CreateUser(string name)
        {
            try
            {
                var user = _bankingService.CreateUser(name);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("CreateAccount/{userId}/{amount}")]
        public IActionResult CreateAccount(int userId, decimal amount)
        {
            try
            {
                var account = _bankingService.CreateAccount(userId, amount);
                return Ok(account);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("DepositAccount/{accountId}/{amount}")]
        public IActionResult Deposit(int accountId, decimal amount)
        {
            try
            {
                var account = _bankingService.Deposit(accountId, amount);
                return Ok(account);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("WithdrawAccount/{accountId}/{amount}")]
        public IActionResult Withdraw(int accountId, decimal amount)
        {
            try
            {
               var acocunt = _bankingService.Withdraw(accountId, amount);
                return Ok(acocunt);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteAccount/{accountId}")]
        public IActionResult DeleteAccount(int accountId)
        {
            try
            {
                var result = _bankingService.DeleteAccount(accountId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
