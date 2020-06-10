using System;
using Xunit;
using Kleene;
using System.Linq;
using System.Collections.Generic;

namespace KleeneTests
{
    public class RepExpressionTests
    {
        [Fact]
        public void GreedyDefaultOrder()
        {
            // Given
            var expression = new RepExpression<char, char>(
                new LiteralExpression<char>('x'));

            // Then
            Assert.Equal(Order.Greedy, expression.Order);
        }

        [Fact]
        public void ZeroDefaultMin()
        {
            // Given
            var expression = new RepExpression<char, char>(
                new LiteralExpression<char>('x'));

            // Then
            Assert.Equal(0, expression.Min);
        }

        [Fact]
        public void MinusOneDefaultMax()
        {
            // Given
            var expression = new RepExpression<char, char>(
                new LiteralExpression<char>('x'));

            // Then
            Assert.Equal(-1, expression.Max);
        }

        [Fact]
        public void NullExpression_Throws()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new RepExpression<char, char>(null!);
            });
        }

        [Fact]
        public void NullInput_Throws()
        {
            // Given
            var expression = new RepExpression<char, char>(
                new LiteralExpression<char>('x'));
            IEnumerable<char> input = null!;

            // Then
            Assert.Throws<ArgumentNullException>(() =>
            {
                expression.Run(input).ToList();
            });
        }

        [Theory]
        [InlineData('x')]
        [InlineData(' ')]
        public void SimpleRepetition_ReturnsChars(char c)
        {
            // Given
            var expression = new RepExpression<char, char>(
                new LiteralExpression<char>(c));
            var input = new[] { c, c, c, c };

            // When
            var result = expression.Run(input);

            // Then
            Assert.Collection(result,
                branch =>
                {
                    Assert.Equal(0, branch.Offset);
                    Assert.Equal(4, branch.Length);
                    Assert.Collection(branch.Output,
                        item => Assert.Equal(c, item),
                        item => Assert.Equal(c, item),
                        item => Assert.Equal(c, item),
                        item => Assert.Equal(c, item));
                },
                branch =>
                {
                    Assert.Equal(0, branch.Offset);
                    Assert.Equal(3, branch.Length);
                    Assert.Collection(branch.Output,
                        item => Assert.Equal(c, item),
                        item => Assert.Equal(c, item),
                        item => Assert.Equal(c, item));
                },
                branch =>
                {
                    Assert.Equal(0, branch.Offset);
                    Assert.Equal(2, branch.Length);
                    Assert.Collection(branch.Output,
                        item => Assert.Equal(c, item),
                        item => Assert.Equal(c, item));
                },
                branch =>
                {
                    Assert.Equal(0, branch.Offset);
                    Assert.Equal(1, branch.Length);
                    Assert.Collection(branch.Output,
                        item => Assert.Equal(c, item));
                },
                branch =>
                {
                    Assert.Equal(0, branch.Offset);
                    Assert.Equal(0, branch.Length);
                    Assert.Empty(branch.Output);
                });
        }

        [Theory]
        [InlineData('x')]
        [InlineData(' ')]
        public void SimpleLazyRepetition_ReturnsChars(char c)
        {
            // Given
            var expression = new RepExpression<char, char>(
                new LiteralExpression<char>(c),
                order: Order.Lazy);
            var input = new[] { c, c, c, c };

            // When
            var result = expression.Run(input);

            // Then
            Assert.Collection(result,
                branch =>
                {
                    Assert.Equal(0, branch.Offset);
                    Assert.Equal(0, branch.Length);
                    Assert.Empty(branch.Output);
                },
                branch =>
                {
                    Assert.Equal(0, branch.Offset);
                    Assert.Equal(1, branch.Length);
                    Assert.Collection(branch.Output,
                        item => Assert.Equal(c, item));
                },
                branch =>
                {
                    Assert.Equal(0, branch.Offset);
                    Assert.Equal(2, branch.Length);
                    Assert.Collection(branch.Output,
                        item => Assert.Equal(c, item),
                        item => Assert.Equal(c, item));
                },
                branch =>
                {
                    Assert.Equal(0, branch.Offset);
                    Assert.Equal(3, branch.Length);
                    Assert.Collection(branch.Output,
                        item => Assert.Equal(c, item),
                        item => Assert.Equal(c, item),
                        item => Assert.Equal(c, item));
                },
                branch =>
                {
                    Assert.Equal(0, branch.Offset);
                    Assert.Equal(4, branch.Length);
                    Assert.Collection(branch.Output,
                        item => Assert.Equal(c, item),
                        item => Assert.Equal(c, item),
                        item => Assert.Equal(c, item),
                        item => Assert.Equal(c, item));
                });
        }

        [Theory]
        [InlineData('x')]
        [InlineData(' ')]
        public void SimpleMinRepetition_ReturnsChars(char c)
        {
            // Given
            var expression = new RepExpression<char, char>(
                new LiteralExpression<char>(c),
                min: 2);
            var input = new[] { c, c, c, c };

            // When
            var result = expression.Run(input);

            // Then
            Assert.Collection(result,
                branch =>
                {
                    Assert.Equal(0, branch.Offset);
                    Assert.Equal(4, branch.Length);
                    Assert.Collection(branch.Output,
                        item => Assert.Equal(c, item),
                        item => Assert.Equal(c, item),
                        item => Assert.Equal(c, item),
                        item => Assert.Equal(c, item));
                },
                branch =>
                {
                    Assert.Equal(0, branch.Offset);
                    Assert.Equal(3, branch.Length);
                    Assert.Collection(branch.Output,
                        item => Assert.Equal(c, item),
                        item => Assert.Equal(c, item),
                        item => Assert.Equal(c, item));
                },
                branch =>
                {
                    Assert.Equal(0, branch.Offset);
                    Assert.Equal(2, branch.Length);
                    Assert.Collection(branch.Output,
                        item => Assert.Equal(c, item),
                        item => Assert.Equal(c, item));
                });
        }

        [Theory]
        [InlineData('x')]
        [InlineData(' ')]
        public void SimpleLazyMinRepetition_ReturnsChars(char c)
        {
            // Given
            var expression = new RepExpression<char, char>(
                new LiteralExpression<char>(c),
                order: Order.Lazy,
                min: 2);
            var input = new[] { c, c, c, c };

            // When
            var result = expression.Run(input);

            // Then
            Assert.Collection(result,
                branch =>
                {
                    Assert.Equal(0, branch.Offset);
                    Assert.Equal(2, branch.Length);
                    Assert.Collection(branch.Output,
                        item => Assert.Equal(c, item),
                        item => Assert.Equal(c, item));
                },
                branch =>
                {
                    Assert.Equal(0, branch.Offset);
                    Assert.Equal(3, branch.Length);
                    Assert.Collection(branch.Output,
                        item => Assert.Equal(c, item),
                        item => Assert.Equal(c, item),
                        item => Assert.Equal(c, item));
                },
                branch =>
                {
                    Assert.Equal(0, branch.Offset);
                    Assert.Equal(4, branch.Length);
                    Assert.Collection(branch.Output,
                        item => Assert.Equal(c, item),
                        item => Assert.Equal(c, item),
                        item => Assert.Equal(c, item),
                        item => Assert.Equal(c, item));
                });
        }

        [Theory]
        [InlineData('x')]
        [InlineData(' ')]
        public void SimpleMaxRepetition_ReturnsChars(char c)
        {
            // Given
            var expression = new RepExpression<char, char>(
                new LiteralExpression<char>(c),
                max: 3);
            var input = new[] { c, c, c, c };

            // When
            var result = expression.Run(input);

            // Then
            Assert.Collection(result,
                branch =>
                {
                    Assert.Equal(0, branch.Offset);
                    Assert.Equal(3, branch.Length);
                    Assert.Collection(branch.Output,
                        item => Assert.Equal(c, item),
                        item => Assert.Equal(c, item),
                        item => Assert.Equal(c, item));
                },
                branch =>
                {
                    Assert.Equal(0, branch.Offset);
                    Assert.Equal(2, branch.Length);
                    Assert.Collection(branch.Output,
                        item => Assert.Equal(c, item),
                        item => Assert.Equal(c, item));
                },
                branch =>
                {
                    Assert.Equal(0, branch.Offset);
                    Assert.Equal(1, branch.Length);
                    Assert.Collection(branch.Output,
                        item => Assert.Equal(c, item));
                },
                branch =>
                {
                    Assert.Equal(0, branch.Offset);
                    Assert.Equal(0, branch.Length);
                    Assert.Empty(branch.Output);
                });
        }

        [Theory]
        [InlineData('x')]
        [InlineData(' ')]
        public void SimpleLazyMaxRepetition_ReturnsChars(char c)
        {
            // Given
            var expression = new RepExpression<char, char>(
                new LiteralExpression<char>(c),
                order: Order.Lazy,
                max: 2);
            var input = new[] { c, c, c, c };

            // When
            var result = expression.Run(input);

            // Then
            Assert.Collection(result,
                branch =>
                {
                    Assert.Equal(0, branch.Offset);
                    Assert.Equal(0, branch.Length);
                    Assert.Empty(branch.Output);
                },
                branch => {
                    Assert.Equal(0, branch.Offset);
                    Assert.Equal(1, branch.Length);
                    Assert.Collection(branch.Output,
                        item => Assert.Equal(c, item));
                },
                branch => {
                    Assert.Equal(0, branch.Offset);
                    Assert.Equal(2, branch.Length);
                    Assert.Collection(branch.Output,
                        item => Assert.Equal(c, item),
                        item => Assert.Equal(c, item));
                });
        }

        [Theory]
        [InlineData('x')]
        [InlineData(' ')]
        public void SimpleMinMaxRepetition_ReturnsChars(char c)
        {
            // Given
            var expression = new RepExpression<char, char>(
                new LiteralExpression<char>(c),
                min: 2,
                max: 3);
            var input = new[] { c, c, c, c };

            // When
            var result = expression.Run(input);

            // Then
            Assert.Collection(result,
                branch =>
                {
                    Assert.Equal(0, branch.Offset);
                    Assert.Equal(3, branch.Length);
                    Assert.Collection(branch.Output,
                        item => Assert.Equal(c, item),
                        item => Assert.Equal(c, item),
                        item => Assert.Equal(c, item));
                },
                branch =>
                {
                    Assert.Equal(0, branch.Offset);
                    Assert.Equal(2, branch.Length);
                    Assert.Collection(branch.Output,
                        item => Assert.Equal(c, item),
                        item => Assert.Equal(c, item));
                });
        }

        [Theory]
        [InlineData('x')]
        [InlineData(' ')]
        public void SimpleLazyMinMaxRepetition_ReturnsChars(char c)
        {
            // Given
            var expression = new RepExpression<char, char>(
                new LiteralExpression<char>(c),
                order: Order.Lazy,
                min: 2,
                max: 3);
            var input = new[] { c, c, c, c };

            // When
            var result = expression.Run(input);

            // Then
            Assert.Collection(result,
                branch =>
                {
                    Assert.Equal(0, branch.Offset);
                    Assert.Equal(2, branch.Length);
                    Assert.Collection(branch.Output,
                        item => Assert.Equal(c, item),
                        item => Assert.Equal(c, item));
                },
                branch =>
                {
                    Assert.Equal(0, branch.Offset);
                    Assert.Equal(3, branch.Length);
                    Assert.Collection(branch.Output,
                        item => Assert.Equal(c, item),
                        item => Assert.Equal(c, item),
                        item => Assert.Equal(c, item));
                });
        }

        [Theory]
        [InlineData('x')]
        [InlineData(' ')]
        public void MaxZero_Returns(char c)
        {
            // Given
            var expression = new RepExpression<char, char>(
                new LiteralExpression<char>(c),
                max: 0);
            var input = new[] { c, c, c, c };

            // When
            var result = expression.Run(input);

            // Then
            Assert.Collection(result,
                branch =>
                {
                    Assert.Equal(0, branch.Offset);
                    Assert.Equal(0, branch.Length);
                    Assert.Empty(branch.Output);
                });
        }

        [Theory]
        [InlineData('x')]
        [InlineData(' ')]
        public void LazyMaxZero_ReturnsChars(char c)
        {
            // Given
            var expression = new RepExpression<char, char>(
                new LiteralExpression<char>(c),
                order: Order.Lazy,
                max: 0);
            var input = new[] { c, c, c, c };

            // When
            var result = expression.Run(input);

            // Then
            Assert.Collection(result,
                branch =>
                {
                    Assert.Equal(0, branch.Offset);
                    Assert.Equal(0, branch.Length);
                    Assert.Empty(branch.Output);
                });
        }

        [Theory]
        [InlineData('x', 'y')]
        [InlineData(' ', '\t')]
        public void MinNoMatch(char c1, char c2)
        {
            // Given
            Assert.NotEqual(c1, c2);
            var expression = new RepExpression<char, char>(
                new LiteralExpression<char>(c1),
                min: 1);
            var input = new[] { c2, c1, c1, c1, c1 };

            // When
            var result = expression.Run(input);

            // Then
            Assert.Empty(result);
        }

        [Theory]
        [InlineData('x', 'y')]
        [InlineData(' ', '\t')]
        public void LazyMinNoMatch(char c1, char c2)
        {
            // Given
            Assert.NotEqual(c1, c2);
            var expression = new RepExpression<char, char>(
                new LiteralExpression<char>(c1),
                order: Order.Lazy,
                min: 1);
            var input = new[] { c2, c1, c1, c1, c1 };

            // When
            var result = expression.Run(input);

            // Then
            Assert.Empty(result);
        }

        [Theory]
        [InlineData('x')]
        [InlineData(' ')]
        public void SingleRepetition_ReturnsChars(char c)
        {
            // Given
            var expression = new RepExpression<char, char>(
                new LiteralExpression<char>(c));
            var input = new[] { c };

            // When
            var result = expression.Run(input);

            // Then
            Assert.Collection(result,
                branch =>
                {
                    Assert.Equal(0, branch.Offset);
                    Assert.Equal(1, branch.Length);
                    Assert.Collection(branch.Output,
                        item => Assert.Equal(c, item));
                },
                branch =>
                {
                    Assert.Equal(0, branch.Offset);
                    Assert.Equal(0, branch.Length);
                    Assert.Empty(branch.Output);
                });
        }

        [Theory]
        [InlineData('x')]
        [InlineData(' ')]
        public void NoRepetition_ReturnsChars(char c)
        {
            // Given
            var expression = new RepExpression<char, char>(
                new LiteralExpression<char>(c));
            var input = new char[] { };

            // When
            var result = expression.Run(input);

            // Then
            Assert.Collection(result,
                branch =>
                {
                    Assert.Equal(0, branch.Offset);
                    Assert.Equal(0, branch.Length);
                    Assert.Empty(branch.Output);
                });
        }

        [Theory]
        [InlineData('x', 'y')]
        [InlineData(' ', '\t')]
        public void PartialRepetition_ReturnsChars(char c1, char c2)
        {
            // Given
            Assert.NotEqual(c1, c2);
            var expression = new RepExpression<char, char>(
                new LiteralExpression<char>(c1));
            var input = new[] { c1, c1, c1, c1, c2 };

            // When
            var result = expression.Run(input);

            // Then
            Assert.Collection(result,
                branch =>
                {
                    Assert.Equal(0, branch.Offset);
                    Assert.Equal(4, branch.Length);
                    Assert.Collection(branch.Output,
                        item => Assert.Equal(c1, item),
                        item => Assert.Equal(c1, item),
                        item => Assert.Equal(c1, item),
                        item => Assert.Equal(c1, item));
                },
                branch =>
                {
                    Assert.Equal(0, branch.Offset);
                    Assert.Equal(3, branch.Length);
                    Assert.Collection(branch.Output,
                        item => Assert.Equal(c1, item),
                        item => Assert.Equal(c1, item),
                        item => Assert.Equal(c1, item));
                },
                branch =>
                {
                    Assert.Equal(0, branch.Offset);
                    Assert.Equal(2, branch.Length);
                    Assert.Collection(branch.Output,
                        item => Assert.Equal(c1, item),
                        item => Assert.Equal(c1, item));
                },
                branch =>
                {
                    Assert.Equal(0, branch.Offset);
                    Assert.Equal(1, branch.Length);
                    Assert.Collection(branch.Output,
                        item => Assert.Equal(c1, item));
                },
                branch =>
                {
                    Assert.Equal(0, branch.Offset);
                    Assert.Equal(0, branch.Length);
                    Assert.Empty(branch.Output);
                });
        }

        [Theory]
        [InlineData('x', 'y')]
        [InlineData(' ', '\t')]
        public void StartsWithWrongChar_Returns(char c1, char c2)
        {
            // Given
            Assert.NotEqual(c1, c2);
            var expression = new RepExpression<char, char>(
                new LiteralExpression<char>(c1));
            var input = new[] { c2, c1, c1, c1, c1 };

            // When
            var result = expression.Run(input);

            // Then
            Assert.Collection(result,
                branch =>
                {
                    Assert.Equal(0, branch.Offset);
                    Assert.Equal(0, branch.Length);
                    Assert.Empty(branch.Output);
                });
        }

        [Fact]
        public void EmptyInput_ReturnsEmpty()
        {
            // Given
            var expression = new RepExpression<char, char>(
                new LiteralExpression<char>('x'));
            var input = new char[] { };

            // When
            var result = expression.Run(input);

            // Then
            Assert.Collection(result,
                branch =>
                {
                    Assert.Equal(0, branch.Offset);
                    Assert.Equal(0, branch.Length);
                    Assert.Empty(branch.Output);
                });
        }
    }
}
