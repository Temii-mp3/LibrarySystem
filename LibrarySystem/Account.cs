using System;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

public class Account
{
	string email;
	int id;
	string userName;
	string password;
	List<Book> books;
	List<Room> rooms;
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
		books = new List<Book>();
		rooms = new List<Room>();
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
		books.Add(b);
	}

	public void addRoom(Room? r)
	{
        rooms.Add(r);
    }

	public int removeBook(Book b)
	{
		if (books.Remove(b))
		{
			return 0;
		}
		else
		{
			return -1;
		}

    }


		public int removeRoom(Room r)
	{
        if (rooms.Remove(r))
        {
            return 0;
        }
        else
        {
            return -1;
        }

    }

	public List<Book> getBooks()
	{
		return books;
	}

	public List<Room> getRooms()
	{
		return rooms;
	}

	public void updateRoooms(List<Room> rooms)
	{
		this.rooms = rooms;
	}

	public void updateBooks(List<Book> books)
	{
		this.books = books;
	}
}
