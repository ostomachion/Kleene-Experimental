using System;
using System.Collections.Generic;
using System.Linq;

namespace Kleene
{
    public class ConjunctionExpression : Expression
    {
        public Expression Leader { get; }
        public Expression Follower { get; }

        public ConjunctionExpression(Expression leader, Expression follower)
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

        public override IEnumerable<NondeterministicStructure?> Run()
        {
            var leader = this.Leader.Run();
            var follower = this.Follower.Run();

            return overlap(leader, follower);

            static IEnumerable<NondeterministicStructure?> overlap(IEnumerable<NondeterministicStructure?> leader, IEnumerable<NondeterministicStructure?> follower)
            {
                return leader.SelectMany(x => follower.Where(y => y?.Name == x?.Name)
                    .Select(y => y is null ? null : new NondeterministicStructure(y.Name, overlap(x!.FirstChild, y.FirstChild), overlap(x.NextSibling, y.NextSibling))));
            }
        }
    }
}