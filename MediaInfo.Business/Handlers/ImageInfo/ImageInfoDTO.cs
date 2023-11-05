using System.ComponentModel.DataAnnotations;

namespace MediaInfo.Business.Handlers
{
    public class ImageInfoQueryResult
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public long Size { get; set; }
        public string Location { get; set; }
        public DateTime Time { get; set; }
    }

    public class ImageInfoQueryFilterRequest : QueryFilterRequest
    {
    }

    public class CreateImageInfoRequest
    {
        public string? Name { get; set; }

        [Required]
        public long? Size { get; set; }
        
        [Required]
        public string Location { get; set; }
        
        [Required]
        public string Time { get; set; }
    }
}
