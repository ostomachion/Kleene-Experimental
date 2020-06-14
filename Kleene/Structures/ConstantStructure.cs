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
            if (this.Done)
                throw new NotImplementedException();

            return this;
        }

        public override Structure Advance()
        {
            if (this.Done)
                throw new InvalidOperationException();

            return new ConstantStructure<T>(this.Value, true);
        }
    }
}