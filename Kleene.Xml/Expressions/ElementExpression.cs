using System.Collections.Generic;

namespace Kleene.Xml
{
    public class ElementExpression : Expression<RunnableElement>
    {
        public Expression<ObjectSequence<Runnable<char>>> Name { get; }

        public Expression<ObjectSequence<RunnableElement>> Children { get; }

        public ElementExpression(Expression<ObjectSequence<Runnable<char>>> name, Expression<ObjectSequence<RunnableElement>> children)
        {
            Name = name;
            Children = children;
        }

        public override IEnumerable<NondeterministicObject<RunnableElement>> Run()
        {
            foreach (var name in this.Name.Run())
            {
                yield return new NondeterministicElement(name, this.Children.Run());
            }
        }
    }
}
