using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvertisementApp.Entites
{
    public class AppUser:BaseEntity
    {
        public string FirstName { get; set; }
        public string SurName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public int GenderID { get; set; }
        public Gender Gender { get; set; }
        public ICollection<AppUserRole> AppUserRoles { get; set; }
        public ICollection<AdvertisementAppUser> AdvertisementAppUsers { get; set; }

    }
}
