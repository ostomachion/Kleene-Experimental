using System;
using Xunit;
using Kleene.Xml;
using System.Xml.Linq;
using System.Linq;
using Kleene;

namespace XmlTests
{
    public class ExpressionHelperTests
    {
        [Fact]
        public void EmptyElement()
        {
            // Given
            var structure = StructureHelper.CreateStructure(XElement.Parse(
                @"<foo/>"
            ));

            var expression = ExpressionHelper.ParseElement(XElement.Parse(
                @"<foo/>"
            ));

            // When
            var test = expression.Run(structure);
            var results = expression.Run(structure).SelectMany(x => x!.Collapse());

            // Then
            Assert.Collection(results, item => Assert.Equal(structure, item));
        }

        [Fact]
        public void EmptyElementAll()
        {
            // Given
            var structure = StructureHelper.CreateStructure(XElement.Parse(
                @"<foo/>"
            ));

            var expression = ExpressionHelper.ParseElement(XElement.Parse(
                @"<k:elem xmlns:k='http://hufford.io/kleene/xml'>
                    <k:name>
                        <k:ns><k:rep><k:any/></k:rep></k:ns>
                        <k:local><k:rep><k:any/></k:rep></k:local>
                    </k:name>
                    <k:attrs><k:rep><k:any/></k:rep></k:attrs>
                    <k:value><k:rep><k:any/></k:rep></k:value>
                </k:elem>"
            ));

            // When
            var results = expression.Run(structure).SelectMany(x => x!.Collapse());

            // Then
            Assert.Collection(results, item => Assert.Equal(structure, item));
        }

        [Fact]
        public void EmptyElementLong()
        {
            // Given
            var structure = StructureHelper.CreateStructure(XElement.Parse(
                @"<foo/>"
            ));

            var expression = ExpressionHelper.ParseElement(XElement.Parse(
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
            var results = expression.Run(structure).SelectMany(x => x!.Collapse());

            // Then
            Assert.Collection(results, item => Assert.Equal(structure, item));
        }

        [Fact]
        public void EmptyElementNameShorthand()
        {
            // Given
            var structure = StructureHelper.CreateStructure(XElement.Parse(
                @"<foo/>"
            ));

            var expression = ExpressionHelper.ParseElement(XElement.Parse(
                @"<k:elem xmlns:k='http://hufford.io/kleene/xml'>
                    <k:name><foo/></k:name>
                    <k:attrs/>
                    <k:value/>
                </k:elem>"
            ));

            // When
            var results = expression.Run(structure).SelectMany(x => x!.Collapse());

            // Then
            Assert.Collection(results, item => Assert.Equal(structure, item));
        }

        [Fact]
        public void EmptyElementNS()
        {
            // Given
            var structure = StructureHelper.CreateStructure(XElement.Parse(
                @"<foo xmlns='http://example.com'/>"
            ));

            var expression = ExpressionHelper.ParseElement(XElement.Parse(
                @"<foo xmlns='http://example.com'/>"
            ));

            // When
            var results = expression.Run(structure).SelectMany(x => x!.Collapse());

            // Then
            Assert.Collection(results, item => Assert.Equal(structure, item));
        }

        [Fact]
        public void EmptyElementNSLong()
        {
            // Given
            var structure = StructureHelper.CreateStructure(XElement.Parse(
                @"<foo xmlns='http://example.com'/>"
            ));

            var expression = ExpressionHelper.ParseElement(XElement.Parse(
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
            var results = expression.Run(structure).SelectMany(x => x!.Collapse());

            // Then
            Assert.Collection(results, item => Assert.Equal(structure, item));
        }

        [Fact]
        public void EmptyElementNSNameShorthand()
        {
            // Given
            var structure = StructureHelper.CreateStructure(XElement.Parse(
                @"<foo xmlns='http://example.com'/>"
            ));

            var expression = ExpressionHelper.ParseElement(XElement.Parse(
                @"<k:elem xmlns:k='http://hufford.io/kleene/xml' xmlns='http://example.com'>
                    <k:name><foo/></k:name>
                    <k:attrs/>
                    <k:value/>
                </k:elem>"
            ));

            // When
            var results = expression.Run(structure).SelectMany(x => x!.Collapse());

            // Then
            Assert.Collection(results, item => Assert.Equal(structure, item));
        }

        [Fact]
        public void EmptyElementNSPrefix()
        {
            var structure = StructureHelper.CreateStructure(XElement.Parse(
                @"<ex:foo xmlns:ex='http://example.com'/>"
            ));

            var expression = ExpressionHelper.ParseElement(XElement.Parse(
                @"<ex:foo xmlns:ex='http://example.com'/>"
            ));

            // When
            var results = expression.Run(structure).SelectMany(x => x!.Collapse());

            // Then
            Assert.Collection(results, item => Assert.Equal(structure, item));
        }

        [Fact]
        public void Attribute()
        {
            var structure = StructureHelper.CreateStructure(XElement.Parse(
                @"<foo bar='123'/>"
            ));

            var expression = ExpressionHelper.ParseElement(XElement.Parse(
                @"<foo bar='123'/>"
            ));

            // When
            var results = expression.Run(structure).SelectMany(x => x!.Collapse());

            // Then
            Assert.Collection(results, item => Assert.Equal(structure, item));
        }

        [Fact]
        public void AttributesAll()
        {
            var structure = StructureHelper.CreateStructure(XElement.Parse(
                @"<foo bar='123'/>"
            ));

            var expression = ExpressionHelper.ParseElement(XElement.Parse(
                @"<k:elem xmlns:k='http://hufford.io/kleene/xml'>
                    <k:name>
                        <k:ns/>
                        <k:local>foo</k:local>
                    </k:name>
                    <k:attrs><k:rep><k:any/></k:rep></k:attrs>
                    <k:value/>
                </k:elem>"
            ));

            // When
            var results = expression.Run(structure).SelectMany(x => x!.Collapse());

            // Then
            Assert.Collection(results, item => Assert.Equal(structure, item));
        }

        [Fact]
        public void AttributeAll()
        {
            var structure = StructureHelper.CreateStructure(XElement.Parse(
                @"<foo bar='123'/>"
            ));

            var expression = ExpressionHelper.ParseElement(XElement.Parse(
                @"<k:elem xmlns:k='http://hufford.io/kleene/xml'>
                    <k:name>
                        <k:ns/>
                        <k:local>foo</k:local>
                    </k:name>
                    <k:attrs>
                        <k:attr>
                            <k:name>
                                <k:ns><k:rep><k:any/></k:rep></k:ns>
                                <k:local><k:rep><k:any/></k:rep></k:local>
                            </k:name>
                            <k:value><k:rep><k:any/></k:rep></k:value>
                        </k:attr>
                    </k:attrs>
                    <k:value/>
                </k:elem>"
            ));

            // When
            var results = expression.Run(structure).SelectMany(x => x!.Collapse());

            // Then
            Assert.Collection(results, item => Assert.Equal(structure, item));
        }

        [Fact]
        public void AttributeLong()
        {
            var structure = StructureHelper.CreateStructure(XElement.Parse(
                @"<foo bar='123'/>"
            ));

            var expression = ExpressionHelper.ParseElement(XElement.Parse(
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
            var results = expression.Run(structure).SelectMany(x => x!.Collapse());

            // Then
            Assert.Collection(results, item => Assert.Equal(structure, item));
        }

        [Fact]
        public void AttributeNameShorthand()
        {
            var structure = StructureHelper.CreateStructure(XElement.Parse(
                @"<foo bar='123'/>"
            ));

            var expression = ExpressionHelper.ParseElement(XElement.Parse(
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
            var results = expression.Run(structure).SelectMany(x => x!.Collapse());

            // Then
            Assert.Collection(results, item => Assert.Equal(structure, item));
        }

        [Fact]
        public void AttributeShorthand()
        {
            var structure = StructureHelper.CreateStructure(XElement.Parse(
                @"<foo bar='123'/>"
            ));

            var expression = ExpressionHelper.ParseElement(XElement.Parse(
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
            var results = expression.Run(structure).SelectMany(x => x!.Collapse());

            // Then
            Assert.Collection(results, item => Assert.Equal(structure, item));
        }

        [Fact]
        public void AttributeNS()
        {
            var structure = StructureHelper.CreateStructure(XElement.Parse(
                @"<foo ex:bar='123' xmlns:ex='http://example.com'/>"
            ));

            var expression = ExpressionHelper.ParseElement(XElement.Parse(
                @"<foo ex:bar='123' xmlns:ex='http://example.com'/>"
            ));

            // When
            var results = expression.Run(structure).SelectMany(x => x!.Collapse());

            // Then
            Assert.Collection(results, item => Assert.Equal(structure, item));
        }

        [Fact]
        public void AttributeNSLong()
        {
            var structure = StructureHelper.CreateStructure(XElement.Parse(
                @"<foo ex:bar='123' xmlns:ex='http://example.com'/>"
            ));

            var expression = ExpressionHelper.ParseElement(XElement.Parse(
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
            var results = expression.Run(structure).SelectMany(x => x!.Collapse());

            // Then
            Assert.Collection(results, item => Assert.Equal(structure, item));
        }

        [Fact]
        public void AttributeNSNameShorthand()
        {
            var structure = StructureHelper.CreateStructure(XElement.Parse(
                @"<foo ex:bar='123' xmlns:ex='http://example.com'/>"
            ));

            var expression = ExpressionHelper.ParseElement(XElement.Parse(
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
            var results = expression.Run(structure).SelectMany(x => x!.Collapse());

            // Then
            Assert.Collection(results, item => Assert.Equal(structure, item));
        }

        [Fact]
        public void AttributeNSShorthand()
        {
            var structure = StructureHelper.CreateStructure(XElement.Parse(
                @"<foo ex:bar='123' xmlns:ex='http://example.com'/>"
            ));

            var expression = ExpressionHelper.ParseElement(XElement.Parse(
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
            var results = expression.Run(structure).SelectMany(x => x!.Collapse());

            // Then
            Assert.Collection(results, item => Assert.Equal(structure, item));
        }

        [Fact]
        public void TwoAttributes()
        {
            var structure = StructureHelper.CreateStructure(XElement.Parse(
                @"<foo bar='123' baz='wat'/>"
            ));

            var expression = ExpressionHelper.ParseElement(XElement.Parse(
                @"<foo bar='123' baz='wat'/>"
            ));

            // When
            var results = expression.Run(structure).SelectMany(x => x!.Collapse());

            // Then
            Assert.Collection(results, item => Assert.Equal(structure, item));
        }

        [Fact]
        public void TwoAttributesLong()
        {
            var structure = StructureHelper.CreateStructure(XElement.Parse(
                @"<foo bar='123' baz='wat'/>"
            ));

            var expression = ExpressionHelper.ParseElement(XElement.Parse(
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
            var results = expression.Run(structure).SelectMany(x => x!.Collapse());

            // Then
            Assert.Collection(results, item => Assert.Equal(structure, item));
        }

        [Fact]
        public void TextChild()
        {
            var structure = StructureHelper.CreateStructure(XElement.Parse(
                @"<foo>Hello, world!</foo>"
            ));

            var expression = ExpressionHelper.ParseElement(XElement.Parse(
                @"<foo>Hello, world!</foo>"
            ));

            // When
            var results = expression.Run(structure).SelectMany(x => x!.Collapse());

            // Then
            Assert.Collection(results, item => Assert.Equal(structure, item));
        }

        [Fact]
        public void TextChildAll()
        {
            var structure = StructureHelper.CreateStructure(XElement.Parse(
                @"<foo>Hello, world!</foo>"
            ));

            var expression = ExpressionHelper.ParseElement(XElement.Parse(
                @"<foo xmlns:k='http://hufford.io/kleene/xml'><k:rep><k:any/></k:rep></foo>"
            ));

            // When
            var results = expression.Run(structure).SelectMany(x => x!.Collapse());

            // Then
            Assert.Collection(results, item => Assert.Equal(structure, item));
        }

        [Fact]
        public void TextChildLong()
        {
            var structure = StructureHelper.CreateStructure(XElement.Parse(
                @"<foo>Hello, world!</foo>"
            ));

            var expression = ExpressionHelper.ParseElement(XElement.Parse(
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
            var results = expression.Run(structure).SelectMany(x => x!.Collapse());

            // Then
            Assert.Collection(results, item => Assert.Equal(structure, item));
        }

        [Fact]
        public void WhitespaceChild()
        {
            var structure = StructureHelper.CreateStructure(XElement.Parse(
                @"<foo>    </foo>"
            ));

            var expression = ExpressionHelper.ParseElement(XElement.Parse(
                @"<foo>    </foo>"
            ));

            // When
            var results = expression.Run(structure).SelectMany(x => x!.Collapse());

            // Then
            Assert.Collection(results, item => Assert.Equal(structure, item));
        }

        [Fact]
        public void ElementChild()
        {
            var structure = StructureHelper.CreateStructure(XElement.Parse(
                @"<foo>
                    <bar/>
                </foo>"
            ));

            var expression = ExpressionHelper.ParseElement(XElement.Parse(
                @"<foo>
                    <bar/>
                </foo>"
            ));

            // When
            var results = expression.Run(structure).SelectMany(x => x!.Collapse());

            // Then
            Assert.Collection(results, item => Assert.Equal(structure, item));
        }

        [Fact]
        public void ElementChildAll()
        {
            var structure = StructureHelper.CreateStructure(XElement.Parse(
                @"<foo>
                    <bar/>
                </foo>"
            ));

            var expression = ExpressionHelper.ParseElement(XElement.Parse(
                @"<foo xmlns:k='http://hufford.io/kleene/xml'><k:rep><k:any/></k:rep></foo>"
            ));

            // When
            var results = expression.Run(structure).SelectMany(x => x!.Collapse());

            // Then
            Assert.Collection(results, item => Assert.Equal(structure, item));
        }

        [Fact]
        public void ElementChildLong()
        {
            var structure = StructureHelper.CreateStructure(XElement.Parse(
                @"<foo>
                    <bar/>
                </foo>"
            ));

            var expression = ExpressionHelper.ParseElement(XElement.Parse(
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
            var results = expression.Run(structure).SelectMany(x => x!.Collapse());

            // Then
            Assert.Collection(results, item => Assert.Equal(structure, item));
        }

        [Fact]
        public void ElementChildElementShorthand()
        {
            var structure = StructureHelper.CreateStructure(XElement.Parse(
                @"<foo>
                    <bar/>
                </foo>"
            ));

            var expression = ExpressionHelper.ParseElement(XElement.Parse(
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
            var results = expression.Run(structure).SelectMany(x => x!.Collapse());

            // Then
            Assert.Collection(results, item => Assert.Equal(structure, item));
        }

        [Fact]
        public void TwoElementChildrenLong()
        {
            var structure = StructureHelper.CreateStructure(XElement.Parse(
                @"<foo>
                    <bar/>
                    <baz/>
                </foo>"
            ));

            var expression = ExpressionHelper.ParseElement(XElement.Parse(
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
            var results = expression.Run(structure).SelectMany(x => x!.Collapse());

            // Then
            Assert.Collection(results, item => Assert.Equal(structure, item));
        }

        [Fact]
        public void TwoElementChildrenAll()
        {
            var structure = StructureHelper.CreateStructure(XElement.Parse(
                @"<foo>
                    <bar/>
                    <baz/>
                </foo>"
            ));

            var expression = ExpressionHelper.ParseElement(XElement.Parse(
                @"<foo xmlns:k='http://hufford.io/kleene/xml'><k:rep><k:any/></k:rep></foo>"
            ));

            // When
            var results = expression.Run(structure).SelectMany(x => x!.Collapse());

            // Then
            Assert.Collection(results, item => Assert.Equal(structure, item));
        }

        [Fact]
        public void TextAndElementChildren()
        {
            var structure = StructureHelper.CreateStructure(XElement.Parse(
                @"<foo><bar/>baz</foo>"
            ));

            var expression = ExpressionHelper.ParseElement(XElement.Parse(
                @"<foo><bar/>baz</foo>"
            ));

            // When
            var results = expression.Run(structure).SelectMany(x => x!.Collapse());

            // Then
            Assert.Collection(results, item => Assert.Equal(structure, item));
        }

        [Fact]
        public void TextAndElementChildrenAll()
        {
            var structure = StructureHelper.CreateStructure(XElement.Parse(
                @"<foo><bar/>baz</foo>"
            ));

            var expression = ExpressionHelper.ParseElement(XElement.Parse(
                @"<k:elem xmlns:k='http://hufford.io/kleene/xml'>
                    <k:name>
                        <k:ns/>
                        <k:local>foo</k:local>
                    </k:name>
                    <k:attrs/>
                    <k:value>
                        <k:rep><k:any/></k:rep>
                    </k:value>
                </k:elem>"
            ));

            // When
            var results = expression.Run(structure).SelectMany(x => x!.Collapse());

            // Then
            Assert.Collection(results, item => Assert.Equal(structure, item));
        }

        [Fact]
        public void TextAndElementChildrenMixed()
        {
            var structure = StructureHelper.CreateStructure(XElement.Parse(
                @"<foo>baz<bar/></foo>"
            ));

            var expression = ExpressionHelper.ParseElement(XElement.Parse(
                @"<k:elem xmlns:k='http://hufford.io/kleene/xml'>
                    <k:name>
                        <k:ns/>
                        <k:local>foo</k:local>
                    </k:name>
                    <k:attrs/>
                    <k:value>baz<bar/></k:value>
                </k:elem>"
            ));

            // When
            var results = expression.Run(structure).SelectMany(x => x!.Collapse());

            // Then
            Assert.Collection(results, item => Assert.Equal(structure, item));
        }
    }
}
