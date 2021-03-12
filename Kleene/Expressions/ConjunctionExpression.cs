using System;
using System.Collections.Generic;
using System.Linq;

namespace Kleene
{
    public class ConjunctionExpression<T> : Expression<T> where T : IRunnable<T>
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

        public override IEnumerable<NondeterministicObject<T>?> Run()
        {
            return this.Leader.Run().SelectMany(x => this.Follower.Run().SelectMany(y => NondeterministicObject<T>.Overlap(x, y)));
        }
    }
}
