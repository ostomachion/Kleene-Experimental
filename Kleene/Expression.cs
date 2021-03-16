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

        public void Step()
        {
            if (this.Done)
                throw new InvalidOperationException();

            this.Done = InnerStep(out T? value);
            this.Value = value;
        }

        public void Reset()
        {
            this.Value = null;
            this.Done = false;
            InnerReset();
        }

        protected abstract bool InnerStep(out T? value);

        protected abstract void InnerReset();

        public IEnumerable<T> Generate()
        {
            while (!this.Done)
            {
                this.Step();
                if (this.Value is T)
                    yield return this.Value;
            }
        }
    }
}
