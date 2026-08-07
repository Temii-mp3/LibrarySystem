using System;

public class Room
{
	string type;
	int id;
	bool canBook;
	public Room(string t, int i, bool flag)
	{
		type = t;
		id = i;
		canBook = flag;
	}




    public override string ToString()
    {
		return "Room ID: " + id + "\nRoom Type: " + type + "\n";
    }

	public int getId()
	{
		return id;
	}

	public bool checkBooked()
	{
		return canBook;
	}

	public void setBooked(bool b)
	{
		canBook = b;
	}

}
