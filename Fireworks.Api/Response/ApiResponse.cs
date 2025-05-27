namespace Fireworks.Api.Response;

public class ApiResponse<T>
{
    public string Status { get; set; } = null!;
    public T? Value { get; set; }
    public List<string> Errors { get; set; } = [];
    public Dictionary<string, string[]> ValidationErrors { get; set; } = new();
}
