using System;
using System.Reflection.Metadata.Ecma335;

public class Book
{
	public int ISBN{get; set;}
	public string Name{get; set;}
	public string Author{get; set;}
	public bool CanBorrow{get; set;}

	public Book(int isbn,string n, string a, bool flag)
	{
		ISBN = isbn;
		Name = n;
		Author = a;
		CanBorrow = flag;

	}




    public override string ToString()
    {
        return $"Name: {Name}\n Author: {Author}\n ISBN: {ISBN}\n Borrowed:{(CanBorrow ? "No" : "Yes")}\n";
    }
}
