using Advertisement.Dto;
using AdvertisementApp.Business.Extensions;
using AdvertisementApp.Business.Interfaces;
using AdvertisementApp.Common;
using AdvertisementApp.Common.Enums;
using AdvertisementApp.DataAccess.UnitofWork;
using AdvertisementApp.Entites;
using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvertisementApp.Business.Services
{
    public class AdvertisementAppUserService: IAdvertisementAppUserService
    {
        private readonly IUow _uow;
        private readonly IValidator<AdvertisementAppUserCreateDto> _validator;
        private readonly IMapper _mapper;
        public AdvertisementAppUserService(IUow uow, IValidator<AdvertisementAppUserCreateDto> validator, IMapper mapper)
        {
            _uow = uow;
            _validator = validator;
            _mapper = mapper;
        }
        public async Task<IResponse<AdvertisementAppUserCreateDto>> CreateAsync(AdvertisementAppUserCreateDto dto)
        {
            var result = _validator.Validate(dto);
            if (result.IsValid)
            {
                var control =await _uow.GetRepository<AdvertisementAppUser>().GetByFilter(x => x.AppUserId == dto.AppUserId && x.AdvertisementId==dto.AdvertisementId) ;
                if (control==null)
                {
                    var createadvertisementAppUser = _mapper.Map<AdvertisementAppUser>(dto);
                    await _uow.GetRepository<AdvertisementAppUser>().Create(createadvertisementAppUser);
                    await _uow.SaveChanges();
                    return new Response<AdvertisementAppUserCreateDto>(ResponseType.Success, dto);
                }
                List<CustomValidationError> errors = new List<CustomValidationError>() { new CustomValidationError() { ErrorMessage = "Daha önce başvuruldu" } };
                return new Response<AdvertisementAppUserCreateDto>(dto,errors);
            }
            return new Response<AdvertisementAppUserCreateDto>(dto, result.ConvertToCustomValidationError());
        }
        public async Task<List<AdvertisementAppUserListDto>> Getlist(AdvertisementAppUserStatusType type)
        {
            var query = _uow.GetRepository<AdvertisementAppUser>().GetQuery();
            var list=await query.Include(x => x.Advertisement).Include(x => x.AdvertisementUserStatus).Include(x => x.AppUser).ThenInclude(x=>x.Gender).Include(x => x.MilitaryStatus).Where(x=>x.AdvertisementUserStatusId==(int)type).ToListAsync();

            return _mapper.Map<List< AdvertisementAppUserListDto>>(list);
        }
        public async Task SetStatusAsync(int AdvertisementAppUserId, AdvertisementAppUserStatusType type)
        {
            var entityunchanged =await _uow.GetRepository<AdvertisementAppUser>().FindAsync(AdvertisementAppUserId);
            var entitychanged =await _uow.GetRepository<AdvertisementAppUser>().GetByFilter(x=>x.Id== AdvertisementAppUserId);
            entitychanged.AdvertisementUserStatusId = (int)type;
            _uow.GetRepository<AdvertisementAppUser>().Updated(entitychanged, entityunchanged);
            await _uow.SaveChanges();
        }
       
    }
}
