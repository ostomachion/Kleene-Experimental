using System;
using Xunit;
using Kleene;
using System.Linq;

namespace KleeneTests
{
    public class AltExpressionTests
    {
        public class Constructor
        {
        }

        public class Run
        {
            [Theory]
            [InlineData('f', 'o')]
            [InlineData(' ', '\t')]
            public void TwoChars_ReturnsChars(char c1, char c2)
            {
                // Given
                var expression = new AltExpression<ConstantExpression<char>>(new[] {
                    new ConstantExpression<char>(c1),
                    new ConstantExpression<char>(c2),
                });

                // When
                var result = expression.Run();

                // Then
                Assert.Collection(result,
                    branch =>
                    {
                        Assert.Equal(c1, branch.Value);
                    },
                    branch =>
                    {
                        Assert.Equal(c2, branch.Value);
                    });
            }
        }
    }
}
