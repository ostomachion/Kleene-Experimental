using System;

namespace Kleene
{
    public abstract class Structure
    {
        public bool Done { get; }

        protected Structure(bool done)
        {
            this.Done = done;
        }

        public abstract Structure GetCurrent();

        public abstract Structure Advance();
    }
}