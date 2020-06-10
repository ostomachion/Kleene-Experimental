using System;
using Xunit;
using Kleene;
using System.Linq;
using System.Collections.Generic;

namespace KleeneTests
{
    public class LiteralExpressionTests
    {
        [Fact]
        public void NullInput_Throws() {
            // Given
            var expression = new LiteralExpression<char>('x');
            IEnumerable<char> input = null!;

            // Then
            Assert.Throws(typeof(ArgumentNullException), () => {
                expression.Run(input).ToList();
            });
        }
        
        [Theory]
        [InlineData('c')]
        [InlineData('X')]
        [InlineData('9')]
        [InlineData('#')]
        [InlineData(' ')]
        [InlineData('\n')]
        [InlineData('\0')]
        [InlineData('μ')]
        public void RunOnSameChar_ReturnsSameChar(char c)
        {
            // Given
            var expression = new LiteralExpression<char>(c);
            var input = new [] { c };
            
            // When
            var result = expression.Run(input);

            // Then
            Assert.Collection(result,
                branch => {
                    Assert.Equal(0, branch.Offset);
                    Assert.Equal(1, branch.Length);
                    Assert.Collection(branch.Output,
                        item => Assert.Equal(c, item));
                });
        }

        [Theory]
        [InlineData('c')]
        [InlineData('X')]
        [InlineData('9')]
        [InlineData('#')]
        [InlineData(' ')]
        [InlineData('\n')]
        [InlineData('\0')]
        [InlineData('μ')]
        public void RunOnStartsWithSameChar_ReturnsSameChar(char c)
        {
            // Given
            var expression = new LiteralExpression<char>(c);
            var input = new [] { c, 'T', 'e', 's', 't', '.' };
            
            // When
            var result = expression.Run(input);

            // Then
            Assert.Collection(result,
                branch => {
                    Assert.Equal(0, branch.Offset);
                    Assert.Equal(1, branch.Length);
                    Assert.Collection(branch.Output,
                        item => Assert.Equal(c, item));
                });
        }

        [Theory]
        [InlineData('c')]
        [InlineData('X')]
        [InlineData('9')]
        [InlineData('#')]
        [InlineData(' ')]
        [InlineData('\n')]
        [InlineData('\0')]
        [InlineData('μ')]
        public void RunOnEmpty_ReturnsNothing(char c)
        {
            // Given
            var expression = new LiteralExpression<char>(c);
            var input = Enumerable.Empty<char>();
            
            // When
            var result = expression.Run(input);

            // Then
            Assert.Empty(result);
        }

        [Theory]
        [InlineData('c', "foo")]
        [InlineData('o', "foo")]
        [InlineData('X', " X")]
        [InlineData('9', "n")]
        [InlineData('#', "*****")]
        [InlineData(' ', "Hello, world!")]
        [InlineData('\n', " ")]
        [InlineData('\0', " \\ 0 ")]
        [InlineData('μ', "mu")]
        public void RunOnStartsWithDifferentChar_ReturnsNothing(char c, string input)
        {
            // Given
            Assert.NotEmpty(input);
            Assert.NotEqual(c, input.First());
            var expression = new LiteralExpression<char>(c);
            
            // When
            var result = expression.Run(input);

            // Then
            Assert.Empty(result);
        }
    }
}
