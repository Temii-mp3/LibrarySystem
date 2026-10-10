using Library.Infrastructure;
using Library.Infrastructure.Auth;
using LibraryDomain.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Text.RegularExpressions;
public class AccountService : IAccountService
{

    private readonly IAccountRepository _repo;
    private readonly IPasswordHasher<Account> _hasher;
    private readonly ITokenProvider _provider;
    private readonly IConfiguration _config;
    public AccountService(IAccountRepository repo, IPasswordHasher<Account> hasher, ITokenProvider provider, IConfiguration config)
    {
        _repo = repo;
        _hasher = hasher;
        _provider = provider;
        _config = config;
    }
    public async Task<Account> AddAccountToDB(string email, string password, string username)
    {

        if (!CheckEmail(email) || !CheckPassword(password) || !CheckUser(username))
            throw new LoginException();

        if (await _repo.LookupAccount(email) is not null)
            throw new AccountExistsException();

        Account user = new Account
        {
            Email = email,
            Username = username
        };
        var hashedPassword = _hasher.HashPassword(user, password);
        user.Password = hashedPassword;
        Account result = await _repo.AddAccount(user);

        return result;
    }

    public async Task<Account> LookupAccount(string email)
    {

        if (!CheckEmail(email))
            throw new LoginException();
        Account? result = await _repo.LookupAccount(email);

        if (result is not null)
            return result;
        throw new AccountNotFoundException();

    }

    public async Task<Account> DeleteAccount(string email)
    {
        Account? user = await _repo.LookupAccount(email);
        if (user is null)
            throw new AccountNotFoundException();
        Account result = await _repo.DeleteAccount(user);
        if (result is null)
            throw new GenericException();
        return result;

    }

    static bool CheckEmail(string? email)
    {
        if (email is not null)
        {
            return Regex.IsMatch(email, @"^[\w\.-]+@[\w\.-]+\.\w+$");
        }
        return false;
    }

    static bool CheckPassword(string? password)
    {
        if (password is not null)
        {
            return Regex.IsMatch(password, @"^\w{8,}$");
        }
        return false;
    }

    static bool CheckUser(String? user)
    {
        if (user is not null)
        {
            return Regex.IsMatch(user, @"^\w{3,}$");
        }

        return false;
    }
    public async Task<ICollection<Account>> GetAllAccounts()
    {

        ICollection<Account> accounts = await _repo.GetAllAccounts();
        if (accounts is null)
            throw new GenericException();
        return accounts;
    }

    //public async Task<Account> UpdateAccount(int id, Account changes)
    //{
    //    return null;
    //}

    public async Task<string> LoginUser(string email, string password)
    {
        if (!CheckEmail(email) || !CheckPassword(password))
            throw new LoginException();
        Account? result = await _repo.LookupAccount(email);
        if (result is null)
            throw new AccountNotFoundException();
        PasswordVerificationResult isValidPswd = _hasher.VerifyHashedPassword(result, result.Password, password);

        if (isValidPswd == PasswordVerificationResult.Success)
        {
            var token = _provider.Create(result);
            return token;
        }
        else if (isValidPswd == PasswordVerificationResult.SuccessRehashNeeded)
        {
            var token = _provider.Create(result);
            return token; //TODO update this to rehash logik
        }

        throw new LoginException();
    }
}