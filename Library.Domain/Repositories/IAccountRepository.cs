using LibraryDomain.Models;

public interface IAccountRepository
{
    public Task<Account> AddAccount(Account a);
    public Task<Account> DeleteAccount(Account a);
    public Task<Account> LookupAccount(string email, string password);
    public Task<Account> LookupAccount(Account a);
    public Task<Account?> LookupAccount(string email);
    public Task<Account> UpdateAccount(Account a);


}