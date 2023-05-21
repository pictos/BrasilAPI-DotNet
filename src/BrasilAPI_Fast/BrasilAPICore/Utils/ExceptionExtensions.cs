namespace BrasilAPI.Utils;

static class ExceptionExtensions
{
    public static void ThrowIfNull(object? obj)
    {
        if (obj is null)
            throw new ArgumentNullException(nameof(obj));
    }
}
