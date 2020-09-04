using System;
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
                    CreateStructure(element.FirstAttribute),
                new Structure("value",
                    CreateStructure(element.FirstNode)))),
            CreateStructure(element.NextNode));
        }

        public static Structure CreateStructure(XName name, Structure? next = null) {
            return new Structure("name",
                new Structure("ns",
                    TextHelper.CreateStructure(name.NamespaceName),
                new Structure("local",
                    TextHelper.CreateStructure(name.LocalName))),
            next);
        }

        public static Structure? CreateStructure(XAttribute attribute)
        {
            if (attribute is null)
                return null;

            return new Structure("attr",
                CreateStructure(attribute.Name,
                new Structure("value",
                    TextHelper.CreateStructure(attribute.Value))));
        }

        public static Structure? CreateStructure(XText? text)
        {
            return TextHelper.CreateStructure(text?.Value, CreateStructure(text?.NextNode));
        }
    }
}
