using System;
using Xunit;
using Kleene;
using System.Linq;

namespace KleeneTests
{
    public class AltExpressionTests
    {
        [Theory]
        [InlineData('f', 'o')]
        [InlineData(' ', '\t')]
        public void FirstChoiceOfTwoChars_ReturnsFirstChar(char c1, char c2)
        {
            // Given
            var expression = new AltExpression<char, char>(new[] {
                new LiteralExpression<char>(c1),
                new LiteralExpression<char>(c2)
            });
            var input = new[] { c1 };

            // When
            var result = expression.Run(input);

            // Then
            Assert.Collection(result,
                branch => Assert.Collection(branch,
                    item => Assert.Equal(item, c1)));
        }

        [Theory]
        [InlineData('f', 'o')]
        [InlineData(' ', '\t')]
        public void SecondChoiceOfTwoChars_ReturnsSecondChar(char c1, char c2)
        {
            // Given
            var expression = new AltExpression<char, char>(new[] {
                new LiteralExpression<char>(c1),
                new LiteralExpression<char>(c2)
            });
            var input = new[] { c2 };

            // When
            var result = expression.Run(input);

            // Then
            Assert.Collection(result,
                branch => Assert.Collection(branch,
                    item => Assert.Equal(item, c2)));
        }

        [Theory]
        [InlineData('f', 'o')]
        [InlineData(' ', '\t')]
        public void TwoChoicesEmptyInput_ReturnsEmpty(char c1, char c2)
        {
            // Given
            var expression = new AltExpression<char, char>(new[] {
                new LiteralExpression<char>(c1),
                new LiteralExpression<char>(c2)
            });
            var input = new char[] { };

            // When
            var result = expression.Run(input);

            // Then
            Assert.Empty(result);
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
            var expression = new AltExpression<char, char>(new[] {
                new LiteralExpression<char>(c1),
                new LiteralExpression<char>(c2),
                new LiteralExpression<char>(c3),
            });
            var input = new[] { c };

            // When
            var result = expression.Run(input);

            // Then
            Assert.Collection(result,
                branch => Assert.Collection(branch,
                    item => Assert.Equal(item, c)));
        }

        [Theory]
        [InlineData('b', 'a', 'r')]
        [InlineData(' ', '\r', '\n')]
        public void ThreeChoicesEmptyInput_ReturnsEmpty(char c1, char c2, char c3)
        {
            // Given
            var expression = new AltExpression<char, char>(new[] {
                new LiteralExpression<char>(c1),
                new LiteralExpression<char>(c2),
                new LiteralExpression<char>(c3),
            });
            var input = new char[] { };

            // When
            var result = expression.Run(input);

            // Then
            Assert.Empty(result);
        }

        [Theory]
        [InlineData('x', 'y', 'z')]
        [InlineData('J', ' ', 'H')]
        public void TwoChoicesNoMatch_ReturnsEmpty(char c1, char c2, char c)
        {
            // Given
            Assert.DoesNotContain(c, new[] { c1, c2 });
            var expression = new AltExpression<char, char>(new[] {
                new LiteralExpression<char>(c1),
                new LiteralExpression<char>(c2)
            });
            var input = new[] { c };

            // When
            var result = expression.Run(input);

            // Then
            Assert.Empty(result);
        }

        [Theory]
        [InlineData('b', 'a', 'r', ' ')]
        [InlineData('u', 'v', 'w', 'U')]
        [InlineData('J', 'o', 's', 'h')]
        public void ThreeChoicesNoMatch_ReturnsEmpty(char c1, char c2, char c3, char c)
        {
            // Given
            Assert.DoesNotContain(c, new[] { c1, c2, c3 });
            var expression = new AltExpression<char, char>(new[] {
                new LiteralExpression<char>(c1),
                new LiteralExpression<char>(c2),
                new LiteralExpression<char>(c3)
            });
            var input = new[] { c };

            // When
            var result = expression.Run(input);

            // Then
            Assert.Empty(result);
        }

        [Theory]
        [InlineData('x')]
        [InlineData(' ')]
        [InlineData('?')]
        public void OneChoiceMatch_ReturnsChoice(char c)
        {
            // Given
            var expression = new AltExpression<char, char>(new[] {
                new LiteralExpression<char>(c)
            });
            var input = new[] { c };

            // When
            var result = expression.Run(input);

            // Then
            Assert.Collection(result,
                branch => Assert.Collection(branch,
                    item => Assert.Equal(item, c)));
        }

        [Theory]
        [InlineData('x', 'y')]
        [InlineData(' ', 'y')]
        [InlineData('?', 'y')]
        public void OneChoiceNoMatch_ReturnsEmpty(char c1, char c)
        {
            // Given
            Assert.NotEqual(c1, c);
            var expression = new AltExpression<char, char>(new[] {
                new LiteralExpression<char>(c1)
            });
            var input = new[] { c };

            // When
            var result = expression.Run(input);

            // Then
            Assert.Empty(result);
        }

        [Theory]
        [InlineData('x')]
        [InlineData('\\')]
        [InlineData('\0')]
        public void OneChoiceEmptyInput_ReturnsEmpty(char c)
        {
            // Given
            var expression = new AltExpression<char, char>(new[] {
                new LiteralExpression<char>(c)
            });
            var input = new char[] { };

            // When
            var result = expression.Run(input);

            // Then
            Assert.Empty(result);
        }

        [Theory]
        [InlineData('x')]
        [InlineData('\\')]
        [InlineData('\0')]
        public void ZeroChoices_ReturnsEmpty(char c)
        {
            // Given
            var expression = new AltExpression<char, char>(Enumerable.Empty<Expression<char, char>>());
            var input = new char[] { c };

            // When
            var result = expression.Run(input);

            // Then
            Assert.Empty(result);
        }

        [Fact]
        public void ZeroChoicesEmptyInput_ReturnsEmpty()
        {
            // Given
            var expression = new AltExpression<char, char>(Enumerable.Empty<Expression<char, char>>());
            var input = new char[] { };

            // When
            var result = expression.Run(input);

            // Then
            Assert.Empty(result);
        }

        [Theory]
        [InlineData('M')]
        [InlineData('_')]
        public void DuplicateChoice_ReturnChoiceTwice(char c)
        {
            // Given
            var expression = new AltExpression<char, char>(new [] {
                new LiteralExpression<char>(c),
                new LiteralExpression<char>(c)
            });
            var input = new char[] { c };

            // When
            var result = expression.Run(input);

            // Then
            Assert.Collection(result,
                branch => Assert.Collection(branch,
                    item => Assert.Equal(item, c)),
                branch => Assert.Collection(branch,
                    item => Assert.Equal(item, c)));
        }

        [Theory]
        [InlineData('N')]
        [InlineData('-')]
        public void DuplicateChoiceEmptyInput_ReturnsEmpty(char c)
        {
            // Given
            var expression = new AltExpression<char, char>(new [] {
                new LiteralExpression<char>(c),
                new LiteralExpression<char>(c)
            });
            var input = new char[] { };

            // When
            var result = expression.Run(input);

            // Then
            Assert.Empty(result);
        }
    }
}
