namespace Kleene
{
    public abstract class Result<T> where T : notnull
    {
        public abstract T Value { get; }
    }
}
