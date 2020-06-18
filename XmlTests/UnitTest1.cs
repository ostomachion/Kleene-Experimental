using System;
using Xunit;
using Kleene.Xml;
using System.Xml.Linq;
using System.Linq;

namespace XmlTests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var structure = StructureParser.ParseElement(XElement.Parse(
                @"<foo>
                    <bar name='value'>Hello, world!</bar>
                </foo>"
            ));

            var expression = ExpressionParser.ParseElement(XElement.Parse(
                @"<foo xmlns:k='http://hufford.io/kleene/xml'>
                    <k:alt>
                        <k:item><baz>Goodbye, world!</baz></k:item>
                        <k:item><bar name='value'>Hello, world!</bar></k:item>
                    </k:alt>
                </foo>"
            ));

            var result = expression.Run(new[] { structure }, 0);

            Assert.True(result.Any());

            ;
        }
    }
}
