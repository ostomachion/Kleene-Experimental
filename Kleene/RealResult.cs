namespace Kleene
{
    public class RealResult<T> : Result<T> where T : notnull
    {
        public T Value { get; }

        public RealResult(T value)
        {
            Value = value;
        }
    }
}
