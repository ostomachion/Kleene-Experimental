using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Kleene
{
    public class Structure
    {
        public string Name { get; }

        public Structure? FirstChild { get; }
        public Structure? NextSibling { get; }

        public IEnumerable<Structure> Children
        {
            get
            {
                return FirstChild is Structure first
                    ? EnumerableExt.Yield(first).Concat(getSiblingsAfterSelf(first))
                    : Enumerable.Empty<Structure>();

                static IEnumerable<Structure> getSiblingsAfterSelf(Structure structure)
                {
                    return structure.NextSibling is Structure next
                        ? EnumerableExt.Yield(next).Concat(getSiblingsAfterSelf(next))
                        : Enumerable.Empty<Structure>();
                }
            }
        }

        public Structure(string name, Structure? firstChild = null, Structure? nextSibling = null)
        {
            this.Name = name;
            this.FirstChild = firstChild;
            this.NextSibling = nextSibling;
        }

        public override string ToString()
        {
            string value = this.Name;
            value += this.FirstChild is null ? ";" :
                this.FirstChild.NextSibling is null ?
                " " + this.FirstChild + ";" :
                " {\n" + this.FirstChild.ToString().Replace("\n", "\t\n") + "\n} ";

            if (this.NextSibling is Structure)
                value += "\n" + this.NextSibling;

            return value;
        }
    }
}
