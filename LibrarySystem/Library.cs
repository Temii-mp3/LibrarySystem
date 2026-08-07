using System;

public class Library
{
	Book[] books;
	Room[] rooms;
	public Library()
	{
		books = new Book[100];
		rooms = new Room[100];

        books[0] = new Book(101, "The C Programming Language", "Brian Kernighan", true);
        books[1] = new Book(102, "Introduction to Algorithms", "Thomas Cormen", true);
        books[2] = new Book(103, "Effective Java", "Joshua Bloch", true);
        books[3] = new Book(104, "Head First Design Patterns", "Eric Freeman", true);
        books[4] = new Book(105, "Clean Code", "Robert C. Martin", true);
        books[5] = new Book(106, "Building Microservices", "Sam Newman", true);
        books[6] = new Book(107, "The Clean Coder", "Robert C. Martin", true);
        books[7] = new Book(108, "Design Patterns", "Erich Gamma", true);
        books[8] = new Book(109, "The Pragmatic Programmer", "David Thomas", true);
        books[9] = new Book(110, "Structure and Interpretation of Computer Programs", "Harold Abelson", true);

        // 10 dummy rooms
        rooms[0] = new Room("Study Room", 201, true);
        rooms[1] = new Room("Conference Room", 202, true);
        rooms[2] = new Room("Reading Hall", 203, true);
        rooms[3] = new Room("Computer Lab", 204, true);
        rooms[4] = new Room("Study Room", 205, true);
        rooms[5] = new Room("Meeting Room", 206, true);
        rooms[6] = new Room("Quiet Zone", 207, true);
        rooms[7] = new Room("Group Study Room", 208, true);
        rooms[8] = new Room("Archive Room", 209, true);
        rooms[9] = new Room("Multimedia Room", 210, true);
    }

    public Book[] getBooks()
    {
        return books;
    }

    public Book? getBook(int isbn)
    {
        foreach (Book book in books)
        {
            if (isbn == book.getISBN())
                return book;
        }

        return null;
    }

    public Room[] getRooms()
    {
        return rooms;
    }

    public Room? getRoom(int id)
    {
        foreach (Room room in rooms)
        {
            if (id == room.getId())
                return room;
        }

        return null;
    }


}
