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
            var result = expression.Run(new [] { structure }, 0);
            
            // Then
            Assert.Collection(result,
                branch => Assert.Collection(branch,
                    item => {
                        Assert.IsType<ElementStructure>(item);
                        var structure = (ElementStructure)item;
                        Assert.Equal("", String.Join("", structure.Name.NS.Value.Select(x => x.Value)));
                        Assert.Equal("foo", String.Join("", structure.Name.Local.Value.Select(x => x.Value)));
                        Assert.Empty(structure.Attributes.Value);
                        Assert.Empty(structure.Value);
                    }
                )
            );
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
            var result = expression.Run(new [] { structure }, 0);
            
            // Then
            Assert.Collection(result,
                branch => Assert.Collection(branch,
                    item => {
                        Assert.IsType<ElementStructure>(item);
                        var structure = (ElementStructure)item;
                        Assert.Equal("", String.Join("", structure.Name.NS.Value.Select(x => x.Value)));
                        Assert.Equal("foo", String.Join("", structure.Name.Local.Value.Select(x => x.Value)));
                        Assert.Empty(structure.Attributes.Value);
                        Assert.Empty(structure.Value);
                    }
                )
            );
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
            var result = expression.Run(new [] { structure }, 0);
            
            // Then
            Assert.Collection(result,
                branch => Assert.Collection(branch,
                    item => {
                        Assert.IsType<ElementStructure>(item);
                        var structure = (ElementStructure)item;
                        Assert.Equal("", String.Join("", structure.Name.NS.Value.Select(x => x.Value)));
                        Assert.Equal("foo", String.Join("", structure.Name.Local.Value.Select(x => x.Value)));
                        Assert.Empty(structure.Attributes.Value);
                        Assert.Empty(structure.Value);
                    }
                )
            );
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
            var result = expression.Run(new [] { structure }, 0);
            
            // Then
            Assert.Collection(result,
                branch => Assert.Collection(branch,
                    item => {
                        Assert.IsType<ElementStructure>(item);
                        var structure = (ElementStructure)item;
                        Assert.Equal("", String.Join("", structure.Name.NS.Value.Select(x => x.Value)));
                        Assert.Equal("foo", String.Join("", structure.Name.Local.Value.Select(x => x.Value)));
                        Assert.Empty(structure.Attributes.Value);
                        Assert.Empty(structure.Value);
                    }
                )
            );
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
            var result = expression.Run(new [] { structure }, 0);
            
            // Then
            Assert.Collection(result,
                branch => Assert.Collection(branch,
                    item => {
                        Assert.IsType<ElementStructure>(item);
                        var structure = (ElementStructure)item;
                        Assert.Equal("http://example.com", String.Join("", structure.Name.NS.Value.Select(x => x.Value)));
                        Assert.Equal("foo", String.Join("", structure.Name.Local.Value.Select(x => x.Value)));
                        Assert.Empty(structure.Attributes.Value);
                        Assert.Empty(structure.Value);
                    }
                )
            );
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
            var result = expression.Run(new [] { structure }, 0);
            
            // Then
            Assert.Collection(result,
                branch => Assert.Collection(branch,
                    item => {
                        Assert.IsType<ElementStructure>(item);
                        var structure = (ElementStructure)item;
                        Assert.Equal("http://example.com", String.Join("", structure.Name.NS.Value.Select(x => x.Value)));
                        Assert.Equal("foo", String.Join("", structure.Name.Local.Value.Select(x => x.Value)));
                        Assert.Empty(structure.Attributes.Value);
                        Assert.Empty(structure.Value);
                    }
                )
            );
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
            var result = expression.Run(new [] { structure }, 0);
            
            // Then
            Assert.Collection(result,
                branch => Assert.Collection(branch,
                    item => {
                        Assert.IsType<ElementStructure>(item);
                        var structure = (ElementStructure)item;
                        Assert.Equal("http://example.com", String.Join("", structure.Name.NS.Value.Select(x => x.Value)));
                        Assert.Equal("foo", String.Join("", structure.Name.Local.Value.Select(x => x.Value)));
                        Assert.Empty(structure.Attributes.Value);
                        Assert.Empty(structure.Value);
                    }
                )
            );
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
            var result = expression.Run(new [] { structure }, 0);
            
            // Then
            Assert.Collection(result,
                branch => Assert.Collection(branch,
                    item => {
                        Assert.IsType<ElementStructure>(item);
                        var structure = (ElementStructure)item;
                        Assert.Equal("http://example.com", String.Join("", structure.Name.NS.Value.Select(x => x.Value)));
                        Assert.Equal("foo", String.Join("", structure.Name.Local.Value.Select(x => x.Value)));
                        Assert.Empty(structure.Attributes.Value);
                        Assert.Empty(structure.Value);
                    }
                )
            );
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
            var result = expression.Run(new [] { structure }, 0);
            
            // Then
            Assert.Collection(result,
                branch => Assert.Collection(branch,
                    item => {
                        Assert.IsType<ElementStructure>(item);
                        var structure = (ElementStructure)item;
                        Assert.Equal("", String.Join("", structure.Name.NS.Value.Select(x => x.Value)));
                        Assert.Equal("foo", String.Join("", structure.Name.Local.Value.Select(x => x.Value)));
                        Assert.Collection(structure.Attributes.Value,
                            attr => {
                                Assert.Equal("", String.Join("", attr.Name.NS.Value.Select(x => x.Value)));
                                Assert.Equal("bar", String.Join("", attr.Name.Local.Value.Select(x => x.Value)));
                                Assert.Equal("123", String.Join("", attr.Value.Value.Select(x => x.Value)));
                            });
                        Assert.Empty(structure.Value);
                    }
                )
            );
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
            var result = expression.Run(new [] { structure }, 0);
            
            // Then
            Assert.Collection(result,
                branch => Assert.Collection(branch,
                    item => {
                        Assert.IsType<ElementStructure>(item);
                        var structure = (ElementStructure)item;
                        Assert.Equal("", String.Join("", structure.Name.NS.Value.Select(x => x.Value)));
                        Assert.Equal("foo", String.Join("", structure.Name.Local.Value.Select(x => x.Value)));
                        Assert.Collection(structure.Attributes.Value,
                            attr => {
                                Assert.Equal("", String.Join("", attr.Name.NS.Value.Select(x => x.Value)));
                                Assert.Equal("bar", String.Join("", attr.Name.Local.Value.Select(x => x.Value)));
                                Assert.Equal("123", String.Join("", attr.Value.Value.Select(x => x.Value)));
                            });
                        Assert.Empty(structure.Value);
                    }
                )
            );
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
            var result = expression.Run(new [] { structure }, 0);
            
            // Then
            Assert.Collection(result,
                branch => Assert.Collection(branch,
                    item => {
                        Assert.IsType<ElementStructure>(item);
                        var structure = (ElementStructure)item;
                        Assert.Equal("", String.Join("", structure.Name.NS.Value.Select(x => x.Value)));
                        Assert.Equal("foo", String.Join("", structure.Name.Local.Value.Select(x => x.Value)));
                        Assert.Collection(structure.Attributes.Value,
                            attr => {
                                Assert.Equal("", String.Join("", attr.Name.NS.Value.Select(x => x.Value)));
                                Assert.Equal("bar", String.Join("", attr.Name.Local.Value.Select(x => x.Value)));
                                Assert.Equal("123", String.Join("", attr.Value.Value.Select(x => x.Value)));
                            });
                        Assert.Empty(structure.Value);
                    }
                )
            );
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
            var result = expression.Run(new [] { structure }, 0);
            
            // Then
            Assert.Collection(result,
                branch => Assert.Collection(branch,
                    item => {
                        Assert.IsType<ElementStructure>(item);
                        var structure = (ElementStructure)item;
                        Assert.Equal("", String.Join("", structure.Name.NS.Value.Select(x => x.Value)));
                        Assert.Equal("foo", String.Join("", structure.Name.Local.Value.Select(x => x.Value)));
                        Assert.Collection(structure.Attributes.Value,
                            attr => {
                                Assert.Equal("", String.Join("", attr.Name.NS.Value.Select(x => x.Value)));
                                Assert.Equal("bar", String.Join("", attr.Name.Local.Value.Select(x => x.Value)));
                                Assert.Equal("123", String.Join("", attr.Value.Value.Select(x => x.Value)));
                            });
                        Assert.Empty(structure.Value);
                    }
                )
            );
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
            var result = expression.Run(new [] { structure }, 0);
            
            // Then
            Assert.Collection(result,
                branch => Assert.Collection(branch,
                    item => {
                        Assert.IsType<ElementStructure>(item);
                        var structure = (ElementStructure)item;
                        Assert.Equal("", String.Join("", structure.Name.NS.Value.Select(x => x.Value)));
                        Assert.Equal("foo", String.Join("", structure.Name.Local.Value.Select(x => x.Value)));
                        Assert.Collection(structure.Attributes.Value,
                            attr => {
                                Assert.Equal("", String.Join("", attr.Name.NS.Value.Select(x => x.Value)));
                                Assert.Equal("bar", String.Join("", attr.Name.Local.Value.Select(x => x.Value)));
                                Assert.Equal("123", String.Join("", attr.Value.Value.Select(x => x.Value)));
                            });
                        Assert.Empty(structure.Value);
                    }
                )
            );
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
            var result = expression.Run(new [] { structure }, 0);
            
            // Then
            Assert.Collection(result,
                branch => Assert.Collection(branch,
                    item => {
                        Assert.IsType<ElementStructure>(item);
                        var structure = (ElementStructure)item;
                        Assert.Equal("", String.Join("", structure.Name.NS.Value.Select(x => x.Value)));
                        Assert.Equal("foo", String.Join("", structure.Name.Local.Value.Select(x => x.Value)));
                        Assert.Collection(structure.Attributes.Value,
                            attr => {
                                Assert.Equal("", String.Join("", attr.Name.NS.Value.Select(x => x.Value)));
                                Assert.Equal("bar", String.Join("", attr.Name.Local.Value.Select(x => x.Value)));
                                Assert.Equal("123", String.Join("", attr.Value.Value.Select(x => x.Value)));
                            });
                        Assert.Empty(structure.Value);
                    }
                )
            );
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
            var result = expression.Run(new [] { structure }, 0);
            
            // Then
            Assert.Collection(result,
                branch => Assert.Collection(branch,
                    item => {
                        Assert.IsType<ElementStructure>(item);
                        var structure = (ElementStructure)item;
                        Assert.Equal("", String.Join("", structure.Name.NS.Value.Select(x => x.Value)));
                        Assert.Equal("foo", String.Join("", structure.Name.Local.Value.Select(x => x.Value)));
                        Assert.Collection(structure.Attributes.Value,
                            attr => {
                                Assert.Equal("http://example.com", String.Join("", attr.Name.NS.Value.Select(x => x.Value)));
                                Assert.Equal("bar", String.Join("", attr.Name.Local.Value.Select(x => x.Value)));
                                Assert.Equal("123", String.Join("", attr.Value.Value.Select(x => x.Value)));
                            });
                        Assert.Empty(structure.Value);
                    }
                )
            );
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
            var result = expression.Run(new [] { structure }, 0);
            
            // Then
            Assert.Collection(result,
                branch => Assert.Collection(branch,
                    item => {
                        Assert.IsType<ElementStructure>(item);
                        var structure = (ElementStructure)item;
                        Assert.Equal("", String.Join("", structure.Name.NS.Value.Select(x => x.Value)));
                        Assert.Equal("foo", String.Join("", structure.Name.Local.Value.Select(x => x.Value)));
                        Assert.Collection(structure.Attributes.Value,
                            attr => {
                                Assert.Equal("http://example.com", String.Join("", attr.Name.NS.Value.Select(x => x.Value)));
                                Assert.Equal("bar", String.Join("", attr.Name.Local.Value.Select(x => x.Value)));
                                Assert.Equal("123", String.Join("", attr.Value.Value.Select(x => x.Value)));
                            });
                        Assert.Empty(structure.Value);
                    }
                )
            );
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
            var result = expression.Run(new [] { structure }, 0);
            
            // Then
            Assert.Collection(result,
                branch => Assert.Collection(branch,
                    item => {
                        Assert.IsType<ElementStructure>(item);
                        var structure = (ElementStructure)item;
                        Assert.Equal("", String.Join("", structure.Name.NS.Value.Select(x => x.Value)));
                        Assert.Equal("foo", String.Join("", structure.Name.Local.Value.Select(x => x.Value)));
                        Assert.Collection(structure.Attributes.Value,
                            attr => {
                                Assert.Equal("http://example.com", String.Join("", attr.Name.NS.Value.Select(x => x.Value)));
                                Assert.Equal("bar", String.Join("", attr.Name.Local.Value.Select(x => x.Value)));
                                Assert.Equal("123", String.Join("", attr.Value.Value.Select(x => x.Value)));
                            });
                        Assert.Empty(structure.Value);
                    }
                )
            );
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
            var result = expression.Run(new [] { structure }, 0);
            
            // Then
            Assert.Collection(result,
                branch => Assert.Collection(branch,
                    item => {
                        Assert.IsType<ElementStructure>(item);
                        var structure = (ElementStructure)item;
                        Assert.Equal("", String.Join("", structure.Name.NS.Value.Select(x => x.Value)));
                        Assert.Equal("foo", String.Join("", structure.Name.Local.Value.Select(x => x.Value)));
                        Assert.Collection(structure.Attributes.Value,
                            attr => {
                                Assert.Equal("http://example.com", String.Join("", attr.Name.NS.Value.Select(x => x.Value)));
                                Assert.Equal("bar", String.Join("", attr.Name.Local.Value.Select(x => x.Value)));
                                Assert.Equal("123", String.Join("", attr.Value.Value.Select(x => x.Value)));
                            });
                        Assert.Empty(structure.Value);
                    }
                )
            );
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
            var result = expression.Run(new [] { structure }, 0);
            
            // Then
            Assert.Collection(result,
                branch => Assert.Collection(branch,
                    item => {
                        Assert.IsType<ElementStructure>(item);
                        var structure = (ElementStructure)item;
                        Assert.Equal("", String.Join("", structure.Name.NS.Value.Select(x => x.Value)));
                        Assert.Equal("foo", String.Join("", structure.Name.Local.Value.Select(x => x.Value)));
                        Assert.Collection(structure.Attributes.Value,
                            attr => {
                                Assert.Equal("", String.Join("", attr.Name.NS.Value.Select(x => x.Value)));
                                Assert.Equal("bar", String.Join("", attr.Name.Local.Value.Select(x => x.Value)));
                                Assert.Equal("123", String.Join("", attr.Value.Value.Select(x => x.Value)));
                            },
                            attr => {
                                Assert.Equal("", String.Join("", attr.Name.NS.Value.Select(x => x.Value)));
                                Assert.Equal("baz", String.Join("", attr.Name.Local.Value.Select(x => x.Value)));
                                Assert.Equal("wat", String.Join("", attr.Value.Value.Select(x => x.Value)));
                            });
                        Assert.Empty(structure.Value);
                    }
                )
            );
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
            var result = expression.Run(new [] { structure }, 0);
            
            // Then
            Assert.Collection(result,
                branch => Assert.Collection(branch,
                    item => {
                        Assert.IsType<ElementStructure>(item);
                        var structure = (ElementStructure)item;
                        Assert.Equal("", String.Join("", structure.Name.NS.Value.Select(x => x.Value)));
                        Assert.Equal("foo", String.Join("", structure.Name.Local.Value.Select(x => x.Value)));
                        Assert.Collection(structure.Attributes.Value,
                            attr => {
                                Assert.Equal("", String.Join("", attr.Name.NS.Value.Select(x => x.Value)));
                                Assert.Equal("bar", String.Join("", attr.Name.Local.Value.Select(x => x.Value)));
                                Assert.Equal("123", String.Join("", attr.Value.Value.Select(x => x.Value)));
                            },
                            attr => {
                                Assert.Equal("", String.Join("", attr.Name.NS.Value.Select(x => x.Value)));
                                Assert.Equal("baz", String.Join("", attr.Name.Local.Value.Select(x => x.Value)));
                                Assert.Equal("wat", String.Join("", attr.Value.Value.Select(x => x.Value)));
                            });
                        Assert.Empty(structure.Value);
                    }
                )
            );
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
            var result = expression.Run(new [] { structure }, 0);
            
            // Then
            Assert.Collection(result,
                branch => Assert.Collection(branch,
                    item => {
                        Assert.IsType<ElementStructure>(item);
                        var structure = (ElementStructure)item;
                        Assert.Equal("", String.Join("", structure.Name.NS.Value.Select(x => x.Value)));
                        Assert.Equal("foo", String.Join("", structure.Name.Local.Value.Select(x => x.Value)));
                        Assert.Empty(structure.Attributes.Value);
                        Assert.All(structure.Value, x => Assert.IsType<ConstantStructure<char>>(x));
                        Assert.Equal("Hello, world!", String.Join("", structure.Value.Cast<ConstantStructure<char>>().Select(x => x.Value)));
                    }
                )
            );
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
            var result = expression.Run(new [] { structure }, 0);
            
            // Then
            Assert.Collection(result,
                branch => Assert.Collection(branch,
                    item => {
                        Assert.IsType<ElementStructure>(item);
                        var structure = (ElementStructure)item;
                        Assert.Equal("", String.Join("", structure.Name.NS.Value.Select(x => x.Value)));
                        Assert.Equal("foo", String.Join("", structure.Name.Local.Value.Select(x => x.Value)));
                        Assert.Empty(structure.Attributes.Value);
                        Assert.All(structure.Value, x => Assert.IsType<ConstantStructure<char>>(x));
                        Assert.Equal("Hello, world!", String.Join("", structure.Value.Cast<ConstantStructure<char>>().Select(x => x.Value)));
                    }
                )
            );
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
            var result = expression.Run(new [] { structure }, 0);
            
            // Then
            Assert.Collection(result,
                branch => Assert.Collection(branch,
                    item => {
                        Assert.IsType<ElementStructure>(item);
                        var structure = (ElementStructure)item;
                        Assert.Equal("", String.Join("", structure.Name.NS.Value.Select(x => x.Value)));
                        Assert.Equal("foo", String.Join("", structure.Name.Local.Value.Select(x => x.Value)));
                        Assert.Empty(structure.Attributes.Value);
                        Assert.All(structure.Value, x => Assert.IsType<ConstantStructure<char>>(x));
                        Assert.Equal("Hello, world!", String.Join("", structure.Value.Cast<ConstantStructure<char>>().Select(x => x.Value)));
                    }
                )
            );
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
            
            Assert.Equal("", String.Join("", structure.Name.NS.Value.Select(x => x.Value)));
            Assert.Equal("foo", String.Join("", structure.Name.Local.Value.Select(x => x.Value)));
            Assert.Empty(structure.Attributes.Value);
            Assert.Empty(structure.Value);
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
            var result = expression.Run(new [] { structure }, 0);
            
            // Then
            Assert.Collection(result,
                branch => Assert.Collection(branch,
                    item => {
                        Assert.IsType<ElementStructure>(item);
                        var structure = (ElementStructure)item;
                        Assert.Equal("", String.Join("", structure.Name.NS.Value.Select(x => x.Value)));
                        Assert.Equal("foo", String.Join("", structure.Name.Local.Value.Select(x => x.Value)));
                        Assert.Empty(structure.Attributes.Value);
                        Assert.Collection(structure.Value,
                            node => {
                                Assert.IsType<ElementStructure>(node);
                                Assert.Equal("", String.Join("", ((ElementStructure)node).Name.NS.Value.Select(x => x.Value)));
                                Assert.Equal("bar", String.Join("", ((ElementStructure)node).Name.Local.Value.Select(x => x.Value)));
                                Assert.Empty(((ElementStructure)node).Attributes.Value);
                                Assert.Empty(((ElementStructure)node).Value);
                            }
                        );
                    }
                )
            );
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
            var result = expression.Run(new [] { structure }, 0);
            
            // Then
            Assert.Collection(result,
                branch => Assert.Collection(branch,
                    item => {
                        Assert.IsType<ElementStructure>(item);
                        var structure = (ElementStructure)item;
                        Assert.Equal("", String.Join("", structure.Name.NS.Value.Select(x => x.Value)));
                        Assert.Equal("foo", String.Join("", structure.Name.Local.Value.Select(x => x.Value)));
                        Assert.Empty(structure.Attributes.Value);
                        Assert.Collection(structure.Value,
                            node => {
                                Assert.IsType<ElementStructure>(node);
                                Assert.Equal("", String.Join("", ((ElementStructure)node).Name.NS.Value.Select(x => x.Value)));
                                Assert.Equal("bar", String.Join("", ((ElementStructure)node).Name.Local.Value.Select(x => x.Value)));
                                Assert.Empty(((ElementStructure)node).Attributes.Value);
                                Assert.Empty(((ElementStructure)node).Value);
                            }
                        );
                    }
                )
            );
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
            var result = expression.Run(new [] { structure }, 0);
            
            // Then
            Assert.Collection(result,
                branch => Assert.Collection(branch,
                    item => {
                        Assert.IsType<ElementStructure>(item);
                        var structure = (ElementStructure)item;
                        Assert.Equal("", String.Join("", structure.Name.NS.Value.Select(x => x.Value)));
                        Assert.Equal("foo", String.Join("", structure.Name.Local.Value.Select(x => x.Value)));
                        Assert.Empty(structure.Attributes.Value);
                        Assert.Collection(structure.Value,
                            node => {
                                Assert.IsType<ElementStructure>(node);
                                Assert.Equal("", String.Join("", ((ElementStructure)node).Name.NS.Value.Select(x => x.Value)));
                                Assert.Equal("bar", String.Join("", ((ElementStructure)node).Name.Local.Value.Select(x => x.Value)));
                                Assert.Empty(((ElementStructure)node).Attributes.Value);
                                Assert.Empty(((ElementStructure)node).Value);
                            }
                        );
                    }
                )
            );
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
            var result = expression.Run(new [] { structure }, 0);
            
            // Then
            Assert.Collection(result,
                branch => Assert.Collection(branch,
                    item => {
                        Assert.IsType<ElementStructure>(item);
                        var structure = (ElementStructure)item;
                        Assert.Equal("", String.Join("", structure.Name.NS.Value.Select(x => x.Value)));
                        Assert.Equal("foo", String.Join("", structure.Name.Local.Value.Select(x => x.Value)));
                        Assert.Empty(structure.Attributes.Value);
                        Assert.Collection(structure.Value,
                            node => {
                                Assert.IsType<ElementStructure>(node);
                                Assert.Equal("", String.Join("", ((ElementStructure)node).Name.NS.Value.Select(x => x.Value)));
                                Assert.Equal("bar", String.Join("", ((ElementStructure)node).Name.Local.Value.Select(x => x.Value)));
                                Assert.Empty(((ElementStructure)node).Attributes.Value);
                                Assert.Empty(((ElementStructure)node).Value);
                            }
                        );
                    }
                )
            );
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
            var result = expression.Run(new [] { structure }, 0);
            
            // Then
            Assert.Collection(result,
                branch => Assert.Collection(branch,
                    item => {
                        Assert.IsType<ElementStructure>(item);
                        var structure = (ElementStructure)item;
                        Assert.Equal("", String.Join("", structure.Name.NS.Value.Select(x => x.Value)));
                        Assert.Equal("foo", String.Join("", structure.Name.Local.Value.Select(x => x.Value)));
                        Assert.Empty(structure.Attributes.Value);
                        Assert.Collection(structure.Value,
                            node => {
                                Assert.IsType<ElementStructure>(node);
                                Assert.Equal("", String.Join("", ((ElementStructure)node).Name.NS.Value.Select(x => x.Value)));
                                Assert.Equal("bar", String.Join("", ((ElementStructure)node).Name.Local.Value.Select(x => x.Value)));
                                Assert.Empty(((ElementStructure)node).Attributes.Value);
                                Assert.Empty(((ElementStructure)node).Value);
                            },
                            node => {
                                Assert.IsType<ElementStructure>(node);
                                Assert.Equal("", String.Join("", ((ElementStructure)node).Name.NS.Value.Select(x => x.Value)));
                                Assert.Equal("baz", String.Join("", ((ElementStructure)node).Name.Local.Value.Select(x => x.Value)));
                                Assert.Empty(((ElementStructure)node).Attributes.Value);
                                Assert.Empty(((ElementStructure)node).Value);
                            }
                        );
                    }
                )
            );
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
            var result = expression.Run(new [] { structure }, 0);
            
            // Then
            Assert.Collection(result,
                branch => Assert.Collection(branch,
                    item => {
                        Assert.IsType<ElementStructure>(item);
                        var structure = (ElementStructure)item;
                        Assert.Equal("", String.Join("", structure.Name.NS.Value.Select(x => x.Value)));
                        Assert.Equal("foo", String.Join("", structure.Name.Local.Value.Select(x => x.Value)));
                        Assert.Empty(structure.Attributes.Value);
                        Assert.Collection(structure.Value,
                            node => {
                                Assert.IsType<ElementStructure>(node);
                                Assert.Equal("", String.Join("", ((ElementStructure)node).Name.NS.Value.Select(x => x.Value)));
                                Assert.Equal("bar", String.Join("", ((ElementStructure)node).Name.Local.Value.Select(x => x.Value)));
                                Assert.Empty(((ElementStructure)node).Attributes.Value);
                                Assert.Empty(((ElementStructure)node).Value);
                            },
                            node => {
                                Assert.IsType<ElementStructure>(node);
                                Assert.Equal("", String.Join("", ((ElementStructure)node).Name.NS.Value.Select(x => x.Value)));
                                Assert.Equal("baz", String.Join("", ((ElementStructure)node).Name.Local.Value.Select(x => x.Value)));
                                Assert.Empty(((ElementStructure)node).Attributes.Value);
                                Assert.Empty(((ElementStructure)node).Value);
                            }
                        );
                    }
                )
            );
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
            var result = expression.Run(new [] { structure }, 0);
            
            // Then
            Assert.Collection(result,
                branch => Assert.Collection(branch,
                    item => {
                        Assert.IsType<ElementStructure>(item);
                        var structure = (ElementStructure)item;
                        Assert.Equal("", String.Join("", structure.Name.NS.Value.Select(x => x.Value)));
                        Assert.Equal("foo", String.Join("", structure.Name.Local.Value.Select(x => x.Value)));
                        Assert.Empty(structure.Attributes.Value);
                        Assert.Collection(structure.Value,
                            node => {
                                Assert.IsType<ElementStructure>(node);
                                Assert.Equal("", String.Join("", ((ElementStructure)node).Name.NS.Value.Select(x => x.Value)));
                                Assert.Equal("bar", String.Join("", ((ElementStructure)node).Name.Local.Value.Select(x => x.Value)));
                                Assert.Empty(((ElementStructure)node).Attributes.Value);
                                Assert.Empty(((ElementStructure)node).Value);
                            },
                            node => {
                                Assert.IsType<ConstantStructure<char>>(node);
                                Assert.Equal('b', ((ConstantStructure<char>)node).Value);
                            },
                            node => {
                                Assert.IsType<ConstantStructure<char>>(node);
                                Assert.Equal('a', ((ConstantStructure<char>)node).Value);
                            },
                            node => {
                                Assert.IsType<ConstantStructure<char>>(node);
                                Assert.Equal('z', ((ConstantStructure<char>)node).Value);
                            }
                        );
                    }
                )
            );
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
            var result = expression.Run(new [] { structure }, 0);
            
            // Then
            Assert.Collection(result,
                branch => Assert.Collection(branch,
                    item => {
                        Assert.IsType<ElementStructure>(item);
                        var structure = (ElementStructure)item;
                        Assert.Equal("", String.Join("", structure.Name.NS.Value.Select(x => x.Value)));
                        Assert.Equal("foo", String.Join("", structure.Name.Local.Value.Select(x => x.Value)));
                        Assert.Empty(structure.Attributes.Value);
                        Assert.Collection(structure.Value,
                            node => {
                                Assert.IsType<ElementStructure>(node);
                                Assert.Equal("", String.Join("", ((ElementStructure)node).Name.NS.Value.Select(x => x.Value)));
                                Assert.Equal("bar", String.Join("", ((ElementStructure)node).Name.Local.Value.Select(x => x.Value)));
                                Assert.Empty(((ElementStructure)node).Attributes.Value);
                                Assert.Empty(((ElementStructure)node).Value);
                            },
                            node => {
                                Assert.IsType<ConstantStructure<char>>(node);
                                Assert.Equal('b', ((ConstantStructure<char>)node).Value);
                            },
                            node => {
                                Assert.IsType<ConstantStructure<char>>(node);
                                Assert.Equal('a', ((ConstantStructure<char>)node).Value);
                            },
                            node => {
                                Assert.IsType<ConstantStructure<char>>(node);
                                Assert.Equal('z', ((ConstantStructure<char>)node).Value);
                            }
                        );
                    }
                )
            );
        }
    }
}
