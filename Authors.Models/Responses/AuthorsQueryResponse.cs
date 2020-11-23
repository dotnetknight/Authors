using Authors.Models.Models;
using System.Collections.Generic;

namespace Authors.Models.Responses
{
    public class AuthorsQueryResponse : BaseResponse
    {
        public IEnumerable<AuthorDto> Authors { get; set; }
    }
}
