using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvertisementApp.Entites
{
    public class AdvertisementUserStatus:BaseEntity
    {
        public string Definition { get; set; }
        public List<AdvertisementAppUser>  AdvertisementAppUsers { get; set; }
    }
}
