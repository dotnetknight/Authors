using Authors.CQRS.Interfaces;
using Authors.Domain.AuthorEntity;
using Authors.Domain.CourseEntity;
using Authors.Models.Commands;
using Authors.Models.Exceptions;
using Authors.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System;
using System.Threading.Tasks;

namespace Authors.API.Handlers.Command
{
    public class UpdateCourseForAuthorCommandHandler : ICommandHandlerAsync<UpdateCourseForAuthorCommand>
    {
        #region Properties
        private readonly IActionContextAccessor _actionContextAccessor;
        private readonly IRepository<Author> _authorsRepository;
        private readonly IRepository<Course> _coursesRepository;
        private readonly IMapper _mapper;

        #endregion

        #region Constructor

        public UpdateCourseForAuthorCommandHandler(
            IActionContextAccessor actionContextAccessor,
            IRepository<Author> authorsRepository,
            IRepository<Course> coursesRepository,
            IMapper mapper)
        {
            _actionContextAccessor = actionContextAccessor;
            _authorsRepository = authorsRepository;
            _coursesRepository = coursesRepository;
            _mapper = mapper;
        }

        #endregion

        public Task HandleAsync(UpdateCourseForAuthorCommand command)
        {
            try
            {
                Guid authorId = new Guid(_actionContextAccessor.ActionContext.RouteData.Values["AuthorId"].ToString());
                Guid courseId = new Guid(_actionContextAccessor.ActionContext.RouteData.Values["CourseId"].ToString());

                bool authorExists = _authorsRepository.Exists(authorId);
                bool courseExists = _coursesRepository.Exists(courseId);

                if (!authorExists)
                    throw new AuthorNotFound("Author with this id not found in the system.");

                if (!courseExists)
                    throw new CourseNotFound("Course with this id not found in the system.");


                var course = _coursesRepository.Get(courseId);
                var courseToPatch = _mapper.Map(course, command);
                
                command.JsonPatchDocument.ApplyTo(courseToPatch);

                var mappedCourse = _mapper.Map(courseToPatch, course);

                _coursesRepository.Update(mappedCourse);

                return Task.CompletedTask;
            }

            catch (AuthorNotFound)
            {
                throw;
            }

            catch (CourseNotFound)
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
