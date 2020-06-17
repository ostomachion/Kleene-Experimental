using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Kleene.Xml
{
    public class ElementExpression : Expression
    {
        public NameExpression Name { get; }
        public Expression Value { get; }

        public ElementExpression(NameExpression name, Expression value)
        {
            this.Name = name;
            this.Value = value;
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
                foreach (var valueResult in this.Value.Run(element.Value, 0))
                {
                    yield return new[] { new ElementStructure((NameStructure)nameResult.Single(), valueResult.Cast<NodeStructure>()) };
                }
            }
        }
    }
}