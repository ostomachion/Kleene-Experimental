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
    }
}