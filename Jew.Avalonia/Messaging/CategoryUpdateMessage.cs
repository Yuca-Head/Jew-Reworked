using System.Collections.Generic;

namespace Jew.Avalonia.Messaging;

public sealed record CategoryUpdateMessage(params IEnumerable<string> CategoriesChangedName);