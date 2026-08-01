using System;

public class Room
{
	string type;
	int id;
	public Room(string t, int i)
	{
		type = t;
		id = i;
	}




    public override string ToString()
    {
		return "Room ID: " + id + "\nRoom Type: " + type + "\n";
    }

}
