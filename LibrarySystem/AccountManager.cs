using Microsoft.VisualBasic.FileIO;
using System;
using System.Reflection.Metadata.Ecma335;

public static class AccountManager
{
	static Account[] accounts;
	static int i = 0;
	 static AccountManager()
	{
		accounts = new Account[100];
	}

	public static int addAccount(Account a)
	{
		accounts[i++] = a;
		return 0;
	}

	public static int deleteAccount(Account a)
	{
		for(int i = 0; i < accounts.Length; i++)
		{
			if (accounts[i] == a)
			{
				for ( int k = i; k < (i-accounts.Length); k++)
				{
					accounts[i] = accounts[i + 1];
				}
				return 0;
			}
		}


		return -1;
	}

	public static int updateAccount(Account a)
	{
		for (int i = 0; i < accounts.Length; i++)
		{
			if(a.getID() == accounts[i].getID())
			{
				accounts[i] = a;
				return 0;
			}
		}
        return -1;
    }

	public static int addBookToAccount(Book? b, Account a)
	{
        for (int i = 0; i < accounts.Length; i++)
        {
            if (a.getID() == accounts[i].getID())
            {
				accounts[i].addBook(b);
				b.setBorrow(false);
				return 0;
            }
        }
        return -1;

    }

	public static Account? LookupAccount(string e, string p)
	{
		foreach (Account a in accounts)
		{
			if (a.getEmail() == e && a.getPassword() == p)
				return a;
		}
		return null;
	}

	public static Book[]  booksInAccount(Account a)
	{
        foreach (Account b in accounts)
        {
            if (b.getID() == a.getID())
                return b.getBooks();
        }
		return [];
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
