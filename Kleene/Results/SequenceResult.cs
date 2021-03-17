using System.Collections.Immutable;
using System.Collections.Generic;
using System.Linq;

namespace Kleene
{
    public class SequenceResult<T> : Result<ImmutableList<T>> where T : notnull
    {
        public static readonly SequenceResult<T> Empty = new(Enumerable.Empty<Result<T>>());

        public ImmutableList<Result<T>> Results { get; }
        public override ImmutableList<T> Value => Results.Select(x => x.Value).ToImmutableList();

        public SequenceResult(IEnumerable<Result<T>> results)
        {
            this.Results = results.ToImmutableList();
        }
    }
}
