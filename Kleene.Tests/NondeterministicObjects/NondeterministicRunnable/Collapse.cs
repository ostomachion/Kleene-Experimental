using System.Linq;
using System;
using Xunit;
using Kleene;

namespace Kleene.Tests.NondeterministicRunnable
{
    public class Collapse
    {
        [Fact]
        public void Collapse_ReturnsSameValue()
        {
            // Given
            var obj = new NondeterministicRunnable<int>(1);

            // When
            var collapsed = obj.Collapse();

            // Then
            Assert.Collection(collapsed,
                item => Assert.Equal(1, item.Value));
        }
    }
}
