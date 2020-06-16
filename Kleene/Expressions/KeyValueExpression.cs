using System;
using System.Collections.Generic;
using System.Linq;

namespace Kleene
{
    public class KeyValueExpression : Expression
    {
        public Expression Key { get; }
        public Expression Value { get; }

        public KeyValueExpression(Expression key, Expression value)
        {
            this.Key = key ?? throw new ArgumentNullException(nameof(key));
            this.Value = value ?? throw new ArgumentNullException(nameof(value));
        }

        public override IEnumerable<Expression> Run()
        {
            foreach (var key in this.Key.Run())
            {
                foreach (var value in this.Value.Run())
                {
                    yield return new KeyValueExpression(key, value);
                }
            }
        }
    }
}