using System;
using System.Collections.Generic;
using System.Linq;

namespace Kleene
{
    public class SequenceStructure<T> : Structure
    where T : Structure
    {
        private readonly int index;
        public IEnumerable<T> Value { get; }
        public T? Current => this.index == this.Value.Count() ? null : this.Value.ElementAt(this.index);

        public SequenceStructure(IEnumerable<T> value)
        {
            if (value.Contains(null!))
            {
                throw new ArgumentException("Value must not contain null.", nameof(value));
            }

            this.Value = value ?? throw new ArgumentNullException(nameof(value));
            this.index = 0;
        }

        internal SequenceStructure(IEnumerable<T> value, int index)
        {
            if (value.Contains(null!))
            {
                throw new ArgumentException("Value must not contain null.", nameof(value));
            }

            if (index < 0 || index > value.Count())
            {
                throw new ArgumentException("Index must be greater than zero and less than the size of the value.", nameof(index));
            }

            this.Value = value ?? throw new ArgumentNullException(nameof(value));
            this.index = index;
        }
    }
}