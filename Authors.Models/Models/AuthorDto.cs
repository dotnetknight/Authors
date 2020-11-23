using System;

namespace Authors.Models.Models
{
    public class AuthorDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string MainCategory { get; set; }
    }
}
