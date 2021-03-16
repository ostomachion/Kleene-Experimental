using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Kleene
{
    public abstract class Expression<T> where T : class
    {
        public T? Value { get; private set; }

        public bool Done { get; private set; }

        public void Step(Expression<T> anchor)
        {
            if (this.Done)
                throw new InvalidOperationException();

            this.Done = InnerStep(out T? value, anchor);
            this.Value = value;
        }

        public void Reset()
        {
            this.Value = null;
            this.Done = false;
            InnerReset();
        }

        protected abstract bool InnerStep(out T? value, Expression<T> anchor);

        protected abstract void InnerReset();

        public IEnumerable<T> Generate()
        {
            while (!this.Done)
            {
                this.Step(new AnyExpression<T>());
                if (this.Value is T)
                    yield return this.Value;
            }
        }
    }
}
