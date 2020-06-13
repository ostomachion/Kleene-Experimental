using System;
using System.Collections.Generic;
using System.Linq;

namespace Kleene
{
    public class SequenceStructure<T> : Structure
        where T : Structure
    {
        public IEnumerable<T> Value { get; }

        public SequenceStructure(IEnumerable<T> value)
        {
            if (value.Contains(null!))
            {
                throw new ArgumentException("Value must not contain null.", nameof(value));
            }

            this.Value = value ?? throw new ArgumentNullException(nameof(value));
        }
    }
}