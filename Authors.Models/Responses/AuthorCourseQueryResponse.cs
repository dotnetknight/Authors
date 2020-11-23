using Authors.Models.Models;

namespace Authors.Models.Responses
{
    public class AuthorCourseQueryResponse : BaseResponse
    {
        public CourseDto Course { get; set; }
    }
}
