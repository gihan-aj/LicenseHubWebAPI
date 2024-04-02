namespace LicenseHubWebAPI.Domain.Entities
{
    public class Package
    {
        public required string PackageCode { get; set; }
        public required string PackageName { get; set; }
        public string Description { get; set;} = string.Empty;

    }
}
