using System;
using System.Collections.Generic;
using System.Linq;
using Kleene;

namespace Kleene.Xml
{
    public class TextExpression : Expression
    {
        public SequenceExpression Value { get; }

        public TextExpression(SequenceExpression value)
        {
            this.Value = value ?? throw new ArgumentNullException(nameof(value));
        }

        public override IEnumerable<Expression> Run()
        {
            foreach (var value in this.Value.Run().Cast<SequenceExpression>())
            {
                yield return new TextExpression(value);
            }
        }
    }
}
