using System;
using Xunit;
using Kleene;
using System.Linq;
using System.Collections.Generic;

namespace KleeneTests
{
    public class StructureExpressionTests
    {
        [Fact]
        public void NullName_Throws()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new StructureExpression(null!);
            });
        }

        [Fact]
        public void NullNameWithExpression_Throws()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new StructureExpression(null!, SequenceExpression.Empty);
            });
        }

        [Fact]
        public void NullExpression_Throws()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new StructureExpression("foo", null!);
            });
        }

        [Fact]
        public void NoChildren_ReturnsSameNameNoChildren()
        {
            // Given
            var expression = new StructureExpression("foo");

            // When
            var results = expression.Run();

            // Then
            Assert.Collection(results,
                result =>
                {
                    Assert.NotNull(result);
                    Assert.Equal("foo", result!.Name);
                    Assert.Collection(result.FirstChild,
                        item =>
                        {
                            Assert.Null(item);
                        }
                    );
                    Assert.Collection(result.NextSibling,
                        item =>
                        {
                            Assert.Null(item);
                        }
                    );
                }
            );
        }

        [Fact]
        public void WithExpression_ReturnsStructureWithChild()
        {
            // Given
            var expression = new StructureExpression("foo", new StructureExpression("bar"));

            // When
            var results = expression.Run();

            // Then
            Assert.Collection(results,
                result =>
                {
                    Assert.NotNull(result);
                    Assert.Equal("foo", result!.Name);
                    Assert.Collection(result.FirstChild,
                        item =>
                        {
                            Assert.NotNull(item);
                            Assert.Equal("bar", item!.Name);
                            Assert.Collection(item.FirstChild,
                                item =>
                                {
                                    Assert.Null(item);
                                }
                            );
                            Assert.Collection(item.NextSibling,
                                item =>
                                {
                                    Assert.Null(item);
                                }
                            );
                        }
                    );
                    Assert.Collection(result.NextSibling,
                        item =>
                        {
                            Assert.Null(item);
                        }
                    );
                }
            );
        }
    }
}