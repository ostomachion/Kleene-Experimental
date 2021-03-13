using System.Collections;
using System.Linq;
using System;
using Xunit;
using Kleene;

namespace Kleene.Tests.NondeterministicRunnable
{
    public class Overlap
    {
        [Fact]
        public void OverlapSameValue_ReturnsSameValue()
        {
            // Given
            var leader = new NondeterministicRunnable<int>(1);
            var follower = new NondeterministicRunnable<int>(1);

            // When
            var overlap = leader.Overlap(follower);

            // Then
            Assert.Collection(overlap,
                item => {
                   Assert.IsType<NondeterministicRunnable<int>>(item);
                   var cast = item as NondeterministicRunnable<int>;
                   Assert.Equal(1, cast.Value); 
                });
        }

        [Fact]
        public void OverlapDifferentValues_ReturnsNothing()
        {
            // Given
            var leader = new NondeterministicRunnable<int>(1);
            var follower = new NondeterministicRunnable<int>(2);

            // When
            var overlap = leader.Overlap(follower);

            // Then
            Assert.Empty(overlap);
        }
    }
}
