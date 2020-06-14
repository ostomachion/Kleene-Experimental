using System;
using Xunit;
using Kleene;

namespace KleeneTests
{
    public class LiteralExpressionTests
    {
        public class Constructor
        {
            [Fact]
            public void BasicConstructorChar()
            {
                // When
                var expression = new ConstantExpression<ConstantStructure<char>>(new ConstantStructure<char>('c'));

                // Then
                Assert.Equal('c', expression.Value.Value);
            }

            [Fact]
            public void NullValue_Throws()
            {
                Assert.Throws<ArgumentNullException>(() =>
                {
                    var expression = new ConstantExpression<ConstantStructure<char>>(null!);
                });
            }
        }

        public class Run
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
            public void ConstantStructureChar_Run(char value)
            {
                // Given
                var expression = new ConstantExpression<ConstantStructure<char>>(new ConstantStructure<char>(value));

                // When
                var result = expression.Run();

                // Then
                Assert.Collection(result,
                    branch =>
                    {
                        Assert.Equal(value, branch.Value);
                    });
            }

            [Theory]
            [InlineData(0)]
            [InlineData(1)]
            [InlineData(-2)]
            [InlineData(Int32.MaxValue)]
            [InlineData(Int32.MinValue)]
            public void ConstantStructureInt_Run(int value)
            {
                // Given
                var expression = new ConstantExpression<ConstantStructure<int>>(new ConstantStructure<int>(value));

                // When
                var result = expression.Run();

                // Then
                Assert.Collection(result,
                    branch =>
                    {
                        Assert.Equal(value, branch.Value);
                    });
            }

            [Theory]
            [InlineData("")]
            [InlineData("foo")]
            [InlineData("Hello, world!")]
            [InlineData(null)]
            public void ConstantStructureString_Run(string? value)
            {
                // Given
                var expression = new ConstantExpression<ConstantStructure<string?>>(new ConstantStructure<string?>(value));

                // When
                var result = expression.Run();

                // Then
                Assert.Collection(result,
                    branch =>
                    {
                        Assert.Equal(value, branch.Value);
                    });
            }
        }
    }
}
