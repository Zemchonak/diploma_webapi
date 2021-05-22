using AutoMapper;
using FitnessCenterManagement.BusinessLogic.Dtos;
using FitnessCenterManagement.DataAccess.Entities;

namespace FitnessCenterManagement.BusinessLogic.Mapper
{
    public class BusinessLogicMapperFirstProfile : Profile
    {
        public BusinessLogicMapperFirstProfile()
        {
            CreateMap<AbonementCard, AbonementCardDto>().ReverseMap();
            CreateMap<Abonement, AbonementDto>().ReverseMap();
            CreateMap<CustomerCategory, CustomerCategoryDto>().ReverseMap();
            CreateMap<Customer, CustomerDto>().ReverseMap();
            CreateMap<DateEvent, DateEventDto>().ReverseMap();
            CreateMap<CardEventItem, CardEventItemDto>().ReverseMap();
            CreateMap<FitnessEvent, FitnessEventDto>().ReverseMap();
            CreateMap<Language, LanguageDto>().ReverseMap();
        }
    }

    public class BusinessLogicMapperSecondProfile : Profile
    {
        public BusinessLogicMapperSecondProfile()
        {
            CreateMap<Message, MessageDto>().ReverseMap();
            CreateMap<Review, ReviewDto>().ReverseMap();
            CreateMap<Service, ServiceDto>().ReverseMap();
            CreateMap<Specialization, SpecializationDto>().ReverseMap();
            CreateMap<Trainer, TrainerDto>().ReverseMap();
            CreateMap<Venue, VenueDto>().ReverseMap();
            CreateMap<WeeklyEvent, WeeklyEventDto>().ReverseMap();
        }
    }
}
