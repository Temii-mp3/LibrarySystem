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
        LibraryContext library = new();


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

                        if (library.Accounts.Any(f => f.Email == email))
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
                            library.Accounts.Add(userAccount);
                            library.SaveChanges();
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
                            userAccount = library.Accounts.SingleOrDefault(account => account.Email == email);
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
                            library.Accounts.Remove(userAccount);
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
                        string? isbnInput;
                        try
                        {
                            if (userAccount is null)
                                throw new LoginException("You need to log in");
                            Console.WriteLine("Which Book would you like to Borrow? type ISBN");
                            library.Books.ForEachAsync(Console.WriteLine);
                            isbnInput = Console.ReadLine();
                            book = library.Books.FirstOrDefault(b => b.Isbn == isbnInput);
                            if (book is null)
                                throw new BookNotFoundException("Book not found");
                            if (book.BorrowedBy is null)
                            {
                                book.BorrowedBy = userAccount.Id;
                                Console.WriteLine($"Book borrowed!");
                                library.SaveChanges();
                            }
                            else
                                Console.WriteLine("An error occured, book is borrowed or no valid account");
                        }
                        catch (BookNotFoundException e)
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
                            library.Books.ForEachAsync(Console.WriteLine);
                            isbnInput = Console.ReadLine();
                            book = library.Books.FirstOrDefault(b => b.Isbn == isbnInput);
                            if (book is null)
                                throw new BookNotFoundException("Book not found");
                            if (book is not null)
                            {
                                book.BorrowedBy = null;
                                Console.WriteLine("Book returned successfully");
                                library.SaveChanges();

                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);

                        }
                        break;


                    case 3:
                        library.Books.ForEachAsync(Console.WriteLine);
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
                        try
                        {
                            if (userAccount is null)
                                throw new LoginException("You need to log in");

                            Console.WriteLine("Which room would you like to Borrow? type ID");
                            library.Rooms.ForEachAsync(Console.WriteLine);

                            string? idInput = Console.ReadLine();
                            Room? room = library.Rooms.FirstOrDefault(r => r.Id == idInput);

                            if (room == null)
                                throw new RoomNotFoundException("Room not found");

                            if (room.Bookedby is null)
                            {
                                room.Bookedby = userAccount.Id;
                                Console.WriteLine($"Room booked by user {userAccount.Username}");
                                library.SaveChanges();

                            }
                            else
                            {
                                Console.WriteLine("An err occurred, room is currently booked");
                            }
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
                            library.Rooms.ForEachAsync(Console.WriteLine);
                            idInput = Console.ReadLine();
                            room = library.Rooms.FirstOrDefault(r => r.Id == idInput);
                            if (room is null)
                                throw new RoomNotFoundException("Room not found");
                            room.Bookedby = null;
                            Console.WriteLine("Room checked out successfully!");
                            library.SaveChanges();
                            break;
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                            break;
                        }

                    case 3:
                        library.Rooms.ForEachAsync(Console.WriteLine);
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
