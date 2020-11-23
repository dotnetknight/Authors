using Authors.CQRS.Interfaces;
using Authors.Domain.AuthorEntity;
using Authors.Domain.CourseEntity;
using Authors.Models.Commands;
using Authors.Models.Exceptions;
using Authors.Repository;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System;
using System.Threading.Tasks;

namespace Authors.API.Handlers.Command
{
    public class DeleteAuthorCourseCommandHandler : ICommandHandlerAsync<DeleteAuthorCourseCommand>
    {
        #region Properties
        private readonly IActionContextAccessor _actionContextAccessor;
        private readonly IRepository<Author> _authorsRepository;
        private readonly IRepository<Course> _coursesRepository;
        #endregion

        #region Constructor

        public DeleteAuthorCourseCommandHandler(
            IActionContextAccessor actionContextAccessor,
            IRepository<Author> authorsRepository,
            IRepository<Course> coursesRepository)
        {
            _actionContextAccessor = actionContextAccessor;
            _authorsRepository = authorsRepository;
            _coursesRepository = coursesRepository;
        }

        #endregion

        public Task HandleAsync(DeleteAuthorCourseCommand command)
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
                
                _coursesRepository.Delete(course);

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
