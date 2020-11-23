using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Authors.API.Handlers.Command;
using Authors.API.Handlers.Query;
using Authors.CQRS.Interfaces;
using Authors.Domain.AuthorEntity;
using Authors.Models.Commands;
using Authors.Models.Models;
using Authors.Models.Queries;
using Authors.Models.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Authors.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        #region Properties

        private readonly IQueryBusAsync _queryBus;
        private readonly ICommandBusAsync _commandBus;

        #endregion

        #region Constructor
        public AuthorsController(IQueryBusAsync queryBus, ICommandBusAsync commandBus)
        {
            _queryBus = queryBus;
            _commandBus = commandBus;
        }
        #endregion

        /// <summary>
        /// Returns all authors
        /// </summary>
        /// <response code="200">Returns authors in the system</response>
        [HttpGet(Name = "Authors")]
        [ProducesResponseType(typeof(AuthorsQueryResponse), 200)]
        [ResponseCache(Duration = 10)]
        public async Task<ActionResult<AuthorsQueryResponse>> Authors([FromQuery] AuthorsQuery query)
        {
            var result = await _queryBus.ExecuteAsync<AuthorsQuery, AuthorsQueryResponse>(query);
            return Ok(result);
        }

        /// <summary>
        /// Creates new author in the system
        /// </summary>
        /// <param name="command"></param>
        /// <response code="201">Creates new author in the system</response>
        /// <response code="400">Unable to add new user in the system due to validation error</response>
        [HttpPost(Name = "CreateAuthor")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), 400)]
        public async Task<ActionResult<CreateAuthorCommandResponse>> CreateAuthor([FromBody] CreateAuthorCommand command)
        {
            var result = await _commandBus.ExecuteAsync<CreateAuthorCommand, CreateAuthorCommandResponse>(command);
            return CreatedAtAction("Author",
                new { AuthorId = result.Id }, result);
        }

        /// <summary>
        /// Return author with specific id
        /// </summary>
        /// <param name="query"></param>
        /// <response code="200">Author with id</response>
        /// <response code="404">Author not found</response>
        [HttpGet("{AuthorId}", Name = "Author")]
        [ProducesResponseType(typeof(AuthorQueryResponse), 200)]
        [ProducesResponseType(typeof(ErrorModel), 404)]
        public async Task<ActionResult<AuthorQueryResponse>> Author([FromRoute] AuthorQuery query)
        {
            var result = await _queryBus.ExecuteAsync<AuthorQuery, AuthorQueryResponse>(query);
            return Ok(result);
        }

        #region HttpOptions
        /// <summary>
        /// Communication options for a given URL
        /// </summary>
        /// <response code="200">Returns allowed http methods</response>
        [HttpOptions(Name = "Options")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult AuthorsControllerOptions()
        {
            Response.Headers.Add("Allow", "GET,POST,OPTIONS");
            return Ok();
        }
        #endregion
    }
}