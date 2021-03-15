using System;
using System.Linq;
using System.Xml.Linq;

namespace Kleene.Xml
{
    public static class XLinqExtensions
    {
        public static RunnableElement ToRunnable(this XElement element)
        {
            if (element.Name.NamespaceName is not "")
                throw new NotImplementedException();
            
            return new RunnableElement(element.Name.LocalName,
                new ObjectSequence<RunnableElement>(element.Nodes().Select(x => x switch {
                    XElement child => child.ToRunnable(),
                    XText ws when String.IsNullOrWhiteSpace(ws.Value) => null,
                    _ => throw new NotImplementedException()
                }).OfType<RunnableElement>()));
        }
    }
}