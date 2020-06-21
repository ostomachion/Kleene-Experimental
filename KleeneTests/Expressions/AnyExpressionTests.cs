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
            var input = new [] { new ConstantStructure<char>(c) };
            
            // When
            var result = expression.Run(input, 0);

            // Then
            Assert.Collection(result,
                branch => Assert.Collection(branch,
                    item => Assert.Equal(c, ((ConstantStructure<char>)item).Value)));
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
            var input = new [] { c, 'T', 'e', 's', 't', '.' }.Select(x => new ConstantStructure<char>(x));
            
            // When
            var result = expression.Run(input, 0);

            // Then
            Assert.Collection(result,
                branch => Assert.Collection(branch,
                    item => Assert.Equal(c, ((ConstantStructure<char>)item).Value)));
        }

        [Fact]
        public void RunOnEmpty_ReturnsNothing()
        {
            // Given
            var expression = new AnyExpression();
            var input = Enumerable.Empty<ConstantStructure<char>>();
            
            // When
            var result = expression.Run(input, 0);

            // Then
            Assert.Empty(result);
        }
    }
}