using System.Runtime.Intrinsics.X86;
using System.Collections;
using System.Linq;
using System;
using Xunit;
using Kleene;
using Kleene.Tests.NondeterministicObjects;

namespace Kleene.Tests.NondeterministicObjectSet
{
    public class Overlap
    {
        #region single item leader
        [Fact]
        public void SingleItemOverlapEmpty_GeneratesNothing()
        {
            // Given Test
            var leader = Samples.Set(1);
            var follower = Samples.EmptySet();

            // When
            var results = leader.Overlap(follower).SelectMany(x => x.Collapse());

            // Then
            Assert.Empty(results);
        }

        [Fact]
        public void SingleItemOverlapSameItem_GeneratesOne()
        {
            // Given
            var leader = Samples.Set(1);
            var follower = Samples.Set(1);

            // When
            var results = leader.Overlap(follower).SelectMany(x => x.Collapse());

            // Then
            Assert.Single(results);
        }

        [Fact]
        public void SingleItemOverlapDifferentItem_GeneratesNothing()
        {
            // Given
            var leader = Samples.Set(1);
            var follower = Samples.Set(2);

            // When
            var results = leader.Overlap(follower).SelectMany(x => x.Collapse());

            // Then
            Assert.Empty(results);
        }

        [Fact]
        public void SingleItemOverlapTwoItemsMatchingPrefix_GeneratesNothing()
        {
            // Given
            var leader = Samples.Set(1);
            var follower = Samples.Set(1, 2);

            // When
            var results = leader.Overlap(follower).SelectMany(x => x.Collapse());

            // Then
            Assert.Empty(results);
        }

        [Fact]
        public void SingleItemOverlapTwoItemsMatchingTail_GeneratesNothing()
        {
            // Given
            var leader = Samples.Set(1);
            var follower = Samples.Set(2, 1);

            // When
            var results = leader.Overlap(follower).SelectMany(x => x.Collapse());

            // Then
            Assert.Empty(results);
        }

        [Fact]
        public void SingleItemOverlapTwoItemsMatchingHeadAndTail_GeneratesNothing()
        {
            // Given
            var leader = Samples.Set(1);
            var follower = Samples.Set(1, 1);

            // When
            var results = leader.Overlap(follower).SelectMany(x => x.Collapse());

            // Then
            Assert.Empty(results);
        }

        [Fact]
        public void SingleItemOverlapTwoItemsNoMatchingPrefix_GeneratesNothing()
        {
            // Given
            var leader = Samples.Set(1);
            var follower = Samples.Set(2, 3);

            // When
            var results = leader.Overlap(follower).SelectMany(x => x.Collapse());

            // Then
            Assert.Empty(results);
        }

        [Fact]
        public void SingleItemOverlapTwoTailsMatchingPrefix_GeneratesNothing()
        {
            // Given
            var leader = Samples.Set(1);
            var follower = Samples.TwoTailsSet(1, 2, 3);

            // When
            var results = leader.Overlap(follower).SelectMany(x => x.Collapse());

            // Then
            Assert.Empty(results);
        }

        [Fact]
        public void SingleItemOverlapTwoTailsMatchingFirstTail_GeneratesNothing()
        {
            // Given
            var leader = Samples.Set(1);
            var follower = Samples.TwoTailsSet(2, 1, 3);

            // When
            var results = leader.Overlap(follower).SelectMany(x => x.Collapse());

            // Then
            Assert.Empty(results);
        }

        [Fact]
        public void SingleItemOverlapTwoTailsMatchingSecondTail_GeneratesNothing()
        {
            // Given
            var leader = Samples.Set(1);
            var follower = Samples.TwoTailsSet(2, 3, 1);

            // When
            var results = leader.Overlap(follower).SelectMany(x => x.Collapse());

            // Then
            Assert.Empty(results);
        }

        [Fact]
        public void SingleItemOverlapTwoTailsMatchingAll_GeneratesNothing()
        {
            // Given
            var leader = Samples.Set(1);
            var follower = Samples.TwoTailsSet(1, 1, 1);

            // When
            var results = leader.Overlap(follower).SelectMany(x => x.Collapse());

            // Then
            Assert.Empty(results);
        }

        [Fact]
        public void SingleItemOverlapTwoTailsNoMatch_GeneratesNothing()
        {
            // Given
            var leader = Samples.Set(1);
            var follower = Samples.TwoTailsSet(2, 3, 4);

            // When
            var results = leader.Overlap(follower).SelectMany(x => x.Collapse());

            // Then
            Assert.Empty(results);
        }
        #endregion


        #region two item leader
        [Fact]
        public void TwoItemsOverlapEmpty_GeneratesNothing()
        {
            // Given
            var leader = Samples.Set(1, 2);
            var follower = Samples.EmptySet();

            // When
            var results = leader.Overlap(follower).SelectMany(x => x.Collapse());

            // Then
            Assert.Empty(results);
        }

        [Fact]
        public void TwoItemsOverlapSingleItemPrefix_GeneratesNothing()
        {
            // Given
            var leader = Samples.Set(1, 2);
            var follower = Samples.Set(1);

            // When
            var results = leader.Overlap(follower).SelectMany(x => x.Collapse());

            // Then
            Assert.Empty(results);
        }

        [Fact]
        public void TwoItemsOverlapSingleItemTail_GeneratesNothing()
        {
            // Given
            var leader = Samples.Set(1, 2);
            var follower = Samples.Set(2);

            // When
            var results = leader.Overlap(follower).SelectMany(x => x.Collapse());

            // Then
            Assert.Empty(results);
        }

        [Fact]
        public void TwoItemsOverlapSingleItemBoth_GeneratesNothing()
        {
            // Given
            var leader = Samples.Set(1, 1);
            var follower = Samples.Set(1);

            // When
            var results = leader.Overlap(follower).SelectMany(x => x.Collapse());

            // Then
            Assert.Empty(results);
        }

        [Fact]
        public void TwoItemsOverlapSingleItemNoMatch_GeneratesEmpty()
        {
            // Given
            var leader = Samples.Set(1, 2);
            var follower = Samples.Set(3);

            // When
            var results = leader.Overlap(follower).SelectMany(x => x.Collapse());

            // Then
            Assert.Empty(results);
        }

        [Fact]
        public void TwoItemsOverlapSameTwoItems_GeneratesOne()
        {
            // Given
            var leader = Samples.Set(1, 2);
            var follower = Samples.Set(1, 2);

            // When
            var results = leader.Overlap(follower).SelectMany(x => x.Collapse());

            // Then
            Assert.Single(results);
        }

        [Fact]
        public void TwoItemsOverlapSwitched_GeneratesOne()
        {
            // Given
            var leader = Samples.Set(1, 2);
            var follower = Samples.Set(2, 1);

            // When
            var results = leader.Overlap(follower).SelectMany(x => x.Collapse());

            // Then
            Assert.Single(results);
        }

        [Fact]
        public void TwoSameItemsOverlapSame_GeneratesTwo()
        {
            // Given
            var leader = Samples.Set(1, 1);
            var follower = Samples.Set(1, 1);

            // When
            var results = leader.Overlap(follower).SelectMany(x => x.Collapse());

            // Then
            Assert.Equal(2, results.Count());
        }

        [Fact]
        public void TwoItemsOverlapTwoItemsPrefix_ReturnsEmpty()
        {
            // Given
            var leader = Samples.Set(1, 2);
            var follower = Samples.Set(1, 3);

            // When
            var results = leader.Overlap(follower).SelectMany(x => x.Collapse());

            // Then
            Assert.Empty(results);
        }
        #endregion
    }
}
