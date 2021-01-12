using System.Collections.Generic;
using System.Linq;

namespace Kleene.Xml
{
    public class AttributeListExpression : Expression
    {
        public Expression Value { get; }

        public AttributeListExpression(Expression value)
        {
            this.Value = value ?? throw new System.ArgumentNullException(nameof(value));
        }

        public override IEnumerable<Result> Run(IEnumerable<Structure> input, int index)
        {
            if (index == input.Count())
                yield break;

            var structure = input.ElementAt(index);
            if (structure is not AttributeListStructure attributes)
                yield break;

            // TODO: Match attributes in order for now.
            foreach (var result in this.Value.Run(attributes.Value, 0).Where(x => x.Length == attributes.Value.Count()))
            {
                yield return new Result(input, index, 1, this, new [] { result });
            }
        }
    }
}