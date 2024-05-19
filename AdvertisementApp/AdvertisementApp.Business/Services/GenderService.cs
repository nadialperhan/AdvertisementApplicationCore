using Advertisement.Dto;
using AdvertisementApp.Business.Interfaces;
using AdvertisementApp.DataAccess.UnitofWork;
using AdvertisementApp.Entites;
using AutoMapper;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvertisementApp.Business.Services
{
    public class GenderService: Service<GenderCreateDto, GenderUpdateDto, GenderListDto, Gender>, IGenderService
    {
        public GenderService(IMapper mapper, IValidator<GenderCreateDto> createvalidator, IValidator<GenderUpdateDto> updatevalidator, IUow uow) :base(mapper,createvalidator,updatevalidator,uow)
        {

        }
    }
}
