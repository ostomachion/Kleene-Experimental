using System;
using Xunit;
using Kleene;

namespace KleeneTests
{
    public class SequenceExpressionTests
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
                var expression = new SequenceExpression(new[] {
                    new ConstantExpression<char>(new ConstantStructure<char>(c1)),
                    new ConstantExpression<char>(new ConstantStructure<char>(c2)),
                });

                var input = new SequenceStructure(new [] {
                    new ConstantStructure<char>(c1),
                    new ConstantStructure<char>(c2),
                });

                // When
                var result = expression.Run(input);

                // Then
                Assert.Collection(result,
                    branch =>
                    {
                        Assert.IsType<SequenceStructure>(branch);
                        var structure = (branch as SequenceStructure)!;
                        Assert.True(branch.Done);
                        Assert.Collection(structure.Value,
                            item => {
                                Assert.Equal(true, item.Done);
                                Assert.IsType<ConstantStructure<char>>(item);
                                Assert.Equal(c1, (item as ConstantStructure<char>)?.Value);
                            },
                            item => {
                                Assert.Equal(true, item.Done);
                                Assert.IsType<ConstantStructure<char>>(item);
                                Assert.Equal(c2, (item as ConstantStructure<char>)?.Value);
                            }
                        );
                    });
            }
        }
    }
}
