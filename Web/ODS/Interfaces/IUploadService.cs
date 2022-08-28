namespace ODS.Interfaces
{
    public interface IUploadService
    {
        Task<bool> DeleteFileAsync(string filePath);
        Task<string> UploadFileAsync(string filePath, MemoryStream data);
    }
}
