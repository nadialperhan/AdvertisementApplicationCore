using Advertisement.Dto.Interfaces;
using Advertisement.Dto;
using AdvertisementApp.Common;
using AdvertisementApp.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvertisementApp.Business.Interfaces
{
    public interface IService<CreateDto,UpdateDto,ListDto,T> 
        where CreateDto:class,IDto,new()
        where UpdateDto : class, IUpdatedDto, new()
        where ListDto : class, IDto, new()
        where T:BaseEntity
    {
        Task<IResponse<CreateDto>> CreateAsync(CreateDto dto);
        Task<IResponse<UpdateDto>> UpdateAsync(UpdateDto dto);
        Task<IResponse<IDto>> GeTByIdAsync<IDto>(int id);
        Task<IResponse> RemoveAsync(int id);

        Task<IResponse<List<ListDto>>> GetAllAsync();

    }
}
