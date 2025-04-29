using BookStore.Domain.Common;
using BookStore.Domain.Entities;
using bookstore_backend.BookStore.Domain.Common;
using bookstore_backend.BookStore.Domain.Interfaces;

namespace BookStore.Domain.Interfaces
{
    public interface IBookRepository : IPaginationRepository<Book>
    {
        Task<PagedResult<Author>> GetAllBooksAsync(PaginationParameters parameters);
        Task<Book?> GetBookByIdAsync(int id);
        Task<Book> AddBookAsync(Book book);
        Task<Book> UpdateBookAsync(int id, Book book);
        Task<bool> DeleteBookAsync(int id);
    }
}