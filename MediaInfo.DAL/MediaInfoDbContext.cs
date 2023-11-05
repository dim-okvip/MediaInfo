namespace MediaInfo.DAL
{
    public class MediaInfoDbContext : DbContext
    {
        protected readonly IConfiguration _configuration;

        public MediaInfoDbContext(DbContextOptions<MediaInfoDbContext> options, IConfiguration configuration)
           : base(options)
        {
            _configuration = configuration;
        }

        #region DbSet
        public virtual DbSet<ImageInfo> ImageInfos { get; set; }
        #endregion

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string connectionString = _configuration.GetConnectionString("MediaInfoDB");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ImageInfoEntityTypeConfiguration());
        }
    }
}
