using Microsoft.VisualBasic.FileIO;
using System;
using System.Reflection.Metadata.Ecma335;

public static class AccountManager
{
	static List<Account> accounts;
	static int i = 0;
	 static AccountManager()
	{
		accounts = new List<Account>();
	}

	public static int addAccount(Account a)
	{
		accounts[i++] = a;
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

	public static int addBookToAccount(Book? b, Account a)
	{

		if (accounts.Contains(a))
		{
			a.addBook(b);
			return 0;
		}
        return -1;

    }

	public static Account? LookupAccount(string e, string p)
	{
		if(accounts.Exists(a => a.getEmail() == e && a.getPassword() == p))
		{
			return accounts.Find(a => a.getEmail() == e && a.getPassword() == p);
		}

		return null;
	}

	public static Book[]?  booksInAccount(Account a)
	{
		if (accounts.Contains(a))
		{
			return a.getBooks();
		}

		return null;
	}

	public static int returnBook(int isbn, Account a)
	{
		Book[] bookArr = a.getBooks();

		
        for (int i = 0; i < bookArr.Length; i++)
        {
            if (bookArr[i].getISBN() == isbn)
            {
				bookArr[i].setBorrow(false);
                for (int k = i; k < (bookArr.Length - i); k++)
                {
                    bookArr[i] = bookArr[i + 1];
                }
				a.updateBooks(bookArr);
                return 0;
            }
        }


        return -1;
	}

	public static int addRoomToAccount(Room? b, Account a)
	{
        for (int i = 0; i < accounts.Length; i++)
        {
            if (a.getID() == accounts[i].getID())
            {
                accounts[i].addRoom(b);
                b.setBooked(true);
                return 0;
            }
        }
        return -1;
    }

	public static Room[] roomsInAccount(Account a)
	{
        foreach (Account b in accounts)
        {
            if (b.getID() == a.getID())
                return b.getRooms();
        }
        return [];
    }

	public static int checkoutRoom(int roomID, Account a)
	{
        Room[] roomArr = a.getRooms();
        for (int i = 0; i < roomArr.Length; i++)
        {
            if (roomArr[i].getId() == roomID)
            {
                roomArr[i].setBooked(false);
                for (int k = i; k < (roomArr.Length-i); k++)
                {
                    roomArr[i] = roomArr[i + 1];
                }

				a.updateRoooms(roomArr);
                return 0;
            }
        }


        return -1;
    }
}
