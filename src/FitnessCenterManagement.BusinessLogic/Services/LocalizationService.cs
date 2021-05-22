using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FitnessCenterManagement.BusinessLogic.Dtos;
using FitnessCenterManagement.BusinessLogic.Interfaces;
using FitnessCenterManagement.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace FitnessCenterManagement.BusinessLogic.Services
{
    internal class LocalizationService : ILocalizationService
    {
        private const string _defaultCultureName = "English";
        private const string _defaultCultureValue = "en-US";

        private readonly IEntityService<Language> _languageService;

        private readonly IMapper _mapper;

        public LocalizationService(IEntityService<Language> languageService, IMapper mapper)
        {
            _languageService = languageService;
            _mapper = mapper;
        }

        public LanguageDto DefaultLanguage
        {
            get => new LanguageDto
            {
                Code = _defaultCultureValue,
                Name = _defaultCultureName,
            };
        }

        public async Task<IReadOnlyCollection<LanguageDto>> GetAllAsync()
        {
            return _mapper.Map<IReadOnlyCollection<LanguageDto>>((await _languageService.GetAll().ToListAsync()).AsReadOnly());
        }

        public async Task<LanguageDto> GetByIdAsync(int id)
        {
            var nonDto = await _languageService.GetByIdAsync(id);
            return (nonDto is null) ? null : new LanguageDto
            {
                Id = nonDto.Id,
                Name = nonDto.Name,
                Code = nonDto.Code,
            };
        }
    }
}
