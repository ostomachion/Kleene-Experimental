using System.Collections.Generic;

namespace Kleene.Xml
{
    public class NondeterministicAttribute : NondeterministicObject<RunnableAttribute>
    {
        // TODO: Namespaces.
        public NondeterministicObject<ObjectSequence<Runnable<char>>> Name { get; }

        public NondeterministicObject<ObjectSequence<Runnable<char>>> Value { get; }

        public NondeterministicAttribute(NondeterministicObject<ObjectSequence<Runnable<char>>> name, NondeterministicObject<ObjectSequence<Runnable<char>>> value)
        {
            this.Name = name;
            this.Value = value;
        }

        public override IEnumerable<RunnableAttribute> Collapse()
        {
            // TODO:
            throw new System.NotImplementedException();
        }

        public override bool Equals(object? obj)
        {
            // TODO:
            throw new System.NotImplementedException();
        }

        public override bool Equals(NondeterministicObject<RunnableAttribute>? other)
        {
            // TODO:
            throw new System.NotImplementedException();
        }

        public override int GetHashCode()
        {
            // TODO:
            throw new System.NotImplementedException();
        }

        public override IEnumerable<NondeterministicObject<RunnableAttribute>> Overlap(NondeterministicObject<RunnableAttribute> other)
        {
            // TODO:
            throw new System.NotImplementedException();
        }
    }
}
