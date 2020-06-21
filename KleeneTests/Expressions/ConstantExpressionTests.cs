using System;
using Xunit;
using Kleene;
using System.Linq;
using System.Collections.Generic;

namespace KleeneTests
{
    public class ConstantExpressionTests
    {
        [Fact]
        public void NullInput_Throws() {
            // Given
            var expression = new ConstantExpression<char>('x');
            IEnumerable<Structure> input = null!;

            // Then
            Assert.Throws<ArgumentNullException>(() => {
                expression.Run(input, 0).ToList();
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
            var expression = new ConstantExpression<char>(c);
            var input = new [] { new ConstantStructure<char>(c) };
            
            // When
            var results = expression.Run(input, 0);

            // Then
            Assert.Collection(results,
                result => {
                    Assert.Equal(input, result.Input);
                    Assert.Equal(0, result.Index);
                    Assert.Equal(1, result.Length);
                    Assert.Equal(expression, result.Source);
                    Assert.Empty(result.Children);
                }
            );
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
            var expression = new ConstantExpression<char>(c);
            var input = new [] { c, 'T', 'e', 's', 't', '.' }.Select(x => new ConstantStructure<char>(x)).ToArray();
            
            // When
            var results = expression.Run(input, 0);

            // Then
            Assert.Collection(results,
                result => {
                    Assert.Equal(input, result.Input);
                    Assert.Equal(0, result.Index);
                    Assert.Equal(1, result.Length);
                    Assert.Equal(expression, result.Source);
                    Assert.Empty(result.Children);
                }
            );
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
            var expression = new ConstantExpression<char>(c);
            var input = Enumerable.Empty<ConstantStructure<char>>().ToArray();
            
            // When
            var results = expression.Run(input, 0);

            // Then
            Assert.Empty(results);
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
        public void RunOnStartsWithDifferentChar_ReturnsNothing(char c, string inputString)
        {
            // Given
            Assert.NotEmpty(inputString);
            Assert.NotEqual(c, inputString.First());
            var input = inputString.Select(c => new ConstantStructure<char>(c)).ToArray();
            var expression = new ConstantExpression<char>(c);
            
            // When
            var results = expression.Run(input, 0);

            // Then
            Assert.Empty(results);
        }
    }
}