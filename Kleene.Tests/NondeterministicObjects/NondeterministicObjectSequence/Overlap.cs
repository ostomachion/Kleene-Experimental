using System.Collections;
using System.Linq;
using System;
using Xunit;
using Kleene;
using Kleene.Tests.NondeterministicObjects;

namespace Kleene.Tests.NondeterministicObjectSequence
{
    public class Overlap
    {
        #region single item leader
        [Fact]
        public void SingleItemOverlapEmpty_ReturnsNothing()
        {
            // Given
            var leader = Samples.Sequence(1);
            var follower = Samples.EmptySequence();

            // When
            var overlap = leader.Overlap(follower);

            // Then
            Assert.Empty(overlap);
        }

        [Fact]
        public void SingleItemOverlapSameItem_ReturnsValue()
        {
            // Given
            var leader = Samples.Sequence(1);
            var follower = Samples.Sequence(1);

            // When
            var overlap = leader.Overlap(follower);

            // Then
            Assert.Collection(overlap,
                item =>
                {
                    Assert.IsType<NondeterministicObjectSequence<Runnable<int>>>(item);
                    var cast = item as NondeterministicObjectSequence<Runnable<int>>;

                    Assert.IsType<NondeterministicRunnable<int>>(cast.Head);
                    var castHead = cast.Head as NondeterministicRunnable<int>;
                    Assert.Equal(1, castHead.Value);

                    Assert.Collection(cast.Tail,
                        item =>
                        {
                            Assert.IsType<NondeterministicEmptyObjectSequence<Runnable<int>>>(item);
                        });
                });
        }

        [Fact]
        public void SingleItemOverlapDifferentItem_ReturnsNothing()
        {
            // Given
            var leader = Samples.Sequence(1);
            var follower = Samples.Sequence(2);

            // When
            var overlap = leader.Overlap(follower);

            // Then
            Assert.Empty(overlap);
        }

        [Fact]
        public void SingleItemOverlapTwoItemsMatchingPrefix_ReturnsEmptyTail()
        {
            // Given
            var leader = Samples.Sequence(1);
            var follower = Samples.Sequence(1, 2);

            // When
            var overlap = leader.Overlap(follower);

            // Then
            Assert.Collection(overlap,
                item => {
                    Assert.IsType<NondeterministicObjectSequence<Runnable<int>>>(item);
                    var cast = item as NondeterministicObjectSequence<Runnable<int>>;

                    Assert.IsType<NondeterministicRunnable<int>>(cast.Head);
                    var castHead = cast.Head as NondeterministicRunnable<int>;
                    Assert.Equal(1, castHead.Value);

                    Assert.Empty(cast.Tail);
                });
        }

        [Fact]
        public void SingleItemOverlapTwoItemsNoMatchingPrefix_ReturnsNothing()
        {
            // Given
            var leader = Samples.Sequence(1);
            var follower = Samples.Sequence(2, 3);

            // When
            var overlap = leader.Overlap(follower);

            // Then
            Assert.Empty(overlap);
        }

        [Fact]
        public void SingleItemOverlapEmptyTailMatchingPrefix_ReturnsEmptyTail()
        {
            // Given
            var leader = Samples.Sequence(1);
            var follower = Samples.EmptyTail(1);

            // When
            var overlap = leader.Overlap(follower);

            // Then
            Assert.Collection(overlap,
                item => {
                    Assert.IsType<NondeterministicObjectSequence<Runnable<int>>>(item);
                    var cast = item as NondeterministicObjectSequence<Runnable<int>>;

                    Assert.IsType<NondeterministicRunnable<int>>(cast.Head);
                    var castHead = cast.Head as NondeterministicRunnable<int>;
                    Assert.Equal(1, castHead.Value);

                    Assert.Empty(cast.Tail);
                });
        }

        [Fact]
        public void SingleItemOverlapEmptyTailNoMatchingPrefix_ReturnsNothing()
        {
            // Given
            var leader = Samples.Sequence(1);
            var follower = Samples.EmptyTail(2);

            // When
            var overlap = leader.Overlap(follower);

            // Then
            Assert.Empty(overlap);
        }

        [Fact]
        public void SingleItemOverlapTwoTailsMatchingPrefix_ReturnsEmptyTail()
        {
            // Given
            var leader = Samples.Sequence(1);
            var follower = Samples.TwoTails(1, 2, 3);

            // When
            var overlap = leader.Overlap(follower);

            // Then
            Assert.Collection(overlap,
                item => {
                    Assert.IsType<NondeterministicObjectSequence<Runnable<int>>>(item);
                    var cast = item as NondeterministicObjectSequence<Runnable<int>>;

                    Assert.IsType<NondeterministicRunnable<int>>(cast.Head);
                    var castHead = cast.Head as NondeterministicRunnable<int>;
                    Assert.Equal(1, castHead.Value);

                    Assert.Empty(cast.Tail);
                });
        }

        [Fact]
        public void SingleItemOverlapTwoTailsNoMatchingPrefix_ReturnsNothing()
        {
            // Given
            var leader = Samples.Sequence(1);
            var follower = Samples.TwoTails(2, 3, 1);

            // When
            var overlap = leader.Overlap(follower);

            // Then
            Assert.Empty(overlap);
        }
        #endregion


        #region two item leader
        [Fact]
        public void TwoItemsOverlapEmpty_ReturnsNothing()
        {
            // Given
            var leader = Samples.Sequence(1, 2);
            var follower = Samples.EmptySequence();

            // When
            var overlap = leader.Overlap(follower);

            // Then
            Assert.Empty(overlap);
        }

        [Fact]
        public void TwoItemsOverlapSingleItemPrefix_ReturnsEmptyTail()
        {
            // Given
            var leader = Samples.Sequence(1, 2);
            var follower = Samples.Sequence(1);

            // When
            var overlap = leader.Overlap(follower);

            // Then
            Assert.Collection(overlap,
                item =>
                {
                    Assert.IsType<NondeterministicObjectSequence<Runnable<int>>>(item);
                    var cast = item as NondeterministicObjectSequence<Runnable<int>>;

                    Assert.IsType<NondeterministicRunnable<int>>(cast.Head);
                    var castHead = cast.Head as NondeterministicRunnable<int>;
                    Assert.Equal(1, castHead.Value);

                    Assert.Empty(cast.Tail);
                });
        }

        [Fact]
        public void TwoItemsOverlapSingleItemNoPrefix_ReturnsEmpty()
        {
            // Given
            var leader = Samples.Sequence(1, 2);
            var follower = Samples.Sequence(2);

            // When
            var overlap = leader.Overlap(follower);

            // Then
            Assert.Empty(overlap);
        }

        [Fact]
        public void TwoItemsOverlapSameTwoItems_ReturnsTwoItems()
        {
            // Given
            var leader = Samples.Sequence(1, 2);
            var follower = Samples.Sequence(1, 2);

            // When
            var overlap = leader.Overlap(follower);

            // Then
            Assert.Collection(overlap,
                item => {
                    Assert.IsType<NondeterministicObjectSequence<Runnable<int>>>(item);
                    var cast = item as NondeterministicObjectSequence<Runnable<int>>;

                    Assert.IsType<NondeterministicRunnable<int>>(cast.Head);
                    var castHead = cast.Head as NondeterministicRunnable<int>;
                    Assert.Equal(1, castHead.Value);


                    Assert.Collection(cast.Tail,
                      item => {
                        Assert.IsType<NondeterministicObjectSequence<Runnable<int>>>(item);
                        var cast = item as NondeterministicObjectSequence<Runnable<int>>;

                        Assert.IsType<NondeterministicRunnable<int>>(cast.Head);
                        var castHead = cast.Head as NondeterministicRunnable<int>;
                        Assert.Equal(2, castHead.Value);


                        Assert.Collection(cast.Tail,
                            item =>
                            {
                                Assert.IsType<NondeterministicEmptyObjectSequence<Runnable<int>>>(item);
                            });
                    });
                });
        }

        [Fact]
        public void TwoItemsOverlapTwoItemsPrefix_ReturnsEmptyTail()
        {
            // Given
            var leader = Samples.Sequence(1, 2);
            var follower = Samples.Sequence(1, 3);

            // When
            var overlap = leader.Overlap(follower);

            // Then
            Assert.Collection(overlap,
                item => {
                    Assert.IsType<NondeterministicObjectSequence<Runnable<int>>>(item);
                    var cast = item as NondeterministicObjectSequence<Runnable<int>>;

                    Assert.IsType<NondeterministicRunnable<int>>(cast.Head);
                    var castHead = cast.Head as NondeterministicRunnable<int>;
                    Assert.Equal(1, castHead.Value);


                    Assert.Empty(cast.Tail);
                });
        }

        [Fact]
        public void TwoItemsOverlapTwoItemsNoMatchingPrefix_ReturnsNothing()
        {
            // Given
            var leader = Samples.Sequence(1, 2);
            var follower = Samples.Sequence(2, 3);

            // When
            var overlap = leader.Overlap(follower);

            // Then
            Assert.Empty(overlap);
        }

        [Fact]
        public void TwoItemsOverlapEmptyTailMatchingPrefix_ReturnsEmptyTail()
        {
            // Given
            var leader = Samples.Sequence(1, 2);
            var follower = Samples.EmptyTail(1);

            // When
            var overlap = leader.Overlap(follower);

            // Then
            Assert.Collection(overlap,
                item => {
                    Assert.IsType<NondeterministicObjectSequence<Runnable<int>>>(item);
                    var cast = item as NondeterministicObjectSequence<Runnable<int>>;

                    Assert.IsType<NondeterministicRunnable<int>>(cast.Head);
                    var castHead = cast.Head as NondeterministicRunnable<int>;
                    Assert.Equal(1, castHead.Value);

                    Assert.Empty(cast.Tail);
                });
        }

        [Fact]
        public void TwoItemsOverlapEmptyTailNoMatchingPrefix_ReturnsNothing()
        {
            // Given
            var leader = Samples.Sequence(1, 2);
            var follower = Samples.EmptyTail(2);

            // When
            var overlap = leader.Overlap(follower);

            // Then
            Assert.Empty(overlap);
        }

        [Fact]
        public void TwoItemsOverlapTwoTailsMatching_ReturnsValues()
        {
            // Given
            var leader = Samples.Sequence(1, 2);
            var follower = Samples.TwoTails(1, 2, 3);

            // When
            var overlap = leader.Overlap(follower);

            // Then
            Assert.Collection(overlap,
                item => {
                    Assert.IsType<NondeterministicObjectSequence<Runnable<int>>>(item);
                    var cast = item as NondeterministicObjectSequence<Runnable<int>>;

                    Assert.IsType<NondeterministicRunnable<int>>(cast.Head);
                    var castHead = cast.Head as NondeterministicRunnable<int>;
                    Assert.Equal(1, castHead.Value);


                    Assert.Collection(cast.Tail,
                      item => {
                        Assert.IsType<NondeterministicObjectSequence<Runnable<int>>>(item);
                        var cast = item as NondeterministicObjectSequence<Runnable<int>>;

                        Assert.IsType<NondeterministicRunnable<int>>(cast.Head);
                        var castHead = cast.Head as NondeterministicRunnable<int>;
                        Assert.Equal(2, castHead.Value);


                        Assert.Collection(cast.Tail,
                            item =>
                            {
                                Assert.IsType<NondeterministicEmptyObjectSequence<Runnable<int>>>(item);
                            });
                    });
                });
        }

        [Fact]
        public void TwoItemsOverlapTwoTailsMatchingPrefix_ReturnsEmptyTail()
        {
            // Given
            var leader = Samples.Sequence(1, 2);
            var follower = Samples.TwoTails(1, 3, 3);

            // When
            var overlap = leader.Overlap(follower);

            // Then
            Assert.Collection(overlap,
                item => {
                    Assert.IsType<NondeterministicObjectSequence<Runnable<int>>>(item);
                    var cast = item as NondeterministicObjectSequence<Runnable<int>>;

                    Assert.IsType<NondeterministicRunnable<int>>(cast.Head);
                    var castHead = cast.Head as NondeterministicRunnable<int>;
                    Assert.Equal(1, castHead.Value);

                    Assert.Empty(cast.Tail);
                });
        }

        [Fact]
        public void TwoItemsOverlapTwoTailsNoMatchingPrefix_ReturnsNothing()
        {
            // Given
            var leader = Samples.Sequence(1, 2);
            var follower = Samples.TwoTails(2, 3, 1);

            // When
            var overlap = leader.Overlap(follower);

            // Then
            Assert.Empty(overlap);
        }
        #endregion

        
        #region empty tail leader
        [Fact]
        public void EmptyTailOverlapEmpty_ReturnsNothing()
        {
            // Given
            var leader = Samples.EmptyTail(1);
            var follower = Samples.EmptySequence();

            // When
            var overlap = leader.Overlap(follower);

            // Then
            Assert.Empty(overlap);
        }

        [Fact]
        public void EmptyTailOverlapSameItem_ReturnsEmptyTail()
        {
            // Given
            var leader = Samples.EmptyTail(1);
            var follower = Samples.Sequence(1);

            // When
            var overlap = leader.Overlap(follower);

            // Then
            Assert.Collection(overlap,
                item =>
                {
                    Assert.IsType<NondeterministicObjectSequence<Runnable<int>>>(item);
                    var cast = item as NondeterministicObjectSequence<Runnable<int>>;

                    Assert.IsType<NondeterministicRunnable<int>>(cast.Head);
                    var castHead = cast.Head as NondeterministicRunnable<int>;
                    Assert.Equal(1, castHead.Value);

                    Assert.Empty(cast.Tail);
                });
        }

        [Fact]
        public void EmptyTailOverlapDifferentItem_ReturnsNothing()
        {
            // Given
            var leader = Samples.EmptyTail(1);
            var follower = Samples.Sequence(2);

            // When
            var overlap = leader.Overlap(follower);

            // Then
            Assert.Empty(overlap);
        }

        [Fact]
        public void EmptyTailOverlapTwoItemsMatchingPrefix_ReturnsEmptyTail()
        {
            // Given
            var leader = Samples.EmptyTail(1);
            var follower = Samples.Sequence(1, 2);

            // When
            var overlap = leader.Overlap(follower);

            // Then
            Assert.Collection(overlap,
                item => {
                    Assert.IsType<NondeterministicObjectSequence<Runnable<int>>>(item);
                    var cast = item as NondeterministicObjectSequence<Runnable<int>>;

                    Assert.IsType<NondeterministicRunnable<int>>(cast.Head);
                    var castHead = cast.Head as NondeterministicRunnable<int>;
                    Assert.Equal(1, castHead.Value);

                    Assert.Empty(cast.Tail);
                });
        }

        [Fact]
        public void EmptyTailOverlapTwoItemsNoMatchingPrefix_ReturnsNothing()
        {
            // Given
            var leader = Samples.EmptyTail(1);
            var follower = Samples.Sequence(2, 3);

            // When
            var overlap = leader.Overlap(follower);

            // Then
            Assert.Empty(overlap);
        }

        [Fact]
        public void EmptyTailOverlapEmptyTailMatchingPrefix_ReturnsEmptyTail()
        {
            // Given
            var leader = Samples.EmptyTail(1);
            var follower = Samples.EmptyTail(1);

            // When
            var overlap = leader.Overlap(follower);

            // Then
            Assert.Collection(overlap,
                item => {
                    Assert.IsType<NondeterministicObjectSequence<Runnable<int>>>(item);
                    var cast = item as NondeterministicObjectSequence<Runnable<int>>;

                    Assert.IsType<NondeterministicRunnable<int>>(cast.Head);
                    var castHead = cast.Head as NondeterministicRunnable<int>;
                    Assert.Equal(1, castHead.Value);

                    Assert.Empty(cast.Tail);
                });
        }

        [Fact]
        public void EmptyTailOverlapEmptyTailNoMatchingPrefix_ReturnsNothing()
        {
            // Given
            var leader = Samples.EmptyTail(1);
            var follower = Samples.EmptyTail(2);

            // When
            var overlap = leader.Overlap(follower);

            // Then
            Assert.Empty(overlap);
        }

        [Fact]
        public void EmptyTailOverlapTwoTailsMatchingPrefix_ReturnsEmptyTail()
        {
            // Given
            var leader = Samples.EmptyTail(1);
            var follower = Samples.TwoTails(1, 2, 3);

            // When
            var overlap = leader.Overlap(follower);

            // Then
            Assert.Collection(overlap,
                item => {
                    Assert.IsType<NondeterministicObjectSequence<Runnable<int>>>(item);
                    var cast = item as NondeterministicObjectSequence<Runnable<int>>;

                    Assert.IsType<NondeterministicRunnable<int>>(cast.Head);
                    var castHead = cast.Head as NondeterministicRunnable<int>;
                    Assert.Equal(1, castHead.Value);

                    Assert.Empty(cast.Tail);
                });
        }

        [Fact]
        public void EmptyTailOverlapTwoTailsNoMatchingPrefix_ReturnsNothing()
        {
            // Given
            var leader = Samples.EmptyTail(1);
            var follower = Samples.TwoTails(2, 3, 1);

            // When
            var overlap = leader.Overlap(follower);

            // Then
            Assert.Empty(overlap);
        }
        #endregion



        #region two tail leader
        [Fact]
        public void TwoTailsOverlapEmpty_ReturnsNothing()
        {
            // Given
            var leader = Samples.TwoTails(1, 2, 3);
            var follower = Samples.EmptySequence();

            // When
            var overlap = leader.Overlap(follower);

            // Then
            Assert.Empty(overlap);
        }

        [Fact]
        public void TwoTailsOverlapSingleItemPrefix_ReturnsEmptyTail()
        {
            // Given
            var leader = Samples.TwoTails(1, 2, 3);
            var follower = Samples.Sequence(1);

            // When
            var overlap = leader.Overlap(follower);

            // Then
            Assert.Collection(overlap,
                item =>
                {
                    Assert.IsType<NondeterministicObjectSequence<Runnable<int>>>(item);
                    var cast = item as NondeterministicObjectSequence<Runnable<int>>;

                    Assert.IsType<NondeterministicRunnable<int>>(cast.Head);
                    var castHead = cast.Head as NondeterministicRunnable<int>;
                    Assert.Equal(1, castHead.Value);

                    Assert.Empty(cast.Tail);
                });
        }

        [Fact]
        public void TwoTailsOverlapSingleItemNoPrefix_ReturnsEmpty()
        {
            // Given
            var leader = Samples.TwoTails(1, 2, 3);
            var follower = Samples.Sequence(2);

            // When
            var overlap = leader.Overlap(follower);

            // Then
            Assert.Empty(overlap);
        }

        [Fact]
        public void TwoTailsOverlapSameTwoItems_ReturnsTwoItems()
        {
            // Given
            var leader = Samples.TwoTails(1, 2, 3);
            var follower = Samples.Sequence(1, 2);

            // When
            var overlap = leader.Overlap(follower);

            // Then
            Assert.Collection(overlap,
                item => {
                    Assert.IsType<NondeterministicObjectSequence<Runnable<int>>>(item);
                    var cast = item as NondeterministicObjectSequence<Runnable<int>>;

                    Assert.IsType<NondeterministicRunnable<int>>(cast.Head);
                    var castHead = cast.Head as NondeterministicRunnable<int>;
                    Assert.Equal(1, castHead.Value);


                    Assert.Collection(cast.Tail,
                      item => {
                        Assert.IsType<NondeterministicObjectSequence<Runnable<int>>>(item);
                        var cast = item as NondeterministicObjectSequence<Runnable<int>>;

                        Assert.IsType<NondeterministicRunnable<int>>(cast.Head);
                        var castHead = cast.Head as NondeterministicRunnable<int>;
                        Assert.Equal(2, castHead.Value);


                        Assert.Collection(cast.Tail,
                            item =>
                            {
                                Assert.IsType<NondeterministicEmptyObjectSequence<Runnable<int>>>(item);
                            });
                    });
                });
        }

        [Fact]
        public void TwoTailsOverlapTwoItemsPrefix_ReturnsEmptyTail()
        {
            // Given
            var leader = Samples.TwoTails(1, 2, 4);
            var follower = Samples.Sequence(1, 3);

            // When
            var overlap = leader.Overlap(follower);

            // Then
            Assert.Collection(overlap,
                item => {
                    Assert.IsType<NondeterministicObjectSequence<Runnable<int>>>(item);
                    var cast = item as NondeterministicObjectSequence<Runnable<int>>;

                    Assert.IsType<NondeterministicRunnable<int>>(cast.Head);
                    var castHead = cast.Head as NondeterministicRunnable<int>;
                    Assert.Equal(1, castHead.Value);


                    Assert.Empty(cast.Tail);
                });
        }

        [Fact]
        public void TwoTailsOverlapTwoItemsNoMatchingPrefix_ReturnsNothing()
        {
            // Given
            var leader = Samples.TwoTails(1, 2, 3);
            var follower = Samples.Sequence(2, 3);

            // When
            var overlap = leader.Overlap(follower);

            // Then
            Assert.Empty(overlap);
        }

        [Fact]
        public void TwoTailsOverlapEmptyTailMatchingPrefix_ReturnsEmptyTail()
        {
            // Given
            var leader = Samples.TwoTails(1, 2, 3);
            var follower = Samples.EmptyTail(1);

            // When
            var overlap = leader.Overlap(follower);

            // Then
            Assert.Collection(overlap,
                item => {
                    Assert.IsType<NondeterministicObjectSequence<Runnable<int>>>(item);
                    var cast = item as NondeterministicObjectSequence<Runnable<int>>;

                    Assert.IsType<NondeterministicRunnable<int>>(cast.Head);
                    var castHead = cast.Head as NondeterministicRunnable<int>;
                    Assert.Equal(1, castHead.Value);

                    Assert.Empty(cast.Tail);
                });
        }

        [Fact]
        public void TwoTailsOverlapEmptyTailNoMatchingPrefix_ReturnsNothing()
        {
            // Given
            var leader = Samples.TwoTails(1, 2, 3);
            var follower = Samples.EmptyTail(2);

            // When
            var overlap = leader.Overlap(follower);

            // Then
            Assert.Empty(overlap);
        }

        [Fact]
        public void TwoTailsOverlapTwoTailsMatching_ReturnsValues()
        {
            // Given
            var leader = Samples.TwoTails(1, 2, 3);
            var follower = Samples.TwoTails(1, 2, 3);

            // When
            var overlap = leader.Overlap(follower);

            // Then
            Assert.Collection(overlap,
                item => {
                    Assert.IsType<NondeterministicObjectSequence<Runnable<int>>>(item);
                    var cast = item as NondeterministicObjectSequence<Runnable<int>>;

                    Assert.IsType<NondeterministicRunnable<int>>(cast.Head);
                    var castHead = cast.Head as NondeterministicRunnable<int>;
                    Assert.Equal(1, castHead.Value);


                    Assert.Collection(cast.Tail,
                        item => {
                            Assert.IsType<NondeterministicObjectSequence<Runnable<int>>>(item);
                            var cast = item as NondeterministicObjectSequence<Runnable<int>>;

                            Assert.IsType<NondeterministicRunnable<int>>(cast.Head);
                            var castHead = cast.Head as NondeterministicRunnable<int>;
                            Assert.Equal(2, castHead.Value);


                            Assert.Collection(cast.Tail,
                                item =>
                                {
                                    Assert.IsType<NondeterministicEmptyObjectSequence<Runnable<int>>>(item);
                                });
                        },
                        item => {
                            Assert.IsType<NondeterministicObjectSequence<Runnable<int>>>(item);
                            var cast = item as NondeterministicObjectSequence<Runnable<int>>;

                            Assert.IsType<NondeterministicRunnable<int>>(cast.Head);
                            var castHead = cast.Head as NondeterministicRunnable<int>;
                            Assert.Equal(3, castHead.Value);


                            Assert.Collection(cast.Tail,
                                item =>
                                {
                                    Assert.IsType<NondeterministicEmptyObjectSequence<Runnable<int>>>(item);
                                });
                        });
                });
        }

        [Fact]
        public void TwoTailsOverlapTwoTailsOneMatchingMatching_ReturnsValues()
        {
            // Given
            var leader = Samples.TwoTails(1, 2, 3);
            var follower = Samples.TwoTails(1, 1, 3);

            // When
            var overlap = leader.Overlap(follower);

            // Then
            Assert.Collection(overlap,
                item => {
                    Assert.IsType<NondeterministicObjectSequence<Runnable<int>>>(item);
                    var cast = item as NondeterministicObjectSequence<Runnable<int>>;

                    Assert.IsType<NondeterministicRunnable<int>>(cast.Head);
                    var castHead = cast.Head as NondeterministicRunnable<int>;
                    Assert.Equal(1, castHead.Value);


                    Assert.Collection(cast.Tail,
                        item => {
                            Assert.IsType<NondeterministicObjectSequence<Runnable<int>>>(item);
                            var cast = item as NondeterministicObjectSequence<Runnable<int>>;

                            Assert.IsType<NondeterministicRunnable<int>>(cast.Head);
                            var castHead = cast.Head as NondeterministicRunnable<int>;
                            Assert.Equal(3, castHead.Value);


                            Assert.Collection(cast.Tail,
                                item =>
                                {
                                    Assert.IsType<NondeterministicEmptyObjectSequence<Runnable<int>>>(item);
                                });
                        });
                });
        }

        [Fact]
        public void TwoTailsOverlapTwoTailsMatchingPrefix_ReturnsEmptyTail()
        {
            // Given
            var leader = Samples.TwoTails(1, 2, 4);
            var follower = Samples.TwoTails(1, 3, 3);

            // When
            var overlap = leader.Overlap(follower);

            // Then
            Assert.Collection(overlap,
                item => {
                    Assert.IsType<NondeterministicObjectSequence<Runnable<int>>>(item);
                    var cast = item as NondeterministicObjectSequence<Runnable<int>>;

                    Assert.IsType<NondeterministicRunnable<int>>(cast.Head);
                    var castHead = cast.Head as NondeterministicRunnable<int>;
                    Assert.Equal(1, castHead.Value);

                    Assert.Empty(cast.Tail);
                });
        }

        [Fact]
        public void TwoTailsOverlapTwoTailsNoMatchingPrefix_ReturnsNothing()
        {
            // Given
            var leader = Samples.TwoTails(1, 2, 3);
            var follower = Samples.TwoTails(2, 3, 1);

            // When
            var overlap = leader.Overlap(follower);

            // Then
            Assert.Empty(overlap);
        }
        #endregion
    }
}
