using System;
using Xunit;
using Kleene;
using System.Linq;
using System.Collections.Generic;

namespace KleeneTests
{
    public class LiteralExpressionTests
    {
        [Theory]
        [InlineData('c')]
        [InlineData('X')]
        [InlineData('9')]
        [InlineData('#')]
        [InlineData(' ')]
        [InlineData('\n')]
        [InlineData('\0')]
        [InlineData('μ')]
        public void Run(char c)
        {
            // Given
            var expression = new ConstantExpression<ConstantStructure<char>>(new ConstantStructure<char>(c));
            
            // When
            var result = expression.Run();

            // Then
            Assert.Collection(result,
                branch => {
                    Assert.Equal(c, branch.Value);
                });
        }
    }
}
