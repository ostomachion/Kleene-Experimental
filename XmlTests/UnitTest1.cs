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
        public void Literal()
        {
            var structure = StructureParser.ParseElement(XElement.Parse(
                @"<foo>
                    <bar name='value'>Hello, world!</bar>
                </foo>"
            ));

            var expression = ExpressionParser.ParseElement(XElement.Parse(
                @"<foo>
                    <bar name='value'>Hello, world!</bar>
                </foo>"
            ));

            var result = expression.Run(new[] { structure }, 0);

            Assert.True(result.Any());
        }

        [Fact]
        public void Expanded()
        {
            var structure = StructureParser.ParseElement(XElement.Parse(
                @"<foo>
                    <bar name='value'>Hello, world!</bar>
                </foo>"
            ));

            var expression = ExpressionParser.ParseElement(XElement.Parse(
                @"<k:elem xmlns:k='http://hufford.io/kleene/xml'>
                    <k:name>
                        <k:ns/>
                        <k:local>foo</k:local>
                    </k:name>
                    <k:attrs/>
                    <k:value>
                        <k:elem>
                            <k:name>
                                <k:ns/>
                                <k:local>bar</k:local>
                            </k:name>
                            <k:attrs>
                                <k:attr>
                                    <k:name>
                                        <k:ns/>
                                        <k:local>name</k:local>
                                    </k:name>
                                    <k:value>value</k:value>
                                </k:attr>
                            </k:attrs>
                            <k:value>Hello, world!</k:value>
                        </k:elem>
                    </k:value>
                </k:elem>"
            ));
        }

        [Fact]
        public void NameShorthand()
        {
            var structure = StructureParser.ParseElement(XElement.Parse(
                @"<foo xmlns='example.com'/>"
            ));

            var expression = ExpressionParser.ParseElement(XElement.Parse(
                @"<k:elem xmlns:k='http://hufford.io/kleene/xml' xmlns='example.com'>
                    <k:name><foo/></k:name>
                    <k:attrs/>
                    <k:value/>
                </k:elem>"
            ));

            var result = expression.Run(new[] { structure }, 0);

            Assert.True(result.Any());
        }

        [Fact]
        public void AttributeShorthand()
        {
            var structure = StructureParser.ParseElement(XElement.Parse(
                @"<foo ns:bar='baz' xmlns:ns='example.com'/>"
            ));

            var expression = ExpressionParser.ParseElement(XElement.Parse(
                @"<k:elem xmlns:k='http://hufford.io/kleene/xml' xmlns:ns='example.com'>
                    <k:name><foo/></k:name>
                    <k:attrs>
                        <k:attr ns:bar='baz'/>
                    </k:attrs>
                    <k:value/>
                </k:elem>"
            ));

            var result = expression.Run(new[] { structure }, 0);

            Assert.True(result.Any());
        }

        [Fact]
        public void Alt()
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
        }

        [Fact]
        public void Any()
        {
            var structure = StructureParser.ParseElement(XElement.Parse(
                @"<foo>
                    <bar name='value'>Hello, world!</bar>
                </foo>"
            ));

            var expression = ExpressionParser.ParseElement(XElement.Parse(
                @"<k:any xmlns:k='http://hufford.io/kleene/xml'/>"
            ));

            var result = expression.Run(new[] { structure }, 0);

            Assert.True(result.Any());
        }
    }
}
