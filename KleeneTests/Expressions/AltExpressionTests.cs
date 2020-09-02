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
            var expression = new AltExpression(new [] {
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
                            Assert.Null(item);
                        }
                    );
                },
                result =>
                {
                    Assert.NotNull(result);
                    Assert.Equal("bar", result!.Name);
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
    }
}