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
    public class Collapse
    {
        [Fact]
        public void Simple_ReturnsElement()
        {
            // Given
            var element = new XElement("foo", new XElement("bar")).ToRunnable().ToExpression().Run().Single();

            // When
            var results = element.Collapse();

            // Then
            Assert.Collection(results,
                item =>
                {
                    Assert.Equal("foo", item.Name);
                    Assert.Collection(item.Children,
                        child =>
                        {
                            Assert.Equal("bar", child.Name);
                            Assert.Empty(child.Children);
                        });
                });
        }
    }
}
