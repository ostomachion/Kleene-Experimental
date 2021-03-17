using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Kleene
{
    public abstract class Expression<T> where T : notnull
    {
        public Result<T>? Value { get; private set; }

        public bool Done { get; private set; }

        public void Step(Expression<T> anchor)
        {
            if (this.Done)
                throw new InvalidOperationException();

            this.Done = InnerStep(out Result<T>? value, anchor);
            this.Value = value;
        }

        public void Reset()
        {
            this.Value = null;
            this.Done = false;
            InnerReset();
        }

        protected abstract bool InnerStep(out Result<T>? value, Expression<T> anchor);

        protected abstract void InnerReset();

        public IEnumerable<T> Generate()
        {
            while (!this.Done)
            {
                this.Step(new AnyExpression<T>());
                if (this.Value is RealResult<T> result)
                {
                    yield return result.Value;
                }
                else if (this.Value is AnyResult<T>)
                {
                    throw new InvalidOperationException();
                }
            }
        }
    }
}
