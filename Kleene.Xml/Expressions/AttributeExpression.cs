using System.Collections.Generic;

namespace Kleene.Xml
{
    public class AttributeExpression : Expression<RunnableAttribute>
    {
        // TODO: Namespaces.
        public Expression<ObjectSequence<Runnable<char>>> Name { get; }

        public Expression<ObjectSequence<Runnable<char>>> Value { get; }

        public AttributeExpression(Expression<ObjectSequence<Runnable<char>>> name, Expression<ObjectSequence<Runnable<char>>> value)
        {
            Name = name;
            Value = value;
        }

        public override IEnumerable<NondeterministicObject<RunnableAttribute>> Run()
        {
            // TODO:
            throw new System.NotImplementedException();
        }
    }
}
