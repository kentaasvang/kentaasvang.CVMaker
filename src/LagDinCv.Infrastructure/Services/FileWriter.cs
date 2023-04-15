using LagDinCv.Application.Interfaces;

namespace LagDinCv.Infrastructure.Services;

public class FileWriter : IFileWriter
{
    public async Task<string> WriteContentToFileAsync(string content, string filePath)
    {
        await File.WriteAllTextAsync(filePath, content);
        return filePath;
    }
}