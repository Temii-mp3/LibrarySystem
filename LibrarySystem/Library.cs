using System;

public class Library
{
    List<Book> books;
    List<Room> rooms;
	public Library()
	{
		books = new List<Book>();
        rooms = new List<Room>();

        books.Add(new Book(101, "The C Programming Language", "Brian Kernighan", true));
        books.Add(new Book(102, "Introduction to Algorithms", "Thomas Cormen", true));
        books.Add(new Book(103, "Effective Java", "Joshua Bloch", true));
        books.Add(new Book(104, "Head First Design Patterns", "Eric Freeman", true));
        books.Add(new Book(105, "Clean Code", "Robert C. Martin", true));
        books.Add(new Book(106, "Building Microservices", "Sam Newman", true));
        books.Add(new Book(107, "The Clean Coder", "Robert C. Martin", true));
        books.Add(new Book(108, "Design Patterns", "Erich Gamma", true));
        books.Add(new Book(109, "The Pragmatic Programmer", "David Thomas", true));
        books.Add(new Book(110, "Structure and Interpretation of Computer Programs", "Harold Abelson", true));

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

    public List<Book> getBooks()
    {
        return books;
    }

    public Book? getBook(int isbn)
    {
        Book book = books.Find(b => b.getISBN() == isbn);
        if (book == null)
        {
            return null;
        }
        return book;
    }

    public List<Room> getRooms()
    {
        return rooms;
    }

    public Room? getRoom(int id)
    {
        Room room = rooms.Find(b => b.getId() == id);
        if (room == null)
        {
            return null;
        }
        return room;
    }


}
