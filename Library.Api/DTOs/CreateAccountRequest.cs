public record CreateAccountRequest(string Email, string Password, string Username);
public record DeleteAccountRequst(string Email);
public record LookupAccountRequest(string Email);

public record CreateBookRequest(string Isbn, string Author, string Name);

public record BookDTO(string Isbn);

public record AccountDTO(string email);