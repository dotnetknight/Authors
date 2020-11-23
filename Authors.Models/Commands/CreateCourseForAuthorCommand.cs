using Authors.CQRS.Interfaces;
using System;

namespace Authors.Models.Commands
{
    public class CreateCourseForAuthorCommand : ICommand
    {
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
