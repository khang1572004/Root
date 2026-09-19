namespace Service.Application.Common;

public sealed class NotFoundException(string resourceName, object key)
    : Exception($"{resourceName} with key '{key}' was not found.")
{
}
