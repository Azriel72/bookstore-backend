using BookStore.Application.DTOs;
using BookStore.Application.Interfaces;
using BookStore.Domain.Entities;
using BookStore.Domain.Interfaces;

namespace BookStore.Application.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _authorRepository;

        public AuthorService(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task<IEnumerable<AuthorDto>> GetAllAuthorsAsync()
        {
            var authors = await _authorRepository.GetAllAuthorsAsync();
            return authors.Select(MapToDto);
        }

        public async Task<AuthorDto?> GetAuthorByIdAsync(int id)
        {
            var author = await _authorRepository.GetAuthorByIdAsync(id);
            return author != null ? MapToDto(author) : null;
        }

        public async Task<AuthorDto> AddAuthorAsync(AuthorDto authorDto)
        {
            var author = MapToEntity(authorDto);
            var addedAuthor = await _authorRepository.AddAuthorAsync(author);
            return MapToDto(addedAuthor);
        }

        public async Task<AuthorDto> UpdateAuthorAsync(int id, AuthorDto authorDto)
        {
            var author = MapToEntity(authorDto);
            var updatedAuthor = await _authorRepository.UpdateAuthorAsync(id, author);
            return MapToDto(updatedAuthor);
        }

        public async Task<bool> DeleteAuthorAsync(int id)
        {
            return await _authorRepository.DeleteAuthorAsync(id);
        }

        // Helper methods for mapping
        private static AuthorDto MapToDto(Author author)
        {
            return new AuthorDto
            {
                Id = author.Id,
                IdBook = author.IdBook,
                FirstName = author.FirstName,
                LastName = author.LastName
            };
        }

        private static Author MapToEntity(AuthorDto authorDto)
        {
            return new Author
            {
                Id = authorDto.Id,
                IdBook = authorDto.IdBook,
                FirstName = authorDto.FirstName,
                LastName = authorDto.LastName
            };
        }
    }
}