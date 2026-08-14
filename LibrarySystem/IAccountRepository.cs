public interface IAccountRepository
{
    public Account addAccount(Account a); 
    public Account deleteAccount(Account a);
    public Account LookupAccount(string email, string password);
    public Account LookupAccount(Account a);
    public Account updateAccount(Account a);


}