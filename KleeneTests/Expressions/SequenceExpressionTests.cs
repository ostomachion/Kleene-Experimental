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
                    new ConstantExpression<char>('c'),
                    null!
                });
            });
        }

        [Fact]
        public void NullInput_Throws()
        {
            // Given
            var expression = new SequenceExpression(new[] {
                new ConstantExpression<char>('x'),
                new ConstantExpression<char>('y')
            });
            IEnumerable<Structure> input = null!;

            // Then
            Assert.Throws<ArgumentNullException>(() =>
            {
                expression.Run(input, 0).ToList();
            });
        }

        [Theory]
        [InlineData('f', 'o')]
        [InlineData(' ', '\t')]
        public void TwoChars_ReturnsChars(char c1, char c2)
        {
            // Given
            var expression = new SequenceExpression(new[] {
                new ConstantExpression<char>(c1),
                new ConstantExpression<char>(c2)
            });
            var input = new[] { c1, c2 }.Select(x => new ConstantStructure<char>(x));

            // When
            var result = expression.Run(input, 0);

            // Then
            Assert.Collection(result,
                branch =>
                {
                    Assert.Collection(branch,
                        item => Assert.Equal(c1, ((ConstantStructure<char>)item).Value),
                        item => Assert.Equal(c2, ((ConstantStructure<char>)item).Value));
                });
        }

        [Theory]
        [InlineData('f', 'o')]
        [InlineData(' ', '\t')]
        public void TwoCharsWrongOrder_ReturnsNothing(char c1, char c2)
        {
            // Given
            Assert.NotEqual(c1, c2);
            var expression = new SequenceExpression(new[] {
                new ConstantExpression<char>(c1),
                new ConstantExpression<char>(c2)
            });
            var input = new[] { c2, c1 }.Select(x => new ConstantStructure<char>(x));

            // When
            var result = expression.Run(input, 0);

            // Then
            Assert.Empty(result);
        }

        [Theory]
        [InlineData('f', 'o')]
        [InlineData(' ', '\t')]
        public void TwoCharsEmptyInput_ReturnsEmpty(char c1, char c2)
        {
            // Given
            var expression = new SequenceExpression(new[] {
                new ConstantExpression<char>(c1),
                new ConstantExpression<char>(c2)
            });
            var input = new char[] { }.Select(x => new ConstantStructure<char>(x));

            // When
            var result = expression.Run(input, 0);

            // Then
            Assert.Empty(result);
        }

        [Theory]
        [InlineData('b', 'a', 'r')]
        [InlineData(' ', '\r', '\n')]
        public void ThreeChars_ReturnsChars(char c1, char c2, char c3)
        {
            // Given
            var expression = new SequenceExpression(new[] {
                new ConstantExpression<char>(c1),
                new ConstantExpression<char>(c2),
                new ConstantExpression<char>(c3),
            });
            var input = new[] { c1, c2, c3 }.Select(x => new ConstantStructure<char>(x));

            // When
            var result = expression.Run(input, 0);

            // Then
            Assert.Collection(result,
                branch =>
                {
                    Assert.Collection(branch,
                        item => Assert.Equal(c1, ((ConstantStructure<char>)item).Value),
                        item => Assert.Equal(c2, ((ConstantStructure<char>)item).Value),
                        item => Assert.Equal(c3, ((ConstantStructure<char>)item).Value));
                });
        }

        [Theory]
        [InlineData('b', 'a', 'r')]
        [InlineData(' ', '\r', '\n')]
        public void ThreeCharsWrongOrder_ReturnsChars(char c1, char c2, char c3)
        {
            // Given
            Assert.NotEqual(c2, c3);
            var expression = new SequenceExpression(new[] {
                new ConstantExpression<char>(c1),
                new ConstantExpression<char>(c2),
                new ConstantExpression<char>(c3),
            });
            var input = new[] { c1, c3, c2 }.Select(x => new ConstantStructure<char>(x));

            // When
            var result = expression.Run(input, 0);

            // Then
            Assert.Empty(result);
        }

        [Theory]
        [InlineData('b', 'a', 'r')]
        [InlineData(' ', '\r', '\n')]
        public void ThreeCharsEmptyInput_ReturnsEmpty(char c1, char c2, char c3)
        {
            // Given
            var expression = new SequenceExpression(new[] {
                new ConstantExpression<char>(c1),
                new ConstantExpression<char>(c2),
                new ConstantExpression<char>(c3),
            });
            var input = new char[] { }.Select(x => new ConstantStructure<char>(x));

            // When
            var result = expression.Run(input, 0);

            // Then
            Assert.Empty(result);
        }

        [Theory]
        [InlineData('x', 'y', 'z')]
        [InlineData('J', ' ', 'H')]
        public void TwoCharsPartialMatch_ReturnsEmpty(char c1, char c2, char c)
        {
            // Given
            Assert.NotEqual(c, c2);
            var expression = new SequenceExpression(new[] {
                new ConstantExpression<char>(c1),
                new ConstantExpression<char>(c2)
            });
            var input = new[] { c1, c }.Select(x => new ConstantStructure<char>(x));

            // When
            var result = expression.Run(input, 0);

            // Then
            Assert.Empty(result);
        }

        [Theory]
        [InlineData('x')]
        [InlineData(' ')]
        [InlineData('?')]
        public void OneCharMatch_ReturnsChar(char c)
        {
            // Given
            var expression = new SequenceExpression(new[] {
                new ConstantExpression<char>(c)
            });
            var input = new[] { c }.Select(x => new ConstantStructure<char>(x));

            // When
            var result = expression.Run(input, 0);

            // Then
            Assert.Collection(result,
                branch =>
                {
                    Assert.Collection(branch,
                        item => Assert.Equal(c, ((ConstantStructure<char>)item).Value));
                });
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
                new ConstantExpression<char>(c1)
            });
            var input = new[] { c }.Select(x => new ConstantStructure<char>(x));

            // When
            var result = expression.Run(input, 0);

            // Then
            Assert.Empty(result);
        }

        [Theory]
        [InlineData('x')]
        [InlineData('\\')]
        [InlineData('\0')]
        public void OneCharEmptyInput_ReturnsEmpty(char c)
        {
            // Given
            var expression = new SequenceExpression(new[] {
                new ConstantExpression<char>(c)
            });
            var input = new char[] { }.Select(x => new ConstantStructure<char>(x));

            // When
            var result = expression.Run(input, 0);

            // Then
            Assert.Empty(result);
        }

        [Theory]
        [InlineData('x')]
        [InlineData('\\')]
        [InlineData('\0')]
        public void ZeroChars_Returns(char c)
        {
            // Given
            var expression = new SequenceExpression(Enumerable.Empty<Expression>());
            var input = new char[] { c }.Select(x => new ConstantStructure<char>(x));

            // When
            var result = expression.Run(input, 0);

            // Then
            Assert.Collection(result,
                branch =>
                {
                    Assert.Empty(branch);
                });
        }

        [Fact]
        public void ZeroCharsEmptyInput_Returns()
        {
            // Given
            var expression = new SequenceExpression(Enumerable.Empty<Expression>());
            var input = new char[] { }.Select(x => new ConstantStructure<char>(x));

            // When
            var result = expression.Run(input, 0);

            // Then
            Assert.Collection(result,
                branch =>
                {
                    Assert.Empty(branch);
                });
        }

        [Theory]
        [InlineData('M')]
        [InlineData('_')]
        public void DuplicateChars_ReturnChars(char c)
        {
            // Given
            var expression = new SequenceExpression(new[] {
                new ConstantExpression<char>(c),
                new ConstantExpression<char>(c)
            });
            var input = new char[] { c, c }.Select(x => new ConstantStructure<char>(x));

            // When
            var result = expression.Run(input, 0);

            // Then
            Assert.Collection(result,
                branch =>
                {
                    Assert.Collection(branch,
                        item => Assert.Equal(c, ((ConstantStructure<char>)item).Value),
                        item => Assert.Equal(c, ((ConstantStructure<char>)item).Value));
                });
        }

        [Fact]
        public void Backtrack()
        {
            // Given
            var expression = new SequenceExpression(new[] {
                new AltExpression(new Expression [] {
                    new ConstantExpression<char>('x'),
                    new ConstantExpression<char>('x'),
                }),
                new AltExpression(new Expression [] {
                    new ConstantExpression<char>('y'),
                    new ConstantExpression<char>('y'),
                })
            });
            var input = new char[] { 'x', 'y' }.Select(x => new ConstantStructure<char>(x));

            // When
            var result = expression.Run(input, 0);

            // Then
            Assert.Collection(result,
                branch =>
                {
                    Assert.Collection(branch,
                        item => Assert.Equal('x', ((ConstantStructure<char>)item).Value),
                        item => Assert.Equal('y', ((ConstantStructure<char>)item).Value));
                },
                branch =>
                {
                    Assert.Collection(branch,
                        item => Assert.Equal('x', ((ConstantStructure<char>)item).Value),
                        item => Assert.Equal('y', ((ConstantStructure<char>)item).Value));
                },
                branch =>
                {
                    Assert.Collection(branch,
                        item => Assert.Equal('x', ((ConstantStructure<char>)item).Value),
                        item => Assert.Equal('y', ((ConstantStructure<char>)item).Value));
                },
                branch =>
                {
                    Assert.Collection(branch,
                        item => Assert.Equal('x', ((ConstantStructure<char>)item).Value),
                        item => Assert.Equal('y', ((ConstantStructure<char>)item).Value));
                });
        }
    }
}