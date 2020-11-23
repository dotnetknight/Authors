using System;
using System.Collections.Generic;
using Authors.Models.Models;
using Microsoft.AspNetCore.Mvc;

namespace Authors.API.Controllers
{
    [Route("api")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    [Produces("application/json")]
    public class RootController : ControllerBase
    {
        /// <summary>
        /// Creates root links for application
        /// </summary>
        /// <returns></returns>
        [HttpGet(Name = "GetRoot")]
        public IActionResult GetRoot()
        {
            var links = new List<LinkModel>
            {
                new LinkModel(Url.Link("GetRoot", new { }),
                "self",
                "GET"
                ),

                new LinkModel(Url.Link("Author", new { AuthorId = new Guid() }),
                "get_author",
                "GET"
                ),

                new LinkModel(Url.Link("Authors", new { }),
                "list_of_authors",
                "GET"
                ),

                new LinkModel(Url.Link("CreateAuthor", new { }),
                "create_author",
                "POST"
                ),

                new LinkModel(Url.Link("Courses", new { }),
                "author_courses",
                "GET"
                ),

                new LinkModel(Url.Link("CreateCourseForAuthor", new { }),
                "create_course_for_author",
                "POST"
                ),

                new LinkModel(Url.Link("UpdateCourseForAuthor", new { }),
                "update_course_for_author",
                "POST"
                ),

                new LinkModel(Url.Link("Options", new { }),
                "authors_controller_options",
                "OPTIONS"
                ),
            };

            return Ok(links);
        }
    }
}