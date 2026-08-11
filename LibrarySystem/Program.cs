using System;
using System.Text.RegularExpressions;
using System.Xml.Linq;

public class Program
{
    static void Main(String[] args)
    {
        int userInput;
        string? user = "";
        string? email = "";
        string? password = "";
        string? tempUser = "";
        char input=' ';

        /*^ start of string

        [\w\.-]+one or more word characters, dots, or hyphens

        @ literal at symbol

        [\w\.-]+ domain name part

        \. literal dot

        \w+ top - level domain

        $ end of string*/


        Account? userAccount = null;
        Library lib = new Library();
        Random rand = new Random();
        do
        {
            mainMenu();
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

                    while (!checkEmail(email))
                    {
                        Console.WriteLine("Invalid Email");
                        email = Console.ReadLine();
                    }


                    Console.WriteLine("Enter Username");
                    tempUser = Console.ReadLine();


                    while (checkUser(tempUser))
                    {
                        Console.WriteLine("Invalid Username");
                        tempUser = Console.ReadLine();
                    }
                    Console.WriteLine("Enter Password");
                    password = Console.ReadLine();

                    while (checkPassword(password))
                    {
                        Console.WriteLine("Invalid Password");
                        password = Console.ReadLine();
                    }

                    int id = rand.Next(1, 1000);
                    Console.WriteLine("Creating User......");
                    userAccount = new Account(email, tempUser, password, id);
                    AccountManager.addAccount(userAccount);
                    Console.WriteLine("Account Created Successfully");
                    user = tempUser;
                    break;
                case 2:
                    do
                    {
                        try
                        {
                            Console.WriteLine("Log in Console press 0 to quit");
                            Console.WriteLine("Enter Email");
                            email = Console.ReadLine();

                            if (email is "0")
                            {
                                break;
                            }
                            while (!checkEmail(email))
                            {
                                Console.WriteLine("Invalid Email");
                                email = Console.ReadLine();
                            }

                            Console.WriteLine("Enter Password");
                            password = Console.ReadLine();

                            while (!checkPassword(password))
                            {
                                Console.WriteLine("Invalid Password");
                                password = Console.ReadLine();
                            }

                            Console.WriteLine("Logging in....");

                            Account tempAcc = AccountManager.LookupAccount(email, password);
                            userAccount = tempAcc;
                            user = userAccount.Username;
                        }
                        catch (AccountNotFoundException e)
                        {
                            Console.WriteLine(e.Message);
                        }
                    } while (userAccount is null);

                    break;
                case 3:
                    bookServices();
                    break;

                case 4:
                    roomServices();
                    break;
                case 5:
                    Console.WriteLine("Are you sure you want to exit? y/n");
                    string? choice = Console.ReadLine();
                    if(choice is not null)
                    {
                        input = Convert.ToChar(choice);
                    }

                    char.ToLower(input);
                    if (input == 'y')
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
                        Book? book;
                        int isbnInput;
                        try
                        {
                            do
                            {
                                Console.WriteLine("Which Book would you like to Borrow? type ISBN");
                                UtilityClass<Book>.dump(lib.Books);
                                isbnInput = Convert.ToInt32(Console.ReadLine());
                                book = lib.getBook(isbnInput);
                            } while (book is null);
                        }
                        catch (BookNotFoundException e)
                        {
                            Console.WriteLine(e.Message);
                            break;
                        }

                        if (book.CanBorrow)
                        {
                            AccountManager.addBookToAccount(lib.getBook(isbnInput), userAccount);
                            Console.WriteLine($"Book borrowed by user {userAccount.Username}");
                        }
                        else
                        {
                            Console.WriteLine("An error occured, book may be borrowed or no valid account");
                        }

                        break;

                    case 2:
                        int isbn;
                        Book tempBook;
                                try
                                {
                                    Console.WriteLine("Which book would you like to return");
                                    UtilityClass<Book>.dump(AccountManager.booksInAccount(userAccount));
                                    isbn = Convert.ToInt32(Console.ReadLine());
                                    tempBook = AccountManager.returnBook(isbn, userAccount);
                                }catch(BookNotFoundException e)
                                {
                                    Console.WriteLine("Book not found");
                                    break;
                                }
                        Console.WriteLine("Book returned successfully");
                        break;

                    case 3:
                        UtilityClass<Book>.dump(AccountManager.booksInAccount(userAccount));
                        break;
                }

            } while (userInput != 4);

        }

        void roomServices()
        {
            do
            {
                Console.WriteLine("Welcome to room services, what would you like to do?" +
                                "\n1. Book room" +
                                "\n 2. Checkout room" +
                                "\n 3. Booked rooms" +
                                "\n 4. Go Back");

                userInput = Convert.ToInt32(Console.ReadLine());
                switch (userInput)
                {
                    case 1:
                        Console.WriteLine("Which room would you like to Borrow? type ID");
                        UtilityClass<Room>.dump(lib.Rooms);
                        int idInput = Convert.ToInt32(Console.ReadLine());
                        Room? room = lib.getRoom(idInput);
                        while (room == null)
                        {
                            Console.WriteLine("Invalid ISBN. Try Again");
                            idInput = Convert.ToInt32(Console.ReadLine());
                            room = lib.getRoom(idInput);
                        }

                        if (room.CanBook)
                        {
                            AccountManager.addRoomToAccount(lib.getRoom(idInput), userAccount);
                            Console.WriteLine($"Room booked by user {userAccount.Username}");
                        }
                        else
                        {
                            Console.WriteLine("An err occurred, room is currently booked or invalid account");
                        }

                        break;

                    case 2:
                        Console.WriteLine("Which room would you like to checkout");
                        UtilityClass<Room>.dump(AccountManager.roomsInAccount(userAccount));
                        int id = Convert.ToInt32(Console.ReadLine());
                        if (AccountManager.checkoutRoom(id, userAccount) == -1)
                        {
                            Console.WriteLine("An Error Occured");
                        }
                        else
                        {
                            Console.WriteLine("Room checked out successfully!");
                        }
                        break;

                    case 3:
                        UtilityClass<Room>.dump(AccountManager.roomsInAccount(userAccount));
                        break;
                }

            } while (userInput != 4);
        }

        void mainMenu()
        {
            Console.WriteLine("Hello, Welcome to the Library! what would you like to do: \n" +
                                "1. Create an account\n" +
                                "2. Log in to an account\n" +
                                "3. Book Services\n" +
                                "4. Room Services\n" +
                                "5. Log out\n" +
                                "6. Quit");
            Console.WriteLine($"Currently Logged in as: {(string.IsNullOrEmpty(user) ? "Guest" : user)}");
        }
    }

    static bool checkEmail(string? email)
    {
        if (email is not null)
        {
            return Regex.IsMatch(email, @"^[\w\.-]+@[\w\.-]+\.\w+$");
        }

    static bool checkPassword(string password)
    {
        if (Regex.IsMatch(password, @"^\w{8,}$"))
        {
            return true;
        }


        return false;
    }

    static bool checkUser(String user)
    {
        if (Regex.IsMatch(user, @"^\w{3,}$"))
        {
            return true;
        }

        return false;
    }
}
}
