using System;
using Xunit;
using Kleene;
using System.Linq;
using System.Collections.Generic;

namespace KleeneTests
{
    public class AllExpressionTests
    {
        [Fact]
        public void GreedyDefaultOrder()
        {
            // Given
            var expression = new AllExpression();

            // Then
            Assert.Equal(Order.Greedy, expression.Order);
        }
        
        [Fact]
        public void NullInput_Throws()
        {
            // Given
            var expression = new AllExpression();
            IEnumerable<ConstantStructure<char>> input = null!;

            // Then
            Assert.Throws<ArgumentNullException>(() =>
            {
                expression.Run(input, 0).ToList();
            });
        }

        [Fact]
        public void GreedyTest()
        {
            // Given
            var expression = new AllExpression(Order.Greedy);
            var input = new[] { 't', 'e', 's', 't' }.Select(x => new ConstantStructure<char>(x)).ToArray();

            // When
            var results = expression.Run(input, 0);

            // Then
            Assert.Collection(results,
                result => {
                    Assert.Equal(input, result.Input);
                    Assert.Equal(0, result.Index);
                    Assert.Equal(4, result.Length);
                    Assert.Equal(expression, result.Expression);
                    Assert.Empty(result.Children);
                },
                result => {
                    // Assert.Equal(input, result.Input);
                    Assert.Equal(0, result.Index);
                    Assert.Equal(3, result.Length);
                    Assert.Equal(expression, result.Expression);
                    Assert.Empty(result.Children);
                },
                result => {
                    // Assert.Equal(input, result.Input);
                    Assert.Equal(0, result.Index);
                    Assert.Equal(2, result.Length);
                    Assert.Equal(expression, result.Expression);
                    Assert.Empty(result.Children);
                },
                result => {
                    // Assert.Equal(input, result.Input);
                    Assert.Equal(0, result.Index);
                    Assert.Equal(1, result.Length);
                    Assert.Equal(expression, result.Expression);
                    Assert.Empty(result.Children);
                },
                result => {
                    // Assert.Equal(input, result.Input);
                    Assert.Equal(0, result.Index);
                    Assert.Equal(0, result.Length);
                    Assert.Equal(expression, result.Expression);
                    Assert.Empty(result.Children);
                }
            );
        }

        [Fact]
        public void LazyTest()
        {
            // Given
            var expression = new AllExpression(Order.Lazy);
            var input = new[] { 't', 'e', 's', 't' }.Select(x => new ConstantStructure<char>(x)).ToArray();

            // When
            var results = expression.Run(input, 0);

            // Then
            Assert.Collection(results,
                result => {
                    Assert.Equal(input, result.Input);
                    Assert.Equal(0, result.Index);
                    Assert.Equal(0, result.Length);
                    Assert.Equal(expression, result.Expression);
                    Assert.Empty(result.Children);
                },
                result => {
                    Assert.Equal(input, result.Input);
                    Assert.Equal(0, result.Index);
                    Assert.Equal(1, result.Length);
                    Assert.Equal(expression, result.Expression);
                    Assert.Empty(result.Children);
                },
                result => {
                    Assert.Equal(input, result.Input);
                    Assert.Equal(0, result.Index);
                    Assert.Equal(2, result.Length);
                    Assert.Equal(expression, result.Expression);
                    Assert.Empty(result.Children);
                },
                result => {
                    Assert.Equal(input, result.Input);
                    Assert.Equal(0, result.Index);
                    Assert.Equal(3, result.Length);
                    Assert.Equal(expression, result.Expression);
                    Assert.Empty(result.Children);
                },
                result => {
                    Assert.Equal(input, result.Input);
                    Assert.Equal(0, result.Index);
                    Assert.Equal(4, result.Length);
                    Assert.Equal(expression, result.Expression);
                    Assert.Empty(result.Children);
                }
            );
        }

        [Fact]
        public void EmptyInputGreedy()
        {
            // Given
            var expression = new AllExpression(Order.Greedy);
            var input = new char[] { }.Select(x => new ConstantStructure<char>(x)).ToArray();

            // When
            var results = expression.Run(input, 0);

            // Then
            Assert.Collection(results,
                result => {
                    Assert.Equal(input, result.Input);
                    Assert.Equal(0, result.Index);
                    Assert.Equal(0, result.Length);
                    Assert.Equal(expression, result.Expression);
                    Assert.Empty(result.Children);
                }
            );
        }

        [Fact]
        public void EmptyLazyGreedy()
        {
            // Given
            var expression = new AllExpression(Order.Lazy);
            var input = new char[] { }.Select(x => new ConstantStructure<char>(x)).ToArray();

            // When
            var results = expression.Run(input, 0);

            // Then
            Assert.Collection(results,
                result => {
                    Assert.Equal(input, result.Input);
                    Assert.Equal(0, result.Index);
                    Assert.Equal(0, result.Length);
                    Assert.Equal(expression, result.Expression);
                    Assert.Empty(result.Children);
                }
            );
        }
    }
}