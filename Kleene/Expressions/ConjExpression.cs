using System;
using System.Collections.Generic;
using System.Linq;

namespace Kleene
{
    public class ConjExpression<T> : Expression<T>
    where T : Expression<T>
    {
        public Expression<T> Leader { get; }
        public Expression<T> Follower { get; }

        public ConjExpression(Expression<T> leader, Expression<T> follower)
        {
            this.Leader = leader ?? throw new ArgumentNullException(nameof(leader));
            this.Follower = follower ?? throw new ArgumentNullException(nameof(follower));
        }

        public override IEnumerable<T> Run()
        {
            throw new NotImplementedException();
            // foreach (var attempt in this.Leader.Run(input))
            // {
            //     foreach (var result in this.Follower.Run(attempt))
            //     {
            //         yield return result;
            //     }
            // }
        }
    }
}