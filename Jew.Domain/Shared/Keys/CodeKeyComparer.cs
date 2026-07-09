using System.Diagnostics.CodeAnalysis;

namespace Jew.Domain.Shared.Keys;

public sealed class CodeKeyComparer : IEqualityComparer<CodeKey>
{
    public bool Equals(CodeKey x, CodeKey y)
    {
        if(x.Length != y.Length)
            return false;
        return StringComparer.OrdinalIgnoreCase.Equals(x.Key, y.Key);
    }

    public int GetHashCode([DisallowNull] CodeKey obj)
    {
        return StringComparer.OrdinalIgnoreCase.GetHashCode(obj.Key);
    }
}