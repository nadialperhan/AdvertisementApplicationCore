using Advertisement.Dto.Interfaces;
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
    public class Service<CreateDto, UpdateDto, ListDto, T> : IService<CreateDto, UpdateDto, ListDto, T>
         where CreateDto : class, IDto, new()
        where UpdateDto : class, IUpdatedDto, new()
        where ListDto : class, IDto, new()
        where T : BaseEntity
    {
        private readonly IMapper _mapper;
        private readonly IValidator<CreateDto> _createDtovalidator;
        private readonly IValidator<UpdateDto> _updateDtovalidator;
        private readonly IUow _uow;

        public Service(IMapper mapper, IValidator<CreateDto> createDtovalidator, IValidator<UpdateDto> updateDtovalidator, IUow uow)
        {
            _mapper = mapper;
            _createDtovalidator = createDtovalidator;
            _updateDtovalidator = updateDtovalidator;
            _uow = uow;
        }

        public async Task<IResponse<CreateDto>> CreateAsync(CreateDto dto)
        {
            var result = _createDtovalidator.Validate(dto);
            if (result.IsValid)
            {
                var createdentity = _mapper.Map<T>(dto);
                await _uow.GetRepository<T>().Create(createdentity);
                await _uow.SaveChanges();
                return new Response<CreateDto>(ResponseType.Success, dto);
            }
            return new Response<CreateDto>(dto, result.ConvertToCustomValidationError());
        }

        public async Task<IResponse<List<ListDto>>> GetAllAsync()
        {
            var data = await _uow.GetRepository<T>().GetAllAsync();
            var dto = _mapper.Map<List<ListDto>>(data);
            return new Response<List<ListDto>>(ResponseType.Success, dto);
        }

        public async Task<IResponse<IDto>> GeTByIdAsync<IDto>(int id)
        {
            var data = await _uow.GetRepository<T>().GetByFilter(x => x.Id == id);
            if (data == null)
            {
                return new Response<IDto>(ResponseType.Success, $"{id} li data bulunamadı");
            }
            var dto = _mapper.Map<IDto>(data);
            return new Response<IDto>(ResponseType.Success, dto);
        }

        public async Task<IResponse> RemoveAsync(int id)
        {
            var data = await _uow.GetRepository<T>().FindAsync(id);
            if (data == null)
            {
                return new Response(ResponseType.NotFound, $"{id} li data bulunamadı");
            }
            _uow.GetRepository<T>().Remove(data);
            await _uow.SaveChanges();
            return new Response(ResponseType.Success, "başarıyla silindi");
        }

        public async Task<IResponse<UpdateDto>> UpdateAsync(UpdateDto dto)
        {
            var result=_updateDtovalidator.Validate(dto);
            if (result.IsValid)
            {
                var unchangeddata = await _uow.GetRepository<T>().FindAsync(dto.Id);
                if (unchangeddata == null)
                {
                    return new Response<UpdateDto>(ResponseType.NotFound, $"{dto.Id} li data bulunamadı");

                }
                var entity = _mapper.Map<T>(dto);
                _uow.GetRepository<T>().Updated(entity, unchangeddata);
                await _uow.SaveChanges();
                return new Response<UpdateDto>(ResponseType.Success, dto);
            }
            return new Response<UpdateDto>(dto, result.ConvertToCustomValidationError());
        }
    }
}
