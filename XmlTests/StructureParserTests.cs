using System;
using Xunit;
using Kleene.Xml;
using System.Xml.Linq;
using System.Linq;
using Kleene;

namespace XmlTests
{
    public class StructureParserTests
    {
        [Fact]
        public void EmptyElement()
        {
            var structure = StructureParser.ParseElement(XElement.Parse(
                @"<foo/>"
            ));
            
            Assert.Equal("", String.Join("", structure.Name.NS.Value.Select(x => x.Value)));
            Assert.Equal("foo", String.Join("", structure.Name.Local.Value.Select(x => x.Value)));
            Assert.Empty(structure.Attributes.Value);
            Assert.Empty(structure.Value);
        }

        [Fact]
        public void EmptyElementNS()
        {
            var structure = StructureParser.ParseElement(XElement.Parse(
                @"<foo xmlns='http://example.com'/>"
            ));
            
            Assert.Equal("http://example.com", String.Join("", structure.Name.NS.Value.Select(x => x.Value)));
            Assert.Equal("foo", String.Join("", structure.Name.Local.Value.Select(x => x.Value)));
            Assert.Empty(structure.Attributes.Value);
            Assert.Empty(structure.Value);
        }

        [Fact]
        public void EmptyElementNSPrefix()
        {
            var structure = StructureParser.ParseElement(XElement.Parse(
                @"<ex:foo xmlns:ex='http://example.com'/>"
            ));
            
            Assert.Equal("http://example.com", String.Join("", structure.Name.NS.Value.Select(x => x.Value)));
            Assert.Equal("foo", String.Join("", structure.Name.Local.Value.Select(x => x.Value)));
            Assert.Empty(structure.Attributes.Value);
            Assert.Empty(structure.Value);
        }

        [Fact]
        public void Attribute()
        {
            var structure = StructureParser.ParseElement(XElement.Parse(
                @"<foo bar='123'/>"
            ));
            
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

        [Fact]
        public void AttributeNS()
        {
            var structure = StructureParser.ParseElement(XElement.Parse(
                @"<foo ex:bar='123' xmlns:ex='http://example.com'/>"
            ));
            
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

        [Fact]
        public void TwoAttributes()
        {
            var structure = StructureParser.ParseElement(XElement.Parse(
                @"<foo bar='123' baz='wat'/>"
            ));
            
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
        
        [Fact]
        public void TextChild()
        {
            var structure = StructureParser.ParseElement(XElement.Parse(
                @"<foo>Hello, world!</foo>"
            ));
            
            Assert.Equal("", String.Join("", structure.Name.NS.Value.Select(x => x.Value)));
            Assert.Equal("foo", String.Join("", structure.Name.Local.Value.Select(x => x.Value)));
            Assert.Empty(structure.Attributes.Value);
            Assert.All(structure.Value, x => Assert.IsType<ConstantStructure<char>>(x));
            Assert.Equal("Hello, world!", String.Join("", structure.Value.Cast<ConstantStructure<char>>().Select(x => x.Value)));
        }
        
        [Fact]
        public void WhitespaceChild()
        {
            var structure = StructureParser.ParseElement(XElement.Parse(
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
        
        [Fact]
        public void TwoElementChildren()
        {
            var structure = StructureParser.ParseElement(XElement.Parse(
                @"<foo>
                    <bar/>
                    <baz/>
                </foo>"
            ));
            
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
        
        [Fact]
        public void TextAndElementChildren()
        {
            var structure = StructureParser.ParseElement(XElement.Parse(
                @"<foo><bar/>baz</foo>"
            ));
            
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
    }
}
