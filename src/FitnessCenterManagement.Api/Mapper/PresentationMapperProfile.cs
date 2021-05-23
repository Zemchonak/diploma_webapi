using AutoMapper;
using FitnessCenterManagement.Api.Models;
using FitnessCenterManagement.BusinessLogic.Dtos;

namespace FitnessCenterManagement.Api.Mapper
{
    public class PresentationMapperProfile : Profile
    {
        public PresentationMapperProfile()
        {
            CreateMap<SpecializationModel, SpecializationDto>().ReverseMap();
            CreateMap<ServiceModel, ServiceDto>().ReverseMap();
            CreateMap<VenueModel, VenueDto>().ReverseMap();
        }
    }
}
