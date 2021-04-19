using BookStore.Core;
using BookStore.Core.Models;
using BookStore.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Services
{
    public class BookService : IBookService
    {
        private readonly IUnitOfWork _unitOfWork;
        public BookService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }


        public async Task<Book> Create(Book book)
        {
            await _unitOfWork.Books.AddAsync(book);
            await _unitOfWork.CommitAsync();
            return book;
        }

        public async Task Delete(Book book)
        {
            _unitOfWork.Books.Remove(book);
            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<Book>> GetAllWithAuthors()
        {
            return await _unitOfWork.Books.GetAllWithAuthorAsync();
        }

        public async Task<Book> GetBookById(int id)
        {
            return await _unitOfWork.Books.GetWithAuthorByIdAsync(id);
        }

        public async Task<IEnumerable<Book>> GetBooksByArtistId(int authorId)
        {
            return await _unitOfWork.Books.GetAllWithAuthorByAuthorIdAsync(authorId);
        }

        public async Task Update(Book book, Book updatedBook)
        {
            updatedBook.Name = book.Name;
            updatedBook.AuthorId = book.AuthorId;

            await _unitOfWork.CommitAsync();
        }
    }
}
