using Advertisement.Dto;
using AdvertisementApp.Common.Enums;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvertisementApp.Business.ValidationRules
{
    public class AdvertisementAppUserCreateDtoValidator:AbstractValidator<AdvertisementAppUserCreateDto>
    {
        public AdvertisementAppUserCreateDtoValidator()
        {
            RuleFor(x => x.AdvertisementId).NotEmpty();
            RuleFor(x => x.AdvertisementUserStatusId).NotEmpty();
            RuleFor(x => x.AppUserId).NotEmpty();
            RuleFor(x => x.CvPath).NotEmpty().WithMessage("Bir Cv dosyası seçiniz");
            RuleFor(x => x.EndDate).NotEmpty().When(x=>x.MilitaryStatusID==(int)MilitaryStatusType.Tecilli).WithMessage("Tecil tarihi boş olamaz");
        }
    }
}
