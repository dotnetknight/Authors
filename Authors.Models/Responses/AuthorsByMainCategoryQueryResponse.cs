﻿using Authors.Models.Models;
using System.Collections.Generic;

namespace Authors.Models.Responses
{
    public class AuthorsByMainCategoryQueryResponse : BaseResponse
    {
        public IEnumerable<AuthorDto> Authors { get; set; }
    }
}
