using System;
using System.Collections.Generic;

namespace Kleene
{
    public class SequenceStructure<T> : Structure
        where T : Structure
    {
        public IEnumerable<T> Value { get; }

        public SequenceStructure(IEnumerable<T> value)
        {
            this.Value = value;
        }
    }
}