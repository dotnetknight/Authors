using Authors.CQRS.Interfaces;
using System.Collections.Generic;

namespace Authors.Models.Commands
{
    public class CreateAuthorCommand : ICommand
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MainCategory { get; set; }
        public ICollection<CoursesDto> Courses { get; set; } = new List<CoursesDto>();
    }

    public class CoursesDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
