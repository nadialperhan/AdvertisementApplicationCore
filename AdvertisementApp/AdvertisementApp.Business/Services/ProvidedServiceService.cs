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
    public class ProvidedServiceService:Service<ProvidedServiceCreateDto, ProvidedServiceUpdateDto, ProvidedServiceListDto, ProvidedService>,IProvidedServiceService
    {
        
        public ProvidedServiceService(IMapper mapper,IValidator<ProvidedServiceCreateDto> createdtovalidator,IValidator<ProvidedServiceUpdateDto> updatedtovalidator,IUow uow):base(mapper,createdtovalidator,updatedtovalidator,uow)
        {
            
        }

    }
}
