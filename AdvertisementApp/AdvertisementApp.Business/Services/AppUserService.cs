using Advertisement.Dto;
using AdvertisementApp.Business.Extensions;
using AdvertisementApp.Business.Interfaces;
using AdvertisementApp.Common;
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
    public class AppUserService : Service<AppUserCreateDto, AppUserUpdateDto, AppUserListDto, AppUser>, IAppUserService
    {
        private readonly IUow _uow;
        private readonly IMapper _mapper;
        private readonly IValidator<AppUserCreateDto> _createvalidator;
        private readonly IValidator<AppUserLoginDto> _loginvalidator;

        public AppUserService(IMapper mapper, IValidator<AppUserCreateDto> createvalidator, IValidator<AppUserUpdateDto> updatevalidator, IUow uow, IValidator<AppUserLoginDto> loginvalidator) : base(mapper, createvalidator, updatevalidator, uow)
        {
            _uow = uow;
            _mapper = mapper;
            _createvalidator = createvalidator;
            _loginvalidator = loginvalidator;
        }
        public async Task<IResponse<AppUserCreateDto>> CreateWithRole(AppUserCreateDto dto, int roleId)
        {
            var validationresult = _createvalidator.Validate(dto);
            if (validationresult.IsValid)
            {
                var user = _mapper.Map<AppUser>(dto);
                await _uow.GetRepository<AppUser>().Create(user);
                await _uow.GetRepository<AppUserRole>().Create(new AppUserRole()
                {
                    AppUser = user,
                    AppRoleId = roleId
                });
                await _uow.SaveChanges();

                return new Response<AppUserCreateDto>(ResponseType.Success, dto);
            }
            return new Response<AppUserCreateDto>(dto, validationresult.ConvertToCustomValidationError());
        }
        public async Task<IResponse<AppUserListDto>> CheckUserAsync(AppUserLoginDto dto)
        {
            var response = _loginvalidator.Validate(dto);
            if (response.IsValid)
            {
                var user = await _uow.GetRepository<AppUser>().GetByFilter(x => x.UserName == dto.UserName && x.Password == dto.PassWord);
                if (user != null)
                {
                    var userdto = _mapper.Map<AppUserListDto>(user);
                    return new Response<AppUserListDto>(ResponseType.Success, userdto);
                }
                return new Response<AppUserListDto>(ResponseType.NotFound, "kullanıcı bulunamadı");
            }
            return new Response<AppUserListDto>(ResponseType.ValidationError, "kullanıcı veya şifre boş olamaz");
        }
        public async Task<IResponse<List<AppRoleListDto>>> GetRolesByUserIdAsync(int userid)
        {
            var roles = await _uow.GetRepository<AppRole>().GetAllAsync(x => x.AppUserRoles.Any(x => x.AppUserId == userid));
            if (roles == null)
            {
                return new Response<List<AppRoleListDto>>(ResponseType.NotFound, "rol bulunamadı");
            }
            else
            {
                var listdto = _mapper.Map<List< AppRoleListDto>>(roles);
                return new Response<List<AppRoleListDto>>(ResponseType.Success, listdto);
            }
        }
    }
}
