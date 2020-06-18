using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Kleene.Xml
{
    [DebuggerDisplay("{" + nameof(ToString) + "(),nq}")]
    public class AttributeListStructure : Structure
    {
        public IEnumerable<AttributeStructure> Value { get; }

        public AttributeListStructure(IEnumerable<AttributeStructure> value)
        {
            this.Value = value ?? throw new System.ArgumentNullException(nameof(value));
        }

        public override string ToString() => String.Join("", this.Value.Select(x => $" {x}"));
    }
}