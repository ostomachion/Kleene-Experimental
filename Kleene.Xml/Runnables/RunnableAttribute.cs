using System;

namespace Kleene.Xml
{
    public class RunnableAttribute : IRunnable<RunnableAttribute>
    {
        public string Name { get; }

        public string Value { get; }

        public RunnableAttribute(string name, string value)
        {
            this.Name = name;
            this.Value = value;
        }

        public Expression<RunnableAttribute> ToExpression()
        {
            // TODO:
            throw new NotImplementedException();
        }
    }
}
