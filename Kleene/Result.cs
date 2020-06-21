using System.Collections.Generic;
using System.Linq;

namespace Kleene
{
    public class Result
    {
        public IEnumerable<Structure> Input { get; }
        public int Index { get; }
        public int Length { get; }
        public Expression Expression { get; }
        public IEnumerable<Result> Children { get; }

        public Result(IEnumerable<Structure> input, int index, int length, Expression source, IEnumerable<Result> children)
        {
            this.Input = input ?? throw new System.ArgumentNullException(nameof(input));
            this.Index = index;
            this.Length = length;
            this.Expression = source ?? throw new System.ArgumentNullException(nameof(source));
            this.Children = children ?? throw new System.ArgumentNullException(nameof(children));
        }

        public IEnumerable<Result> GetCaptures()
        {
            foreach (var result in this.Children)
            {
                if (result.Expression is CaptureExpression)
                {
                    yield return result;
                }
                else
                {
                    foreach (var capture in result.GetCaptures())
                    {
                        yield return capture;
                    }
                }
            }
        }

        public IEnumerable<Result> GetCaptures(string name) => GetCaptures().Where(x => (x.Expression as CaptureExpression)!.Name == name);
    }
}