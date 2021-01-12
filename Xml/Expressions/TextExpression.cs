using System;
using System.Collections.Generic;
using System.Linq;

namespace Kleene.Xml
{
    public class TextExpression : Expression
    {
        public Expression Value { get; }

        public TextExpression(Expression value)
        {
            this.Value = value ?? throw new ArgumentNullException(nameof(value));
        }

        public override IEnumerable<Result> Run(IEnumerable<Structure> input, int index)
        {
            if (index == input.Count())
                yield break;

            var structure = input.ElementAt(index);
            if (structure is not TextStructure text)
                yield break;

            foreach (var result in this.Value.Run(text.Value, 0).Where(x => x.Length == text.Value.Count()))
            {
                yield return new Result(input, index, 1, this, new [] { result });
            }
        }
    }
}