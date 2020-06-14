namespace Kleene
{
    public abstract class StructurePointer<T>
        where T : Structure
    {
        public T Structure { get; }
        public bool Done { get; }

        public StructurePointer(T structure)
        {
            this.Structure = structure;
        }

        protected StructurePointer(T structure, bool done)
        {
            this.Structure = structure;
            this.Done = done;
        }

        public abstract Structure GetCurrent();

        public abstract StructurePointer<T> Advance();
    }
}