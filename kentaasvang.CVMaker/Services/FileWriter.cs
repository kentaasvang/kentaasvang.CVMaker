namespace kentaasvang.CVMaker.Services;

public class FileWriter
{
    public async Task<string> WriteContentToFileAsync(string content, string filePath)
    {
        await File.WriteAllTextAsync(filePath, content);
        return filePath;
    }
}