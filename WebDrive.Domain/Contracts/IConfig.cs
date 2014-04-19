namespace WebDrive.Domain.Contracts
{
    public interface IConfig
    {
        string FileDirectory { get; }
        string ConnectionString { get; }
        string PictureDirectory { get; }
        string MessageDirectory { get; }
    }
}