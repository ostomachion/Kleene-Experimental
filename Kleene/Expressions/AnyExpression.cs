using System;
using System.Collections.Generic;
using System.Linq;

namespace Kleene
{
    public class AnyExpression : Expression
    {
        public override IEnumerable<IEnumerable<Structure>> Run(IEnumerable<Structure> input, int index)
        {
            if (index == input.Count())
                yield break;

            var structure = input.ElementAt(index);
            yield return new[] { structure };
        }
    }
}