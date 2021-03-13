using System;

namespace Kleene.Tests.NondeterministicObjects
{
    public static class Samples
    {
        public static AnyObject<Runnable<int>> Any() => new();

        public static NondeterministicRunnable<int> Runnable(int i = 1) => new(i);

        public static NondeterministicEmptyObjectSequence<Runnable<int>> EmptySequence() => new();

        public static NondeterministicObjectSequence<Runnable<int>> Sequence(int i = 1) => new(
            new NondeterministicRunnable<int>(i),
            new [] { new NondeterministicEmptyObjectSequence<Runnable<int>>() });

        public static NondeterministicObjectSequence<Runnable<int>> Sequence(int i = 1, int j = 2) => new(
            new NondeterministicRunnable<int>(i),
            new [] {
                new NondeterministicObjectSequence<Runnable<int>>(
                    new NondeterministicRunnable<int>(j),
                    new [] { new NondeterministicEmptyObjectSequence<Runnable<int>>() }
                )});

        public static NondeterministicObjectSequence<Runnable<int>> EmptyTail(int i = 1) => new (
            new NondeterministicRunnable<int>(i),
            Array.Empty<NondeterministicEmptyObjectSequence<Runnable<int>>>());

        public static NondeterministicObjectSequence<Runnable<int>> TwoTails(int i = 1, int j = 2, int k = 3) => new(
            new NondeterministicRunnable<int>(i),
            new [] {
                new NondeterministicObjectSequence<Runnable<int>>(
                    new NondeterministicRunnable<int>(j),
                    new [] { new NondeterministicEmptyObjectSequence<Runnable<int>>() }
                ),
                new NondeterministicObjectSequence<Runnable<int>>(
                    new NondeterministicRunnable<int>(k),
                    new [] { new NondeterministicEmptyObjectSequence<Runnable<int>>() }
                )
            });
    }
}