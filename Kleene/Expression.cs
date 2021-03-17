using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Kleene
{
    public abstract class Expression<T> where T : notnull
    {
        public Result<T>? Result { get; private set; }

        public bool Done { get; private set; }

        public void Step(Expression<T> anchor)
        {
            if (this.Done)
                throw new InvalidOperationException();

            this.Done = InnerStep(out Result<T>? value, anchor);
            this.Result = value;
        }

        public void Reset()
        {
            this.Result = null;
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
                if (this.Result is not null)
                    yield return this.Result.Value;
            }
        }
    }
}
