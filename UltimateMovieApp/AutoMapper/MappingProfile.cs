using AutoMapper;
using Entities.DataTransferObjects.Company;
using Entities.DataTransferObjects.Employee;
using Entities.DataTransferObjects.Movie;
using Entities.DataTransferObjects.MovieUser;
using Entities.Models;

namespace UltimateMovieApp.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Company, CompaniesDto>()
                .ForMember(c => c.FullAddress,
                opt => opt.MapFrom(x => string.Join(' ', x.Address, x.Country)));


            CreateMap<CompanyForCreationDto, Company>();

            CreateMap<CompanyForUpdateDto, Company>();


            CreateMap<Employee, EmployeeDto>();

            CreateMap<EmployeeForCreationDto, Employee>();

            CreateMap<EmployeeForUpdateDto, Employee>();

            CreateMap<EmployeeForUpdateDto, Employee>().ReverseMap();


            CreateMap<Movie, MovieDto>();

            CreateMap<MovieForCreateDto, Movie>();

            CreateMap<MovieForUpdateDto, Movie>();

            CreateMap<MovieUserForRegistrationDto, MovieUser>();
        }
    }
}
