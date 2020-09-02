using System;
using Xunit;
using Kleene;
using System.Linq;
using System.Collections.Generic;

namespace KleeneTests
{
    public class StructureExpressionTests
    {
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