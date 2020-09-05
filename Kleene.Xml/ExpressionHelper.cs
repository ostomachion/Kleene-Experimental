using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Kleene.Xml
{
    public static class ExpressionHelper
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
                return SequenceExpression.Empty;
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
                        if (element.Nodes().OfType<XText>().Any(t =>
                                !String.IsNullOrWhiteSpace(t.Value)) ||
                                element.Elements().Any(el => el.Name != NS + "item"))
                            throw new Exception();

                        return new AltExpression(element.Elements(NS + "item").Select(item =>
                            ParseNodes(item.Nodes())
                        ));
                    case "elem":
                        return new StructureExpression("elem", new SequenceExpression(new[] {
                            ParseName(element.Element(NS + "name")),
                            ParseAttributes(element.Element(NS + "attrs")),
                            new StructureExpression("value", ParseNodes(element.Element(NS + "value").Nodes()))
                        }));
                    case "attr":
                        return ParseAttribute(element);
                    default:
                        throw new Exception();
                }
            }
            else
            {
                return new StructureExpression("elem", new SequenceExpression(new[] {
                    ParseName(element.Name),
                    ParseAttributes(element.Attributes()),
                    new StructureExpression("value", ParseNodes(element.Nodes()))
                }));
            }
        }

        private static StructureExpression ParseName(XName name) => new StructureExpression("name", new SequenceExpression(new [] {
            new StructureExpression("ns", ParseText(name.NamespaceName)),
            new StructureExpression("local", ParseText(name.LocalName)),
        }));

        private static StructureExpression ParseName(XElement element)
        {
            if (element.Elements().Count() == 1)
            {
                XElement name = element.Elements().Single();
                return new StructureExpression("name", new SequenceExpression(new [] {
                    new StructureExpression("ns", ParseText(name.Name.NamespaceName)),
                    new StructureExpression("local", ParseText(name.Name.LocalName)),
                }));
            }
            else
            {
                return new StructureExpression("name", new SequenceExpression(new [] {
                    new StructureExpression("ns", ParseNodes(element.Element(NS + "ns").Nodes())),
                    new StructureExpression("local", ParseNodes(element.Element(NS + "local").Nodes()))
                }));
            }
        }

        public static Expression ParseText(XText text)
        {
            return ParseString(text.Value);
        }

        public static Expression ParseText(string text)
        {
            return ParseString(text);
        }

        private static Expression ParseString(string value) => (Expression)TextHelper.CreateStructure(value);

        private static Expression ParseAttributes(IEnumerable<XAttribute> attributes)
        {
            // TODO: Permute
            attributes = attributes.Where(x => x.Name.Namespace != XNamespace.Xmlns && x.Name != "xmlns" && x.Name.Namespace != XNamespace.Xml);
            return new StructureExpression("attrs", new SequenceExpression(attributes.Select(ParseAttribute)));
        }

        private static Expression ParseAttributes(XElement element)
        {
            // TODO: Permute
            return new StructureExpression("attrs", ParseNodes(element.Nodes()));
        }

        private static Expression ParseAttribute(XAttribute attribute)
        {
            return new StructureExpression("attr", new SequenceExpression(new [] {
                ParseName(attribute.Name),
                new StructureExpression("value", ParseText(attribute.Value))
            }));
        }

        private static Expression ParseAttribute(XElement element)
        {
            if (element.Elements().Any())
            {
                return new StructureExpression("attr", new SequenceExpression(new [] {
                    ParseName(element.Element(NS + "name")),
                    new StructureExpression("value", ParseNodes(element.Element(NS + "value").Nodes()))
                }));
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
    }
}