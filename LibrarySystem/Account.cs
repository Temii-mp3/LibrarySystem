using System;
using System.Runtime.CompilerServices;

public class Account
{
	string email;
	int id;
	string userName;
	string password;
	Book[] books;
	int i;
	public Account(string e, string u, string p, int id)
	{
		email = e;
		userName = u;
		password = p;
		this.id = id;
		i = 0;
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

	public void addBook(Book b)
	{
		books[i++] = b;
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
}
