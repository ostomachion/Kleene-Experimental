using System.Linq;
using System;
using Xunit;
using Kleene;
using Kleene.Tests.NondeterministicObjects;

namespace Kleene.Tests.NondeterministicEmptyObjectSequence
{
    public class Collapse
    {
        [Fact]
        public void Collapse_ReturnsSameValue()
        {
            // Given
            var obj = Samples.EmptySequence();

            // When
            var collapsed = obj.Collapse();

            // Then
            Assert.Collection(collapsed,
                item => Assert.Empty(item));
        }
    }
}
