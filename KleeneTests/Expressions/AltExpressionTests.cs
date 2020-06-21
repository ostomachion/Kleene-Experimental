using System;
using Xunit;
using Kleene;
using System.Linq;
using System.Collections.Generic;

namespace KleeneTests
{
    public class AltExpressionTests
    {
        [Fact]
        public void NullExpressions_Throws() {
            Assert.Throws<ArgumentNullException>(() => {
                new AltExpression(null!);
            });
        }

        [Fact]
        public void NullExpression_Throws() {
            Assert.Throws<ArgumentException>(() => {
                new AltExpression(new Expression[] {
                    null!
                });
            });
        }

        [Fact]
        public void NullAndNotNullExpression_Throws() {
            Assert.Throws<ArgumentException>(() => {
                new AltExpression(new Expression[] {
                    new ConstantExpression<char>('c'),
                    null!
                });
            });
        }

        [Fact]
        public void NullInput_Throws() {
            // Given
            var expression = new AltExpression(new[] {
                new ConstantExpression<char>('x'),
                new ConstantExpression<char>('y')
            });
            IEnumerable<Structure> input = null!;

            // Then
            Assert.Throws<ArgumentNullException>(() => {
                expression.Run(input, 0).ToList();
            });
        }

        [Theory]
        [InlineData('f', 'o')]
        [InlineData(' ', '\t')]
        public void FirstChoiceOfTwoChars_ReturnsFirstChar(char c1, char c2)
        {
            // Given
            var item1 = new ConstantExpression<char>(c1);
            var item2 = new ConstantExpression<char>(c2);
            var expression = new AltExpression(new[] { item1, item2 });
            var input = new[] { c1 }.Select(x => new ConstantStructure<char>(x)).ToArray();

            // When
            var results = expression.Run(input, 0);

            // Then
            Assert.Collection(results,
                result => {
                    Assert.Equal(input, result.Input);
                    Assert.Equal(0, result.Index);
                    Assert.Equal(1, result.Length);
                    Assert.Equal(expression, result.Source);
                    Assert.Collection(result.Children,
                        result => {
                            Assert.Equal(input, result.Input);
                            Assert.Equal(0, result.Index);
                            Assert.Equal(1, result.Length);
                            Assert.Equal(item1, result.Source);
                            Assert.Empty(result.Children);
                        }
                    );
                }
            );
        }

        [Theory]
        [InlineData('f', 'o')]
        [InlineData(' ', '\t')]
        public void SecondChoiceOfTwoChars_ReturnsSecondChar(char c1, char c2)
        {
            // Given
            var item1 = new ConstantExpression<char>(c1);
            var item2 = new ConstantExpression<char>(c2);
            var expression = new AltExpression(new[] { item1, item2 });
            var input = new[] { c2 }.Select(x => new ConstantStructure<char>(x)).ToArray();

            // When
            var results = expression.Run(input, 0);

            // Then
            Assert.Collection(results,
                result => {
                    Assert.Equal(input, result.Input);
                    Assert.Equal(0, result.Index);
                    Assert.Equal(1, result.Length);
                    Assert.Equal(expression, result.Source);
                    Assert.Collection(result.Children,
                        result => {
                            Assert.Equal(input, result.Input);
                            Assert.Equal(0, result.Index);
                            Assert.Equal(1, result.Length);
                            Assert.Equal(item2, result.Source);
                            Assert.Empty(result.Children);
                        }
                    );
                }
            );
        }

        [Theory]
        [InlineData('f', 'o')]
        [InlineData(' ', '\t')]
        public void TwoChoicesEmptyInput_ReturnsEmpty(char c1, char c2)
        {
            // Given
            var expression = new AltExpression(new[] {
                new ConstantExpression<char>(c1),
                new ConstantExpression<char>(c2)
            });
            var input = new char[] { }.Select(x => new ConstantStructure<char>(x)).ToArray();

            // When
            var results = expression.Run(input, 0);

            // Then
            Assert.Empty(results);
        }

        [Theory]
        [InlineData('b', 'a', 'r', 'b')]
        [InlineData('b', 'a', 'r', 'a')]
        [InlineData('b', 'a', 'r', 'r')]
        [InlineData(' ', '\r', '\n', ' ')]
        [InlineData(' ', '\r', '\n', '\r')]
        [InlineData(' ', '\r', '\n', '\n')]
        public void ThreeChoices_ReturnsMatchingChoice(char c1, char c2, char c3, char c)
        {
            // Given
            Assert.Contains(c, new[] { c1, c2, c3 });
            var expression = new AltExpression(new[] {
                new ConstantExpression<char>(c1),
                new ConstantExpression<char>(c2),
                new ConstantExpression<char>(c3),
            });
            var input = new[] { c }.Select(x => new ConstantStructure<char>(x)).ToArray();

            // When
            var results = expression.Run(input, 0);

            // Then
            Assert.Collection(results,
                result => {
                    Assert.Equal(input, result.Input);
                    Assert.Equal(0, result.Index);
                    Assert.Equal(1, result.Length);
                    Assert.Equal(expression, result.Source);
                    Assert.Collection(result.Children,
                        result => {
                            Assert.Equal(input, result.Input);
                            Assert.Equal(0, result.Index);
                            Assert.Equal(1, result.Length);
                            Assert.Empty(result.Children);
                        }
                    );
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
                new ConstantExpression<char>(c1),
                new ConstantExpression<char>(c2),
                new ConstantExpression<char>(c3),
            });
            var input = new char[] { }.Select(x => new ConstantStructure<char>(x)).ToArray();

            // When
            var results = expression.Run(input, 0);

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
                new ConstantExpression<char>(c1),
                new ConstantExpression<char>(c2)
            });
            var input = new[] { c }.Select(x => new ConstantStructure<char>(x)).ToArray();

            // When
            var results = expression.Run(input, 0);

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
                new ConstantExpression<char>(c1),
                new ConstantExpression<char>(c2),
                new ConstantExpression<char>(c3)
            });
            var input = new[] { c }.Select(x => new ConstantStructure<char>(x)).ToArray();

            // When
            var results = expression.Run(input, 0);

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
            var item = new ConstantExpression<char>(c);
            var expression = new AltExpression(new[] { item });
            var input = new[] { c }.Select(x => new ConstantStructure<char>(x)).ToArray();

            // When
            var results = expression.Run(input, 0);

            // Then
            Assert.Collection(results,
                result => {
                    Assert.Equal(input, result.Input);
                    Assert.Equal(0, result.Index);
                    Assert.Equal(1, result.Length);
                    Assert.Equal(expression, result.Source);
                    Assert.Collection(result.Children,
                        result => {
                            Assert.Equal(input, result.Input);
                            Assert.Equal(0, result.Index);
                            Assert.Equal(1, result.Length);
                            Assert.Equal(item, result.Source);
                            Assert.Empty(result.Children);
                        }
                    );
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
                new ConstantExpression<char>(c1)
            });
            var input = new[] { c }.Select(x => new ConstantStructure<char>(x)).ToArray();

            // When
            var results = expression.Run(input, 0);

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
                new ConstantExpression<char>(c)
            });
            var input = new char[] { }.Select(x => new ConstantStructure<char>(x)).ToArray();

            // When
            var results = expression.Run(input, 0);

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
            var input = new char[] { c }.Select(x => new ConstantStructure<char>(x)).ToArray();

            // When
            var results = expression.Run(input, 0);

            // Then
            Assert.Empty(results);
        }

        [Fact]
        public void ZeroChoicesEmptyInput_ReturnsEmpty()
        {
            // Given
            var expression = new AltExpression(Enumerable.Empty<Expression>());
            var input = new char[] { }.Select(x => new ConstantStructure<char>(x)).ToArray();

            // When
            var results = expression.Run(input, 0);

            // Then
            Assert.Empty(results);
        }

        [Theory]
        [InlineData('M')]
        [InlineData('_')]
        public void DuplicateChoice_ReturnChoiceTwice(char c)
        {
            // Given
            var item1 = new ConstantExpression<char>(c);
            var item2 = new ConstantExpression<char>(c);
            var expression = new AltExpression(new [] { item1, item2 });
            var input = new char[] { c }.Select(x => new ConstantStructure<char>(x)).ToArray();

            // When
            var results = expression.Run(input, 0);

            // Then
            Assert.Collection(results,
                result => {
                    Assert.Equal(input, result.Input);
                    Assert.Equal(0, result.Index);
                    Assert.Equal(1, result.Length);
                    Assert.Equal(expression, result.Source);
                    Assert.Collection(result.Children,
                        result => {
                            Assert.Equal(input, result.Input);
                            Assert.Equal(0, result.Index);
                            Assert.Equal(1, result.Length);
                            Assert.Equal(item1, result.Source);
                            Assert.Empty(result.Children);
                        }
                    );
                },
                result => {
                    Assert.Equal(input, result.Input);
                    Assert.Equal(0, result.Index);
                    Assert.Equal(1, result.Length);
                    Assert.Equal(expression, result.Source);
                    Assert.Collection(result.Children,
                        result => {
                            Assert.Equal(input, result.Input);
                            Assert.Equal(0, result.Index);
                            Assert.Equal(1, result.Length);
                            Assert.Equal(item2, result.Source);
                            Assert.Empty(result.Children);
                        }
                    );
                }
            );
        }

        [Theory]
        [InlineData('N')]
        [InlineData('-')]
        public void DuplicateChoiceEmptyInput_ReturnsEmpty(char c)
        {
            // Given
            var expression = new AltExpression(new [] {
                new ConstantExpression<char>(c),
                new ConstantExpression<char>(c)
            });
            var input = new char[] { }.Select(x => new ConstantStructure<char>(x)).ToArray();

            // When
            var results = expression.Run(input, 0);

            // Then
            Assert.Empty(results);
        }
    }
}