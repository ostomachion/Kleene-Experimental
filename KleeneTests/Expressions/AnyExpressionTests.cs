using System;
using Xunit;
using Kleene;
using System.Linq;
using System.Collections.Generic;

namespace KleeneTests
{
    public class AnyExpressionTests
    {
        [Fact]
        public void NullInput_Throws() {
            // Given
            var expression = new AnyExpression();
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
        public void RunOnChar_ReturnsChar(char c)
        {
            // Given
            var expression = new AnyExpression();
            var input = new [] { new ConstantStructure<char>(c) }.ToArray();
            
            // When
            var results = expression.Run(input, 0);

            // Then
            Assert.Collection(results,
                result => {
                    Assert.Equal(input, result.Input);
                    Assert.Equal(0, result.Index);
                    Assert.Equal(1, result.Length);
                    Assert.Equal(expression, result.Expression);
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
        public void RunOnChars_ReturnsFirstChar(char c)
        {
            // Given
            var expression = new AnyExpression();
            var input = new [] { c, 'T', 'e', 's', 't', '.' }.Select(x => new ConstantStructure<char>(x)).ToArray();
            
            // When
            var results = expression.Run(input, 0);

            // Then
            Assert.Collection(results,
                result => {
                    Assert.Equal(input, result.Input);
                    Assert.Equal(0, result.Index);
                    Assert.Equal(1, result.Length);
                    Assert.Equal(expression, result.Expression);
                    Assert.Empty(result.Children);
                }
            );
        }

        [Fact]
        public void RunOnEmpty_ReturnsNothing()
        {
            // Given
            var expression = new AnyExpression();
            var input = Enumerable.Empty<ConstantStructure<char>>().ToArray();
            
            // When
            var results = expression.Run(input, 0);

            // Then
            Assert.Empty(results);
        }
    }
}