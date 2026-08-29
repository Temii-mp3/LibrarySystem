using LibrarySystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Data;
using System.Text.RegularExpressions;
using System.Xml.Linq;

public class Program
{

    static void Main(String[] args)
    {
        RunProgram();
    }
    static async void RunProgram()
    {
        LibraryContext context = new();
        AccountRepositry repo = new(context);
        AccountService service = new(repo);
        int userInput;
        string? user = "";
        string? email = "";
        string? password = "";
        string? tempUser = "";
        char input = ' ';

        /*^ start of string

        [\w\.-]+one or more word characters, dots, or hyphens

        @ literal at symbol

        [\w\.-]+ domain name part

        \. literal dot

        \w+ top - level domain

        $ end of string*/


        Account? userAccount = null;
        do
        {

            try
            {

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

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
                    try
                    {
                        Console.WriteLine("Enter Email");
                        email = Console.ReadLine();

                        while (!checkEmail(email))
                        {
                            Console.WriteLine("Invalid Email");
                            email = Console.ReadLine();
                        }


                        Console.WriteLine("Enter Username");
                        tempUser = Console.ReadLine();


                        while (!checkUser(tempUser))
                        {
                            Console.WriteLine("Invalid Username");
                            tempUser = Console.ReadLine();
                        }
                        Console.WriteLine("Enter Password");
                        password = Console.ReadLine();

                        while (!checkPassword(password))
                        {
                            Console.WriteLine("Invalid Password");
                            password = Console.ReadLine();
                        }

                        Console.WriteLine("Creating User......");

                        var result = repo.LookupAccount(email);

                        if (result.Result is not null)
                        {
                            throw new AccountExistsException("Account Exists");
                        }
                        if (email is not null && password is not null && tempUser is not null)
                        {
                            userAccount = new Account
                            {
                                Email = email,
                                Password = password,
                                Username = tempUser
                            };
                        }
                        else
                        {
                            Console.WriteLine("You are missing a field");
                        }

                        if (userAccount is not null)
                        {
                            await repo.AddAccount(userAccount);
                            Console.WriteLine("Account Created Successfully");
                            user = tempUser;
                        }
                        break;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        break;
                    }

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
                            userAccount = await repo.LookupAccount(email, password);
                            if (userAccount is null)
                            {
                                throw new AccountNotFoundException();
                            }
                            user = userAccount.Username;
                            break;
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
                    if (choice is not null)
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
                case 6:
                    try
                    {
                        Console.WriteLine("Are you sure you want to delete account?");
                        choice = Console.ReadLine();

                        if (choice is not null)
                            input = Convert.ToChar(choice);
                        if (input is 'y' && userAccount is not null)
                        {
                            Console.WriteLine("Deleting account...");
                            if (repo.DeleteAccount(userAccount) is not null)
                            {
                                Console.WriteLine("Account deleted");
                            }
                            throw new GenericException("Account could not be deleted");
                        }
                        else
                            throw new LoginException("You need to log in");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    break;
                default:
                    throw new InvalidInputException("Invalid input");

            }

        } while (userInput != 6);

        async void bookServices()
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
                        string? isbnInput;
                        try
                        {
                            if (userAccount is null)
                                throw new LoginException("You need to log in");
                            Console.WriteLine("Which Book would you like to Borrow? type ISBN");
                            repo.PrintBooks();
                            isbnInput = Console.ReadLine();
                            book = await repo.GetBookfromDb(isbnInput);
                            if (book is null)
                                throw new BookNotFoundException("Book not found");
                            if (book.BorrowedBy is not null)
                                throw new BookBorrowedException();
                            if (await repo.AddBookToAccount(userAccount, book) is not null)
                                Console.WriteLine("Book Borrowed Successfully!");

                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }



                        break;

                    case 2:
                        try
                        {
                            if (userAccount is null)
                                throw new LoginException("You need to log in");
                            Console.WriteLine("Which book would you like to return");
                            repo.PrintBorrowedBooks(userAccount);
                            isbnInput = Console.ReadLine();
                            if (await repo.ReturnBook(userAccount, isbnInput) is not null)
                                Console.WriteLine("Book Returned Successfully");
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);

                        }
                        break;


                    case 3:
                        repo.PrintBorrowedBooks(userAccount);
                        break;
                }

            } while (userInput != 4);

        }

        async void roomServices()
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
                        try
                        {
                            if (userAccount is null)
                                throw new LoginException("You need to log in");

                            Console.WriteLine("Which room would you like to Borrow? type ID");
                            repo.PrintRooms();

                            string? idInput = Console.ReadLine();
                            Room? room = await repo.GetRoomFromDb(idInput);

                            if (room == null)
                                throw new RoomNotFoundException("Room not found");
                            if (room.Bookedby is not null)
                                throw new RoomBookedException();
                            if (await repo.AddRoomToAccount(room, userAccount) is not null)
                                Console.WriteLine("Room Booked Successfully");
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);

                        }
                        break;


                    case 2:
                        try
                        {
                            if (userAccount is null)
                                throw new LoginException("You need to log in");
                            string? idInput;
                            Room? room;
                            Console.WriteLine("Which room would you like to checkout");
                            repo.PrintRooms();
                            idInput = Console.ReadLine();
                            if (await repo.CheckoutRoom(userAccount, idInput) is not null)
                                Console.WriteLine("Room Checkout Out Successfully");
                            break;
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                            break;
                        }

                    case 3:
                        repo.PrintBookedRooms(userAccount);
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
                                "6. Delete Account\n" +
                                "7. Quit");
            Console.WriteLine($"Currently Logged in as: {(string.IsNullOrEmpty(user) ? "Guest" : user)}");
        }
    }

    static bool checkEmail(string? email)
    {
        if (email is not null)
        {
            return Regex.IsMatch(email, @"^[\w\.-]+@[\w\.-]+\.\w+$");
        }
        return false;
    }

    static bool checkPassword(string? password)
    {
        if (password is not null)
        {
            return Regex.IsMatch(password, @"^\w{8,}$");
        }
        return false;
    }

    static bool checkUser(String? user)
    {
        if (user is not null)
        {
            return Regex.IsMatch(user, @"^\w{3,}$");
        }

        return false;
    }
}
