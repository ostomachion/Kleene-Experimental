using System.Collections.ObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Kleene.Xml
{
    public class RunnableAttributeList : IRunnable<RunnableAttributeList>
    {
        public Dictionary<string, RunnableAttribute> Attributes { get; }

        public RunnableAttributeList(IEnumerable<RunnableAttribute> attributes)
        {
            this.Attributes = attributes.ToDictionary(x => x.Name.ToString(), x => x);
        }

        public Expression<RunnableAttributeList> ToExpression()
        {
            // TODO:
            throw new NotImplementedException();
        }
    }
}