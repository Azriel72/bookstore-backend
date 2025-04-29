using BookStore.Domain.Entities;

namespace BookStore.Domain.Interfaces
{
    public interface IAuthorRepository
    {
        Task<IEnumerable<Author>> GetAllAuthorsAsync();
        Task<Author?> GetAuthorByIdAsync(int id);
        Task<Author> AddAuthorAsync(Author author);
        Task<Author> UpdateAuthorAsync(int id, Author author);
        Task<bool> DeleteAuthorAsync(int id);
    }
}