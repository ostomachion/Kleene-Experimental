using System;

namespace Kleene
{
    public class ConstantStructure<T> : Structure
    {
        public T Value { get; }
        public ConstantStructure(T value)
        {
            this.Value = value;
        }
    }
}