using System;
using System.Collections.Generic;
using System.Linq;

namespace Kleene
{
    public class AnyExpression<T> : Expression<T> where T : notnull
    {
        protected override bool InnerStep(out Result<T>? value)
        {
            if (this.Anchor is null)
            {
                value = new AnyResult<T>();
                return true;
            }
            else
            {
                this.Anchor.Step();
                value = this.Anchor.Result;
                return this.Anchor.Done;
            }
        }

        protected override void InnerReset() { }
    }
}
