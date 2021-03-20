using System;
using System.Collections.Generic;
using System.Linq;

namespace Kleene
{
    public class DictionaryExpression<TKey, TValue> : Expression<ObjectDictionary<TKey, TValue>> where TKey : IRunnable<TKey> where TValue : IRunnable<TValue>
    {
        public DictionaryExpression()
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<NondeterministicObject<ObjectDictionary<TKey, TValue>>> Run()
        {
            throw new NotImplementedException();
        }
    }
}
