using Microsoft.VisualBasic.FileIO;
using System;
using System.Reflection.Metadata.Ecma335;

public static class AccountManager
{
	const int LIMIT = 5;
	static List<Account> accounts;
	static int i = 0;
	static AccountManager()
	{
		accounts = new List<Account>();
	}

	public static int addAccount(Account a)
	{
		accounts.Add(a);
		return 0;
	}

	public static int deleteAccount(Account a)
	{
		if (accounts.Contains(a))
		{
			accounts.Remove(a);
		}
		return -1;
	}

	public static int updateAccount(Account a)
	{
		int indexofAccount = accounts.FindIndex(acc => acc.Id == a.Id);

		if (accounts.Contains(a))
		{
			accounts.Insert(indexofAccount, a);
			return 0;
		}

		return -1;
	}

	public static Book addBookToAccount(Book? b, Account a)
	{
		if (!accounts.Contains(a))
		{
			throw new AccountNotFoundException("Account not found");
		}

		if (a.Books.Count > 5)
		{
			throw new BookLimitReachedException($"Book Limit of {LIMIT} has been reached");
		}

		a.addBook(b);
		return b;


	}

	public static Account? LookupAccount(string? e, string? p)
	{
		if (accounts.Exists(a => a.Email == e && a.Password == p))
		{
			return accounts.Find(a => a.Email == e && a.Password == p);
		}

		throw new AccountNotFoundException("Account not found");
	}

	public static List<Book>? booksInAccount(Account a)
	{
		if (a is not null && accounts.Contains(a))
		{
			return a.Books;
		}

		return null;
	}

	public static Book returnBook(int isbn, Account a)
	{
		List<Book> bookArr = a.Books;


		Book book = bookArr.Find(r => r.ISBN == isbn);

		if (book != null)
		{
			bookArr.Remove(book);
			return book;
		}
		throw new BookNotFoundException("Book not found");
	}

	public static int addRoomToAccount(Room? b, Account? a)
	{
		if (accounts.Contains(a))
		{
			a.addRoom(b);
			return 0;
		}
		return -1;
	}

	public static List<Room>? roomsInAccount(Account? a)
	{
		if (accounts.Exists(u => u.Id == a.Id))
		{
			return a.Rooms;
		}

		return null;

	}

	public static int checkoutRoom(int bookID, Account a)
	{
		List<Room> bookArr = a.Rooms;

		Room book = bookArr.Find(r => r.Id == bookID);

		if (book != null)
		{
			bookArr.Remove(book);
			return 0;
		}
		return -1;
	}
}
