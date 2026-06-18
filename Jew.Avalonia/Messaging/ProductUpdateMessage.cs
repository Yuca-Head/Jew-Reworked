using System.Collections.Generic;

namespace Jew.Avalonia.Messaging;

public sealed record ProductUpdateMessage(params IEnumerable<string> ProductsChangedCode)
{
    public required Activator Action {get; init;}
    public enum Activator
    {
        Added,
        Modified
    }
}