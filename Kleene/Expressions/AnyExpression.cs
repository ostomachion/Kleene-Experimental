using System;
using System.Collections.Generic;
using System.Linq;

namespace Kleene
{
    public class AnyExpression : Expression
    {
        public override IEnumerable<Result> Run(IEnumerable<Structure> input, int index)
        {
            if (index == input.Count())
                yield break;

            var structure = input.ElementAt(index);
            yield return new Result(input, index, 1, this, Enumerable.Empty<Result>());
        }
    }
}