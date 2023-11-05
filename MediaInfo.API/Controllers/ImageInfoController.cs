using Microsoft.AspNetCore.Mvc;

namespace MediaInfo.API.Controllers
{
    [Route("api")]
    [ApiController]
    public class ImageInfoController : ControllerBase
    {
        private readonly IImageInfoHandler _imageInfoHandler;

        public ImageInfoController(IImageInfoHandler imageInfoHandler)
        {
            _imageInfoHandler = imageInfoHandler;
        }

        #region Read
        [HttpGet]
        [Route("imageInfos")]
        public async Task<IActionResult> GetAllFilter([FromQuery] int? pageNumber, int? pageSize, string? textSearch, Order? orderBy)
        {
            ImageInfoQueryFilterRequest filter = new() { PageNumber = pageNumber, PageSize = pageSize, TextSearch = textSearch, OrderBy = orderBy };
            Response<List<ImageInfoQueryResult>> response = await _imageInfoHandler.GetAsync(filter);
            return Ok(response);
        }
        #endregion

        #region CUD
        [HttpPost]
        [Route("imageInfos")]
        public async Task<IActionResult> Create([FromBody] CreateImageInfoRequest request)
        {
            if (ModelState.IsValid)
            {
                Response<bool> response = await _imageInfoHandler.CreateAsync(request);
                return Ok(response);
            }
            return BadRequest(ModelState);
        }

        [HttpDelete]
        [Route("imageInfos/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            Response<bool> response = await _imageInfoHandler.DeleteAsync(id);
            return Ok(response);
        }
        #endregion
    }
}
