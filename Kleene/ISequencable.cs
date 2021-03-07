namespace Kleene
{
    public interface ISequencable<T> : IRunnable<ISequencable<T>> where T : IRunnable<T>
    {
    }
}