using Authors.CQRS.Interfaces;

namespace Authors.Models.Commands
{
    public class UpdateCourseForAuthorCommand : ICommand
    {
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
