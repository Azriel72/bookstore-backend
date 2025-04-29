using BookStore.Domain.Entities;
using BookStore.Domain.Interfaces;
using BookStore.Infrastructure.ExternalServices;

namespace BookStore.Infrastructure.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly FakeRestApiClient _apiClient;

        public BookRepository(FakeRestApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task<IEnumerable<Book>> GetAllBooksAsync()
        {
            var books = await _apiClient.GetAllAsync<Book>("Books");
            return books ?? Enumerable.Empty<Book>();
        }

        public async Task<Book?> GetBookByIdAsync(int id)
        {
            return await _apiClient.GetAsync<Book>($"Books/{id}");
        }

        public async Task<Book> AddBookAsync(Book book)
        {
            var addedBook = await _apiClient.PostAsync("Books", book);
            return addedBook ?? book;
        }

        public async Task<Book> UpdateBookAsync(int id, Book book)
        {
            var updatedBook = await _apiClient.PutAsync($"Books/{id}", book);
            return updatedBook ?? book;
        }

        public async Task<bool> DeleteBookAsync(int id)
        {
            return await _apiClient.DeleteAsync($"Books/{id}");
        }
    }
}
