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
                    case "rep":
                        return new RepExpression(ParseNodes(element.Nodes()),
                            ParseRepOrder(element.Attribute("order")),
                            ParseMin(element.Attribute("min")),
                            ParseMax(element.Attribute("max")));
                    default:
                        throw new Exception();
                }
            }
            else
            {
                return new ElementExpression(
                    ParseName(element.Name),
                    ParseAttributes(element.Attributes()),
                    ParseNodes(element.Nodes())
                );
            }
        }

        private static NameExpression ParseName(XName name) => new NameExpression(
            ParseText(name.NamespaceName),
            ParseText(name.LocalName)
        );

        public static TextExpression ParseText(XText text)
        {
            return new TextExpression(ParseString(text.Value));
        }
        
        public static TextExpression ParseText(string text)
        {
            return new TextExpression(ParseString(text));
        }

        private static SequenceExpression ParseString(string value) => new SequenceExpression(
            value.Select(c => new ConstantExpression<char>(c))
        );

        private static AttributeListExpression ParseAttributes(IEnumerable<XAttribute> attributes)
        {
            attributes = attributes.Where(x => x.Name.Namespace != XNamespace.Xmlns && x.Name != "xmlns");
            return new AttributeListExpression(new SequenceExpression(attributes.Select(x => new AttributeExpression(
                ParseName(x.Name),
                ParseText(x.Value)
            ))));
        }

        private static SequenceExpression ParseNodes(IEnumerable<XNode> nodes)
        {
            return new SequenceExpression(nodes.Select(ParseNode));
        }

        private static Order ParseRepOrder(XAttribute order) => order?.Value switch
        {
            null => Order.Greedy,
            "greedy" => Order.Greedy,
            "lazy" => Order.Lazy,
            _ => throw new Exception()
        };

        private static int ParseMin(XAttribute min)
        {
            if (min is null)
                return 0;

            return Int32.Parse(min.Value);
        }

        private static int ParseMax(XAttribute max)
        {
            if (max is null)
                return -1;

            return Int32.Parse(max.Value);
        }
    }
}