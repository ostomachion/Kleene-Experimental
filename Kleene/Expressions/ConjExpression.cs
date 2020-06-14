using System;
using System.Collections.Generic;
using System.Linq;

namespace Kleene
{
    public class ConjExpression : Expression
    {
        public Expression Leader { get; }
        public Expression Follower { get; }

        public ConjExpression(Expression leader, Expression follower)
        {
            this.Leader = leader ?? throw new ArgumentNullException(nameof(leader));
            this.Follower = follower ?? throw new ArgumentNullException(nameof(follower));
        }

        public override IEnumerable<TIn> Run<TIn>(TIn input)
        {
            foreach (var attempt in this.Leader.Run(input))
            {
                foreach (var result in this.Follower.Run(attempt))
                {
                    yield return result;
                }
            }
        }
    }
}