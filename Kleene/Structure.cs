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
                return this.FirstChild is Structure first
                    ? EnumerableExt.Yield(first).Concat(first.SiblingsAfterSelf)
                    : Enumerable.Empty<Structure>();
            }
        }

        public IEnumerable<Structure> SiblingsAfterSelf
        {
            get
            {
                return this.NextSibling is Structure next
                    ? EnumerableExt.Yield(next).Concat(next.SiblingsAfterSelf)
                    : Enumerable.Empty<Structure>();
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

        public override bool Equals(object? obj)
        {
            return obj is Structure structure &&
                   Name == structure.Name &&
                   EqualityComparer<Structure?>.Default.Equals(FirstChild, structure.FirstChild) &&
                   EqualityComparer<Structure?>.Default.Equals(NextSibling, structure.NextSibling);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, FirstChild, NextSibling);
        }

        public static explicit operator Structure?(string text) => TextHelper.CreateStructure(text);
    }
}
