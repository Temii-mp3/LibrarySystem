using LibraryDomain.Models;
using System;

public interface IAccountService
{
    public Task<Account> LookupAccount(string email);
    public Task<Account> DeleteAccount(string email);

    public Task<Account> AddAccountToDB(string email, string password, string username);
}