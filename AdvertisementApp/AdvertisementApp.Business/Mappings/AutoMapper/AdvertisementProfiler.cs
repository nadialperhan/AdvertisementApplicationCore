using Advertisement.Dto;
using AdvertisementApp.Entites;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvertisementApp.Business.Mappings.AutoMapper
{
    public class AdvertisementProfiler:Profile
    {
        public AdvertisementProfiler()
        {
            CreateMap<AdvertisementCreateDto, AdvertisementApp.Entites.Advertisement>().ReverseMap();
            CreateMap<AdvertisementUpdateDto, AdvertisementApp.Entites.Advertisement>().ReverseMap();
            CreateMap<AdvertisementListDto, AdvertisementApp.Entites.Advertisement>().ReverseMap();
        }
    }
}
