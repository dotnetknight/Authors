using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Authors.CQRS.Interfaces;
using Authors.Models.Commands;
using Authors.Models.Models;
using Authors.Models.Queries;
using Authors.Models.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Authors.API.Controllers
{
    [Route("api/Authors/{AuthorId}/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        #region Properties
        private readonly IQueryBusAsync _queryBus;
        private readonly ICommandBusAsync _commandBus;

        #endregion

        #region Constructor
        public CoursesController(IQueryBusAsync queryBus, ICommandBusAsync commandBus)
        {
            _queryBus = queryBus;
            _commandBus = commandBus;
        }
        #endregion

        /// <summary>
        /// Returns courses in the system
        /// </summary>
        /// <param name="query"></param>
        /// <response code="200">Returns courses in the system</response>
        /// <response code="404">Author not found</response>
        [HttpGet(Name = "AuthorCourses")]
        [ProducesResponseType(typeof(AuthorCoursesQueryResponse), 200)]
        [ProducesResponseType(typeof(ErrorModel), 404)]
        [ResponseCache(Duration = 10)]
        public async Task<ActionResult<AuthorCoursesQueryResponse>> Courses([FromRoute] AuthorCoursesQuery query)
        {
            var result = await _queryBus.ExecuteAsync<AuthorCoursesQuery, AuthorCoursesQueryResponse>(query);
            return Ok(result);
        }

        /// <summary>
        /// Returns specific course for author
        /// </summary>
        /// <param name="query"></param>
        /// <response code="200">Returns course for author</response>
        /// <response code="404">Author or course not found</response>
        [HttpGet("{CourseId}", Name = "AuthorCourse")]
        [ProducesResponseType(typeof(AuthorCourseQueryResponse), 200)]
        [ProducesResponseType(typeof(ErrorModel), 404)]
        public async Task<ActionResult<AuthorCourseQueryResponse>> AuthorCourse([FromRoute] AuthorCourseQuery query)
        {
            var result = await _queryBus.ExecuteAsync<AuthorCourseQuery, AuthorCourseQueryResponse>(query);

            return Ok(result);
        }

        /// <summary>
        /// Creates course for author
        /// </summary>
        /// <param name="command"></param>
        /// <response code="201">Course for author was created</response>
        /// <response code="404">Author not found</response>
        /// <response code="400">Error occured during field validation</response>
        [HttpPost(Name = "CreateCourseForAuthor")]
        [ProducesResponseType(typeof(CreateCourseForAuthorCommandResponse), 201)]
        [ProducesResponseType(typeof(ErrorModel), 404)]
        [ProducesResponseType(typeof(ValidationProblemDetails), 400)]
        public async Task<ActionResult<CreateCourseForAuthorCommandResponse>> CreateCourseForAuthor([FromBody] CreateCourseForAuthorCommand command)
        {
            var result = await _commandBus.ExecuteAsync<CreateCourseForAuthorCommand, CreateCourseForAuthorCommandResponse>(command);
            return CreatedAtRoute("AuthorCourse",
                new { AuthorId = result.AuthorId, CourseId = result.CourseId }, result);
        }

        /// <summary>
        /// Updates course for author
        /// </summary>
        /// <param name="command"></param>
        /// <response code="204">Course for author was updated</response>
        /// <response code="404">Author or course not found</response>
        /// <response code="400">Error occured during field validation</response>
        [HttpPut("{CourseId}", Name = "UpdateCourseForAuthor")]
        [ProducesResponseType(typeof(UpdateCourseForAuthorCommandResponse), 204)]
        [ProducesResponseType(typeof(ErrorModel), 404)]
        public async Task<ActionResult<UpdateCourseForAuthorCommandResponse>> UpdateCourseForAuthor([FromBody] UpdateCourseForAuthorCommand command)
        {
            await _commandBus.ExecuteAsync(command);
            return NoContent();
        }
    }
}