using Library.Domain.Models;
using System;

public interface IAccountService
{
    public Task<Account> LookupAccount(string email);
    public Task<Account> DeleteAccount(string email);

    public Task<Account> AddAccountToDB(string email, string password, string username);

    public Task<ICollection<Account>> GetAllAccounts();
    public Task<Account> UpdateAccount(string email, Account changes);
    public Task<string> LoginUser(string email, string password);

}