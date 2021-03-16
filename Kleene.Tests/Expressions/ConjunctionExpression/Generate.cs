using System.Linq;
using System;
using Xunit;
using Kleene;

namespace Kleene.Tests.ConjunctionExpression
{
    public class Generate
    {
        [Fact]
        public void DifferentStrings_ReturnsNothing()
        {
            // Given
            var expression = new ConjunctionExpression<string>(
                new LiteralExpression<string>("foo"),
                new LiteralExpression<string>("bar"));

            // When
            var results = expression.Generate();
            
            // Then
            Assert.Empty(results);
        }

        [Fact]
        public void SameStrings_ReturnsOne()
        {
            // Given
            var expression = new ConjunctionExpression<string>(
                new LiteralExpression<string>("foo"),
                new LiteralExpression<string>("foo"));

            // When
            var results = expression.Generate();
            
            // Then
            Assert.Collection(results,
                result => Assert.Equal("foo", result ));
        }
    }
}
