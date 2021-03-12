using System.Linq;
using System;
using Xunit;
using Kleene;

namespace Kleene.Tests.ConjunctionExpression
{
    public class Generate
    {
        [Fact]
        public void DifferentInts_ReturnsNothing()
        {
            // Given
            var expression = new ConjunctionExpression<Runnable<int>>(
                new RunnableExpression<int>(1),
                new RunnableExpression<int>(2));

            // When
            var results = expression.Generate();
            
            // Then
            Assert.Empty(results);
        }

        [Fact]
        public void SameInts_ReturnsOne()
        {
            // Given
            var expression = new ConjunctionExpression<Runnable<int>>(
                new RunnableExpression<int>(1),
                new RunnableExpression<int>(1));

            // When
            var results = expression.Generate();
            
            // Then
            Assert.Collection(results,
                result => Assert.Equal(1, result.Value));
        }
    }
}
