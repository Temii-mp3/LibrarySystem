public class AccountRepositry : IAccountRepository
{
    public  readonly List<Account> accounts  = new();


    public Account addAccount(Account a)
    {
        if (accounts.Exists(f => f.Id == a.Id))
        {
            throw new AccountExistsException("Account already exists");
        }
        else
        {
            accounts.Add(a);
            return a;
        }

        throw new GenericException("Account Exists");
    }

    public Account deleteAccount(Account a)
    {
        if (!accounts.Exists(f => f.Id == a.Id))
        {
            throw new AccountNotFoundException("Account not found");
        }
        else
        {
            accounts.Remove(a);
            return a;
        }
    }
    public Account LookupAccount(string email, string password)
    {
        Account account = accounts.Find(f => f.Email == email && f.Password == password);
        if(account is null)
        {
            throw new AccountNotFoundException("Account not found");
        }

        return account;
    }

    public Account LookupAccount(Account a)
    {
        Account account = accounts.Find(f => f.Id == a.Id);
        if (account is null)
        {
            throw new AccountNotFoundException("Account not found");
        }

        return account;
    }

    public Account updateAccount(Account a)
    {
        int indexofAccount = accounts.FindIndex(acc => acc.Id == a.Id);

        if (accounts.Contains(a))
        {
            accounts.Insert(indexofAccount, a);
            return a;
        }

        throw new AccountNotFoundException("Account not found");
    }

}