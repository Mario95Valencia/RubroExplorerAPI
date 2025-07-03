using System;
using System.Collections.Generic;

namespace vistarubros.Domain.Entities;

public partial class Example
{
    public long Id { get; set; }

    public string? Name { get; set; }

    public bool Status { get; set; }
}
