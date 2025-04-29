using BookStore.Application.DTOs;
using BookStore.Application.Interfaces;
using BookStore.Domain.Entities;
using BookStore.Domain.Interfaces;

namespace BookStore.Application.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;

        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<IEnumerable<BookDto>> GetAllBooksAsync()
        {
            var books = await _bookRepository.GetAllBooksAsync();
            return books.Select(MapToDto);
        }

        public async Task<BookDto?> GetBookByIdAsync(int id)
        {
            var book = await _bookRepository.GetBookByIdAsync(id);
            return book != null ? MapToDto(book) : null;
        }

        public async Task<BookDto> AddBookAsync(BookDto bookDto)
        {
            var book = MapToEntity(bookDto);
            var addedBook = await _bookRepository.AddBookAsync(book);
            return MapToDto(addedBook);
        }

        public async Task<BookDto> UpdateBookAsync(int id, BookDto bookDto)
        {
            var book = MapToEntity(bookDto);
            var updatedBook = await _bookRepository.UpdateBookAsync(id, book);
            return MapToDto(updatedBook);
        }

        public async Task<bool> DeleteBookAsync(int id)
        {
            return await _bookRepository.DeleteBookAsync(id);
        }

        // Helper methods for mapping
        private static BookDto MapToDto(Book book)
        {
            return new BookDto
            {
                Id = book.Id,
                Title = book.Title,
                Description = book.Description,
                PageCount = book.PageCount,
                Excerpt = book.Excerpt,
                PublishDate = book.PublishDate
            };
        }

        private static Book MapToEntity(BookDto bookDto)
        {
            return new Book
            {
                Id = bookDto.Id,
                Title = bookDto.Title,
                Description = bookDto.Description,
                PageCount = bookDto.PageCount,
                Excerpt = bookDto.Excerpt,
                PublishDate = bookDto.PublishDate
            };
        }
    }
}