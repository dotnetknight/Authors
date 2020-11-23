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
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authors.API.Handlers.Query
{
    public class AuthorCoursesQueryHandler : IQueryHandlerAsync<AuthorCoursesQuery, AuthorCoursesQueryResponse>
    {
        #region Properties
        private readonly IRepository<Course> _courseRepository;
        private readonly IRepository<Author> _authorRepository;
        private readonly HATEOASLinksService _hATEOASLinksService;
        private readonly MediaTypeCheckService _mediaTypeCheckService;
        private readonly IMapper _mapper;

        #endregion

        #region Constructor
        public AuthorCoursesQueryHandler(
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

        public Task<AuthorCoursesQueryResponse> HandleAsync(AuthorCoursesQuery query)
        {
            IEnumerable<CourseDto> mappedCourses = new List<CourseDto>();

            try
            {
                bool authorExists = _authorRepository.Exists(query.AuthorId);

                if (!authorExists)
                    throw new AuthorNotFound("Author with requested id not found");

                var courses = _courseRepository
                    .GetAll()
                    .Where(c => c.AuthorId == query.AuthorId);

                mappedCourses = _mapper.Map<List<CourseDto>>(courses);
            }

            catch (AuthorNotFound)
            {
                throw;
            }

            catch (Exception ex)
            {
                throw new BaseApiException(System.Net.HttpStatusCode.InternalServerError, ex.ToString());
            }

            return Task.FromResult(new AuthorCoursesQueryResponse()
            {
                Courses = mappedCourses,
                Links = _mediaTypeCheckService.CanCreateHATEOASLink() ? _hATEOASLinksService.CreateLinksForAuthors(query.AuthorId) : null
            });
        }
    }
}
