namespace Domain.Common.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string key, string objectName) : base($"Queried object {objectName} was not found, Key: {key}") { }
        public NotFoundException(string message) : base(message) { }
    }
}
