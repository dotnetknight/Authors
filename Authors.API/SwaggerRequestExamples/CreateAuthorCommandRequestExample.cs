using Authors.Models.Commands;
using Swashbuckle.AspNetCore.Filters;
using System.Collections.Generic;

namespace Authors.API.SwaggerRequestExamples
{
    public class CreateAuthorCommandRequestExample : IExamplesProvider<CreateAuthorCommand>
    {
        public CreateAuthorCommand GetExamples()
        {
            return new CreateAuthorCommand
            {
                FirstName = "Thomas",
                LastName = "Mann",
                MainCategory = "Philosophy",
                Courses = new List<CoursesDto>()
            };
        }
    }
}
