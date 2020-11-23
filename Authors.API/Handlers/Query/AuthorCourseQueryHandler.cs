using Authors.CQRS.Interfaces;
using Authors.Domain.AuthorEntity;
using Authors.Domain.CourseEntity;
using Authors.Models.Exceptions;
using Authors.Models.Models;
using Authors.Models.Queries;
using Authors.Models.Responses;
using Authors.Repository;
using Authors.Service;
using AutoMapper;
using System;
using System.Threading.Tasks;

namespace Authors.API.Handlers.Query
{
    public class AuthorCourseQueryHandler : IQueryHandlerAsync<AuthorCourseQuery, AuthorCourseQueryResponse>
    {

        #region Properties
        private readonly IRepository<Author> _authorRepository;
        private readonly IRepository<Course> _courseRepository;
        private readonly HATEOASLinksService _hATEOASLinksService;
        private readonly MediaTypeCheckService _mediaTypeCheckService;
        private readonly IMapper _mapper;

        #endregion

        #region Constructor
        public AuthorCourseQueryHandler(
            IRepository<Course> courseRepository,
            IRepository<Author> authorRepository,
            HATEOASLinksService hATEOASLinksService,
            MediaTypeCheckService mediaTypeCheckService,
            IMapper mapper)
        {
            _courseRepository = courseRepository;
            _authorRepository = authorRepository;
            _hATEOASLinksService = hATEOASLinksService;
            _mediaTypeCheckService = mediaTypeCheckService;
            _mapper = mapper;
        }

        #endregion

        public Task<AuthorCourseQueryResponse> HandleAsync(AuthorCourseQuery query)
        {
            CourseDto mappedCourse = new CourseDto();

            try
            {
                bool authorExists = _authorRepository.Exists(query.AuthorId);

                if (!authorExists)
                    throw new AuthorNotFound("Author with requested id not found");

                var course = _courseRepository
                    .Get(query.CourseId);

                if (course == null)
                    throw new CourseNotFound("Course for this author not found");

                mappedCourse = _mapper.Map<CourseDto>(course);
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

            return Task.FromResult(new AuthorCourseQueryResponse()
            {
                Course = mappedCourse,
                Links = _mediaTypeCheckService.CanCreateHATEOASLink() ? _hATEOASLinksService.CreateLinksForAuthors(query.AuthorId) : null
            });
        }
    }
}
