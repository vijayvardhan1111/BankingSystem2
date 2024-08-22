using BankingSystem.Models;
using System;
using Xunit;

public class BankingServiceTests
{
    private readonly IBankingService _bankingService;
    public BankingServiceTests()
    {
        _bankingService = new BankingService();
    }

    [Fact]
    public void CreateUser_ShouldReturnUserWithValidId()
    {
        var user = _bankingService.CreateUser("Vijay Vardhan");
        Assert.NotNull(user);
        Assert.True(user.Id > 0);
        Assert.Equal("Vijay Vardhan", user.Name);
    }

    [Fact]
    public void CreateAccount_WithValidInitialBalance_ShouldReturnAccount()
    {
        var user = _bankingService.CreateUser("Vijay Vardhan");
        var account = _bankingService.CreateAccount(user.Id, 200);
        Assert.NotNull(account);
        Assert.True(account.AccountId > 0);
        Assert.Equal(200, account.Balance);
    }

    [Fact]
    public void CreateAccount_WithInvalidInitialBalance_ShouldThrowException()
    {
        var user = _bankingService.CreateUser("John Doe");
        try
        {
            _bankingService.CreateAccount(user.Id, 50);
            Assert.True(false, "Expected an ArgumentException, but no exception was thrown.");
        }
        catch (ArgumentException ex)
        {
            Assert.Equal("Initial balance must be at least $100", ex.Message);
        }
    }

    [Fact]
    public void Deposit_WithValidAmount_ShouldIncreaseBalance()
    {
        var user = _bankingService.CreateUser("Vijay Vardhan");
        var account = _bankingService.CreateAccount(user.Id, 200);
        _bankingService.Deposit(account.AccountId, 500);
        Assert.Equal(700, account.Balance);
    }

    [Fact]
    public void Deposit_WithInvalidAmount_ShouldThrowException()
    {
        var user = _bankingService.CreateUser("Vijay Vardhan");
        var account = _bankingService.CreateAccount(user.Id, 200);
        try
        {
            _bankingService.Deposit(account.AccountId, 20000);
            Assert.True(false, "Expected an ArgumentException, but no exception was thrown.");
        }
        catch (ArgumentException ex)
        {
            Assert.Equal("Cannot deposit more than $10,000", ex.Message);
        }

        //Assert.Equal("Cannot deposit more than $10,000", exception.Message);
    }

    [Fact]
    public void Withdraw_WithValidAmount_ShouldDecreaseBalance()
    {
        var user = _bankingService.CreateUser("Vijay Vardhan");
        var account = _bankingService.CreateAccount(user.Id, 1000);
        _bankingService.Withdraw(account.AccountId, 400);
        Assert.Equal(600, account.Balance);
    }

    [Fact]
    public void Withdraw_WithInvalidAmount_ShouldThrowException()
    {
        var user = _bankingService.CreateUser("Vijay Vardhan");
        var account = _bankingService.CreateAccount(user.Id, 1000);
        try
        {
            _bankingService.Withdraw(account.AccountId, 950);
            Assert.True(false, "Expected an ArgumentException, but no exception was thrown.");
        }
        catch (ArgumentException ex)
        {
            Assert.Equal("Cannot withdraw more than 90% of total balance", ex.Message);
        }
    }

    [Fact]
    public void Withdraw_ShouldThrowException_WhenBalanceBelowMinimum()
    {
        var user = _bankingService.CreateUser("Vijay Vardhan");
        var account = _bankingService.CreateAccount(user.Id, 200);
        try
        {
            _bankingService.Withdraw(account.AccountId, 150);
            Assert.True(false, "Expected an ArgumentException, but no exception was thrown.");
        }
        catch (ArgumentException ex)
        {
            Assert.Equal("Account balance cannot be less than $100", ex.Message);
        }
    }

    [Fact]
    public void DeleteAccount_WithValidAccountId_ShouldReturnTrue()
    {
        var user = _bankingService.CreateUser("Vijay Vardhan");
        var account = _bankingService.CreateAccount(user.Id, 500);
        var result = _bankingService.DeleteAccount(account.AccountId);
        Assert.True(result);
        Assert.DoesNotContain(account, user.Accounts);
    }

    [Fact]
    public void DeleteAccount_WithInvalidAccountId_ShouldThrowArgumentException()
    {
        var invalidAccountId = -1;
        try
        {
            _bankingService.DeleteAccount(invalidAccountId);
            Assert.True(false, "Expected an ArgumentException, but no exception was thrown.");
        }
        catch (ArgumentException ex)
        {
            Assert.Equal("Account not found", ex.Message);
        }
    }
}
