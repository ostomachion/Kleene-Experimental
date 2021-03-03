using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Kleene.Xml
{
    [DebuggerDisplay("{" + nameof(ToString) + "(),nq}")]
    public class ElementStructure : Structure
    {
        public NameStructure Name { get; }
        public AttributeListStructure Attributes { get; }
        public IEnumerable<Structure> Value { get; }

        public ElementStructure(NameStructure name, AttributeListStructure attributes, IEnumerable<Structure> value)
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