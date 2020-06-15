using System;
using Xunit;
using Kleene;

namespace KleeneTests
{
    public class ConstantExpressionTests
    {
        public class Constructor
        {
            [Fact]
            public void BasicConstructorChar()
            {
                // When
                var expression = new ConstantExpression<char>(new ConstantStructure<char>('c'));

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
                var expression = new ConstantExpression<char>(new ConstantStructure<char>(value));

                // When
                var result = expression.Run(new ConstantStructure<char>(value));

                // Then
                Assert.Collection(result,
                    branch =>
                    {
                        Assert.IsType<ConstantStructure<char>>(branch);
                        var structure = (branch as ConstantStructure<char>)!;
                        Assert.True(branch.Done);
                        Assert.Equal(value, structure.Value);
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
                var expression = new ConstantExpression<int>(new ConstantStructure<int>(value));

                // When
                var result = expression.Run(new ConstantStructure<int>(value));

                // Then
                Assert.Collection(result,
                    branch =>
                    {
                        Assert.IsType<ConstantStructure<int>>(branch);
                        var structure = (branch as ConstantStructure<int>)!;
                        Assert.True(branch.Done);
                        Assert.Equal(value, structure.Value);
                    });
            }

            [Theory]
            [InlineData("")]
            [InlineData("foo")]
            [InlineData("Hello, world!")]
            public void ConstantStructureString_Run(string value)
            {
                // Given
                var expression = new ConstantExpression<string>(new ConstantStructure<string>(value));

                // When
                var result = expression.Run(new ConstantStructure<string>(value));

                // Then
                Assert.Collection(result,
                    branch =>
                    {
                        Assert.IsType<ConstantStructure<string>>(branch);
                        var structure = (branch as ConstantStructure<string>)!;
                        Assert.True(branch.Done);
                        Assert.Equal(value, structure.Value);
                    });
            }
        }
    }
}
