using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advertisement.Dto
{
    public class AdvertisementAppUserCreateDto
    {
        public int AppUserId { get; set; }
        public int AdvertisementId { get; set; }
        public int AdvertisementUserStatusId { get; set; }
        public int MilitaryStatusID { get; set; }
        public DateTime? EndDate { get; set; }
        public int WorkExperience { get; set; }
        public string CvPath { get; set; }
    }
}
