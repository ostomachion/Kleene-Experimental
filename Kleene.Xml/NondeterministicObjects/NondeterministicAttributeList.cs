using System;
using System.Collections.Generic;

namespace Kleene.Xml
{
    public class NondeterministicAttributeList : NondeterministicObject<RunnableAttributeList>
    {
        public override IEnumerable<RunnableAttributeList> Collapse()
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }
    }
}
