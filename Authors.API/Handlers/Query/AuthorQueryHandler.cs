using Authors.CQRS.Interfaces;
using Authors.Domain.AuthorEntity;
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
    public class AuthorQueryHandler : IQueryHandlerAsync<AuthorQuery, AuthorQueryResponse>
    {
        #region Properties

        private readonly IRepository<Author> _authorRepository;
        private readonly HATEOASLinksService _hATEOASLinksService;
        private readonly MediaTypeCheckService _mediaTypeCheckService;
        private readonly IMapper _mapper;

        #endregion

        #region Constructor
        public AuthorQueryHandler(
            IRepository<Author> authorRepository,
            HATEOASLinksService hATEOASLinksService,
            MediaTypeCheckService mediaTypeCheckService,
            IMapper mapper)
        {
            _authorRepository = authorRepository;
            _hATEOASLinksService = hATEOASLinksService;
            _mediaTypeCheckService = mediaTypeCheckService;
            _mapper = mapper;
        }

        #endregion

        public Task<AuthorQueryResponse> HandleAsync(AuthorQuery query)
        {
            AuthorDto mappedAuthor = new AuthorDto();

            try
            {
                var author = _authorRepository.Get(query.AuthorId);
                            
                if (author == null)
                    throw new AuthorNotFound("Author with requested id not found");

                mappedAuthor = _mapper.Map<AuthorDto>(author);
            }

            catch (AuthorNotFound)
            {
                throw;
            }

            catch (Exception ex)
            {
                throw new BaseApiException(System.Net.HttpStatusCode.InternalServerError, ex.ToString());
            }

            return Task.FromResult(new AuthorQueryResponse()
            {
                Author = mappedAuthor,
                Links = _mediaTypeCheckService.CanCreateHATEOASLink() ? _hATEOASLinksService.CreateLinksForAuthors(query.AuthorId) : null
            });
        }
    }
}