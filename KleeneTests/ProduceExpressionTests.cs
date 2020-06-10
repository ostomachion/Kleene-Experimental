using System;
using Xunit;
using Kleene;
using System.Linq;
using System.Collections.Generic;

namespace KleeneTests
{
    public class ProduceExpressionTests
    {
        [Fact]
        public void NullInput_Throws() {
            // Given
            var expression = new ProduceExpression<char, char>('x');
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
        public void RunOnSameChar_ReturnsSelf(char c)
        {
            // Given
            var expression = new ProduceExpression<char, char>(c);
            var input = new [] { c };
            
            // When
            var result = expression.Run(input);

            // Then
            Assert.Collection(result,
                branch => {
                    Assert.Equal(0, branch.Offset);
                    Assert.Equal(0, branch.Length);
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
        public void RunOnStartsWithSameChar_ReturnsSefl(char c)
        {
            // Given
            var expression = new ProduceExpression<char, char>(c);
            var input = new [] { c, 'T', 'e', 's', 't', '.' };
            
            // When
            var result = expression.Run(input);

            // Then
            Assert.Collection(result,
                branch => {
                    Assert.Equal(0, branch.Offset);
                    Assert.Equal(0, branch.Length);
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
        public void RunOnEmpty_ReturnsSelf(char c)
        {
            // Given
            var expression = new ProduceExpression<char, char>(c);
            var input = Enumerable.Empty<char>();
            
            // When
            var result = expression.Run(input);

            // Then
            Assert.Collection(result,
                branch => {
                    Assert.Equal(0, branch.Offset);
                    Assert.Equal(0, branch.Length);
                    Assert.Collection(branch.Output,
                        item => Assert.Equal(c, item));
                });
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
        public void RunOnStartsWithDifferentChar_ReturnsSelf(char c, string input)
        {
            // Given
            Assert.NotEmpty(input);
            Assert.NotEqual(c, input.First());
            var expression = new ProduceExpression<char, char>(c);
            
            // When
            var result = expression.Run(input);

            // Then
            Assert.Collection(result,
                branch => {
                    Assert.Equal(0, branch.Offset);
                    Assert.Equal(0, branch.Length);
                    Assert.Collection(branch.Output,
                        item => Assert.Equal(item, c));
                });
        }
    }
}
