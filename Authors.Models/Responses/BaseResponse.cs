using Authors.Models.Models;
using System.Collections.Generic;

namespace Authors.Models.Responses
{
    public class BaseResponse
    {
        public IEnumerable<LinkModel> Links { get; set; } = null;
    }
}
