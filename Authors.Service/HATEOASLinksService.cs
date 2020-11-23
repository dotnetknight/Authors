using Authors.Models.Models;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using System;
using System.Collections.Generic;

namespace Authors.Service
{
    public class HATEOASLinksService
    {

        #region Properties
        private IUrlHelperFactory _urlHelperFactory;
        private IActionContextAccessor _actionContextAccessor;

        #endregion

        #region Constructor

        public HATEOASLinksService(
            IUrlHelperFactory urlHelperFactory,
            IActionContextAccessor actionContextAccessor)
        {
            _urlHelperFactory = urlHelperFactory;
            _actionContextAccessor = actionContextAccessor;
        }

        #endregion

        public IEnumerable<LinkModel> CreateLinksForAuthors(Guid authorId)
        {
            var urlHelper = _urlHelperFactory.GetUrlHelper(_actionContextAccessor.ActionContext);

            var links = new List<LinkModel>
            {
                new LinkModel(urlHelper.Link("Author", new { AuthorId = authorId }),
                "self",
                "GET"
                ),

                new LinkModel(urlHelper.Link("Authors", new { }),
                "list_of_Authors",
                "GET"
                ),

                new LinkModel(urlHelper.Link("Author", new { }),
                "create_author",
                "POST"
                ),

                new LinkModel(urlHelper.Link("Courses", new { }),
                "author_courses",
                "GET"
                ),

                new LinkModel(urlHelper.Link("CreateCourseForAuthor", new { }),
                "create_course_for_author",
                "POST"
                ),

                new LinkModel(urlHelper.Link("UpdateCourseForAuthor", new { }),
                "update_course_for_author",
                "POST"
                )
            };

            return links;
        }
    }
}
