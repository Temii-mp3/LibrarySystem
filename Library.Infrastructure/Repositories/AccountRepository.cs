using LibraryDomain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

public class AccountRepositry : IAccountRepository
{
    LibraryContext _context;

    public AccountRepositry(LibraryContext context)
    {
        _context = context;
    }


    public async Task<Account> AddAccount(Account a)
    {
        try
        {
            await _context.Accounts.AddAsync(a);
            if (await _context.SaveChangesAsync() >= 1)
                return a;
        }
        catch (DbUpdateException)
        {
            throw new GenericException("Something went wrong");
        }
        throw new GenericException("Something went wrong");
    }

    public async Task<Account> DeleteAccount(Account a)
    {
        if (!(await _context.Accounts.AnyAsync(f => f.Id == a.Id)))
            throw new AccountNotFoundException("Account not found");
        _context.Accounts.Remove(a);
        if (await _context.SaveChangesAsync() >= 1)
            return a;
        throw new GenericException("Account cant be deleted");

    }
    public async Task<Account> LookupAccount(string email, string password)
    {
        Account? account = await _context.Accounts.FirstOrDefaultAsync(f => f.Email == email && f.Password == password);

        if (account is null)
            throw new AccountNotFoundException();
        return account;
    }

    public async Task<Account?> LookupAccount(string email)
    {
        Account? account = await _context.Accounts.FirstOrDefaultAsync(f => f.Email == email);

        if (account is null)
            return null;
        return account;
    }

    public async Task<Account> LookupAccount(Account a)
    {
        Account? account = await _context.Accounts.FirstOrDefaultAsync(f => f.Id == a.Id);

        if (account is null)
            throw new AccountNotFoundException();
        return account;
    }


    public async Task<Account> UpdateAccount(Account a)
    {
        Account? account = await _context.Accounts.FirstOrDefaultAsync(f => f.Id == a.Id);
        account = a;
        if (await _context.SaveChangesAsync() >= 1)
            return a;
        throw new GenericException("Cannot Update Account");
    }





   









}
