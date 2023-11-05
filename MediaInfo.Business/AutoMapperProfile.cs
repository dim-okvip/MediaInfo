namespace MediaInfo.Business
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<ImageInfo, ImageInfoQueryResult>();
        }
    }
}
