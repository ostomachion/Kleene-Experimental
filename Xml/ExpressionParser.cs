using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Kleene;

namespace Kleene.Xml
{
    public static class ExpressionParser
    {
        public static readonly XNamespace NS = "http://hufford.io/kleene/xml";

        public static Expression ParseNode(XNode node)
        {
            if (node is XElement element)
            {
                return ParseElement(element);
            }
            else if (node is XText text)
            {
                return ParseText(text);
            }
            else
            {
                throw new NotSupportedException();
            }
        }

        public static Expression ParseElement(XElement element)
        {
            if (element.Name.Namespace == NS)
            {
                switch (element.Name.LocalName)
                {
                    case "alt":
                        if (element.Nodes().OfType<XText>().Any(t => !String.IsNullOrWhiteSpace(t.Value)) || element.Elements().Any(el => el.Name != NS + "item"))
                            throw new Exception();

                        return new AltExpression(element.Elements(NS + "item").Select(item =>
                            ParseNodes(item.Nodes())
                        ));
                    default:
                        throw new Exception();
                }
            }
            else
            {
                return new ElementExpression(
                    new NameExpression(
                        new TextExpression(new SequenceExpression(
                            element.Name.NamespaceName.Select(c => new ConstantExpression<char>(c))
                        )),
                        new TextExpression(new SequenceExpression(
                            element.Name.LocalName.Select(c => new ConstantExpression<char>(c))
                        ))
                    ),
                    ParseNodes(element.Nodes())
                );
            }
        }

        public static TextExpression ParseText(XText text)
        {
            return new TextExpression(new SequenceExpression(
                text.Value.Select(c => new ConstantExpression<char>(c))
            ));
        }

        private static SequenceExpression ParseNodes(IEnumerable<XNode> nodes)
        {
            return new SequenceExpression(nodes.Select(ParseNode));
        }
    }
}