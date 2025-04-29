using BookStore.Application.DTOs;

namespace BookStore.Application.Interfaces
{
    public interface IAuthorService
    {
        Task<IEnumerable<AuthorDto>> GetAllAuthorsAsync();
        Task<AuthorDto?> GetAuthorByIdAsync(int id);
        Task<AuthorDto> AddAuthorAsync(AuthorDto authorDto);
        Task<AuthorDto> UpdateAuthorAsync(int id, AuthorDto authorDto);
        Task<bool> DeleteAuthorAsync(int id);
    }
}