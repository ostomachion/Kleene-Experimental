using System;
using Xunit;
using Kleene;
using System.Linq;

namespace KleeneTests
{
    public class ProduceExpressionTests
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
        public void RunOnSameChar_ReturnsSelf(char c)
        {
            // Given
            var expression = new ProduceExpression<char, char>(c);
            var input = new [] { c };
            
            // When
            var result = expression.Run(input);

            // Then
            Assert.Collection(result,
                branch => Assert.Collection(branch,
                    item => Assert.Equal(item, c)));
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
        public void RunOnStartsWithSameChar_ReturnsSefl(char c)
        {
            // Given
            var expression = new ProduceExpression<char, char>(c);
            var input = new [] { c, 'T', 'e', 's', 't', '.' };
            
            // When
            var result = expression.Run(input);

            // Then
            Assert.Collection(result,
                branch => Assert.Collection(branch,
                    item => Assert.Equal(item, c)));
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
        public void RunOnEmpty_ReturnsSelf(char c)
        {
            // Given
            var expression = new ProduceExpression<char, char>(c);
            var input = Enumerable.Empty<char>();
            
            // When
            var result = expression.Run(input);

            // Then
            Assert.Collection(result,
                branch => Assert.Collection(branch,
                    item => Assert.Equal(item, c)));
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
        public void RunOnStartsWithDifferentChar_ReturnsSlef(char c, string input)
        {
            // Given
            Assert.NotEmpty(input);
            Assert.NotEqual(c, input.First());
            var expression = new ProduceExpression<char, char>(c);
            
            // When
            var result = expression.Run(input);

            // Then
            Assert.Collection(result,
                branch => Assert.Collection(branch,
                    item => Assert.Equal(item, c)));
        }
    }
}
