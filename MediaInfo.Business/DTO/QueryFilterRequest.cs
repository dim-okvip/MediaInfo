namespace MediaInfo.Business.DTO
{
    public class QueryFilterRequest
    {
        public Guid? Id { get; set; } = null;
        public int? PageSize { get; set; } = null;
        public int? PageNumber { get; set; } = null;
        public string TextSearch { get; set; } = String.Empty;
        public bool? Status { get; set; } = null;
        public Order? OrderBy { get; set; } = null;
    }

    public enum Order
    {
        TIME_ASC,
        TIME_DESC,
    }

    public enum DataSource
    {
        Database,
        Memory
    }
}
