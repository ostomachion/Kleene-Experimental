using System.Linq;
using System;
using Xunit;
using Kleene;

namespace Kleene.Tests.AltExpression
{
    public class Generate
    {
        [Fact]
        public void ZeroChoices_ReturnsNothing()
        {
            // Given
            var expression = new AltExpression<Runnable<int>>(Array.Empty<RunnableExpression<int>>());
            
            // When
            var results = expression.Generate();

            // Then
            Assert.Empty(results);
        }
        
        [Fact]
        public void OneChoices_ReturnsChoice()
        {
            // Given
            var expression = new AltExpression<Runnable<int>>(new RunnableExpression<int>[] { 1 });
            
            // When
            var results = expression.Generate();

            // Then
            Assert.Collection(results,
                item => Assert.Equal(1, item.Value));
        }

        [Fact]
        public void TwoChoices_ReturnsBoth()
        {
            // Given
            var expression = new AltExpression<Runnable<int>>(new RunnableExpression<int>[] { 1, 2 });
            
            // When
            var results = expression.Generate();

            // Then
            Assert.Collection(results,
                item => Assert.Equal(1, item.Value),
                item => Assert.Equal(2, item.Value));
        }
        
        [Fact]
        public void ThreeChoices_ReturnsAll()
        {
            // Given
            var expression = new AltExpression<Runnable<int>>(new RunnableExpression<int>[] { 1, 2, 3 });
            
            // When
            var results = expression.Generate();

            // Then
            Assert.Collection(results,
                item => Assert.Equal(1, item.Value),
                item => Assert.Equal(2, item.Value),
                item => Assert.Equal(3, item.Value));
        }
    }
}