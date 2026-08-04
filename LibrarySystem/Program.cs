using System;

public class Program
{
    static void Main(String[] args)
    {
        int userInput;
        string user = "";
        string email = "";
        string password = "";
        string tempUser = "";
        Account userAccount = null;
        Library lib = new Library();
        Random rand = new Random();
        do
        {
            Console.WriteLine("Hello, Welcome to the Library! what would you like to do: \n" +
                            "1. Create an account\n" +
                            "2. Log in to an account\n" +
                            "3. Book Services\n" +
                            "4. Room Services\n" +
                            "5. Log out\n" +
                            "6. Quit");
            Console.WriteLine($"Currently Logged in as: {(string.IsNullOrEmpty(user) ? "Guest" : user)}");
            userInput = Convert.ToInt32(Console.ReadLine());
            while (userInput > 6 || userInput < 1)
            {
                Console.WriteLine("INVALID INPUT");
                userInput = Convert.ToInt32(Console.ReadLine());
            }
            switch (userInput)
            {
                case 1:
                    Console.WriteLine("Enter Email");
                    email = Console.ReadLine();
                    Console.WriteLine("Enter Username");
                    tempUser = Console.ReadLine();
                    Console.WriteLine("Enter Password");
                    password = Console.ReadLine();
                    int id = rand.Next(1, 1000);
                    Console.WriteLine("Creating User......");
                    userAccount = new Account(email, tempUser, password, id);
                    AccountManager.addAccount(userAccount);
                    Console.WriteLine("Account Created Successfully");
                    user = tempUser;
                    break;
                case 2:
                    Console.WriteLine("Enter Email");
                    email = Console.ReadLine();

                    Console.WriteLine("Enter Password");
                    password = Console.ReadLine();

                    Console.WriteLine("Logging in....");

                    Account tempAcc = AccountManager.LookupAccount(email, password);

                    if (tempAcc == null)
                    {
                        Console.WriteLine("Account not found");
                    }
                    else
                    {
                        userAccount = tempAcc;
                        user = userAccount.getUserName();
                    }
                    break;
                case 3:
                    bookServices();
                    break;

                case 4:
                    roomServices();
                    break;
                case 5:
                    Console.WriteLine("Are you sure you want to exit? y/n");
                    char choice = Convert.ToChar(Console.ReadLine());
                    char.ToLower(choice);
                    if (choice == 'y')
                    {
                        Console.WriteLine("Logging out....");
                        Console.WriteLine("Successfully Logged Out");
                        break;
                    }
                    break;
            }

        } while (userInput != 6);




        void bookServices()
        {
            do
            {
                Console.WriteLine("Welcome to book services, what would you like to do?" +
                                "\n1. Borrow" +
                                "\n 2. Return" +
                                "\n 3. Borrowed Books" +
                                "\n 4. Go Back");
                userInput = Convert.ToInt32(Console.ReadLine());
                switch (userInput)
                {
                    case 1:
                        Console.WriteLine("Which Book would you like to Borrow? type ISBN");
                        UtilityClass<Book>.dump(lib.getBooks());
                        int isbnInput = Convert.ToInt32(Console.ReadLine());
                        Book? book = lib.getBook(isbnInput);
                        while(book == null)
                        {
                            Console.WriteLine("Invalid ISBN. Try Again");
                             isbnInput = Convert.ToInt32(Console.ReadLine());
                            book = lib.getBook(isbnInput);
                        }

                        if (book.canBorrow())
                        {
                            AccountManager.addBookToAccount(lib.getBook(isbnInput), userAccount);
                            Console.WriteLine($"Book borrowed by user {userAccount.getUserName()}");
                        }
                        else
                        {
                            Console.WriteLine("Book is currently borrowed");
                        }

                        break;

                    case 2:
                        Console.WriteLine("Which book would you like to return");
                        UtilityClass<Book>.dump(AccountManager.booksInAccount(userAccount));
                }

            } while (userInput != 4);

        }

        void roomServices()
        {

        }
    }
}
