using System.Linq;
using System;
using Xunit;
using Kleene;

namespace Kleene.Tests.RunnableExpression
{
    public class Generate
    {
        [Fact]
        public void Generate_ReturnsSameValue()
        {
            // Given
            var expression = new LiteralExpression<string>("foo");
            
            // When
            var results = expression.Generate();

            // Then
            Assert.Collection(results,
                item => Assert.Equal("foo", item));
        }
    }
}
