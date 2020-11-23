using Authors.Domain.AuthorEntity;
using Authors.Domain.CourseEntity;
using Authors.Models.Commands;
using Authors.Models.Models;
using AutoMapper;

namespace Authors.API.Mapping
{
    public class EntityToDtoMapperProfile : Profile
    {
        public EntityToDtoMapperProfile()
        {
            CreateMap<Author, AuthorDto>()
                .ForMember(m => m.Name,
                op => op.MapFrom(src => $"{src.FirstName} {src.LastName}"));

            CreateMap<Course, CourseDto>();
            CreateMap<CoursesDto, Course>();
        }
    }
}
