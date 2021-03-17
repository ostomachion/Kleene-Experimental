using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Kleene
{
    public abstract class Expression<T> where T : notnull
    {
        public Result<T>? Result { get; private set; }

        public Expression<T>? Anchor { get; internal set; }

        public bool Done { get; private set; }

        public void Step()
        {
            if (this.Done)
                throw new InvalidOperationException();

            this.Done = InnerStep(out Result<T>? value);
            this.Result = value;
        }

        public void Reset()
        {
            this.Result = null;
            this.Done = false;
            InnerReset();
        }

        protected abstract bool InnerStep(out Result<T>? value);

        protected abstract void InnerReset();

        public IEnumerable<T> Generate()
        {
            while (!this.Done)
            {
                this.Step();
                if (this.Result is not null)
                    yield return this.Result.Value;
            }
        }
    }
}
