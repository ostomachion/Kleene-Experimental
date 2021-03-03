using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Kleene.Xml
{
    [DebuggerDisplay("{" + nameof(ToString) + "(),nq}")]
    public class TextStructure : Structure
    {
        public IEnumerable<ConstantStructure<char>> Value { get; }

        public TextStructure(string value)
        {
            this.Value = value?.Select(c => new ConstantStructure<char>(c)) ?? throw new ArgumentNullException(nameof(value));
        }

        public TextStructure(IEnumerable<ConstantStructure<char>> value)
        {
            this.Value = value ?? throw new ArgumentNullException(nameof(value));
        }

        public override string ToString() => String.Join("", Value.Select(x => x.Value));
    }
}