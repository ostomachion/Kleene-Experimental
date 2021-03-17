using System.Collections.Immutable;
using System.Collections.Generic;
using System.Linq;

namespace Kleene
{
    public class SequenceResult<T> : Result<IEnumerable<T>> where T : notnull
    {
        public static readonly SequenceResult<T> Empty = new(Enumerable.Empty<Result<T>>());

        public IEnumerable<Result<T>> Results { get; }
        public override IEnumerable<T> Value => Results.Select(x => x.Value);

        public SequenceResult(IEnumerable<Result<T>> results)
        {
            this.Results = results;
        }
    }
}
