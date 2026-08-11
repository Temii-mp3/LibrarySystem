

public class Account
{
	public String Email{get; set;}
	public int Id {get; set;}
	public string Username{get; set;}
	public int Limit {get; set;} = 5;
	public string Password{get; set;}
	public List<Book> Books{get; set;} = new List<Book>();
	public List<Room> Rooms{get; set;} = new List<Room>();
	int i;
	int k;
	public Account(string e, string u, string p, int id)
	{
		Email = e;
		Username = u;
		Password = p;
		Id = id;
		i = 0;
		k = 0;
	}

	public void addBook(Book? b)
	{
		Books.Add(b);
	}


	public void addRoom(Room? r)
	{
        Rooms.Add(r);
    }

    

	public int removeBook(Book? b)
	{
		if (Books.Remove(b))
		{
			return 0;
		}
		else
		{
			return -1;
		}
	}



		public int removeRoom(Room r)
	{
        if (Rooms.Remove(r))
        {
            return 0;
        }
        else
        {
            return -1;
        }

    }
}
