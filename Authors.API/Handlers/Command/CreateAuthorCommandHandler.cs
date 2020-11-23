using Authors.CQRS.Interfaces;
using Authors.Domain.AuthorEntity;
using Authors.Models.Commands;
using Authors.Models.Exceptions;
using Authors.Models.Responses;
using Authors.Repository;
using Authors.Service;
using AutoMapper;
using System;
using System.Threading.Tasks;

namespace Authors.API.Handlers.Command
{
    public class CreateAuthorCommandHandler : ICommandHandlerAsync<CreateAuthorCommand, CreateAuthorCommandResponse>
    {
        #region Properties
        private readonly IRepository<Author> _authorRepository;
        private readonly HATEOASLinksService _hATEOASLinksService;
        private readonly MediaTypeCheckService _mediaTypeCheckService;
        private readonly IMapper _mapper;

        #endregion

        #region Constructor
        public CreateAuthorCommandHandler(
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

        public Task<CreateAuthorCommandResponse> HandleAsync(CreateAuthorCommand command)
        {
            try
            {
                Guid authorId = Guid.NewGuid();

                var mappedAuthor = _mapper.Map<Author>(command);

                mappedAuthor.Id = authorId;
                mappedAuthor.AddedDate = DateTime.Now;

                foreach (var course in mappedAuthor.Courses)
                {
                    course.Id = Guid.NewGuid();
                }

                _authorRepository.Insert(mappedAuthor);

                return Task.FromResult(new CreateAuthorCommandResponse()
                {
                    Id = authorId,
                    Links = _mediaTypeCheckService.CanCreateHATEOASLink() ? _hATEOASLinksService.CreateLinksForAuthors(authorId) : null
                });
            }

            catch (Exception ex)
            {
                throw new BaseApiException(System.Net.HttpStatusCode.InternalServerError, ex.ToString());
            }
        }
    }
}
