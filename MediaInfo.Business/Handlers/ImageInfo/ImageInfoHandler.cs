namespace MediaInfo.Business.Handlers
{
    public class ImageInfoHandler : IImageInfoHandler
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<ImageInfoHandler> _logger;

        public ImageInfoHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<ImageInfoHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Response<List<ImageInfoQueryResult>>> GetAsync(ImageInfoQueryFilterRequest filter)
        {
            try
            {
                int totalCountByFilter = 0;
                IRepository<ImageInfo> repository = _unitOfWork.GetRepository<ImageInfo>();
                var allImageInfoQuery = from b in repository.GetAll() select b;

                if (!String.IsNullOrEmpty(filter.TextSearch))
                    allImageInfoQuery = from b in allImageInfoQuery where (b.Name.Contains(filter.TextSearch)) select b;

                totalCountByFilter = allImageInfoQuery.Count();

                if (filter.OrderBy.HasValue)
                {
                    switch (filter.OrderBy)
                    {
                        case Order.TIME_ASC:
                            allImageInfoQuery = allImageInfoQuery.OrderBy(x => x.Time);
                            break;
                        case Order.TIME_DESC:
                            allImageInfoQuery = allImageInfoQuery.OrderByDescending(x => x.Time);
                            break;
                        default:
                            break;
                    }
                }

                if (filter.PageSize.HasValue && filter.PageNumber.HasValue)
                {
                    if (filter.PageSize <= 0)
                        filter.PageSize = 10;

                    if (filter.PageNumber <= 0)
                        filter.PageNumber = 1;

                    int excludedRows = (filter.PageNumber.Value - 1) * (filter.PageSize.Value);
                    if (excludedRows <= 0)
                        excludedRows = 0;

                    allImageInfoQuery = allImageInfoQuery.Skip(excludedRows).Take(filter.PageSize.Value);
                }

                List<ImageInfo> listAllImageInfo = await allImageInfoQuery.ToListAsync();
                List<ImageInfoQueryResult> listImageInfoQueryResult = new();

                listImageInfoQueryResult = _mapper.Map<List<ImageInfoQueryResult>>(listAllImageInfo);

                int dataCount = listImageInfoQueryResult.Count;
                int totalCount = totalCountByFilter > 0 ? totalCountByFilter : repository.Count();

                if (dataCount > 0)
                    return new Response<List<ImageInfoQueryResult>>(status: HttpStatusCode.OK, message: "Truy vấn thông tin ảnh thành công", data: listImageInfoQueryResult, dataCount: dataCount, totalCount: totalCount);
                else
                    return new Response<List<ImageInfoQueryResult>>(status: HttpStatusCode.NoContent, message: "Dữ liệu không tồn tại hoặc đã bị xóa", data: listImageInfoQueryResult, dataCount: dataCount, totalCount: totalCount);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return new Response<List<ImageInfoQueryResult>>(status: HttpStatusCode.InternalServerError, message: ex.Message, data: new List<ImageInfoQueryResult>());
            }
        }

        public async Task<Response<bool>> CreateAsync(CreateImageInfoRequest request)
        {
            try
            {
                IRepository<ImageInfo> repository = _unitOfWork.GetRepository<ImageInfo>();

                ImageInfo imageInfo = new();
                imageInfo.Id = Guid.NewGuid();
                imageInfo.Name = request.Name ?? String.Empty;
                imageInfo.Size = request.Size.Value;
                imageInfo.Location = request.Location;
                imageInfo.Time = DateTime.Parse(request.Time);

                repository.Create(imageInfo);
                await _unitOfWork.SaveChangesAsync();
             
                return new Response<bool>(status: HttpStatusCode.OK, message: $"Thêm mới thông tin ảnh thành công", data: true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return new Response<bool>(status: HttpStatusCode.InternalServerError, message: ex.Message, data: false);
            }
        }

        public async Task<Response<bool>> DeleteAsync(Guid id)
        {
            try
            {
                IRepository<ImageInfo> repository = _unitOfWork.GetRepository<ImageInfo>();
                ImageInfo imageInfoToDelete = repository.Get(x => x.Id == id);
                if (imageInfoToDelete is null)
                    return new Response<bool>(status: HttpStatusCode.NoContent, message: $"Thông tin ảnh với id: {id} không tồn tại hoặc đã bị xóa", data: false);

                repository.Delete(imageInfoToDelete);
                await _unitOfWork.SaveChangesAsync();

                return new Response<bool>(status: HttpStatusCode.OK, message: "Xóa thông tin ảnh thành công", data: true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return new Response<bool>(status: HttpStatusCode.InternalServerError, message: ex.Message, data: false);
            }
        }
    }
}
