using System;
using System.Reflection.Metadata.Ecma335;

public class Book
{
	int ISBN;
	string name;
	string author;
	bool canBorrow;

	public Book(int isbn,string n, string a, bool flag)
	{
		ISBN = isbn;
		name = n;
		author = a;
		canBorrow = flag;
	}

	public void setISBN(int isbn)
	{
		ISBN = isbn;
	}

	public void setAuthor(string author)
	{
		this.author = author;
	}

	public int getISBN()
	{
		return ISBN;
	}

	public string getAuthor()
	{
		return author;
	}

	public bool checkBorrow()
	{
		return canBorrow;
	}

	public void  setBorrow(bool b)
	{
		canBorrow = b;
	}


    public override string ToString()
    {
        return $"Name: {name}\n Author: {author}\n ISBN: {ISBN}\n Borrowed:{(canBorrow ? "No" : "Yes")}\n";
    }
}
