using BookStore.Application.DTOs;
using BookStore.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Linq; // Añadido explícitamente

namespace bookstore_backend.BookStore.API.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;
        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }
        /// <summary>
        /// Get all books with pagination support
        /// </summary>
        /// <param name="pageNumber">Page number (starts from 1)</param>
        /// <param name="pageSize">Number of items per page</param>
        /// <returns>Paginated list of books</returns>
        [HttpGet]
        public async Task<ActionResult<PaginatedResponse<BookDto>>> GetAllBooks(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 9)
        {
            if (pageNumber < 1)
                return BadRequest("Page number must be greater than or equal to 1.");
            if (pageSize < 1 || pageSize > 100)
                return BadRequest("Page size must be between 1 and 100.");
            var books = await _bookService.GetAllBooksAsync();
            // Apply pagination using LINQ
            var totalCount = books.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize); // Corregido el cálculo
            var paginatedBooks = books
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();
            // Create paginated response
            var response = new PaginatedResponse<BookDto>
            {
                Items = paginatedBooks,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount,
                TotalPages = totalPages,
                HasNextPage = pageNumber < totalPages,
                HasPreviousPage = pageNumber > 1
            };
            return Ok(response);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<BookDto>> GetBookById(int id)
        {
            var book = await _bookService.GetBookByIdAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }
        [HttpPost]
        public async Task<ActionResult<BookDto>> AddBook(BookDto bookDto)
        {
            var newBook = await _bookService.AddBookAsync(bookDto);
            return CreatedAtAction(nameof(GetBookById), new { id = newBook.Id }, newBook);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<BookDto>> UpdateBook(int id, BookDto bookDto)
        {
            try
            {
                var updatedBook = await _bookService.UpdateBookAsync(id, bookDto);
                return Ok(updatedBook);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBook(int id)
        {
            var result = await _bookService.DeleteBookAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}