using System;
using System.Collections.Generic;

namespace Kleene.Xml
{
    public class NondeterministicAttributeList : NondeterministicObject<RunnableAttributeList>
    {
        public NondeterministicObject<ObjectSequence<RunnableAttribute>> Value { get; }

        public NondeterministicAttributeList(NondeterministicObject<ObjectSequence<RunnableAttribute>> value)
        {
            Value = value;
        }

        public override IEnumerable<RunnableAttributeList> Collapse()
        {
            foreach (var list in Value.Collapse())
            {
                yield return new RunnableAttributeList(list);
            }
        }

        public override bool Equals(object? obj)
        {
            throw new NotImplementedException();
        }

        public override bool Equals(NondeterministicObject<RunnableAttributeList>? other)
        {
            throw new NotImplementedException();
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<NondeterministicObject<RunnableAttributeList>> Overlap(NondeterministicObject<RunnableAttributeList> other)
        {
            if (other is NondeterministicAttributeList list)
            {
                if (list.Value is NondeterministicEmptyObjectSequence<RunnableAttribute>)
                {
                    // check if this.Value is also empty
                }
                else if (list.Value is NondeterministicObjectSequence<RunnableAttribute> seq)
                {
                    // 1. get seq.Head.Name
                    // 2. find matching attribute name in other
                    // 3. recurse with seq.Tail, after pruning other
                    // 4. reset to step 2 and keep going
                }
                else
                {
                    throw new Exception();
                }
            }
            else
            {
                throw new ArgumentException("Argument type is not supported.", nameof(other));
            }
        }
    }
}
