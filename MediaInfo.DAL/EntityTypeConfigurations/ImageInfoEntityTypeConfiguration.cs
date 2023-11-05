using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MediaInfo.DAL.EntityTypeConfigurations
{
    public class ImageInfoEntityTypeConfiguration : IEntityTypeConfiguration<ImageInfo>
    {
        public void Configure(EntityTypeBuilder<ImageInfo> builder)
        {
            builder.ToTable("ImageInfo").HasKey(x => x.Id);
        }
    }
}
