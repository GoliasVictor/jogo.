public class ResourceLoadException : Exception
{
    public ResourceLoadException() { }

    public ResourceLoadException(string message) : base(message) { }

    public ResourceLoadException(string message, Exception innerException) : base(message, innerException) { }
}