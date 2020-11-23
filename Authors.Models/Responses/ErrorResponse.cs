using Authors.Models.Models;
using System.Collections.Generic;

namespace Authors.Models.Responses
{
    public class ErrorResponse
    {
        public List<ErrorModel> Errors { get; set; } = new List<ErrorModel>();
    }
}
