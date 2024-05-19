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
    public class AppRoleProfiler:Profile
    {
        public AppRoleProfiler()
        {
            CreateMap<AppRoleCreateDto, AppRole>().ReverseMap();
            CreateMap<AppRoleListDto, AppRole>().ReverseMap();
            CreateMap<AppRoleUpdateDto, AppRole>().ReverseMap();
        }
    }
}
