using System;
using Xunit;
using Kleene;
using System.Linq;
using System.Collections.Generic;

namespace KleeneTests
{
    public class AltExpressionTests
    {
        public class Run
        {
            [Fact]
            public void NullExpressions_Throws()
            {
                Assert.Throws<ArgumentNullException>(() =>
                {
                    new AltExpression(null!);
                });
            }

            [Fact]
            public void NullExpression_Throws()
            {
                Assert.Throws<ArgumentException>(() =>
                {
                    new AltExpression(new Expression[] {
                        null!
                    });
                });
            }

            [Fact]
            public void NullAndNotNullExpression_Throws()
            {
                Assert.Throws<ArgumentException>(() =>
                {
                    new AltExpression(new Expression[] {
                        SequenceExpression.Empty,
                        null!
                    });
                });
            }

            [Fact]
            public void ZeroChoices_RetrurnsNothing()
            {
                // Given
                var expression = new AltExpression(Enumerable.Empty<Expression>());

                // When
                var results = expression.Run();

                // Then
                Assert.Empty(results);
            }

            [Fact]
            public void OneChoice_ReturnsChoice()
            {
                // Given
                var expression = new AltExpression(EnumerableExt.Yield(new StructureExpression("foo")));

                // When
                var results = expression.Run();

                // Then
                Assert.Collection(results,
                    result =>
                    {
                        Assert.IsType<NondeterministicStructure>(result);
                        var namedResult = (NondeterministicStructure)result!;
                        Assert.Equal("foo", namedResult.Name);
                        Assert.Collection(namedResult.FirstChild,
                            item =>
                            {
                                Assert.Null(item);
                            }
                        );
                        Assert.Collection(namedResult.NextSibling,
                            item =>
                            {
                                Assert.Null(item);
                            }
                        );
                    }
                );
            }

            [Fact]
            public void TwoChoices_ReturnsBothChoices()
            {
                // Given
                var expression = new AltExpression(new[] {
                    new StructureExpression("foo"),
                    new StructureExpression("bar")
                });

                // When
                var results = expression.Run();

                // Then
                Assert.Collection(results,
                    result =>
                    {
                        Assert.IsType<NondeterministicStructure>(result);
                        var namedResult = (NondeterministicStructure)result!;
                        Assert.Equal("foo", namedResult!.Name);
                        Assert.Collection(namedResult.FirstChild,
                            item =>
                            {
                                Assert.Null(item);
                            }
                        );
                        Assert.Collection(namedResult.NextSibling,
                            item =>
                            {
                                Assert.Null(item);
                            }
                        );
                    },
                    result =>
                    {
                        Assert.IsType<NondeterministicStructure>(result);
                        var namedResult = (NondeterministicStructure)result!;
                        Assert.Equal("bar", namedResult!.Name);
                        Assert.Collection(namedResult.FirstChild,
                            item =>
                            {
                                Assert.Null(item);
                            }
                        );
                        Assert.Collection(namedResult.NextSibling,
                            item =>
                            {
                                Assert.Null(item);
                            }
                        );
                    }
                );
            }
        }

        public class RunOverload
        {
            [Theory]
            [InlineData('f', 'o')]
            [InlineData(' ', '\t')]
            public void FirstChoiceOfTwoChars_ReturnsFirstChar(char c1, char c2)
            {
                // Given
                var item1 = (Expression)TextHelper.CreateStructure(c1);
                var item2 = (Expression)TextHelper.CreateStructure(c2);
                var expression = new AltExpression(new[] { item1, item2 });
                var input = TextHelper.CreateStructure(c1);

                // When
                var results = expression.Run(input).SelectMany(x => x!.Collapse());

                // Then
                Assert.Collection(results,
                    result =>
                    {
                        Assert.Equal(input, result);
                    }
                );
            }

            [Theory]
            [InlineData('b', 'a', 'r')]
            [InlineData(' ', '\r', '\n')]
            public void ThreeChoicesEmptyInput_ReturnsEmpty(char c1, char c2, char c3)
            {
                // Given
                var expression = new AltExpression(new[] {
                    (Expression)TextHelper.CreateStructure(c1),
                    (Expression)TextHelper.CreateStructure(c2),
                    (Expression)TextHelper.CreateStructure(c3),
                });
                var input = TextHelper.CreateStructure("");

                // When
                var results = expression.Run(input).SelectMany(x => x!.Collapse());

                // Then
                Assert.Empty(results);
            }

            [Theory]
            [InlineData('x', 'y', 'z')]
            [InlineData('J', ' ', 'H')]
            public void TwoChoicesNoMatch_ReturnsEmpty(char c1, char c2, char c)
            {
                // Given
                Assert.DoesNotContain(c, new[] { c1, c2 });
                var expression = new AltExpression(new[] {
                    (Expression)TextHelper.CreateStructure(c1),
                    (Expression)TextHelper.CreateStructure(c2)
                });
                var input = TextHelper.CreateStructure(c);

                // When
                var results = expression.Run(input).SelectMany(x => x!.Collapse());

                // Then
                Assert.Empty(results);
            }

            [Theory]
            [InlineData('b', 'a', 'r', ' ')]
            [InlineData('u', 'v', 'w', 'U')]
            [InlineData('J', 'o', 's', 'h')]
            public void ThreeChoicesNoMatch_ReturnsEmpty(char c1, char c2, char c3, char c)
            {
                // Given
                Assert.DoesNotContain(c, new[] { c1, c2, c3 });
                var expression = new AltExpression(new[] {
                    (Expression)TextHelper.CreateStructure(c1),
                    (Expression)TextHelper.CreateStructure(c2),
                    (Expression)TextHelper.CreateStructure(c3)
                });
                var input = TextHelper.CreateStructure(c);

                // When
                var results = expression.Run(input).SelectMany(x => x!.Collapse());

                // Then
                Assert.Empty(results);
            }

            [Theory]
            [InlineData('x')]
            [InlineData(' ')]
            [InlineData('?')]
            public void OneChoiceMatch_ReturnsChoice(char c)
            {
                // Given
                var item = (Expression)TextHelper.CreateStructure(c);
                var expression = new AltExpression(new[] { item });
                var input = TextHelper.CreateStructure(c);

                // When
                var results = expression.Run(input).SelectMany(x => x!.Collapse());

                // Then
                Assert.Collection(results,
                    result =>
                    {
                        Assert.Equal(input, result);
                    }
                );
            }

            [Theory]
            [InlineData('x', 'y')]
            [InlineData(' ', 'y')]
            [InlineData('?', 'y')]
            public void OneChoiceNoMatch_ReturnsEmpty(char c1, char c)
            {
                // Given
                Assert.NotEqual(c1, c);
                var expression = new AltExpression(new[] {
                    (Expression)TextHelper.CreateStructure(c1)
                });
                var input = TextHelper.CreateStructure(c);

                // When
                var results = expression.Run(input).SelectMany(x => x!.Collapse());

                // Then
                Assert.Empty(results);
            }

            [Theory]
            [InlineData('x')]
            [InlineData('\\')]
            [InlineData('\0')]
            public void OneChoiceEmptyInput_ReturnsEmpty(char c)
            {
                // Given
                var expression = new AltExpression(new[] {
                    (Expression)TextHelper.CreateStructure(c)
                });
                var input = TextHelper.CreateStructure("");

                // When
                var results = expression.Run(input).SelectMany(x => x!.Collapse());

                // Then
                Assert.Empty(results);
            }

            [Theory]
            [InlineData('x')]
            [InlineData('\\')]
            [InlineData('\0')]
            public void ZeroChoices_ReturnsEmpty(char c)
            {
                // Given
                var expression = new AltExpression(Enumerable.Empty<Expression>());
                var input = TextHelper.CreateStructure(c);

                // When
                var results = expression.Run(input).SelectMany(x => x!.Collapse());

                // Then
                Assert.Empty(results);
            }

            [Fact]
            public void ZeroChoicesEmptyInput_ReturnsEmpty()
            {
                // Given
                var expression = new AltExpression(Enumerable.Empty<Expression>());
                var input = TextHelper.CreateStructure("");

                // When
                var results = expression.Run(input).SelectMany(x => x!.Collapse());

                // Then
                Assert.Empty(results);
            }

            [Theory]
            [InlineData('M')]
            [InlineData('_')]
            public void DuplicateChoice_ReturnChoiceTwice(char c)
            {
                // Given
                var item1 = (Expression)TextHelper.CreateStructure(c);
                var item2 = (Expression)TextHelper.CreateStructure(c);
                var expression = new AltExpression(new[] { item1, item2 });
                var input = TextHelper.CreateStructure(c);

                // When
                var results = expression.Run(input).SelectMany(x => x!.Collapse());

                // Then
                Assert.Collection(results,
                    result =>
                    {
                        Assert.Equal(input, result);
                    },
                    result =>
                    {
                        Assert.Equal(input, result);
                    }
                );
            }

            [Theory]
            [InlineData('N')]
            [InlineData('-')]
            public void DuplicateChoiceEmptyInput_ReturnsEmpty(char c)
            {
                // Given
                var expression = new AltExpression(new[] {
                    (Expression)TextHelper.CreateStructure(c),
                    (Expression)TextHelper.CreateStructure(c)
                });
                var input = TextHelper.CreateStructure("");

                // When
                var results = expression.Run(input).SelectMany(x => x!.Collapse());

                // Then
                Assert.Empty(results);
            }
        }
    }
}