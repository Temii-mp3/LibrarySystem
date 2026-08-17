using System.Security.Cryptography.X509Certificates;
using System.Xml.Xsl;

[Serializable]
public class AccountNotFoundException : Exception
{
    public AccountNotFoundException() { }
    public AccountNotFoundException(string message) : base(message) { }
};

[Serializable]
public class AccountExistsException : Exception
{
    public AccountExistsException() { }
    public AccountExistsException(string message) : base(message) { }
}

[Serializable]
public class InvalidAccountException : Exception
{
    public InvalidAccountException() { }
    public InvalidAccountException(string message) : base(message) { }
};
[Serializable]
public class BookNotFoundException : Exception
{
    public BookNotFoundException() { }
    public BookNotFoundException(string message) : base(message) { }
};
[Serializable]
public class GenericException : Exception
{
    public GenericException() { }
    public GenericException(string message) : base(message) { }
};
[Serializable]
public class RoomLimitReachedException : Exception
{
    public RoomLimitReachedException() { }
    public RoomLimitReachedException(string message) : base(message) { }
}


[Serializable]
public class BookLimitReachedException : Exception
{
    public BookLimitReachedException() { }
    public BookLimitReachedException(string message) : base(message) { }
};
[Serializable]
public class NotLoggedInException : Exception
{
    public NotLoggedInException() { }
    public NotLoggedInException(string message) : base(message) { }
};
[Serializable]
public class InvalidInputException : Exception
{
    public InvalidInputException() { }
    public InvalidInputException(string message) : base(message) { }
};
[Serializable]
public class BookBorrowedException : Exception
{
    public BookBorrowedException() { }
    public BookBorrowedException(string message) : base(message) { }
}


[Serializable]
public class RoomNotFoundException : Exception
{
    public RoomNotFoundException() { }
    public RoomNotFoundException(string message) : base(message) { }
}


[Serializable]
public class LoginException: Exception
{
    public  LoginException() { }
    public LoginException(string message) : base(message) { }
}
