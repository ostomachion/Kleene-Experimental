using System;

namespace Kleene
{
    public class ConstantStructure<T> : Structure
    {
        public T Value { get; }

        public ConstantStructure(T value)
            : base (false)
        {
            this.Value = value;
        }

        protected ConstantStructure(T value, bool done)
            : base (done)
        {
            this.Value = value;
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