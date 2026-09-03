using LibraryDomain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Infrastructure.Repositories
{
    public class BookRepository : IBookRepository
    {
        LibraryContext _context;

        public BookRepository(LibraryContext context)
        {
            _context = context;
        }


        public async Task<Book> AddBookToAccount(int id, Book b)
        {
            b.BorrowedBy = id;
            if (await _context.SaveChangesAsync() >= 1)
                return b;
            throw new GenericException();
        }

        public async Task<List<Book>> BooksInAccount(Account a)
        {
            List<Book> books = a.Books.ToList();

            return books;
        }

        public async Task<Book> ReturnBook(Book b)
        {
            b.BorrowedBy = null;

            if (await _context.SaveChangesAsync() >= 1)
                return b;
            throw new GenericException();
        }
        public void PrintBooks()
        {
            _context.Books.ForEachAsync(Console.WriteLine);
        }

        public async Task<Book> GetBookfromDb(string isbn)
        {
            Book? book = await _context.Books.FirstOrDefaultAsync(b => b.Isbn == isbn);
            if (book is null)
                throw new BookNotFoundException();
            return book;
        }


        public void PrintBorrowedBooks(Account user)
        {
            foreach (var item in user.Books)
            {
                Console.WriteLine(item);
            }
        }

        public async Task<Book> AddBookToDB(Book b)
        {
            try
            {
                await _context.Books.AddAsync(b);
                if (await _context.SaveChangesAsync() >= 1)
                    return b;
            }
            catch (DbUpdateException)
            {
                throw new GenericException("Something went wrong");
            }
            throw new GenericException("Something went wrong");
        }

    }

}
