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
                var expression = new SequenceExpression<ConstantExpression<char>>(new[] {
                    new ConstantExpression<char>(c1),
                    new ConstantExpression<char>(c2),
                });

                // When
                var result = expression.Run();

                // Then
                Assert.Collection(result,
                    branch =>
                    {
                        Assert.Collection(branch.Expressions,
                            item => {
                                Assert.Collection(item.Run(),
                                    x => Assert.Equal(c1, x.Value));
                            },
                            item => {
                                Assert.Collection(item.Run(),
                                    x => Assert.Equal(c2, x.Value));
                            }
                        );
                    });
            }

            [Theory]
            [InlineData(':', ';', ')', '(')]
            public void Backtrack(char c1, char c2, char c3, char c4)
            {
                // Given
                var expression = new SequenceExpression<ConstantExpression<char>>(new[] {
                    new AltExpression<ConstantExpression<char>>(new [] {
                        new ConstantExpression<char>(c1),
                        new ConstantExpression<char>(c2),
                    }),
                    new AltExpression<ConstantExpression<char>>(new [] {
                        new ConstantExpression<char>(c3),
                        new ConstantExpression<char>(c4),
                    }),
                });

                // When
                var result = expression.Run();

                // Then
                Assert.Collection(result,
                    branch =>
                    {
                        Assert.Collection(branch.Expressions,
                            item => {
                                Assert.Collection(item.Run(),
                                    x => Assert.Equal(c1, x.Value));
                            },
                            item => {
                                Assert.Collection(item.Run(),
                                    x => Assert.Equal(c3, x.Value));
                            }
                        );
                    },
                    branch =>
                    {
                        Assert.Collection(branch.Expressions,
                            item => {
                                Assert.Collection(item.Run(),
                                    x => Assert.Equal(c1, x.Value));
                            },
                            item => {
                                Assert.Collection(item.Run(),
                                    x => Assert.Equal(c4, x.Value));
                            }
                        );
                    },
                    branch =>
                    {
                        Assert.Collection(branch.Expressions,
                            item => {
                                Assert.Collection(item.Run(),
                                    x => Assert.Equal(c2, x.Value));
                            },
                            item => {
                                Assert.Collection(item.Run(),
                                    x => Assert.Equal(c3, x.Value));
                            }
                        );
                    },
                    branch =>
                    {
                        Assert.Collection(branch.Expressions,
                            item => {
                                Assert.Collection(item.Run(),
                                    x => Assert.Equal(c2, x.Value));
                            },
                            item => {
                                Assert.Collection(item.Run(),
                                    x => Assert.Equal(c4, x.Value));
                            }
                        );
                    });
            }
        }
    }
}
