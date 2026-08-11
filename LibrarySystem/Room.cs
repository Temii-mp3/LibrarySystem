using System;

public class Room
{
	public string Type{get; set;}
	public int Id{get; set;}
	public bool CanBook{get; set;}
	public Room(string t, int i, bool flag)
	{
		Type = t;
		Id = i;
		CanBook = flag;
	}




    public override string ToString()
    {
		return "Room ID: " + Id + "\nRoom Type: " + Type + "\n";
    }


}
