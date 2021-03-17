using System;
namespace Kleene
{
    public class AnyResult<T> : Result<T> where T : notnull
    {
        public override T Value => throw new InvalidOperationException();
    }
}
