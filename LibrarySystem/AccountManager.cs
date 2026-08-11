using Microsoft.VisualBasic.FileIO;
using System;
using System.Reflection.Metadata.Ecma335;

public static class AccountManager
{
	static List<Account> accounts { get; set; }
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
		int indexofAccount = accounts.FindIndex(acc => acc.getID() == a.getID());

		if (accounts.Contains(a)){
			accounts.Insert(indexofAccount, a);
			return 0;
		}

		return -1;
    }

	public static int addBookToAccount(Book? b, Account? a)
	{
	
		if (a is not null && accounts.Contains(a))
		{
			a.addBook(b);
			return 0;
		}
        return -1;

    }

	public static Account? LookupAccount(string? e, string? p)
	{
		if(e is not null && p is not null)
		{
            if (accounts.Exists(a => a.getEmail() == e && a.getPassword() == p))
            {
                return accounts.Find(a => a.getEmail() == e && a.getPassword() == p);
            }

        }

        return null;
	}

	public static List<Book>?  booksInAccount(Account? a)
	{
		if (a is not null && accounts.Contains(a))
		{
			return a.getBooks();
		}

		return null;
	}

	public static int returnBook(int isbn, Account? a)
	{
		if(a is not null)
		{
            List<Book>? bookArr = a.getBooks();
            if (bookArr is not null)
            {
                Book? book = bookArr.Find(r => r.getISBN() == isbn);

                if (book is not null)
                {
                    bookArr.Remove(book);
                    return 0;
                }
            }
        }


        return -1;
	}

	public static int addRoomToAccount(Room? b, Account? a)
	{
        if (a is not null && accounts.Contains(a))
        {
            a.addRoom(b);
            return 0;
        }
        return -1;
    }

	public static List<Room>? roomsInAccount(Account? a)
	{
		if(a is not null && accounts.Exists(u => u.getID() == a.getID()))
		{
			return a.getRooms();
		}

		return null;

    }

	public static int checkoutRoom(int roomID, Account? a)
	{

		if(a is not null)
        {
            List<Room>? roomArr = a.getRooms();

            if (roomArr is not null)
            {
                Room? room = roomArr.Find(r => r.getId() == roomID);

                if (room is not null)
                {
                    roomArr.Remove(room);
                    return 0;
                }
            }

        }

        return -1;
    }
}
