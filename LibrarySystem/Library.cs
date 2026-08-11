using System;

public class Library
{
    public List<Room> Rooms{get; set;} = new List<Room>();
    public List<Book> Books{get; set;} = new List<Book>();
	public Library()
	{

        Books.Add(new Book(101, "The C Programming Language", "Brian Kernighan", true));
        Books.Add(new Book(102, "Introduction to Algorithms", "Thomas Cormen", true));
        Books.Add(new Book(103, "Effective Java", "Joshua Bloch", true));
        Books.Add(new Book(104, "Head First Design Patterns", "Eric Freeman", true));
        Books.Add(new Book(105, "Clean Code", "Robert C. Martin", true));
        Books.Add(new Book(106, "Building Microservices", "Sam Newman", true));
        Books.Add(new Book(107, "The Clean Coder", "Robert C. Martin", true));
        Books.Add(new Book(108, "Design Patterns", "Erich Gamma", true));
        Books.Add(new Book(109, "The Pragmatic Programmer", "David Thomas", true));
        Books.Add(new Book(110, "Structure and Interpretation of Computer Programs", "Harold Abelson", true));

        // 10 dummy rooms
        rooms.Add(new Room("Study Room", 201, true));
        rooms.Add(new Room("Conference Room", 202, true));
        rooms.Add(new Room("Reading Hall", 203, true));
        rooms.Add(new Room("Computer Lab", 204, true));
        rooms.Add(new Room("Study Room", 205, true));
        rooms.Add(new Room("Meeting Room", 206, true));
        rooms.Add(new Room("Quiet Zone", 207, true));
        rooms.Add(new Room("Group Study Room", 208, true));
        rooms.Add(new Room("Archive Room", 209, true));
        rooms.Add(new Room("Multimedia Room", 210, true));
    }


    public Book? getBook(int isbn)
    {
        Book book = Books.Find(b => b.ISBN == isbn);
        if (book == null)
        {
            return null;
        }
        return book;
    }


    public Room? getRoom(int id)
    {
        Room room = rooms.Find(b => b.Id == id);
        if (room == null)
        {
            return null;
        }
        return room;
    }


}
