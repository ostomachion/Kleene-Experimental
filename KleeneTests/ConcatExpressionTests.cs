using System;
using Xunit;
using Kleene;
using System.Linq;
using System.Collections.Generic;

namespace KleeneTests
{
    public class ConcatExpressionTests
    {
    //     [Fact]
    //     public void NullExpressions_Throws()
    //     {
    //         Assert.Throws<ArgumentNullException>(() =>
    //         {
    //             new SequenceExpression<char, char>(null!);
    //         });
    //     }

    //     [Fact]
    //     public void NullExpression_Throws()
    //     {
    //         Assert.Throws<ArgumentException>(() =>
    //         {
    //             new SequenceExpression<char, char>(new Expression<char, char>[] {
    //                 null!
    //             });
    //         });
    //     }

    //     [Fact]
    //     public void NullAndNotNullExpression_Throws()
    //     {
    //         Assert.Throws<ArgumentException>(() =>
    //         {
    //             new SequenceExpression<char, char>(new Expression<char, char>[] {
    //                 new ConstantExpression<char>('c'),
    //                 null!
    //             });
    //         });
    //     }

    //     [Fact]
    //     public void NullInput_Throws()
    //     {
    //         // Given
    //         var expression = new SequenceExpression<char, char>(new[] {
    //             new ConstantExpression<char>('x'),
    //             new ConstantExpression<char>('y')
    //         });
    //         IEnumerable<char> input = null!;

    //         // Then
    //         Assert.Throws<ArgumentNullException>(() =>
    //         {
    //             expression.Run(input).ToList();
    //         });
    //     }

    //     [Theory]
    //     [InlineData('f', 'o')]
    //     [InlineData(' ', '\t')]
    //     public void TwoChars_ReturnsChars(char c1, char c2)
    //     {
    //         // Given
    //         var expression = new SequenceExpression<char, char>(new[] {
    //             new ConstantExpression<char>(c1),
    //             new ConstantExpression<char>(c2)
    //         });
    //         var input = new[] { c1, c2 };

    //         // When
    //         var result = expression.Run(input);

    //         // Then
    //         Assert.Collection(result,
    //             branch =>
    //             {
    //                 Assert.Equal(0, branch.Offset);
    //                 Assert.Equal(2, branch.Length);
    //                 Assert.Collection(branch.Output,
    //                     item => Assert.Equal(c1, item),
    //                     item => Assert.Equal(c2, item));
    //             });
    //     }

    //     [Theory]
    //     [InlineData('f', 'o')]
    //     [InlineData(' ', '\t')]
    //     public void TwoCharsWrongOrder_ReturnsNothing(char c1, char c2)
    //     {
    //         // Given
    //         Assert.NotEqual(c1, c2);
    //         var expression = new SequenceExpression<char, char>(new[] {
    //             new ConstantExpression<char>(c1),
    //             new ConstantExpression<char>(c2)
    //         });
    //         var input = new[] { c2, c1 };

    //         // When
    //         var result = expression.Run(input);

    //         // Then
    //         Assert.Empty(result);
    //     }

    //     [Theory]
    //     [InlineData('f', 'o')]
    //     [InlineData(' ', '\t')]
    //     public void TwoCharsEmptyInput_ReturnsEmpty(char c1, char c2)
    //     {
    //         // Given
    //         var expression = new SequenceExpression<char, char>(new[] {
    //             new ConstantExpression<char>(c1),
    //             new ConstantExpression<char>(c2)
    //         });
    //         var input = new char[] { };

    //         // When
    //         var result = expression.Run(input);

    //         // Then
    //         Assert.Empty(result);
    //     }

    //     [Theory]
    //     [InlineData('b', 'a', 'r')]
    //     [InlineData(' ', '\r', '\n')]
    //     public void ThreeChars_ReturnsChars(char c1, char c2, char c3)
    //     {
    //         // Given
    //         var expression = new SequenceExpression<char, char>(new[] {
    //             new ConstantExpression<char>(c1),
    //             new ConstantExpression<char>(c2),
    //             new ConstantExpression<char>(c3),
    //         });
    //         var input = new[] { c1, c2, c3 };

    //         // When
    //         var result = expression.Run(input);

    //         // Then
    //         Assert.Collection(result,
    //             branch =>
    //             {
    //                 Assert.Equal(0, branch.Offset);
    //                 Assert.Equal(3, branch.Length);
    //                 Assert.Collection(branch.Output,
    //                     item => Assert.Equal(c1, item),
    //                     item => Assert.Equal(c2, item),
    //                     item => Assert.Equal(c3, item));
    //             });
    //     }

    //     [Theory]
    //     [InlineData('b', 'a', 'r')]
    //     [InlineData(' ', '\r', '\n')]
    //     public void ThreeCharsWrongOrder_ReturnsChars(char c1, char c2, char c3)
    //     {
    //         // Given
    //         Assert.NotEqual(c2, c3);
    //         var expression = new SequenceExpression<char, char>(new[] {
    //             new ConstantExpression<char>(c1),
    //             new ConstantExpression<char>(c2),
    //             new ConstantExpression<char>(c3),
    //         });
    //         var input = new[] { c1, c3, c2 };

    //         // When
    //         var result = expression.Run(input);

    //         // Then
    //         Assert.Empty(result);
    //     }

    //     [Theory]
    //     [InlineData('b', 'a', 'r')]
    //     [InlineData(' ', '\r', '\n')]
    //     public void ThreeCharsEmptyInput_ReturnsEmpty(char c1, char c2, char c3)
    //     {
    //         // Given
    //         var expression = new SequenceExpression<char, char>(new[] {
    //             new ConstantExpression<char>(c1),
    //             new ConstantExpression<char>(c2),
    //             new ConstantExpression<char>(c3),
    //         });
    //         var input = new char[] { };

    //         // When
    //         var result = expression.Run(input);

    //         // Then
    //         Assert.Empty(result);
    //     }

    //     [Theory]
    //     [InlineData('x', 'y', 'z')]
    //     [InlineData('J', ' ', 'H')]
    //     public void TwoCharsPartialMatch_ReturnsEmpty(char c1, char c2, char c)
    //     {
    //         // Given
    //         Assert.NotEqual(c, c2);
    //         var expression = new SequenceExpression<char, char>(new[] {
    //             new ConstantExpression<char>(c1),
    //             new ConstantExpression<char>(c2)
    //         });
    //         var input = new[] { c1, c };

    //         // When
    //         var result = expression.Run(input);

    //         // Then
    //         Assert.Empty(result);
    //     }

    //     [Theory]
    //     [InlineData('x')]
    //     [InlineData(' ')]
    //     [InlineData('?')]
    //     public void OneCharMatch_ReturnsChar(char c)
    //     {
    //         // Given
    //         var expression = new SequenceExpression<char, char>(new[] {
    //             new ConstantExpression<char>(c)
    //         });
    //         var input = new[] { c };

    //         // When
    //         var result = expression.Run(input);

    //         // Then
    //         Assert.Collection(result,
    //             branch =>
    //             {
    //                 Assert.Equal(0, branch.Offset);
    //                 Assert.Equal(1, branch.Length);
    //                 Assert.Collection(branch.Output,
    //                     item => Assert.Equal(c, item));
    //             });
    //     }

    //     [Theory]
    //     [InlineData('x', 'y')]
    //     [InlineData(' ', 'y')]
    //     [InlineData('?', 'y')]
    //     public void OneCharNoMatch_ReturnsEmpty(char c1, char c)
    //     {
    //         // Given
    //         Assert.NotEqual(c1, c);
    //         var expression = new SequenceExpression<char, char>(new[] {
    //             new ConstantExpression<char>(c1)
    //         });
    //         var input = new[] { c };

    //         // When
    //         var result = expression.Run(input);

    //         // Then
    //         Assert.Empty(result);
    //     }

    //     [Theory]
    //     [InlineData('x')]
    //     [InlineData('\\')]
    //     [InlineData('\0')]
    //     public void OneCharEmptyInput_ReturnsEmpty(char c)
    //     {
    //         // Given
    //         var expression = new SequenceExpression<char, char>(new[] {
    //             new ConstantExpression<char>(c)
    //         });
    //         var input = new char[] { };

    //         // When
    //         var result = expression.Run(input);

    //         // Then
    //         Assert.Empty(result);
    //     }

    //     [Theory]
    //     [InlineData('x')]
    //     [InlineData('\\')]
    //     [InlineData('\0')]
    //     public void ZeroChars_Returns(char c)
    //     {
    //         // Given
    //         var expression = new SequenceExpression<char, char>(Enumerable.Empty<Expression<char, char>>());
    //         var input = new char[] { c };

    //         // When
    //         var result = expression.Run(input);

    //         // Then
    //         Assert.Collection(result,
    //             branch =>
    //             {
    //                 Assert.Equal(0, branch.Offset);
    //                 Assert.Equal(0, branch.Length);
    //                 Assert.Empty(branch.Output);
    //             });
    //     }

    //     [Fact]
    //     public void ZeroCharsEmptyInput_Returns()
    //     {
    //         // Given
    //         var expression = new SequenceExpression<char, char>(Enumerable.Empty<Expression<char, char>>());
    //         var input = new char[] { };

    //         // When
    //         var result = expression.Run(input);

    //         // Then
    //         Assert.Collection(result,
    //             branch =>
    //             {
    //                 Assert.Equal(0, branch.Offset);
    //                 Assert.Equal(0, branch.Length);
    //                 Assert.Empty(branch.Output);
    //             });
    //     }

    //     [Theory]
    //     [InlineData('M')]
    //     [InlineData('_')]
    //     public void DuplicateChars_ReturnChars(char c)
    //     {
    //         // Given
    //         var expression = new SequenceExpression<char, char>(new[] {
    //             new ConstantExpression<char>(c),
    //             new ConstantExpression<char>(c)
    //         });
    //         var input = new char[] { c, c };

    //         // When
    //         var result = expression.Run(input);

    //         // Then
    //         Assert.Collection(result,
    //             branch =>
    //             {
    //                 Assert.Equal(0, branch.Offset);
    //                 Assert.Equal(2, branch.Length);
    //                 Assert.Collection(branch.Output,
    //                     item => Assert.Equal(c, item),
    //                     item => Assert.Equal(c, item));
    //             });
    //     }

    //     [Fact]
    //     public void Backtrack()
    //     {
    //         // Given
    //         var expression = new SequenceExpression<char, char>(new[] {
    //             new AltExpression<char, char>(new Expression<char, char> [] {
    //                 new ProduceExpression<char, char>('w'),
    //                 new ProduceExpression<char, char>('x'),
    //             }),
    //             new AltExpression<char, char>(new Expression<char, char> [] {
    //                 new ProduceExpression<char, char>('y'),
    //                 new ProduceExpression<char, char>('z'),
    //             })
    //         });
    //         var input = new char[] { };

    //         // When
    //         var result = expression.Run(input);

    //         // Then
    //         Assert.Collection(result,
    //             branch =>
    //             {
    //                 Assert.Equal(0, branch.Offset);
    //                 Assert.Equal(0, branch.Length);
    //                 Assert.Collection(branch.Output,
    //                     item => Assert.Equal('w', item),
    //                     item => Assert.Equal('y', item));
    //             },
    //             branch =>
    //             {
    //                 Assert.Equal(0, branch.Offset);
    //                 Assert.Equal(0, branch.Length);
    //                 Assert.Collection(branch.Output,
    //                     item => Assert.Equal('w', item),
    //                     item => Assert.Equal('z', item));
    //             },
    //             branch =>
    //             {
    //                 Assert.Equal(0, branch.Offset);
    //                 Assert.Equal(0, branch.Length);
    //                 Assert.Collection(branch.Output,
    //                     item => Assert.Equal('x', item),
    //                     item => Assert.Equal('y', item));
    //             },
    //             branch =>
    //             {
    //                 Assert.Equal(0, branch.Offset);
    //                 Assert.Equal(0, branch.Length);
    //                 Assert.Collection(branch.Output,
    //                     item => Assert.Equal('x', item),
    //                     item => Assert.Equal('z', item));
    //             });
    //     }
    }
}
