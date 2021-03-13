using System.Xml.Schema;
using System.Linq;
using System;
using Xunit;
using Kleene;
using Kleene.Tests.NondeterministicObjects;

namespace Kleene.Tests.AnyObject
{
    public class Collapse
    {
        [Fact]
        public void Collapse_Throws()
        {
            // Given
            var obj = Samples.Any();

            // Then
            Assert.Throws<InvalidOperationException>(() => obj.Collapse());
        }
    }
}
