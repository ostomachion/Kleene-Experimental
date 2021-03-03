using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Kleene.Xml
{
    [DebuggerDisplay("{" + nameof(ToString) + "(),nq}")]
    public class AttributeStructure : Structure
    {
        public NameStructure Name { get; }
        public TextStructure Value { get; }

        public AttributeStructure(NameStructure name, TextStructure value)
        {
            this.Name = name ?? throw new ArgumentNullException(nameof(name));
            this.Value = value ?? throw new ArgumentNullException(nameof(value));
        }

        public override string ToString() => $"{this.Name}=\"{this.Value}\"";
    }
}