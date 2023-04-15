namespace LagDinCv.Application.Interfaces;

public interface IFileWriter
{
    Task<string> WriteContentToFileAsync(string content, string filePath);
}