using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Kleene.Xml
{
    [DebuggerDisplay("{" + nameof(ToString) + "(),nq}")]
    public class ElementStructure : NodeStructure
    {
        public NameStructure Name { get; }
        public IEnumerable<NodeStructure> Value { get; }

        public ElementStructure(NameStructure name, IEnumerable<NodeStructure> value)
        {
            this.Value = value ?? throw new ArgumentNullException(nameof(value));
            this.Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        public override string ToString()
        {
            return this.Value.Any() ? $"<{this.Name}>{String.Join("", this.Value)}</{this.Name}>" : $"<{this.Name}/>";
        }
    }
}