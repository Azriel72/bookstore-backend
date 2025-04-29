using BookStore.Domain.Entities;
using BookStore.Domain.Interfaces;
using BookStore.Infrastructure.ExternalServices;

namespace BookStore.Infrastructure.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly FakeRestApiClient _apiClient;

        public AuthorRepository(FakeRestApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task<IEnumerable<Author>> GetAllAuthorsAsync()
        {
            var authors = await _apiClient.GetAllAsync<Author>("Authors");
            return authors ?? Enumerable.Empty<Author>();
        }

        public async Task<Author?> GetAuthorByIdAsync(int id)
        {
            return await _apiClient.GetAsync<Author>($"Authors/{id}");
        }

        public async Task<Author> AddAuthorAsync(Author author)
        {
            var addedAuthor = await _apiClient.PostAsync("Authors", author);
            return addedAuthor ?? author;
        }

        public async Task<Author> UpdateAuthorAsync(int id, Author author)
        {
            var updatedAuthor = await _apiClient.PutAsync($"Authors/{id}", author);
            return updatedAuthor ?? author;
        }

        public async Task<bool> DeleteAuthorAsync(int id)
        {
            return await _apiClient.DeleteAsync($"Authors/{id}");
        }
    }
}