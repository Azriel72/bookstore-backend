using BookStore.Application.DTOs;
using BookStore.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace bookstore_backend.BookStore.API.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorService _authorService;
        public AuthorsController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        /// <summary>
        /// Get all authors with pagination support
        /// </summary>
        /// <param name="pageNumber">Page number (starts from 1)</param>
        /// <param name="pageSize">Number of items per page</param>
        /// <returns>Paginated list of authors</returns>
        [HttpGet]
        public async Task<ActionResult<PaginatedResponse<AuthorDto>>> GetAllAuthors(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 9)
        {
            if (pageNumber < 1)
                return BadRequest("Page number must be greater than or equal to 1.");

            if (pageSize < 1 || pageSize > 100)
                return BadRequest("Page size must be between 1 and 100.");

            var authors = await _authorService.GetAllAuthorsAsync();

            // Apply pagination using LINQ
            var totalCount = authors.Count();
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            var paginatedAuthors = authors
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            // Create paginated response
            var response = new PaginatedResponse<AuthorDto>
            {
                Items = paginatedAuthors,
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
        public async Task<ActionResult<AuthorDto>> GetAuthorById(int id)
        {
            var author = await _authorService.GetAuthorByIdAsync(id);
            if (author == null)
            {
                return NotFound();
            }
            return Ok(author);
        }

        [HttpPost]
        public async Task<ActionResult<AuthorDto>> AddAuthor(AuthorDto authorDto)
        {
            var newAuthor = await _authorService.AddAuthorAsync(authorDto);
            return CreatedAtAction(nameof(GetAuthorById), new { id = newAuthor.Id }, newAuthor);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<AuthorDto>> UpdateAuthor(int id, AuthorDto authorDto)
        {
            try
            {
                var updatedAuthor = await _authorService.UpdateAuthorAsync(id, authorDto);
                return Ok(updatedAuthor);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAuthor(int id)
        {
            var result = await _authorService.DeleteAuthorAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}