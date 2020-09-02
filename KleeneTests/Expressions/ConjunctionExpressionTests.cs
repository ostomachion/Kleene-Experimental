using System;
using Xunit;
using Kleene;
using System.Linq;
using System.Collections.Generic;

namespace KleeneTests
{
    public class ConjunctionExpressionTests
    {
        [Fact]
        public void NullLeader_Throws()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new ConjunctionExpression(null!, SequenceExpression.Empty);
            });
        }

        [Fact]
        public void NullFollower_Throws()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new ConjunctionExpression(SequenceExpression.Empty, null!);
            });
        }

        [Fact]
        public void TwoEmptyChoices_RetrurnsEmpty()
        {
            // Given
            var expression = new ConjunctionExpression(SequenceExpression.Empty, SequenceExpression.Empty);

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
        public void TwoMatchingStructures_ReturnsStructure()
        {
            // Given
            var expression = new ConjunctionExpression(new StructureExpression("foo"), new StructureExpression("foo"));

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
        public void TwoDifferentStructures_ReturnsNothing()
        {
            // Given
            var expression = new ConjunctionExpression(new StructureExpression("foo"), new StructureExpression("bar"));

            // When
            var results = expression.Run();

            // Then
            Assert.Empty(results);
        }
    }
}