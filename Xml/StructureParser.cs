using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Kleene;

namespace Kleene.Xml
{
    public static class StructureParser
    {
        public static IEnumerable<Structure> ParseNode(XNode node)
        {
            if (node is XElement element)
            {
                return new [] { ParseElement(element) };
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

        public static ElementStructure ParseElement(XElement element)
        {
            return new ElementStructure(
                ParseName(element.Name),
                ParseAttributes(element.Attributes()),
                element.Nodes().Where(x => !(x is XComment)).SelectMany(ParseNode)
            );
        }

        public static AttributeListStructure ParseAttributes(IEnumerable<XAttribute> attributes) => new AttributeListStructure(
            attributes.Where(x => x.Name.Namespace != XNamespace.Xmlns && x.Name != "xmlns" && x.Name.Namespace != XNamespace.Xml).Select(ParseAttribute)
        );

        public static AttributeStructure ParseAttribute(XAttribute attribute) => new AttributeStructure(
            ParseName(attribute.Name),
            new TextStructure(attribute.Value)
        );

        public static IEnumerable<Structure> ParseText(XText text)
        {
            return text.Value.Select(c => new ConstantStructure<char>(c));
        }

        public static NameStructure ParseName(XName name)
        {
            return new NameStructure(new TextStructure(name.NamespaceName), new TextStructure(name.LocalName));
        }
    }
}