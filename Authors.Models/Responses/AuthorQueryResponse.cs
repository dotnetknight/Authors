using Authors.Models.Models;

namespace Authors.Models.Responses
{
    public class AuthorQueryResponse : BaseResponse
    {
        public AuthorDto Author { get; set; }
    }
}
