using Advertisement.Dto;
using AdvertisementApp.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvertisementApp.Business.Interfaces
{
    public interface IAdvertisementService : IService<AdvertisementCreateDto, AdvertisementUpdateDto, AdvertisementListDto, AdvertisementApp.Entites.Advertisement>
    {
        Task<IResponse<List<AdvertisementListDto>>> GetActives();
    }
}
