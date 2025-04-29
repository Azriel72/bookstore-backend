﻿namespace BookStore.Application.DTOs
{
    public class AuthorDto
    {
        public int Id { get; set; }
        public int IdBook { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
    }
}