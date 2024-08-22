using BankingSystem.Models;
using System.Collections.Generic;

public interface IBankingService
{
    User CreateUser(string name);
    Account CreateAccount(int userId, decimal initialBalance);
    Account Deposit(int accountId, decimal amount);
    Account Withdraw(int accountId, decimal amount);
    bool DeleteAccount(int accountId);
}
