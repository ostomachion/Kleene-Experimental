using System;
using System.Collections.Generic;
using System.Linq;

namespace Kleene
{
    public class ConjunctionExpression<T> : Expression<T> where T : notnull
    {
        public Expression<T> Leader { get; }
        public Expression<T> Follower { get; }

        public ConjunctionExpression(Expression<T> leader, Expression<T> follower)
        {
            if (leader is null)
            {
                throw new ArgumentNullException(nameof(leader));
            }

            if (follower is null)
            {
                throw new ArgumentNullException(nameof(follower));
            }

            this.Leader = leader;
            this.Follower = follower;
        }

        public override void Step()
        {
            // TODO:
            throw new NotImplementedException();
        }
    }
}
