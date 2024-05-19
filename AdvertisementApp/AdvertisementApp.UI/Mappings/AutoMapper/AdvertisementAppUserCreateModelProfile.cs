﻿using Advertisement.Dto;
using AdvertisementApp.UI.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdvertisementApp.UI.Mappings.AutoMapper
{
    public class AdvertisementAppUserCreateModelProfile:Profile
    {
        public AdvertisementAppUserCreateModelProfile()
        {
            CreateMap<AdvertisementAppUserCreateDto, AdvertisementAppUserCreateModel>().ReverseMap();
        }
    }
}
