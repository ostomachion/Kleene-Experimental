using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Kleene
{
    public class Structure
    {
        public string Name { get; }

        public IEnumerable<Structure> Children { get; }

        public Structure(string name, IEnumerable<Structure> children)
        {
            this.Name = name;
            this.Children = children;
        }

        public override string ToString()
        {
            // string value = this.Name;
            // value += this.FirstChild is null ? ";" :
            //     this.FirstChild.NextSibling is null ?
            //     " " + this.FirstChild + ";" :
            //     " {\n" + this.FirstChild.ToString().Replace("\n", "\t\n") + "\n} ";

            // if (this.NextSibling is Structure)
            //     value += "\n" + this.NextSibling;

            // return value;
            throw new NotImplementedException();
        }

        public override bool Equals(object? obj)
        {
            return obj is Structure structure &&
                   Name == structure.Name &&
                   EqualityComparer<IEnumerable<Structure>>.Default.Equals(Children, structure.Children);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Children);
        }

        public static explicit operator Structure?(string text) => TextHelper.CreateStructure(text);
    }
}
