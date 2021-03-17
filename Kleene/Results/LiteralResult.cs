namespace Kleene
{
    public class LiteralResult<T> : Result<T> where T : notnull
    {
        public override T Value { get; }

        public LiteralResult(T value)
        {
            Value = value;
        }
    }
}
