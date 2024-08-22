using BankingSystem.Models;

public class BankingService: IBankingService
{
    private readonly List<User> _users = new List<User>();

    public User CreateUser(string name)
    {
        var user = new User
        {
            Id = _users.Count + 1,
            Name = name
        };
        _users.Add(user);
        return user;
    }

    public Account CreateAccount(int userId, decimal initialBalance)
    {
        if (initialBalance < 100) throw new ArgumentException("Initial balance must be at least $100");

        var user = _users.FirstOrDefault(u => u.Id == userId);
        if (user == null) throw new ArgumentException("User not found");

        var account = new Account
        {
            AccountId = user.Accounts.Count + 1,
            Balance = initialBalance
        };

        user.Accounts.Add(account);
        return account;
    }

    public Account Deposit(int accountId, decimal amount)
    {
        if (amount > 10000) throw new ArgumentException("Cannot deposit more than $10,000");

        var account = _users.SelectMany(u => u.Accounts).FirstOrDefault(a => a.AccountId == accountId);
        if (account == null) throw new KeyNotFoundException("Account not found");

        account.Balance += amount;

        return account;
    }

    public Account Withdraw(int accountId, decimal amount)
    {
        var account = _users.SelectMany(u => u.Accounts).FirstOrDefault(a => a.AccountId == accountId);
        if (account == null) throw new ArgumentException("Account not found");

        if (amount > 0.9m * _users.SelectMany(u => u.Accounts).Where(a => a.AccountId == accountId).Sum(a => a.Balance))
            throw new ArgumentException("Cannot withdraw more than 90% of total balance");

        if (account.Balance - amount < 100) throw new ArgumentException("Account balance cannot be less than $100");

        account.Balance -= amount;

        return account;
    }

    public bool DeleteAccount(int accountId)
    {
        var account = _users.SelectMany(u => u.Accounts).FirstOrDefault(a => a.AccountId == accountId);
        if (account == null) throw new ArgumentException("Account not found");

        var user = _users.FirstOrDefault(u => u.Accounts.Contains(account));
        if (user != null)
            user.Accounts.Remove(account);
        else
            return false;

        return true;
    }
}
