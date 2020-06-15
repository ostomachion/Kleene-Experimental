using System;
using System.Collections.Generic;
using System.Linq;

namespace Kleene
{
    public class KeyValueExpression<T> : Expression<KeyValueExpression<T>>
    where T : Expression<T>
    {
        public SequenceExpression<ConstantExpression<char>> Key { get; }
        public SequenceExpression<ConstantExpression<char>> Value { get; }

        public KeyValueExpression(SequenceExpression<ConstantExpression<char>> key, SequenceExpression<ConstantExpression<char>> value)
        {
            this.Key = key ?? throw new ArgumentNullException(nameof(key));
            this.Value = value ?? throw new ArgumentNullException(nameof(value));
        }

        public override IEnumerable<KeyValueExpression<T>> Run()
        {
            foreach (var key in this.Key.Run())
            {
                foreach (var value in this.Value.Run())
                {
                    yield return new KeyValueExpression<T>(key, value);
                }
            }
        }
    }
}