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

        public override IEnumerable<IEnumerable<Structure>> Run(IEnumerable<Structure> input, int index)
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
                    yield return new[] {
                        new AttributeStructure(
                            (NameStructure)nameResult.Single(),
                            (TextStructure)valueResult.Single())
                    };
                }
            }
        }
    }
}