using System.Xml.Linq;
using Kleene;

namespace Kleene.Xml
{
    public class RunnableElement : IRunnable<RunnableElement>
    {
        public string Name { get; }

        public ObjectSequence<RunnableElement> Children { get; }

        public RunnableElement(string name, ObjectSequence<RunnableElement> children)
        {
            Name = name;
            Children = children;
        }

        public Expression<RunnableElement> ToExpression()
        {
            return new ElementExpression(this.Name.ToRunnable().ToExpression(), this.Children.ToExpression());
        }
    }
}
