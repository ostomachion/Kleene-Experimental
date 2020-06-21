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
            else if (node is XComment)
            {
                return new SequenceExpression(Enumerable.Empty<Expression>());
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
                            ParseOrder(element.Attribute("order")),
                            ParseMin(element.Attribute("min")),
                            ParseMax(element.Attribute("max")));
                    case "any":
                        return new AnyExpression();
                    case "all":
                        return new AllExpression(ParseOrder(element.Attribute("order")));
                    case "capture":
                        return new CaptureExpression(ParseNodes(element.Nodes()), element.Attribute("name").Value);
                    case "atom":
                        return new AtomExpression(ParseNodes(element.Nodes()));
                    case "elem":
                        return new ElementExpression(
                            ParseName(element.Element(NS + "name")),
                            ParseAttributes(element.Element(NS + "attrs")),
                            ParseNodes(element.Element(NS + "value").Nodes()));
                    case "attr":
                        return ParseAttribute(element);
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

        private static NameExpression ParseName(XElement element)
        {
            if (element.Elements().Count() == 1)
            {
                XElement name = element.Elements().Single();
                return new NameExpression(
                    ParseText(name.Name.NamespaceName),
                    ParseText(name.Name.LocalName));
            }
            else
            {
                return new NameExpression(
                    new TextExpression(ParseNodes(element.Element(NS + "ns").Nodes())),
                    new TextExpression(ParseNodes(element.Element(NS + "local").Nodes())));
            }
        }

        public static Expression ParseText(XText text)
        {
            return ParseString(text.Value);
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
            return new AttributeListExpression(new SequenceExpression(attributes.Select(ParseAttribute)));
        }

        private static AttributeListExpression ParseAttributes(XElement element)
        {
            return new AttributeListExpression(ParseNodes(element.Nodes()));
        }

        private static AttributeExpression ParseAttribute(XAttribute attribute)
        {
            return new AttributeExpression(
                ParseName(attribute.Name),
                ParseText(attribute.Value)
            );
        }

        private static AttributeExpression ParseAttribute(XElement element)
        {
            if (element.Elements().Any())
            {
                return new AttributeExpression(
                    ParseName(element.Element(NS + "name")),
                    new TextExpression(ParseNodes(element.Element(NS + "value").Nodes())));
            }
            else
            {
                return ParseAttribute(element.Attributes().Single());
            }
        }

        private static SequenceExpression ParseNodes(IEnumerable<XNode> nodes)
        {
            return new SequenceExpression(nodes.Select(ParseNode));
        }

        private static Order ParseOrder(XAttribute order) => order?.Value switch
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