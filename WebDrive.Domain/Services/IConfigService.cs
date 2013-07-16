namespace LondonUbfWebDrive.Domain.Model
{
    public interface IConfigService
    {
        string BaseFolder { get; }
        string ConnectionString { get; }
    }
}