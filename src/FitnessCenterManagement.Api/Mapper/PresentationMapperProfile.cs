using AutoMapper;
using AutoMapper.Extensions.EnumMapping;
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
            CreateMap<FitnessEventModel, FitnessEventDto>().ReverseMap();
            CreateMap<AbonementModel, AbonementDto>().ReverseMap();

            CreateMap<Enums.AbonementStatus, BusinessLogic.Enums.AbonementStatus>().ConvertUsingEnumMapping().ReverseMap();
            CreateMap<Enums.AbonementCardStatus, BusinessLogic.Enums.AbonementCardStatus>().ConvertUsingEnumMapping().ReverseMap();
            CreateMap<Enums.DateEventStatus, BusinessLogic.Enums.DateEventStatus>().ConvertUsingEnumMapping().ReverseMap();
        }
    }
}
