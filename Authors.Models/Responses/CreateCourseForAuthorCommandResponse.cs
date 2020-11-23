using System;

namespace Authors.Models.Responses
{
    public class CreateCourseForAuthorCommandResponse : BaseResponse
    {
        public Guid AuthorId { get; set; }
        public Guid CourseId { get; set; }
    }
}
