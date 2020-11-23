using Authors.Domain.AuthorEntity;
using Authors.Domain.CourseEntity;
using Authors.Models.Commands;
using AutoMapper;

namespace Authors.API.Mapping
{

    public class CommandToEntityMapperProfile : Profile
    {
        public CommandToEntityMapperProfile()
        {
            CreateMap<CreateAuthorCommand, Author>();
            CreateMap<CreateCourseForAuthorCommand, Course>();
            CreateMap<UpdateCourseForAuthorCommand, Course>();
        }
    }
}
