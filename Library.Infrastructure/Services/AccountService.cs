using LibraryDomain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using System;
using System.Data;
using System.Text.RegularExpressions;
public class AccountService : IAccountService
{

    private readonly IAccountRepository _repo;
    private readonly IPasswordHasher<Account> _hasher;
    public AccountService(IAccountRepository repo, IPasswordHasher<Account> hasher)
    {
        _repo = repo;
        _hasher = hasher;
    }
    public async Task<Account> AddAccountToDB(string email, string password, string username)
    {

        if (!CheckEmail(email))
            throw new InvalidEmailFormatException();
        if (!CheckPassword(password))
            throw new InvalidPasswordFormatException();
        if (!CheckUser(username))
            throw new InvalidUsernameFormatException();

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
            throw new InvalidEmailFormatException();
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
}