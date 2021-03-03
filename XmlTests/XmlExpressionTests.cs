using System;
using Xunit;
using Kleene.Xml;
using System.Xml.Linq;
using System.Linq;
using Kleene;

namespace XmlTests
{
    public class XmlExpressionTests
    {
        [Fact]
        public void EmptyElement()
        {
            // Given
            var structure = StructureParser.ParseElement(XElement.Parse(
                @"<foo/>"
            ));

            var expression = ExpressionParser.ParseElement(XElement.Parse(
                @"<foo/>"
            ));

            // When
            var results = expression.Run(new[] { structure }, 0);

            // Then
            Assert.Single(results);
        }

        [Fact]
        public void EmptyElementAll()
        {
            // Given
            var structure = StructureParser.ParseElement(XElement.Parse(
                @"<foo/>"
            ));

            var expression = ExpressionParser.ParseElement(XElement.Parse(
                @"<k:elem xmlns:k='http://hufford.io/kleene/xml'>
                    <k:name>
                        <k:ns><k:all/></k:ns>
                        <k:local><k:all/></k:local>
                    </k:name>
                    <k:attrs><k:all/></k:attrs>
                    <k:value><k:all/></k:value>
                </k:elem>"
            ));

            // When
            var results = expression.Run(new[] { structure }, 0);

            // Then
            Assert.Single(results);
        }

        [Fact]
        public void EmptyElementLong()
        {
            // Given
            var structure = StructureParser.ParseElement(XElement.Parse(
                @"<foo/>"
            ));

            var expression = ExpressionParser.ParseElement(XElement.Parse(
                @"<k:elem xmlns:k='http://hufford.io/kleene/xml'>
                    <k:name>
                        <k:ns/>
                        <k:local>foo</k:local>
                    </k:name>
                    <k:attrs/>
                    <k:value/>
                </k:elem>"
            ));

            // When
            var results = expression.Run(new[] { structure }, 0);

            // Then
            Assert.Single(results);
        }

        [Fact]
        public void EmptyElementNameShorthand()
        {
            // Given
            var structure = StructureParser.ParseElement(XElement.Parse(
                @"<foo/>"
            ));

            var expression = ExpressionParser.ParseElement(XElement.Parse(
                @"<k:elem xmlns:k='http://hufford.io/kleene/xml'>
                    <k:name><foo/></k:name>
                    <k:attrs/>
                    <k:value/>
                </k:elem>"
            ));

            // When
            var results = expression.Run(new[] { structure }, 0);

            // Then
            Assert.Single(results);
        }

        [Fact]
        public void EmptyElementNS()
        {
            // Given
            var structure = StructureParser.ParseElement(XElement.Parse(
                @"<foo xmlns='http://example.com'/>"
            ));

            var expression = ExpressionParser.ParseElement(XElement.Parse(
                @"<foo xmlns='http://example.com'/>"
            ));

            // When
            var results = expression.Run(new[] { structure }, 0);

            // Then
            Assert.Single(results);
        }

        [Fact]
        public void EmptyElementNSLong()
        {
            // Given
            var structure = StructureParser.ParseElement(XElement.Parse(
                @"<foo xmlns='http://example.com'/>"
            ));

            var expression = ExpressionParser.ParseElement(XElement.Parse(
                @"<k:elem xmlns:k='http://hufford.io/kleene/xml'>
                    <k:name>
                        <k:ns>http://example.com</k:ns>
                        <k:local>foo</k:local>
                    </k:name>
                    <k:attrs/>
                    <k:value/>
                </k:elem>"
            ));

            // When
            var results = expression.Run(new[] { structure }, 0);

            // Then
            Assert.Single(results);
        }

        [Fact]
        public void EmptyElementNSNameShorthand()
        {
            // Given
            var structure = StructureParser.ParseElement(XElement.Parse(
                @"<foo xmlns='http://example.com'/>"
            ));

            var expression = ExpressionParser.ParseElement(XElement.Parse(
                @"<k:elem xmlns:k='http://hufford.io/kleene/xml' xmlns='http://example.com'>
                    <k:name><foo/></k:name>
                    <k:attrs/>
                    <k:value/>
                </k:elem>"
            ));

            // When
            var results = expression.Run(new[] { structure }, 0);

            // Then
            Assert.Single(results);
        }

        [Fact]
        public void EmptyElementNSPrefix()
        {
            var structure = StructureParser.ParseElement(XElement.Parse(
                @"<ex:foo xmlns:ex='http://example.com'/>"
            ));

            var expression = ExpressionParser.ParseElement(XElement.Parse(
                @"<ex:foo xmlns:ex='http://example.com'/>"
            ));

            // When
            var results = expression.Run(new[] { structure }, 0);

            // Then
            Assert.Single(results);
        }

        [Fact]
        public void Attribute()
        {
            var structure = StructureParser.ParseElement(XElement.Parse(
                @"<foo bar='123'/>"
            ));

            var expression = ExpressionParser.ParseElement(XElement.Parse(
                @"<foo bar='123'/>"
            ));

            // When
            var results = expression.Run(new[] { structure }, 0);

            // Then
            Assert.Single(results);
        }

        [Fact]
        public void AttributesAll()
        {
            var structure = StructureParser.ParseElement(XElement.Parse(
                @"<foo bar='123'/>"
            ));

            var expression = ExpressionParser.ParseElement(XElement.Parse(
                @"<k:elem xmlns:k='http://hufford.io/kleene/xml'>
                    <k:name>
                        <k:ns/>
                        <k:local>foo</k:local>
                    </k:name>
                    <k:attrs><k:all/></k:attrs>
                    <k:value/>
                </k:elem>"
            ));

            // When
            var results = expression.Run(new[] { structure }, 0);

            // Then
            Assert.Single(results);
        }

        [Fact]
        public void AttributeAll()
        {
            var structure = StructureParser.ParseElement(XElement.Parse(
                @"<foo bar='123'/>"
            ));

            var expression = ExpressionParser.ParseElement(XElement.Parse(
                @"<k:elem xmlns:k='http://hufford.io/kleene/xml'>
                    <k:name>
                        <k:ns/>
                        <k:local>foo</k:local>
                    </k:name>
                    <k:attrs>
                        <k:attr>
                            <k:name>
                                <k:ns><k:all/></k:ns>
                                <k:local><k:all/></k:local>
                            </k:name>
                            <k:value><k:all/></k:value>
                        </k:attr>
                    </k:attrs>
                    <k:value/>
                </k:elem>"
            ));

            // When
            var results = expression.Run(new[] { structure }, 0);

            // Then
            Assert.Single(results);
        }

        [Fact]
        public void AttributeLong()
        {
            var structure = StructureParser.ParseElement(XElement.Parse(
                @"<foo bar='123'/>"
            ));

            var expression = ExpressionParser.ParseElement(XElement.Parse(
                @"<k:elem xmlns:k='http://hufford.io/kleene/xml'>
                    <k:name>
                        <k:ns/>
                        <k:local>foo</k:local>
                    </k:name>
                    <k:attrs>
                        <k:attr>
                            <k:name>
                                <k:ns/>
                                <k:local>bar</k:local>
                            </k:name>
                            <k:value>123</k:value>
                        </k:attr>
                    </k:attrs>
                    <k:value/>
                </k:elem>"
            ));

            // When
            var results = expression.Run(new[] { structure }, 0);

            // Then
            Assert.Single(results);
        }

        [Fact]
        public void AttributeNameShorthand()
        {
            var structure = StructureParser.ParseElement(XElement.Parse(
                @"<foo bar='123'/>"
            ));

            var expression = ExpressionParser.ParseElement(XElement.Parse(
                @"<k:elem xmlns:k='http://hufford.io/kleene/xml'>
                    <k:name>
                        <k:ns/>
                        <k:local>foo</k:local>
                    </k:name>
                    <k:attrs>
                        <k:attr>
                            <k:name><bar/></k:name>
                            <k:value>123</k:value>
                        </k:attr>
                    </k:attrs>
                    <k:value/>
                </k:elem>"
            ));

            // When
            var results = expression.Run(new[] { structure }, 0);

            // Then
            Assert.Single(results);
        }

        [Fact]
        public void AttributeShorthand()
        {
            var structure = StructureParser.ParseElement(XElement.Parse(
                @"<foo bar='123'/>"
            ));

            var expression = ExpressionParser.ParseElement(XElement.Parse(
                @"<k:elem xmlns:k='http://hufford.io/kleene/xml'>
                    <k:name>
                        <k:ns/>
                        <k:local>foo</k:local>
                    </k:name>
                    <k:attrs>
                        <k:attr bar='123'/>
                    </k:attrs>
                    <k:value/>
                </k:elem>"
            ));

            // When
            var results = expression.Run(new[] { structure }, 0);

            // Then
            Assert.Single(results);
        }

        [Fact]
        public void AttributeNS()
        {
            var structure = StructureParser.ParseElement(XElement.Parse(
                @"<foo ex:bar='123' xmlns:ex='http://example.com'/>"
            ));

            var expression = ExpressionParser.ParseElement(XElement.Parse(
                @"<foo ex:bar='123' xmlns:ex='http://example.com'/>"
            ));

            // When
            var results = expression.Run(new[] { structure }, 0);

            // Then
            Assert.Single(results);
        }

        [Fact]
        public void AttributeNSLong()
        {
            var structure = StructureParser.ParseElement(XElement.Parse(
                @"<foo ex:bar='123' xmlns:ex='http://example.com'/>"
            ));

            var expression = ExpressionParser.ParseElement(XElement.Parse(
                @"<k:elem xmlns:k='http://hufford.io/kleene/xml'>
                    <k:name>
                        <k:ns/>
                        <k:local>foo</k:local>
                    </k:name>
                    <k:attrs>
                        <k:attr>
                            <k:name>
                                <k:ns>http://example.com</k:ns>
                                <k:local>bar</k:local>
                            </k:name>
                            <k:value>123</k:value>
                        </k:attr>
                    </k:attrs>
                    <k:value/>
                </k:elem>"
            ));

            // When
            var results = expression.Run(new[] { structure }, 0);

            // Then
            Assert.Single(results);
        }

        [Fact]
        public void AttributeNSNameShorthand()
        {
            var structure = StructureParser.ParseElement(XElement.Parse(
                @"<foo ex:bar='123' xmlns:ex='http://example.com'/>"
            ));

            var expression = ExpressionParser.ParseElement(XElement.Parse(
                @"<k:elem xmlns:k='http://hufford.io/kleene/xml' xmlns='http://example.com'>
                    <k:name>
                        <k:ns/>
                        <k:local>foo</k:local>
                    </k:name>
                    <k:attrs>
                        <k:attr>
                            <k:name><bar/></k:name>
                            <k:value>123</k:value>
                        </k:attr>
                    </k:attrs>
                    <k:value/>
                </k:elem>"
            ));

            // When
            var results = expression.Run(new[] { structure }, 0);

            // Then
            Assert.Single(results);
        }

        [Fact]
        public void AttributeNSShorthand()
        {
            var structure = StructureParser.ParseElement(XElement.Parse(
                @"<foo ex:bar='123' xmlns:ex='http://example.com'/>"
            ));

            var expression = ExpressionParser.ParseElement(XElement.Parse(
                @"<k:elem xmlns:k='http://hufford.io/kleene/xml' xmlns:ex='http://example.com'>
                    <k:name>
                        <k:ns/>
                        <k:local>foo</k:local>
                    </k:name>
                    <k:attrs>
                        <k:attr ex:bar='123'/>
                    </k:attrs>
                    <k:value/>
                </k:elem>"
            ));

            // When
            var results = expression.Run(new[] { structure }, 0);

            // Then
            Assert.Single(results);
        }

        [Fact]
        public void TwoAttributes()
        {
            var structure = StructureParser.ParseElement(XElement.Parse(
                @"<foo bar='123' baz='wat'/>"
            ));

            var expression = ExpressionParser.ParseElement(XElement.Parse(
                @"<foo bar='123' baz='wat'/>"
            ));

            // When
            var results = expression.Run(new[] { structure }, 0);

            // Then
            Assert.Single(results);
        }

        [Fact]
        public void TwoAttributesLong()
        {
            var structure = StructureParser.ParseElement(XElement.Parse(
                @"<foo bar='123' baz='wat'/>"
            ));

            var expression = ExpressionParser.ParseElement(XElement.Parse(
                @"<k:elem xmlns:k='http://hufford.io/kleene/xml'>
                    <k:name>
                        <k:ns/>
                        <k:local>foo</k:local>
                    </k:name>
                    <k:attrs>
                        <k:attr>
                            <k:name>
                                <k:ns/>
                                <k:local>bar</k:local>
                            </k:name>
                            <k:value>123</k:value>
                        </k:attr>
                        <k:attr>
                            <k:name>
                                <k:ns/>
                                <k:local>baz</k:local>
                            </k:name>
                            <k:value>wat</k:value>
                        </k:attr>
                    </k:attrs>
                    <k:value/>
                </k:elem>"
            ));

            // When
            var results = expression.Run(new[] { structure }, 0);

            // Then
            Assert.Single(results);
        }

        [Fact]
        public void TextChild()
        {
            var structure = StructureParser.ParseElement(XElement.Parse(
                @"<foo>Hello, world!</foo>"
            ));

            var expression = ExpressionParser.ParseElement(XElement.Parse(
                @"<foo>Hello, world!</foo>"
            ));

            // When
            var results = expression.Run(new[] { structure }, 0);

            // Then
            Assert.Single(results);
        }

        [Fact]
        public void TextChildAll()
        {
            var structure = StructureParser.ParseElement(XElement.Parse(
                @"<foo>Hello, world!</foo>"
            ));

            var expression = ExpressionParser.ParseElement(XElement.Parse(
                @"<foo xmlns:k='http://hufford.io/kleene/xml'><k:all/></foo>"
            ));

            // When
            var results = expression.Run(new[] { structure }, 0);

            // Then
            Assert.Single(results);
        }

        [Fact]
        public void TextChildLong()
        {
            var structure = StructureParser.ParseElement(XElement.Parse(
                @"<foo>Hello, world!</foo>"
            ));

            var expression = ExpressionParser.ParseElement(XElement.Parse(
                @"<k:elem xmlns:k='http://hufford.io/kleene/xml'>
                    <k:name>
                        <k:ns/>
                        <k:local>foo</k:local>
                    </k:name>
                    <k:attrs/>
                    <k:value>Hello, world!</k:value>
                </k:elem>"
            ));

            // When
            var results = expression.Run(new[] { structure }, 0);

            // Then
            Assert.Single(results);
        }

        [Fact]
        public void WhitespaceChild()
        {
            var structure = StructureParser.ParseElement(XElement.Parse(
                @"<foo>    </foo>"
            ));

            var expression = ExpressionParser.ParseElement(XElement.Parse(
                @"<foo>    </foo>"
            ));

            // When
            var results = expression.Run(new[] { structure }, 0);

            // Then
            Assert.Single(results);
        }

        [Fact]
        public void ElementChild()
        {
            var structure = StructureParser.ParseElement(XElement.Parse(
                @"<foo>
                    <bar/>
                </foo>"
            ));

            var expression = ExpressionParser.ParseElement(XElement.Parse(
                @"<foo>
                    <bar/>
                </foo>"
            ));

            // When
            var results = expression.Run(new[] { structure }, 0);

            // Then
            Assert.Single(results);
        }

        [Fact]
        public void ElementChildAll()
        {
            var structure = StructureParser.ParseElement(XElement.Parse(
                @"<foo>
                    <bar/>
                </foo>"
            ));

            var expression = ExpressionParser.ParseElement(XElement.Parse(
                @"<foo xmlns:k='http://hufford.io/kleene/xml'><k:all/></foo>"
            ));

            // When
            var results = expression.Run(new[] { structure }, 0);

            // Then
            Assert.Single(results);
        }

        [Fact]
        public void ElementChildLong()
        {
            var structure = StructureParser.ParseElement(XElement.Parse(
                @"<foo>
                    <bar/>
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
                            <k:attrs/>
                            <k:value/>
                        </k:elem>
                    </k:value>
                </k:elem>"
            ));

            // When
            var results = expression.Run(new[] { structure }, 0);

            // Then
            Assert.Single(results);
        }

        [Fact]
        public void ElementChildElementShorthand()
        {
            var structure = StructureParser.ParseElement(XElement.Parse(
                @"<foo>
                    <bar/>
                </foo>"
            ));

            var expression = ExpressionParser.ParseElement(XElement.Parse(
                @"<k:elem xmlns:k='http://hufford.io/kleene/xml'>
                    <k:name>
                        <k:ns/>
                        <k:local>foo</k:local>
                    </k:name>
                    <k:attrs/>
                    <k:value><bar/></k:value>
                </k:elem>"
            ));

            // When
            var results = expression.Run(new[] { structure }, 0);

            // Then
            Assert.Single(results);
        }

        [Fact]
        public void TwoElementChildrenLong()
        {
            var structure = StructureParser.ParseElement(XElement.Parse(
                @"<foo>
                    <bar/>
                    <baz/>
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
                            <k:attrs/>
                            <k:value/>
                        </k:elem>
                        <k:elem>
                            <k:name>
                                <k:ns/>
                                <k:local>baz</k:local>
                            </k:name>
                            <k:attrs/>
                            <k:value/>
                        </k:elem>
                    </k:value>
                </k:elem>"
            ));

            // When
            var results = expression.Run(new[] { structure }, 0);

            // Then
            Assert.Single(results);
        }

        [Fact]
        public void TwoElementChildrenAll()
        {
            var structure = StructureParser.ParseElement(XElement.Parse(
                @"<foo>
                    <bar/>
                    <baz/>
                </foo>"
            ));

            var expression = ExpressionParser.ParseElement(XElement.Parse(
                @"<foo xmlns:k='http://hufford.io/kleene/xml'><k:all/></foo>"
            ));

            // When
            var results = expression.Run(new[] { structure }, 0);

            // Then
            Assert.Single(results);
        }

        [Fact]
        public void TextAndElementChildren()
        {
            var structure = StructureParser.ParseElement(XElement.Parse(
                @"<foo><bar/>baz</foo>"
            ));

            var expression = ExpressionParser.ParseElement(XElement.Parse(
                @"<foo><bar/>baz</foo>"
            ));

            // When
            var results = expression.Run(new[] { structure }, 0);

            // Then
            Assert.Single(results);
        }

        [Fact]
        public void TextAndElementChildrenAll()
        {
            var structure = StructureParser.ParseElement(XElement.Parse(
                @"<foo><bar/>baz</foo>"
            ));

            var expression = ExpressionParser.ParseElement(XElement.Parse(
                @"<k:elem xmlns:k='http://hufford.io/kleene/xml'>
                    <k:name>
                        <k:ns/>
                        <k:local>foo</k:local>
                    </k:name>
                    <k:attrs/>
                    <k:value>
                        <k:all/>
                    </k:value>
                </k:elem>"
            ));

            // When
            var results = expression.Run(new[] { structure }, 0);

            // Then
            Assert.Single(results);
        }

        [Fact]
        public void TextAndElementChildrenMixed()
        {
            var structure = StructureParser.ParseElement(XElement.Parse(
                @"<foo>baz<bar/></foo>"
            ));

            var expression = ExpressionParser.ParseElement(XElement.Parse(
                @"<k:elem xmlns:k='http://hufford.io/kleene/xml'>
                    <k:name>
                        <k:ns/>
                        <k:local>foo</k:local>
                    </k:name>
                    <k:attrs/>
                    <k:value>baz<k:all/></k:value>
                </k:elem>"
            ));

            // When
            var results = expression.Run(new[] { structure }, 0);

            // Then
            Assert.Single(results);
        }
    }
}
