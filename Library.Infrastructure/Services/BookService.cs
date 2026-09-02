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
        public async Task<Book> ReturnBook(string isbn, Account a)
        {
            Account? user = await account_repo.LookupAccount(a);

            if (user is null)
                throw new AccountNotFoundException();
            Book? book = user.Books.FirstOrDefault(b => b.Isbn == isbn);
            if (book is null)
                throw new BookNotFoundException();
            book.BorrowedBy = null;
            return book;

        }

        public async Task<ICollection<Book>> BooksInAccount(Account a)
        {
            Account? user = await account_repo.LookupAccount(a);
            if (user is null)
                throw new AccountNotFoundException("Account not found");

            return user.Books;


        }
        public async Task<Book> AddBookToAccount(Book b, Account a)
        {
            Account? user = await account_repo.LookupAccount(a);
            if (user is null)
                throw new AccountNotFoundException("Account not found");
            if (user.Books.Count > BOOKLIMIT)
                throw new BookLimitReachedException($"Book Limit of {BOOKLIMIT} has been reached");
            b.BorrowedBy = user.Id;
            return b;
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

            Book result = book_repo.AddBookToDB(book);

            if (result is null)
                throw new GenericException();
            return result;
        }
    }
}
