using Advertisement.Dto;
using FluentValidation;

namespace AdvertisementApp.Business.ValidationRules
{
    public class AdvertisementCreateDtoValidator:AbstractValidator<AdvertisementCreateDto>
    {
        public AdvertisementCreateDtoValidator()
        {
            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.Title).NotEmpty();
            
        }
    }
}
