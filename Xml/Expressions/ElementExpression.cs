using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Kleene.Xml
{
    public class ElementExpression : Expression
    {
        public NameExpression Name { get; }
        public AttributeListExpression Attributes { get; }
        public Expression Value { get; }

        public ElementExpression(NameExpression name, AttributeListExpression attributes, Expression value)
        {
            this.Name = name ?? throw new ArgumentNullException(nameof(name));
            this.Attributes = attributes ?? throw new ArgumentNullException(nameof(attributes));
            this.Value = value ?? throw new ArgumentNullException(nameof(value));
        }

        public override IEnumerable<IEnumerable<Structure>> Run(IEnumerable<Structure> input, int index)
        {
            if (index == input.Count())
                yield break;

            var structure = input.ElementAt(index);
            if (!(structure is ElementStructure element))
                yield break;

            foreach (var nameResult in this.Name.Run(new[] { element.Name }, 0))
            {
                foreach (var attributesResult in this.Attributes.Run(new[] { element.Attributes }, 0))
                {
                    foreach (var valueResult in this.Value.Run(element.Value, 0).Where(x => x.Count() == element.Value.Count()))
                    {
                        yield return new[] {
                            new ElementStructure(
                                (NameStructure)nameResult.Single(),
                                (AttributeListStructure)attributesResult.Single(),
                                valueResult)
                        };
                    }
                }
            }
        }
    }
}