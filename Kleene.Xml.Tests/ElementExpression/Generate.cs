using System.Runtime.InteropServices;
using System.Reflection;
using System.Linq;
using System;
using Xunit;
using Kleene;
using Kleene.Xml;
using System.Xml.Linq;

namespace Kleene.Tests.ElementExpressionTests
{
    public class Generate
    {
        [Fact]
        public void Simple_ReturnsElement()
        {
            // Given
            var expression = new XElement("foo", new XElement("bar")).ToRunnable().ToExpression();
            
            // When
            var results = expression.Generate();

            // Then
            Assert.Collection(results,
                item => {
                    Assert.Equal("foo", item.Name);
                    Assert.Collection(item.Children,
                        child => {
                            Assert.Equal("bar", child.Name);
                            Assert.Empty(child.Children);                            
                        });
                });
        }
    }
}
