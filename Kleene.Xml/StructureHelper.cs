using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Kleene;

namespace Kleene.Xml
{
    public static class StructureHelper
    {
        public static Structure? CreateStructure(XNode? node)
        {
            return node switch
            {
                null => null,
                XElement element => CreateStructure(element),
                XText text => CreateStructure(text),
                _ => throw new InvalidOperationException(),
            };
        }

        public static Structure? CreateStructure(XElement? element)
        {
            if (element is null)
                return null;

            return new Structure("elem",
                CreateStructure(element.Name,
                new Structure("attrs",
                    CreateStructure(element.Attributes()),
                new Structure("value",
                    CreateStructure(element.FirstNode)))),
            CreateStructure(element.NextNode));
        }

        public static Structure CreateStructure(XName name, Structure? next = null)
        {
            return new Structure("name",
                new Structure("ns",
                    TextHelper.CreateStructure(name.NamespaceName),
                new Structure("local",
                    TextHelper.CreateStructure(name.LocalName))),
            next);
        }

        public static Structure? CreateStructure(IEnumerable<XAttribute> attributes)
        {
            attributes = attributes.Where(x => x.Name.NamespaceName != XNamespace.Xml && x.Name.Namespace != XNamespace.Xmlns && x.Name != "xmlns");
            return attributes.Any() ? CreateStructure(attributes.First(), CreateStructure(attributes.Skip(1))) : null;
        }

        public static Structure? CreateStructure(XAttribute attribute, Structure? next = null)
        {
            if (attribute is null)
                return null;

            return new Structure("attr",
                CreateStructure(attribute.Name,
                new Structure("value",
                    TextHelper.CreateStructure(attribute.Value))),
            next);
        }

        public static Structure? CreateStructure(XText? text)
        {
            return TextHelper.CreateStructure(text?.Value, CreateStructure(text?.NextNode));
        }
    }
}
