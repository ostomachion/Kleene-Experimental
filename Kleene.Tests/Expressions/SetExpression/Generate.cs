using System.Linq;
using System;
using Xunit;
using Kleene;

namespace Kleene.Tests.Expressions.SeExpression
{
    public class Generate
    {
        [Fact]
        public void ZeroChoices_ReturnsEmpty()
        {
            // Given
            var expression = new SetExpression<Runnable<int>>(Array.Empty<RunnableExpression<int>>());
            
            // When
            var results = expression.Generate();

            // Then
            Assert.Collection(results,
                result => Assert.Empty(result));
        }

        [Fact]
        public void OneChoice_ReturnsChoice()
        {
            // Given
            var expression = new SetExpression<Runnable<int>>(new RunnableExpression<int>[] { 1 });
            
            // When
            var results = expression.Generate();

            // Then
            Assert.Collection(results,
                result => Assert.Collection(result,
                    item => Assert.Equal(1, item.Value)));
        }

        [Fact]
        public void TwoChoices_ReturnsBoth()
        {
            // Given
            var expression = new SetExpression<Runnable<int>>(new RunnableExpression<int>[] { 1, 2 });
            
            // When
            var results = expression.Generate();

            // Then
            Assert.Collection(results,
                result => Assert.Collection(result,
                    item => Assert.Equal(1, item.Value),
                    item => Assert.Equal(2, item.Value)));
        }

        [Fact]
        public void ThreeChoices_ReturnsAll()
        {
            // Given
            var expression = new SetExpression<Runnable<int>>(new RunnableExpression<int>[] { 1, 2, 3 });
            
            // When
            var results = expression.Generate();

            // Then
            Assert.Collection(results,
                result => Assert.Collection(result,
                    item => Assert.Equal(1, item.Value),
                    item => Assert.Equal(2, item.Value),
                    item => Assert.Equal(3, item.Value)));
        }
    }
}