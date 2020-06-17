using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Kleene;

namespace Kleene.Xml
{
    public static class StructureParser
    {
        public static NodeStructure ParseNode(XNode node)
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

        public static ElementStructure ParseElement(XElement element)
        {
            return new ElementStructure(
                ParseName(element.Name),
                element.Nodes().Select(ParseNode)
            );
        }

        public static TextStructure ParseText(XText text)
        {
            return new TextStructure(text.Value);
        }

        public static NameStructure ParseName(XName name)
        {
            return new NameStructure(new TextStructure(name.NamespaceName), new TextStructure(name.LocalName));
        }
    }
}