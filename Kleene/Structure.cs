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
                value += " " + this.NextSibling;
            
            return value;
        }
    }
}
