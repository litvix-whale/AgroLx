namespace Core.Models;

public class ServiceResult<T>
{
    public bool Succeeded { get; set; }
    public T? Data { get; set; }
    public List<string> Errors { get; set; } = new();
}