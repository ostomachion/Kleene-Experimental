using System;
using System.Collections.Generic;
using System.Linq;

namespace Kleene
{
    public class Result<TOut>
    {
        public int Offset { get; }
        public int Length { get; }
        public IEnumerable<TOut> Output { get; }

        public Result(int offset, int length = 0) {
            this.Offset = offset;
            this.Length = length;
            this.Output = Enumerable.Empty<TOut>();
        }

        public Result(int offset, int length, IEnumerable<TOut> output)
        {
            this.Offset = offset;
            this.Length = length;
            this.Output = output;
        }
    }
}