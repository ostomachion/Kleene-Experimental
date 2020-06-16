using System;
using System.Collections.Generic;
using System.Linq;
using Kleene;

namespace Kleene.Xml
{
    public class ElementExpression : Expression
    {
        public NameExpression Name { get; }
        public SequenceExpression Value { get; }

        public ElementExpression(NameExpression name, SequenceExpression value)
        {
            this.Name = name ?? throw new ArgumentNullException(nameof(name));
            this.Value = value ?? throw new ArgumentNullException(nameof(value));
        }

        public override IEnumerable<Expression> Run()
        {
            foreach (var name in this.Name.Run().Cast<NameExpression>())
            {
                foreach (var value in this.Value.Run().Cast<SequenceExpression>())
                {
                    yield return new ElementExpression(name, value);
                }
            }
        }
    }
}
