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

        public static implicit operator ConstantStructure<T>(T value) => new ConstantStructure<T>(value);
        public static implicit operator T(ConstantStructure<T> structure) => structure.Value;
    }
}