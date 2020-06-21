using System.Collections.Generic;
using System.Linq;

namespace Kleene.Xml
{
    public class AttributeExpression : Expression
    {
        public NameExpression Name { get; }
        public TextExpression Value { get; }

        public AttributeExpression(NameExpression name, TextExpression value)
        {
            this.Name = name ?? throw new System.ArgumentNullException(nameof(name));
            this.Value = value ?? throw new System.ArgumentNullException(nameof(value));
        }

        public override IEnumerable<Result> Run(IEnumerable<Structure> input, int index)
        {
            if (index == input.Count())
                yield break;

            var structure = input.ElementAt(index);
            if (!(structure is AttributeStructure attribute))
                yield break;

            foreach (var nameResult in this.Name.Run(new[] { attribute.Name }, 0))
            {
                foreach (var valueResult in this.Value.Run(new [] { attribute.Value }, 0))
                {
                    yield return new Result(input, index, 1, this, new [] { nameResult, valueResult });
                }
            }
        }
    }
}