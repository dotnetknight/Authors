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
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authors.API.Handlers.Query
{
    public class AuthorsQueryHandler : IQueryHandlerAsync<AuthorsQuery, AuthorsQueryResponse>
    {
        #region Properties
        private readonly IRepository<Author> _authorRepository;
        private readonly HATEOASLinksService _hATEOASLinksService;
        private readonly MediaTypeCheckService _mediaTypeCheckService;
        private readonly IMapper _mapper;

        #endregion

        #region Constructor
        public AuthorsQueryHandler(
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

        public Task<AuthorsQueryResponse> HandleAsync(AuthorsQuery query)
        {
            try
            {
                var authors = _authorRepository.GetAll();

                if (string.IsNullOrEmpty(query.MainCategory)
                    && string.IsNullOrEmpty(query.SearchQuery))
                {
                    return CreateResponse(authors);
                }

                if (!string.IsNullOrEmpty(query.MainCategory))
                {
                    authors = authors.Where(a => a.MainCategory.ToLower() == query.MainCategory.ToLower()).ToList();
                    return CreateResponse(authors);
                }

                if (!string.IsNullOrEmpty(query.SearchQuery))
                {
                    authors = authors
                        .Where(a => a.MainCategory.ToLower().Contains(query.SearchQuery.ToLower())
                        || a.FirstName.ToLower().Contains(query.SearchQuery.ToLower())
                        || a.LastName.ToLower().Contains(query.SearchQuery.ToLower())).ToList();

                    return CreateResponse(authors);
                }

                return CreateResponse(authors);
            }

            catch (Exception ex)
            {
                throw new BaseApiException(System.Net.HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        #region PrivateMethods
        private Task<AuthorsQueryResponse> CreateResponse(List<Author> authors)
        {
            return Task.FromResult(new AuthorsQueryResponse()
            {
                Authors = MapAuthorToAuthorDto(authors),
                Links = _mediaTypeCheckService.CanCreateHATEOASLink() ? _hATEOASLinksService.CreateLinksForAuthors(new Guid()) : null
            });
        }

        private List<AuthorDto> MapAuthorToAuthorDto(List<Author> authors)
        {
            return _mapper.Map<List<AuthorDto>>(authors);
        }

        #endregion
    }
}
