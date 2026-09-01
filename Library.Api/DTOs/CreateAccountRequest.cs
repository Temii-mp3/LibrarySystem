public record CreateAccountRequest(string Email, string Password, string Username);
public record DeleteAccountRequst(string Email);
public record LookupAccountRequest(string Email);