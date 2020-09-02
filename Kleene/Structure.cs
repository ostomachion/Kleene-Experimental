using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Kleene
{
    public class Structure
    {
        public string Name { get; }

        public Structure? FirstChildren { get; }
        public Structure? NextSibling { get; }

        public Structure(string name, Structure? fisrtChild, Structure? nextSibling)
        {
            this.Name = name;
            this.FirstChildren = FirstChildren;
            this.NextSibling = nextSibling;
        }
    }
}
