using System;

namespace Kleene
{
    public class ConstantPointer<T> : StructurePointer<ConstantStructure<T>>
    {
        public ConstantPointer(ConstantStructure<T> structure)
            : base(structure) { }

        public ConstantPointer(ConstantStructure<T> structure, bool done)
            : base(structure, done) { }

        public override Structure GetCurrent()
        {
            if (this.Done)
                throw new NotImplementedException();

            return this.Structure;
        }

        public override StructurePointer<ConstantStructure<T>> Advance()
        {
            if (this.Done)
                throw new InvalidOperationException();

            if (!(this.Structure is ConstantStructure<T> constant))
                throw new InvalidOperationException();

            return new ConstantPointer<T>(constant, true);
        }
    }
}