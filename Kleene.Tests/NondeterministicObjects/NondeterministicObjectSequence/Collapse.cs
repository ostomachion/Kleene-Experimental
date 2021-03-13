using System.Xml.Schema;
using System.Linq;
using System;
using Xunit;
using Kleene;
using Kleene.Tests.NondeterministicObjects;

namespace Kleene.Tests.NondeterministicObjectSequence
{
    public class Collapse
    {
        [Fact]
        public void SingleItem_ReturnsSameValue()
        {
            // Given
            var obj = Samples.Sequence(1);

            // When
            var collapsed = obj.Collapse();

            // Then
            Assert.Collection(collapsed,
                item => Assert.Collection(item,
                    item => Assert.Equal(1, item.Value)));
        }

        [Fact]
        public void TwoItems_ReturnsSameValues()
        {
            // Given
            var obj = Samples.Sequence(1, 2);

            // When
            var collapsed = obj.Collapse();

            // Then
            Assert.Collection(collapsed,
                item => Assert.Collection(item,
                    item => Assert.Equal(1, item.Value),
                    item => Assert.Equal(2, item.Value)));
        }

        [Fact]
        public void EmptyTail_ReturnsNothing()
        {
            // Given
            var obj = Samples.EmptyTail();

            // When
            var collapsed = obj.Collapse();

            // Then
            Assert.Empty(collapsed);
        }

        [Fact]
        public void TwoTails_ReturnsTwoSequences()
        {
            // Given
            var obj = Samples.TwoTails(1, 2, 3);

            // When
            var collapsed = obj.Collapse();

            // Then
            Assert.Collection(collapsed,
                item => Assert.Collection(item,
                    item => Assert.Equal(1, item.Value),
                    item => Assert.Equal(2, item.Value)),
                item => Assert.Collection(item,
                    item => Assert.Equal(1, item.Value),
                    item => Assert.Equal(3, item.Value)));
        }
    }
}
