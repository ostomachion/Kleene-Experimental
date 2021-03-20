using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Kleene
{
    public class ObjectDictionary<TKey, TValue> : IRunnable<ObjectDictionary<TKey, TValue>> where TKey : IRunnable<TKey> where TValue : IRunnable<TValue>
    {       
        public ObjectDictionary()
        {
        }

        public Expression<ObjectDictionary<TKey, TValue>> ToExpression()
        {
            throw new NotImplementedException();
        }
    }
}
