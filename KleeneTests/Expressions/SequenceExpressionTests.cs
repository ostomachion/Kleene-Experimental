using System;
using Xunit;
using Kleene;
using System.Linq;
using System.Collections.Generic;

namespace KleeneTests
{
    public class SequenceExpressionTests
    {
        [Fact]
        public void NullExpressions_Throws()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new SequenceExpression(null!);
            });
        }

        [Fact]
        public void NullExpression_Throws()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                new SequenceExpression(new Expression[] {
                    null!
                });
            });
        }

        [Fact]
        public void NullAndNotNullExpression_Throws()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                new SequenceExpression(new Expression[] {
                    SequenceExpression.Empty,
                    null!
                });
            });
        }

        [Fact]
        public void ZeroChoices_RetrurnsEmpty()
        {
            // Given
            var expression = new SequenceExpression(Enumerable.Empty<Expression>());

            // When
            var results = expression.Run();

            // Then
            Assert.Collection(results,
                item =>
                {
                    Assert.Null(item);
                }
            );
        }

        [Fact]
        public void OneChoice_ReturnsChoice()
        {
            // Given
            var expression = new SequenceExpression(EnumerableExt.Yield(new StructureExpression("foo")));

            // When
            var results = expression.Run();

            // Then
            Assert.Collection(results,
                result =>
                {
                    Assert.NotNull(result);
                    Assert.Equal("foo", result!.Name);
                    Assert.Collection(result.FirstChild,
                        item => {
                            Assert.Null(item);
                        }
                    );
                    Assert.Collection(result.NextSibling,
                        item => {
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
            var expression = new SequenceExpression(new[] {
                new StructureExpression("foo"),
                new StructureExpression("bar")
            });

            // When
            var results = expression.Run();

            // Then
            Assert.Collection(results,
                result =>
                {
                    Assert.NotNull(result);
                    Assert.Equal("foo", result!.Name);
                    Assert.Collection(result.FirstChild,
                        item => {
                            Assert.Null(item);
                        }
                    );
                    Assert.Collection(result.NextSibling,
                        item => {
                            Assert.NotNull(item);
                            Assert.Equal("bar", item!.Name);
                            Assert.Collection(item.FirstChild,
                                item => {
                                    Assert.Null(item);
                                }
                            );
                            Assert.Collection(item.NextSibling,
                                item => {
                                    Assert.Null(item);
                                }
                            );
                        }
                    );
                }
            );
        }

        [Theory]
        [InlineData('f', 'o')]
        [InlineData(' ', '\t')]
        public void TwoChars_ReturnsChars(char c1, char c2)
        {
            // Given
            var item1 = (Expression)TextHelper.CreateStructure(c1);
            var item2 = (Expression)TextHelper.CreateStructure(c2);
            var expression = new SequenceExpression(new[] { item1, item2 });
            var input = TextHelper.CreateStructure(new string(new [] { c1, c2 }));
            

            // When
            var results = expression.Run(input).SelectMany(x => x?.Collapse() ?? EnumerableExt.Yield<Structure?>(null));

            // Then
            Assert.Collection(results,
                result => {
                    Assert.Equal(input, result);
                }
            );
        }

        [Theory]
        [InlineData('f', 'o')]
        [InlineData(' ', '\t')]
        public void TwoCharsWrongOrder_ReturnsNothing(char c1, char c2)
        {
            // Given
            Assert.NotEqual(c1, c2);
            var expression = new SequenceExpression(new[] {
                (Expression)TextHelper.CreateStructure(c1),
                (Expression)TextHelper.CreateStructure(c2)
            });
            var input = TextHelper.CreateStructure(new string(new [] { c2, c1 }));

            // When
            var results = expression.Run(input).SelectMany(x => x?.Collapse() ?? EnumerableExt.Yield<Structure?>(null));

            // Then
            Assert.Empty(results);
        }

        [Theory]
        [InlineData('f', 'o')]
        [InlineData(' ', '\t')]
        public void TwoCharsEmptyInput_ReturnsEmpty(char c1, char c2)
        {
            // Given
            var expression = new SequenceExpression(new[] {
                (Expression)TextHelper.CreateStructure(c1),
                (Expression)TextHelper.CreateStructure(c2)
            });
            var input = TextHelper.CreateStructure("");

            // When
            var results = expression.Run(input).SelectMany(x => x?.Collapse() ?? EnumerableExt.Yield<Structure?>(null));

            // Then
            Assert.Empty(results);
        }

        [Theory]
        [InlineData('b', 'a', 'r')]
        [InlineData(' ', '\r', '\n')]
        public void ThreeChars_ReturnsChars(char c1, char c2, char c3)
        {
            // Given
            var item1 = (Expression)TextHelper.CreateStructure(c1);
            var item2 = (Expression)TextHelper.CreateStructure(c2);
            var item3 = (Expression)TextHelper.CreateStructure(c3);
            var expression = new SequenceExpression(new[] { item1, item2, item3 });
            var input = TextHelper.CreateStructure(new string(new [] { c1, c2, c3 }));

            // When
            var results = expression.Run(input).SelectMany(x => x?.Collapse() ?? EnumerableExt.Yield<Structure?>(null));

            // Then
            Assert.Collection(results,
                result => {
                    Assert.Equal(input, result);
                }
            );
        }

        [Theory]
        [InlineData('b', 'a', 'r')]
        [InlineData(' ', '\r', '\n')]
        public void ThreeCharsWrongOrder_ReturnsEmpty(char c1, char c2, char c3)
        {
            // Given
            Assert.NotEqual(c2, c3);
            var expression = new SequenceExpression(new[] {
                (Expression)TextHelper.CreateStructure(c1),
                (Expression)TextHelper.CreateStructure(c2),
                (Expression)TextHelper.CreateStructure(c3),
            });
            var input = TextHelper.CreateStructure(new string(new [] { c1, c3, c2 }));

            // When
            var results = expression.Run(input).SelectMany(x => x?.Collapse() ?? EnumerableExt.Yield<Structure?>(null));

            // Then
            Assert.Empty(results);
        }

        [Theory]
        [InlineData('b', 'a', 'r')]
        [InlineData(' ', '\r', '\n')]
        public void ThreeCharsEmptyInput_ReturnsEmpty(char c1, char c2, char c3)
        {
            // Given
            var expression = new SequenceExpression(new[] {
                (Expression)TextHelper.CreateStructure(c1),
                (Expression)TextHelper.CreateStructure(c2),
                (Expression)TextHelper.CreateStructure(c3),
            });
            var input = TextHelper.CreateStructure("");

            // When
            var results = expression.Run(input).SelectMany(x => x?.Collapse() ?? EnumerableExt.Yield<Structure?>(null));

            // Then
            Assert.Empty(results);
        }

        [Theory]
        [InlineData('x', 'y', 'z')]
        [InlineData('J', ' ', 'H')]
        public void TwoCharsPartialMatch_ReturnsEmpty(char c1, char c2, char c)
        {
            // Given
            Assert.NotEqual(c, c2);
            var expression = new SequenceExpression(new[] {
                (Expression)TextHelper.CreateStructure(c1),
                (Expression)TextHelper.CreateStructure(c2)
            });
            var input = TextHelper.CreateStructure(new string(new [] { c1, c }));

            // When
            var ressult = expression.Run(input).SelectMany(x => x?.Collapse() ?? EnumerableExt.Yield<Structure?>(null));

            // Then
            Assert.Empty(ressult);
        }

        [Theory]
        [InlineData('x')]
        [InlineData(' ')]
        [InlineData('?')]
        public void OneCharMatch_ReturnsChar(char c)
        {
            // Given
            var item = (Expression)TextHelper.CreateStructure(c);
            var expression = new SequenceExpression(new[] { item });
            var input = TextHelper.CreateStructure(new string(new [] { c }));

            // When
            var results = expression.Run(input).SelectMany(x => x?.Collapse() ?? EnumerableExt.Yield<Structure?>(null));

            // Then
            Assert.Collection(results,
                result => {
                    Assert.Equal(input, result);
                }
            );
        }

        [Theory]
        [InlineData('x', 'y')]
        [InlineData(' ', 'y')]
        [InlineData('?', 'y')]
        public void OneCharNoMatch_ReturnsEmpty(char c1, char c)
        {
            // Given
            Assert.NotEqual(c1, c);
            var expression = new SequenceExpression(new[] {
                (Expression)TextHelper.CreateStructure(c1)
            });
            var input = TextHelper.CreateStructure(new string(new [] { c }));

            // When
            var results = expression.Run(input).SelectMany(x => x?.Collapse() ?? EnumerableExt.Yield<Structure?>(null));

            // Then
            Assert.Empty(results);
        }

        [Theory]
        [InlineData('x')]
        [InlineData('\\')]
        [InlineData('\0')]
        public void OneCharEmptyInput_ReturnsEmpty(char c)
        {
            // Given
            var expression = new SequenceExpression(new[] {
                (Expression)TextHelper.CreateStructure(c)
            });
            var input = TextHelper.CreateStructure("");

            // When
            var results = expression.Run(input).SelectMany(x => x?.Collapse() ?? EnumerableExt.Yield<Structure?>(null));

            // Then
            Assert.Empty(results);
        }

        [Fact]
        public void ZeroCharsEmptyInput_Returns()
        {
            // Given
            var expression = new SequenceExpression(Enumerable.Empty<Expression>());
            var input = TextHelper.CreateStructure("");

            // When
            var results = expression.Run(input).SelectMany(x => x?.Collapse() ?? EnumerableExt.Yield<Structure?>(null));

            // Then
            Assert.Collection(results,
                result => {
                    Assert.Equal(input, result);
                }
            );
        }

        [Theory]
        [InlineData('M')]
        [InlineData('_')]
        public void DuplicateChars_ReturnChars(char c)
        {
            // Given
            var item1 = (Expression)TextHelper.CreateStructure(c);
            var item2 = (Expression)TextHelper.CreateStructure(c);
            var expression = new SequenceExpression(new[] { item1, item2 });
            var input = TextHelper.CreateStructure(new string(new [] { c, c }));

            // When
            var results = expression.Run(input).SelectMany(x => x?.Collapse() ?? EnumerableExt.Yield<Structure?>(null));

            // Then
            Assert.Collection(results,
                result => {
                    Assert.Equal(input, result);
                }
            );
        }

        [Fact]
        public void Backtrack()
        {
            // Given
            var item1_1 = (Expression)TextHelper.CreateStructure('x');
            var item1_2 = (Expression)TextHelper.CreateStructure('x');
            var item2_1 = (Expression)TextHelper.CreateStructure('y');
            var item2_2 = (Expression)TextHelper.CreateStructure('y');
            var item1 = new AltExpression(new Expression[] { item1_1, item1_2 });
            var item2 = new AltExpression(new Expression[] { item2_1, item2_2 });
            var expression = new SequenceExpression(new[] { item1, item2 });
            var input = TextHelper.CreateStructure(new string(new [] { 'x', 'y' }));

            // When
            var results = expression.Run(input).SelectMany(x => x?.Collapse() ?? EnumerableExt.Yield<Structure?>(null));

            // Then
            Assert.Collection(results,
                result => {
                    Assert.Equal(input, result);
                },
                result => {
                    Assert.Equal(input, result);
                },
                result => {
                    Assert.Equal(input, result);
                },
                result => {
                    Assert.Equal(input, result);
                }
            );
        }
    }
}