using System.Collections.Generic;

namespace Jew.Avalonia.Messaging;

public sealed record StockStateUpdatedMessage(params IEnumerable<string> ProductsChangedCode);