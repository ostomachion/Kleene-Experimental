using System.Collections.Generic;
using System.Linq;
using Kleene;

namespace Kleene.Xml
{
    public class NameExpression : Expression
    {
        public TextExpression NS { get; }
        public TextExpression Local { get; }

        public NameExpression(TextExpression ns, TextExpression local)
        {
            this.NS = ns;
            this.Local = local;
        }

        public override IEnumerable<Expression> Run()
        {
            foreach (var ns in this.NS.Run().Cast<TextExpression>())
            {
                foreach (var local in this.Local.Run().Cast<TextExpression>())
                {
                    yield return new NameExpression(ns, local);
                }
            }
        }
    }
}