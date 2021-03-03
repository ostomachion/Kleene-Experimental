using System;
using System.Diagnostics;
using System.Linq;

namespace Kleene.Xml
{
    [DebuggerDisplay("{" + nameof(ToString) + "(),nq}")]
    public class NameStructure : Structure
    {
        public TextStructure NS { get; }
        public TextStructure Local { get; }

        public NameStructure(TextStructure ns, TextStructure local)
        {
            this.NS = ns ?? throw new ArgumentNullException(nameof(ns));
            this.Local = local ?? throw new ArgumentNullException(nameof(local));
        }

        public override string ToString()
        {
            return NS.ToString().Any() ? $"{{{this.NS}}}{this.Local}" : this.Local.ToString();
        }
    }
}