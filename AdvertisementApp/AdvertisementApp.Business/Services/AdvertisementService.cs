using Advertisement.Dto;
using AdvertisementApp.Business.Interfaces;
using AdvertisementApp.Common;
using AdvertisementApp.DataAccess.UnitofWork;
using AutoMapper;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvertisementApp.Business.Services
{
    public class AdvertisementService : Service<AdvertisementCreateDto, AdvertisementUpdateDto, AdvertisementListDto, AdvertisementApp.Entites.Advertisement>, IAdvertisementService
    {
        private readonly IUow _uow;
        private readonly IMapper _mapper;
        public AdvertisementService(IMapper mapper, IValidator<AdvertisementCreateDto> createDtovalidator, IValidator<AdvertisementUpdateDto> updatedtovalidator, IUow uow) : base(mapper, createDtovalidator, updatedtovalidator, uow)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<IResponse<List<AdvertisementListDto>>> GetActives()
        {
            var data = await _uow.GetRepository<AdvertisementApp.Entites.Advertisement>().GetAllAsync(x => x.Status,x=>x.CreatedDate,Common.Enums.OrderByType.DESC);
            var mapdto = _mapper.Map<List<AdvertisementListDto>>(data);

            return new Response <List<AdvertisementListDto>>(ResponseType.Success, mapdto);
        }
    }
}
