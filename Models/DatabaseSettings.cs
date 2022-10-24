namespace EcoGasBackend.Models
{
    public class DatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
        public string StationsCollectionName { get; set; } = null!;
        public string OwnerCollectionName { get; set; } = null!;
        public string UsersCollectionName { get; set; } = null!;
    }
}
