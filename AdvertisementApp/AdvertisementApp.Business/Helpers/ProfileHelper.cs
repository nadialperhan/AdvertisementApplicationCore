using AdvertisementApp.Business.Mappings.AutoMapper;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvertisementApp.Business.Helpers
{
    public static class ProfileHelper
    {
        public static List<Profile> GetProfiles()
        {
            return new List<Profile>()
            {
                new ProvidedServiceProfiler(),
                new AdvertisementProfiler(),
                new AppUserProfiler(),
                new GenderProfiler(),
                new AppRoleProfiler(),
                new AdvertisementAppUserProfiler(),
                new MilitaryStatusProfiler(),
                new AdvertisementAppUserStatusProfiler()
            };

        }
    }
}
