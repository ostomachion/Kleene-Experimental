using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Kleene.Xml
{
    public class NameExpression : Expression
    {
        public TextExpression NS { get; }
        public TextExpression Local { get; }

        public NameExpression(TextExpression ns, TextExpression local)
        {
            this.NS = ns ?? throw new ArgumentNullException(nameof(ns));
            this.Local = local ?? throw new ArgumentNullException(nameof(local));
        }

        public override IEnumerable<IEnumerable<Structure>> Run(IEnumerable<Structure> input, int index)
        {
            if (index == input.Count())
                yield break;

            var structure = input.ElementAt(index);
            if (!(structure is NameStructure name))
                yield break;

            foreach (var nsResult in this.NS.Run(new[] { name.NS }, 0).Where(x => x.Count() == 1))
            {
                foreach (var localResult in this.Local.Run(new[] { name.Local }, 0).Where(x => x.Count() == 1))
                {
                    yield return new[] { new NameStructure(
                        nsResult.Cast<TextStructure>().Single(),
                        localResult.Cast<TextStructure>().Single()) };
                }
            }
        }
    }
}