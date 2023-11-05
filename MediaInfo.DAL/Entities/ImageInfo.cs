namespace MediaInfo.DAL.Entities
{
    public class ImageInfo
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public long Size { get; set; }
        public string Location { get; set; }
        public DateTime Time { get; set; }
    }
}