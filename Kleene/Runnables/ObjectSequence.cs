using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Kleene
{
    public class ObjectSequence<T> : IRunnable<ObjectSequence<T>>, IEnumerable<T> where T : IRunnable<T>
    {
        public static readonly ObjectSequence<T> Empty = new (Enumerable.Empty<T>());

        private readonly IEnumerable<T> values;

        public ObjectSequence(IEnumerable<T> values)
        {
            this.values = values;
        }

        public Expression<ObjectSequence<T>> ToExpression()
        {
            return new SequenceExpression<T>(this.values.Select(x => x.ToExpression()));
        }

        public IEnumerator<T> GetEnumerator()
        {
            return values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)values).GetEnumerator();
        }
    }
}
