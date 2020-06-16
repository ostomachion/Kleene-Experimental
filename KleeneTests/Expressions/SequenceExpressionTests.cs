using System;
using Xunit;
using Kleene;
using System.Linq;

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
                    new ConstantExpression<char>(c1),
                    new ConstantExpression<char>(c2),
                });

                // When
                var result = expression.Run().Cast<SequenceExpression>();

                // Then
                Assert.Collection(result,
                    branch =>
                    {
                        Assert.Collection(branch.Expressions.Cast<ConstantExpression<char>>(),
                            item =>
                            {
                                Assert.Equal(c1, item.Value);
                            },
                            item =>
                            {
                                Assert.Equal(c2, item.Value);
                            }
                        );
                    });
            }

            [Theory]
            [InlineData(':', ';', ')', '(')]
            public void Backtrack(char c1, char c2, char c3, char c4)
            {
                // Given
                var expression = new SequenceExpression(new[] {
                    new AltExpression(new [] {
                        new ConstantExpression<char>(c1),
                        new ConstantExpression<char>(c2),
                    }),
                    new AltExpression(new [] {
                        new ConstantExpression<char>(c3),
                        new ConstantExpression<char>(c4),
                    }),
                });

                // When
                var result = expression.Run().Cast<SequenceExpression>();

                // Then
                Assert.Collection(result,
                    branch =>
                    {
                        Assert.Collection(branch.Expressions.Cast<ConstantExpression<char>>(),
                            item =>
                            {
                                Assert.Equal(c1, item.Value);
                            },
                            item =>
                            {
                                Assert.Equal(c3, item.Value);
                            }
                        );
                    },
                    branch =>
                    {
                        Assert.Collection(branch.Expressions.Cast<ConstantExpression<char>>(),
                            item =>
                            {
                                Assert.Equal(c1, item.Value);
                            },
                            item =>
                            {
                                Assert.Equal(c4, item.Value);
                            }
                        );
                    },
                    branch =>
                    {
                        Assert.Collection(branch.Expressions.Cast<ConstantExpression<char>>(),
                            item =>
                            {
                                Assert.Equal(c2, item.Value);
                            },
                            item =>
                            {
                                Assert.Equal(c3, item.Value);
                            }
                        );
                    },
                    branch =>
                    {
                        Assert.Collection(branch.Expressions.Cast<ConstantExpression<char>>(),
                            item =>
                            {
                                Assert.Equal(c2, item.Value);
                            },
                            item =>
                            {
                                Assert.Equal(c4, item.Value);
                            }
                        );
                    });
            }
        }
    }
}
