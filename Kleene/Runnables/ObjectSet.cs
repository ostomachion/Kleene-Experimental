using System.Collections.ObjectModel;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Kleene
{
    public class ObjectSet<T> : IRunnable<ObjectSet<T>>, IEnumerable<T> where T : IRunnable<T>
    {
        public static readonly ObjectSet<T> Empty = new (Enumerable.Empty<T>());

        private readonly IEnumerable<T> values;

        public ObjectSet(IEnumerable<T> values)
        {
            this.values = values;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return values.GetEnumerator();
        }

        public Expression<ObjectSet<T>> ToExpression() => new SetExpression<T>(new SequenceExpression<T>(this.values.Select(x => x.ToExpression())));

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)values).GetEnumerator();
        }
    }
}
