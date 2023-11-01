namespace MyCustomUmbracoProject.Services;

public class Response<T>
{
    public bool IsSuccessful { get; set; }
    public string Message { get; set; }
    public T Data { get; set; }
}