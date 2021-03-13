using System.Xml.Schema;
using System.Linq;
using System;
using Xunit;
using Kleene;
using Kleene.Tests.NondeterministicObjects;

namespace Kleene.Tests.AnyObject
{
    public class Overlap
    {
        [Fact]
        public void OverlapAny_ReturnsAny()
        {
            // Given
            var leader = Samples.Any();
            var follower = Samples.Any();
            
            // When
            var overlap = leader.Overlap(follower);

            // Then
            Assert.Collection(overlap,
                item => Assert.IsType<AnyObject<Runnable<int>>>(item));
        }
    }
}
