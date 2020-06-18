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
        public AttributeListStructure Attributes { get; }
        public IEnumerable<NodeStructure> Value { get; }

        public ElementStructure(NameStructure name, AttributeListStructure attributes, IEnumerable<NodeStructure> value)
        {
            this.Value = value ?? throw new ArgumentNullException(nameof(value));
            this.Attributes = attributes ?? throw new ArgumentNullException(nameof(attributes));
            this.Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        public override string ToString()
        {
            return this.Value.Any() ? $"<{this.Name}{this.Attributes}>{String.Join("", this.Value)}</{this.Name}>" : $"<{this.Name}{this.Attributes}/>";
        }
    }
}