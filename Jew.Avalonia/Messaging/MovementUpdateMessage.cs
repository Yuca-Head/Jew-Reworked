using System;
using System.Collections.Generic;

namespace Jew.Avalonia.Messaging;

public sealed record MovementUpdateMessage(IEnumerable<int>? MovementIds = null, params IEnumerable<Guid>? TransactionsKey);