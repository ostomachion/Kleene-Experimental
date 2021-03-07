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

        public override IEnumerable<NondeterministicStructure?> Run()
        {
            var leader = this.Leader.Run();
            var follower = this.Follower.Run();

            return overlap(leader, follower);

            static IEnumerable<NondeterministicStructure?> overlap(IEnumerable<NondeterministicStructure?> leader, IEnumerable<NondeterministicStructure?> follower)
            {
                return leader.SelectMany(x =>
                {
                    if (x is NamedNondeterministicStructure namedX)
                    {
                        return follower.Select(y =>
                        {
                            if (y is NamedNondeterministicStructure namedY)
                            {
                                return namedX.Name == namedY.Name ? new NamedNondeterministicStructure(namedX.Name,
                                    overlap(namedX.FirstChild, namedY.FirstChild),
                                    overlap(namedX.NextSibling, namedY.NextSibling))
                                    : null;
                            }
                            else if (y is AnyStructure)
                            {
                                return namedX;
                            }
                            else if (y is null)
                            {
                                return null;
                            }
                            else
                            {
                                throw new NotImplementedException();
                            }
                        }).OfType<NondeterministicStructure>();
                    }
                    else if (x is AnyStructure)
                    {
                        return follower;
                    }
                    else if (x is null)
                    {
                        return follower.Where(y => y is null);
                    }
                    else
                    {
                        throw new NotImplementedException();
                    }
                });
            }
        }
    }
}