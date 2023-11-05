namespace MediaInfo.Business.Handlers
{
    public interface IImageInfoHandler
    {
        Task<Response<List<ImageInfoQueryResult>>> GetAsync(ImageInfoQueryFilterRequest filter);
        Task<Response<bool>> CreateAsync(CreateImageInfoRequest request);
        Task<Response<bool>> DeleteAsync(Guid id);
    }
}
