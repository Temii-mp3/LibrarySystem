using LibrarySystem.Models;
using Microsoft.EntityFrameworkCore;
using System;
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

                        Console.WriteLine("Creating User......");

                        if (library.Accounts.Any(f => f.Email == email))
                        {
                            throw new AccountExistsException("Account Exists");
                        }
                        userAccount = new Account
                        {
                            Email = email,
                            Password = password,
                            Username = tempUser
                        };

                        library.Accounts.Add(userAccount);
                        library.SaveChanges();
                        Console.WriteLine("Account Created Successfully");
                        user = tempUser;
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
                                break;
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
                        string isbnInput;
                        try
                        {
                            do
                            {
                                Console.WriteLine("Which Book would you like to Borrow? type ISBN");
                                library.Books.ForEachAsync(Console.WriteLine);
                                isbnInput = Console.ReadLine();
                                book = library.Books.FirstOrDefault(b => b.Isbn == isbnInput);
                            } while (book is null);
                        }
                        catch (BookNotFoundException e)
                        {
                            Console.WriteLine(e.Message);
                            break;
                        }

                        if (book.BorrowedBy is not null && userAccount is not null)
                        {
                            book.BorrowedBy = userAccount.Id;
                            Console.WriteLine($"Book borrowed!");
                        }
                        else
                        {
                            Console.WriteLine("An error occured, book may be borrowed or no valid account");
                        }

                        break;

                    case 2:
                        string isbn;
                        Book tempBook;
                        try
                        {
                            Console.WriteLine("Which book would you like to return");
                            library.Books.ForEachAsync(Console.WriteLine);
                            isbn = Console.ReadLine();
                            book = library.Books.FirstOrDefault(b => b.Isbn == isbn);
                            if (book is null)
                                throw new BookNotFoundException("Book not found");
                        }
                        catch (BookNotFoundException e)
                        {
                            Console.WriteLine(e.Message);
                            break;
                        }
                        if (book is not null && userAccount is not null)
                        {
                            book.BorrowedBy = null;
                            Console.WriteLine("Book returned successfully");
                            break;
                        }

                        throw new GenericException("Something went wrong");

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
                        Console.WriteLine("Which room would you like to Borrow? type ID");
                        library.Rooms.ForEachAsync(Console.WriteLine);

                        string idInput = Console.ReadLine();
                        Room? room = library.Rooms.FirstOrDefault(r => r.Id == idInput);

                        while (room == null)
                        {
                            Console.WriteLine("Invalid ID. Try Again");
                            idInput = Console.ReadLine();
                            room = library.Rooms.FirstOrDefault(r => r.Id == idInput);
                        }

                        if (room.Bookedby is null && userAccount is null)
                        {
                            room.Bookedby = userAccount.Id;
                            
                            Console.WriteLine($"Room booked by user {userAccount.Username}");
                        }
                        else
                        {
                            Console.WriteLine("An err occurred, room is currently booked or invalid account");
                        }

                        break;

                    case 2:
                        Console.WriteLine("Which room would you like to checkout");
                        library.Rooms.ForEachAsync(Console.WriteLine);
                        idInput = Console.ReadLine();
                        room = library.Rooms.FirstOrDefault(r => r.Id == idInput);
                        if (room is null)
                            throw new RoomNotFoundException("Room not found");

                        if (userAccount is not null)
                        {
                            room.Bookedby = null;
                            Console.WriteLine("Room checked out successfully!");
                            break;
                        }
                        else
                            throw new GenericException("Something went wrong");
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
        return false;
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
