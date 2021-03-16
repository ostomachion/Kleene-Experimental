using System.Linq;
using System;
using Xunit;
using Kleene;

namespace Kleene.Tests.AnyExpression
{
    public class Generate
    {
        [Fact]
        public void Generate_Throws()
        {
            // Given
            var expression = new AnyExpression<string>();

            // When
            var results = expression.Generate();
            
            // Then
            Assert.Throws<InvalidOperationException>(() => results.GetEnumerator().MoveNext());
        }
    }
}
