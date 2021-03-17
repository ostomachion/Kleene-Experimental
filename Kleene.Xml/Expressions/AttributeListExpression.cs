using System;
using System.Collections.Generic;

namespace Kleene.Xml
{
    public class AttributeListExpression : Expression<RunnableAttributeList>
    {
        public Expression<ObjectSequence<RunnableAttribute>> Expression { get; }

        public AttributeListExpression(Expression<ObjectSequence<RunnableAttribute>> expression)
        {
            Expression = expression;
        }

        public override IEnumerable<NondeterministicObject<RunnableAttributeList>> Run()
        {
            var results = this.Expression.Run();
            foreach (var result in results)
            {
                yield return new NondeterministicAttributeList(result);
            }
        }
    }
}
