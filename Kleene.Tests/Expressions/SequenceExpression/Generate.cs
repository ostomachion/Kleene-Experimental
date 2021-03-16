using System.Linq;
using System;
using Xunit;
using Kleene;

namespace Kleene.Tests.SequenceExpression
{
    public class Generate
    {
        [Fact]
        public void ZeroChoices_ReturnsEmpty()
        {
            // Given
            var expression = new SequenceExpression<string>(Array.Empty<LiteralExpression<string>>());
            
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
            var expression = new SequenceExpression<string>(new LiteralExpression<string>[] { new("foo") });
            
            // When
            var results = expression.Generate();

            // Then
            Assert.Collection(results,
                result => Assert.Collection(result,
                    item => Assert.Equal("foo", item)));
        }

        [Fact]
        public void TwoChoices_ReturnsBoth()
        {
            // Given
            var expression = new SequenceExpression<string>(new LiteralExpression<string>[] { new("foo"), new("bar") });
            
            // When
            var results = expression.Generate();

            // Then
            Assert.Collection(results,
                result => Assert.Collection(result,
                    item => Assert.Equal("foo", item),
                    item => Assert.Equal("bar", item)));
        }

        [Fact]
        public void ThreeChoices_ReturnsAll()
        {
            // Given
            var expression = new SequenceExpression<string>(new LiteralExpression<string>[] { new("foo"), new("bar"), new("baz") });
            
            // When
            var results = expression.Generate();

            // Then
            Assert.Collection(results,
                result => Assert.Collection(result,
                    item => Assert.Equal("foo", item),
                    item => Assert.Equal("bar", item),
                    item => Assert.Equal("baz", item)));
        }
    }
}