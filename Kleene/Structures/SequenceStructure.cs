using System;
using System.Collections.Generic;
using System.Linq;

namespace Kleene
{
    public class SequenceStructure : Structure
    {
        private readonly int index;
        public IEnumerable<Structure> Value { get; }

        public SequenceStructure(IEnumerable<Structure> value)
            : base(false)
        {
            if (value.Contains(null!))
            {
                throw new ArgumentException("Value must not contain null.", nameof(value));
            }

            this.Value = value ?? throw new ArgumentNullException(nameof(value));
            this.index = 0;
        }

        internal SequenceStructure(IEnumerable<Structure> value, int index)
            : base(index == value.Count())
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

        public override Structure GetCurrent()
        {
            throw new NotImplementedException();
        }

        public override Structure Advance()
        {
            throw new NotImplementedException();
        }
    }
}