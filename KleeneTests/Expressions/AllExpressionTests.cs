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
            var input = new[] { 't', 'e', 's', 't' }.Select(x => new ConstantStructure<char>(x));

            // When
            var result = expression.Run(input, 0);

            // Then
            Assert.Collection(result,
                branch =>
                {
                    Assert.Collection(branch,
                        item => Assert.Equal('t', ((ConstantStructure<char>)item).Value),
                        item => Assert.Equal('e', ((ConstantStructure<char>)item).Value),
                        item => Assert.Equal('s', ((ConstantStructure<char>)item).Value),
                        item => Assert.Equal('t', ((ConstantStructure<char>)item).Value));
                },
                branch =>
                {
                    Assert.Collection(branch,
                        item => Assert.Equal('t', ((ConstantStructure<char>)item).Value),
                        item => Assert.Equal('e', ((ConstantStructure<char>)item).Value),
                        item => Assert.Equal('s', ((ConstantStructure<char>)item).Value));
                },
                branch =>
                {
                    Assert.Collection(branch,
                        item => Assert.Equal('t', ((ConstantStructure<char>)item).Value),
                        item => Assert.Equal('e', ((ConstantStructure<char>)item).Value));
                },
                branch =>
                {
                    Assert.Collection(branch,
                        item => Assert.Equal('t', ((ConstantStructure<char>)item).Value));
                },
                branch =>
                {
                    Assert.Empty(branch);
                });
        }

        [Fact]
        public void LazyTest()
        {
            // Given
            var expression = new AllExpression(Order.Lazy);
            var input = new[] { 't', 'e', 's', 't' }.Select(x => new ConstantStructure<char>(x));

            // When
            var result = expression.Run(input, 0);

            // Then
            Assert.Collection(result,
                branch =>
                {
                    Assert.Empty(branch);
                },
                branch =>
                {
                    Assert.Collection(branch,
                        item => Assert.Equal('t', ((ConstantStructure<char>)item).Value));
                },
                branch =>
                {
                    Assert.Collection(branch,
                        item => Assert.Equal('t', ((ConstantStructure<char>)item).Value),
                        item => Assert.Equal('e', ((ConstantStructure<char>)item).Value));
                },
                branch =>
                {
                    Assert.Collection(branch,
                        item => Assert.Equal('t', ((ConstantStructure<char>)item).Value),
                        item => Assert.Equal('e', ((ConstantStructure<char>)item).Value),
                        item => Assert.Equal('s', ((ConstantStructure<char>)item).Value));
                },
                branch =>
                {
                    Assert.Collection(branch,
                        item => Assert.Equal('t', ((ConstantStructure<char>)item).Value),
                        item => Assert.Equal('e', ((ConstantStructure<char>)item).Value),
                        item => Assert.Equal('s', ((ConstantStructure<char>)item).Value),
                        item => Assert.Equal('t', ((ConstantStructure<char>)item).Value));
                });
        }

        [Fact]
        public void EmptyInputGreedy()
        {
            // Given
            var expression = new AllExpression(Order.Greedy);
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

        [Fact]
        public void EmptyLazyGreedy()
        {
            // Given
            var expression = new AllExpression(Order.Lazy);
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
    }
}