using System.Collections;
using System.Linq;
using System;
using Xunit;
using Kleene;
using Kleene.Tests.NondeterministicObjects;

namespace Kleene.Tests.NondeterministicEmptyObjectSet
{
    public class Overlap
    {
        [Fact]
        public void OverlapEmpty_ReturnsEmpty()
        {
            // Given
            var leader = Samples.EmptySet();
            var follower = Samples.EmptySet();

            // When
            var overlap = leader.Overlap(follower);

            // Then
            Assert.Collection(overlap,
                item => Assert.IsType<NondeterministicEmptyObjectSet<Runnable<int>>>(item));
        }

        [Fact]
        public void OverlapSingleItemList_ReturnsNothing()
        {
            // Given
            var leader = Samples.EmptySet();
            var follower = Samples.Set(1);

            // When
            var overlap = leader.Overlap(follower);

            // Then
            Assert.Empty(overlap);
        }

        [Fact]
        public void OverlapTwoItemList_ReturnsNothing()
        {
            // Given
            var leader = Samples.EmptySet();
            var follower = Samples.Set(1, 2);

            // When
            var overlap = leader.Overlap(follower);

            // Then
            Assert.Empty(overlap);
        }

        [Fact]
        public void OverlapEmptyTail_ReturnsNothing()
        {
            // Given
            var leader = Samples.EmptySet();
            var follower = Samples.EmptyTailSet();

            // When
            var overlap = leader.Overlap(follower);

            // Then
            Assert.Empty(overlap);
        }

        [Fact]
        public void OverlapTwoTails_ReturnsNothing()
        {
            // Given
            var leader = Samples.EmptySet();
            var follower = Samples.TwoTailsSet();

            // When
            var overlap = leader.Overlap(follower);

            // Then
            Assert.Empty(overlap);
        }
    }
}
