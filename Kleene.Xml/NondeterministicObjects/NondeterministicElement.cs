using System.Linq;
using System;
using System.Collections.Generic;

namespace Kleene.Xml
{
    public class NondeterministicElement : NondeterministicObject<RunnableElement>
    {
        public NondeterministicObject<ObjectSequence<Runnable<char>>> Name { get; }

        public IEnumerable<NondeterministicObject<ObjectSequence<RunnableElement>>> Children { get; }

        public NondeterministicElement(NondeterministicObject<ObjectSequence<Runnable<char>>> name, IEnumerable<NondeterministicObject<ObjectSequence<RunnableElement>>> children)
        {
            Name = name;
            Children = children;
        }

        public override IEnumerable<RunnableElement> Collapse()
        {
            foreach (var name in this.Name.Collapse())
            {
                foreach (var children in this.Children
                    .OfType<NondeterministicObject<ObjectSequence<RunnableElement>>>()
                    .SelectMany(x => x.Collapse()))
                {
                    yield return new RunnableElement(new string(name.Select(x => x.Value).ToArray()), children);
                }
            }
        }

        public override IEnumerable<NondeterministicObject<RunnableElement>> Overlap(NondeterministicObject<RunnableElement> other)
        {
            if (other is NondeterministicElement element)
            {
                foreach (var name in this.Name.Overlap(element.Name))
                {
                    yield return new NondeterministicElement(name, NondeterministicObjectSequence<RunnableElement>.Overlap(this.Children, element.Children));
                }
            }
            else
            {
                throw new ArgumentException("Argument type is not supported.", nameof(other));
            }
        }

        public override bool Equals(NondeterministicObject<RunnableElement>? other) => this.Equals(other as object);

        public override bool Equals(object? obj)
        {
            return obj is NondeterministicElement element &&
                   EqualityComparer<NondeterministicObject<ObjectSequence<Runnable<char>>>>.Default.Equals(Name, element.Name) &&
                   Children.SequenceEqual(element.Children);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Children);
        }
    }
}
