using Advertisement.Dto.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advertisement.Dto
{
    public class AppRoleUpdateDto:IUpdatedDto
    {
        public int Id { get; set; }
        public string Definition { get; set; }
    }
}
