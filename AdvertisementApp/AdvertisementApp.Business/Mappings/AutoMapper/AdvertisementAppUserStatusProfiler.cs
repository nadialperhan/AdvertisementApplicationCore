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
    public class AdvertisementAppUserStatusProfiler:Profile
    {
        public AdvertisementAppUserStatusProfiler()
        {
            CreateMap<AdvertisementUserStatus, AdvertisementAppUserStatusListDto>().ReverseMap();
        }
    }
}
