using System;

public class Library
{
	Book[] books;
	Room[] rooms;
	public Library()
	{
		books = new Book[100];
		rooms = new Room[100];

        books[0] = new Book(101, "The C Programming Language", "Brian Kernighan", false);
        books[1] = new Book(102, "Introduction to Algorithms", "Thomas Cormen", false);
        books[2] = new Book(103, "Effective Java", "Joshua Bloch", false);
        books[3] = new Book(104, "Head First Design Patterns", "Eric Freeman", false);
        books[4] = new Book(105, "Clean Code", "Robert C. Martin", false);
        books[5] = new Book(106, "Building Microservices", "Sam Newman", false);
        books[6] = new Book(107, "The Clean Coder", "Robert C. Martin", true);
        books[7] = new Book(108, "Design Patterns", "Erich Gamma", false);
        books[8] = new Book(109, "The Pragmatic Programmer", "David Thomas", false);
        books[9] = new Book(110, "Structure and Interpretation of Computer Programs", "Harold Abelson", false);

        // 10 dummy rooms
        rooms[0] = new Room("Study Room", 201);
        rooms[1] = new Room("Conference Room", 202);
        rooms[2] = new Room("Reading Hall", 203);
        rooms[3] = new Room("Computer Lab", 204);
        rooms[4] = new Room("Study Room", 205);
        rooms[5] = new Room("Meeting Room", 206);
        rooms[6] = new Room("Quiet Zone", 207);
        rooms[7] = new Room("Group Study Room", 208);
        rooms[8] = new Room("Archive Room", 209);
        rooms[9] = new Room("Multimedia Room", 210);
    }

    public Book[] getBooks()
    {
        return books;
    }

    public Room[] getRooms()
    {
        return rooms;
    }


}
