using System.Collections;
using System.Linq;
using System;
using Xunit;
using Kleene;
using Kleene.Tests.NondeterministicObjects;

namespace Kleene.Tests.NondeterministicEmptyObjectSequence
{
    public class Overlap
    {
        [Fact]
        public void OverlapEmpty_ReturnsEmpty()
        {
            // Given
            var leader = Samples.EmptySequence();
            var follower = Samples.EmptySequence();

            // When
            var overlap = leader.Overlap(follower);

            // Then
            Assert.Collection(overlap,
                item => Assert.IsType<NondeterministicEmptyObjectSequence<Runnable<int>>>(item));
        }

        [Fact]
        public void OverlapSingleItemList_ReturnsNothing()
        {
            // Given
            var leader = Samples.EmptySequence();
            var follower = Samples.Sequence(1);

            // When
            var overlap = leader.Overlap(follower);

            // Then
            Assert.Empty(overlap);
        }

        [Fact]
        public void OverlapTwoItemList_ReturnsNothing()
        {
            // Given
            var leader = Samples.EmptySequence();
            var follower = Samples.Sequence(1, 2);

            // When
            var overlap = leader.Overlap(follower);

            // Then
            Assert.Empty(overlap);
        }

        [Fact]
        public void OverlapEmptyTail_ReturnsNothing()
        {
            // Given
            var leader = Samples.EmptySequence();
            var follower = Samples.EmptyTail();

            // When
            var overlap = leader.Overlap(follower);

            // Then
            Assert.Empty(overlap);
        }

        [Fact]
        public void OverlapTwoTails_ReturnsNothing()
        {
            // Given
            var leader = Samples.EmptySequence();
            var follower = Samples.TwoTails();

            // When
            var overlap = leader.Overlap(follower);

            // Then
            Assert.Empty(overlap);
        }
    }
}
