namespace Kleene
{
    public interface IRunnable<T> where T : IRunnable<T>
    {
        Expression<T> ToExpression();
    }
}
