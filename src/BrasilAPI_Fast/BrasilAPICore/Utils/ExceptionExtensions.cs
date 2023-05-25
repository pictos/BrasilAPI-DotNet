using System.Runtime.CompilerServices;

namespace BrasilAPI.Utils;

static class ExceptionExtensions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void ThrowIfNull(object? obj)
    {
        if (obj is null)
            throw new ArgumentNullException(nameof(obj));
    }
}
