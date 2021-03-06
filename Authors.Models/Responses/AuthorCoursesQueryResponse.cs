﻿using Authors.Models.Models;
using System.Collections.Generic;

namespace Authors.Models.Responses
{
    public class AuthorCoursesQueryResponse : BaseResponse
    {
        public IEnumerable<CourseDto> Courses { get; set; }
    }
}
