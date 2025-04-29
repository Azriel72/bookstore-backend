using BookStore.Domain.Common;
using BookStore.Domain.Entities;
using bookstore_backend.BookStore.Domain.Common;
using bookstore_backend.BookStore.Domain.Interfaces;

namespace BookStore.Domain.Interfaces
{
    public interface IAuthorRepository : IPaginationRepository<Author>
    {
        Task<PagedResult<Author>> GetAllAuthorsAsync(PaginationParameters parameters);
        Task<Author?> GetAuthorByIdAsync(int id);
        Task<Author> AddAuthorAsync(Author author);
        Task<Author> UpdateAuthorAsync(int id, Author author);
        Task<bool> DeleteAuthorAsync(int id);
    }
}