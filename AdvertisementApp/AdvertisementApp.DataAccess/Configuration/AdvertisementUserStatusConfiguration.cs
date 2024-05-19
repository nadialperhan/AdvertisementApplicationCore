using AdvertisementApp.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvertisementApp.DataAccess.Configuration
{
    public class AdvertisementUserStatusConfiguration : IEntityTypeConfiguration<AdvertisementUserStatus>
    {
        public void Configure(EntityTypeBuilder<AdvertisementUserStatus> builder)
        {
            builder.Property(x => x.Definition).HasMaxLength(300).IsRequired();
        }
    }
}
