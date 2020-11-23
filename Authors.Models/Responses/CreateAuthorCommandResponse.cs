using System;

namespace Authors.Models.Responses
{
    public class CreateAuthorCommandResponse : BaseResponse
    {
        public Guid Id { get; set; }
    }
}
