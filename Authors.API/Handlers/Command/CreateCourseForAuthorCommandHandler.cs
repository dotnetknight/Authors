using Authors.CQRS.Interfaces;
using Authors.Domain.AuthorEntity;
using Authors.Domain.CourseEntity;
using Authors.Models.Commands;
using Authors.Models.Exceptions;
using Authors.Models.Responses;
using Authors.Repository;
using Authors.Service;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authors.API.Handlers.Command
{
    public class CreateCourseForAuthorCommandHandler : ICommandHandlerAsync<CreateCourseForAuthorCommand, CreateCourseForAuthorCommandResponse>
    {
        #region Properties
        private readonly IActionContextAccessor _actionContextAccessor;
        private readonly IRepository<Author> _authorsRepository;
        private readonly IRepository<Course> _coursesRepository;
        private readonly HATEOASLinksService _hATEOASLinksService;
        private readonly MediaTypeCheckService _mediaTypeCheckService;
        private readonly IMapper _mapper;

        #endregion

        #region Constructor

        public CreateCourseForAuthorCommandHandler(
            IActionContextAccessor actionContextAccessor,
            IRepository<Author> authorsRepository,
            IRepository<Course> coursesRepository,
            HATEOASLinksService hATEOASLinksService,
            MediaTypeCheckService mediaTypeCheckService,
            IMapper mapper)
        {
            _actionContextAccessor = actionContextAccessor;
            _authorsRepository = authorsRepository;
            _coursesRepository = coursesRepository;
            _hATEOASLinksService = hATEOASLinksService;
            _mediaTypeCheckService = mediaTypeCheckService;
            _mapper = mapper;
        }

        #endregion

        public Task<CreateCourseForAuthorCommandResponse> HandleAsync(CreateCourseForAuthorCommand command)
        {
            try 
            {
                Guid authorId = new Guid(_actionContextAccessor.ActionContext.RouteData.Values["AuthorId"].ToString());

                bool authorExists = _authorsRepository.Exists(authorId);

                if (!authorExists)
                    throw new AuthorNotFound("Author with this id not found in the system.");

                Guid courseId = Guid.NewGuid();

                var mappedCourse = _mapper.Map<Course>(command);

                mappedCourse.Id = courseId;
                mappedCourse.AuthorId = authorId;
                mappedCourse.AddedDate = DateTime.Now;

                _coursesRepository.Insert(mappedCourse);

                return Task.FromResult(new CreateCourseForAuthorCommandResponse()
                {
                    AuthorId = authorId,
                    CourseId = courseId,
                    Links = _mediaTypeCheckService.CanCreateHATEOASLink() ? _hATEOASLinksService.CreateLinksForAuthors(authorId) : null
                });
            }

            catch (AuthorNotFound)
            {
                throw;
            }

            catch (Exception ex)
            {
                throw new BaseApiException(System.Net.HttpStatusCode.InternalServerError, ex.ToString());
            }
        }
    }
}
