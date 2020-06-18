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

        public override IEnumerable<IEnumerable<Structure>> Run(IEnumerable<Structure> input, int index)
        {
            if (index == input.Count())
                yield break;

            var structure = input.ElementAt(index);
            if (!(structure is AttributeListStructure attributes))
                yield break;

            // TODO: Match attributes in order for now.
            var count = attributes.Value.Count();
            foreach (var result in this.Value.Run(attributes.Value, 0).Where(x => x.Count() == attributes.Value.Count()))
            {
                yield return new [] { new AttributeListStructure(result.Cast<AttributeStructure>()) };
            }
        }
    }
}