using System;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

public class Account
{
	string email;
	int id;
	string userName;
	string password;
	Book[] books;
	Room[] rooms;
	int i;
	int k;
	public Account(string e, string u, string p, int id)
	{
		email = e;
		userName = u;
		password = p;
		this.id = id;
		i = 0;
		k = 0;
		books = new Book[100];
		rooms = new Room[100];
	}

	public string getUserName()
	{
		return userName;
	}
	public string getEmail()
	{
		return email;
	}
	public string getPassword()
	{
		return password;
	}

    public void setPassword(string p)
    {
		password = p;
    }
    public void setEmail(string e)
    {
		email = e;
    }
    public void setUserName(string u)
    {
		userName = u;
    }

	public int getID()
	{
		return id;
	}

	public void addBook(Book? b)
	{
		books[i++] = b;
	}

	public void addRoom(Room? r)
	{
		rooms[k++] = r;
	}

	public int removeBook(Book b)
	{
        for (int i = 0; i < books.Length; i++)
        {
            if (books[i] == b)
            {
                for (int k = i; k < (i - books.Length); k++)
                {
                    books[i] = books[i + 1];
                }
                return 0;
            }
        }

		return -1;

    }


		public int removeRoom(Room r)
	{
        for (int i = 0; i < rooms.Length; i++)
        {
            if (rooms[i] == r)
            {
                for (int k = i; k < (i - rooms.Length); k++)
                {
                    rooms[i] = rooms[i + 1];
                }
                return 0;
            }
        }

		return -1;

    }

	public Book[] getBooks()
	{
		return books;
	}

	public Room[] getRooms()
	{
		return rooms;
	}

	public void updateRoooms(Room[] rooms)
	{
		this.rooms = rooms;
	}

	public void updateBooks(Book[] books)
	{
		this.books = books;
	}
}
