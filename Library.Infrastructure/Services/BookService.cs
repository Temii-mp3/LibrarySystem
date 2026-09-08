using LibraryDomain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Infrastructure.Services
{

    public class BookService : IBookService
    {
        const int BOOKLIMIT = 5;
        private readonly IBookRepository book_repo;
        private readonly IAccountRepository account_repo;
        public BookService(IBookRepository _book_repo, IAccountRepository _account_repo)
        {
            book_repo = _book_repo;
            account_repo = _account_repo;
        }
        public async Task<Book> ReturnBook(string isbn)
        {
            Book? book = await book_repo.GetBookfromDb(isbn);
            if (book is null)
                throw new BookNotFoundException();

            Book result = await book_repo.ReturnBook(book);
            if (result is not null)
                return book;
            throw new GenericException();
        }

        public async Task<ICollection<Book>> BooksInAccount(Account a)
        {
            Account? user = await account_repo.LookupAccount(a);
            if (user is null)
                throw new AccountNotFoundException("Account not found");

            return user.Books;


        }
        public async Task<Book> AddBookToAccount(string isbn, string email)
        {
            Book? book = await book_repo.GetBookfromDb(isbn);
            if (book is null)
                throw new BookNotFoundException();
            Account? account = await account_repo.LookupAccount(email);
            if (account is null)
                throw new AccountNotFoundException();
            Book result = await book_repo.AddBookToAccount(book, account.Id);
            if (result is not null)
                return result;
            throw new GenericException();
        }

        public async Task<Book> AddBookToLibrary(string isbn, string author, string name)
        {
            if (isbn is null || author is null || name is null)
                throw new GenericException();
            Book book = new Book
            {
                Isbn = isbn,
                Author = author,
                Name = name
            };

            Book result = await book_repo.AddBookToDB(book);

            if (result is null)
                throw new GenericException();
            return result;
        }

        public async Task<ICollection<Book>> GetAllBooks()
        {

            ICollection<Book> books = await book_repo.GetAllBooks();
            return books;
        }

        public async Task<Book> DeleteBook(string isbn)
        {
            Book book = await book_repo.GetBookfromDb(isbn);
            if (book is null)
                throw new BookNotFoundException();
            Book result = await book_repo.DeleteBook(book);
            if (result is null)
                throw new GenericException();
            return result;

        }
    }
}
