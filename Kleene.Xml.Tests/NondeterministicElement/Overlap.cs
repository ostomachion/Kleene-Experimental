using System.Runtime.InteropServices;
using System.Reflection;
using System.Linq;
using System;
using Xunit;
using Kleene;
using Kleene.Xml;
using System.Xml.Linq;

namespace Kleene.Tests.NondeterministicElementTests
{
    public class Overlap
    {
        [Fact]
        public void Test()
        {
            // Given
            var leader = new XElement("foo", new XElement("bar")).ToRunnable().ToExpression().Run().Single();
            var follower = new XElement("foo", new XElement("bar")).ToRunnable().ToExpression().Run().Single();

            // When
            var results = leader.Overlap(follower);

            // Then
            Assert.Collection(results,
                item => Assert.Equal(item, leader));
        }

        [Fact]
        public void Test2()
        {
            // Given
            var leader = new XElement("foo").ToRunnable().ToExpression().Run().Single();
            var follower = new XElement("bar").ToRunnable().ToExpression().Run().Single();

            // When
            var results = leader.Overlap(follower);

            // Then
            Assert.Empty(results);
        }
    }
}
