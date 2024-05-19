using Advertisement.Dto;
using AdvertisementApp.Common;
using AdvertisementApp.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvertisementApp.Business.Interfaces
{
    public interface IAdvertisementAppUserService
    {
        Task<IResponse<AdvertisementAppUserCreateDto>> CreateAsync(AdvertisementAppUserCreateDto dto);
        Task<List<AdvertisementAppUserListDto>> Getlist(AdvertisementAppUserStatusType type);
        Task SetStatusAsync(int AdvertisementAppUserId, AdvertisementAppUserStatusType type);
    }
}
