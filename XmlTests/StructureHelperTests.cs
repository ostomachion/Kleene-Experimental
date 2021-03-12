// using System;
// using Xunit;
// using Kleene.Xml;
// using System.Xml.Linq;
// using System.Linq;
// using Kleene;

// namespace XmlTests
// {
//     public class StructureHelperTests
//     {
//         [Fact]
//         public void EmptyElement()
//         {
//             var structure = StructureHelper.CreateStructure(XElement.Parse(
//                 @"<foo/>"
//             ));
            
//             var expected = new Structure("elem",
//                 new Structure("name",
//                     new Structure("ns",
//                         TextHelper.CreateStructure(""),
//                     new Structure("local",
//                         TextHelper.CreateStructure("foo"))),
//                 new Structure("attrs",
//                     null,
//                 new Structure("value")
//             )));

//             Assert.Equal(expected, structure);
//         }

//         [Fact]
//         public void EmptyElementNS()
//         {
//             var structure = StructureHelper.CreateStructure(XElement.Parse(
//                 @"<foo xmlns='http://example.com'/>"
//             ));

//             var expected = new Structure("elem",
//                 new Structure("name",
//                     new Structure("ns",
//                         TextHelper.CreateStructure("http://example.com"),
//                     new Structure("local",
//                         TextHelper.CreateStructure("foo"))),
//                 new Structure("attrs",
//                     null,
//                 new Structure("value")
//             )));

//             Assert.Equal(expected, structure);
//         }

//         [Fact]
//         public void EmptyElementNSPrefix()
//         {
//             var structure = StructureHelper.CreateStructure(XElement.Parse(
//                 @"<ex:foo xmlns:ex='http://example.com'/>"
//             ));

//             var expected = new Structure("elem",
//                 new Structure("name",
//                     new Structure("ns",
//                         TextHelper.CreateStructure("http://example.com"),
//                     new Structure("local",
//                         TextHelper.CreateStructure("foo"))),
//                 new Structure("attrs",
//                     null,
//                 new Structure("value")
//             )));

//             Assert.Equal(expected, structure);
//         }

//         [Fact]
//         public void Attribute()
//         {
//             var structure = StructureHelper.CreateStructure(XElement.Parse(
//                 @"<foo bar='123'/>"
//             ));

//             var expected = new Structure("elem",
//                 new Structure("name",
//                     new Structure("ns",
//                         TextHelper.CreateStructure(""),
//                     new Structure("local",
//                         TextHelper.CreateStructure("foo"))),
//                 new Structure("attrs",
//                     new Structure("attr",
//                         new Structure("name",
//                             new Structure("ns",
//                                 TextHelper.CreateStructure(""),
//                             new Structure("local",
//                                 TextHelper.CreateStructure("bar"))),
//                         new Structure("value",
//                             TextHelper.CreateStructure("123")))),
//                 new Structure("value")
//             )));

//             Assert.Equal(expected, structure);
//         }

//         [Fact]
//         public void AttributeNS()
//         {
//             var structure = StructureHelper.CreateStructure(XElement.Parse(
//                 @"<foo ex:bar='123' xmlns:ex='http://example.com'/>"
//             ));

//             var expected = new Structure("elem",
//                 new Structure("name",
//                     new Structure("ns",
//                         TextHelper.CreateStructure(""),
//                     new Structure("local",
//                         TextHelper.CreateStructure("foo"))),
//                 new Structure("attrs",
//                     new Structure("attr",
//                         new Structure("name",
//                             new Structure("ns",
//                                 TextHelper.CreateStructure("http://example.com"),
//                             new Structure("local",
//                                 TextHelper.CreateStructure("bar"))),
//                         new Structure("value",
//                             TextHelper.CreateStructure("123")))),
//                 new Structure("value")
//             )));

//             Assert.Equal(expected, structure);
//         }

//         [Fact]
//         public void TwoAttributes()
//         {
//             var structure = StructureHelper.CreateStructure(XElement.Parse(
//                 @"<foo bar='123' baz='wat'/>"
//             ));

//             var expected = new Structure("elem",
//                 new Structure("name",
//                     new Structure("ns",
//                         TextHelper.CreateStructure(""),
//                     new Structure("local",
//                         TextHelper.CreateStructure("foo"))),
//                 new Structure("attrs",
//                     new Structure("attr",
//                         new Structure("name",
//                             new Structure("ns",
//                                 TextHelper.CreateStructure(""),
//                             new Structure("local",
//                                 TextHelper.CreateStructure("bar"))),
//                         new Structure("value",
//                             TextHelper.CreateStructure("123"))),
//                     new Structure("attr",
//                         new Structure("name",
//                             new Structure("ns",
//                                 TextHelper.CreateStructure(""),
//                             new Structure("local",
//                                 TextHelper.CreateStructure("baz"))),
//                         new Structure("value",
//                             TextHelper.CreateStructure("wat"))))),
//                     new Structure("value")
//             )));

//             Assert.Equal(expected, structure);
//         }
        
//         [Fact]
//         public void TextChild()
//         {
//             var structure = StructureHelper.CreateStructure(XElement.Parse(
//                 @"<foo>Hello, world!</foo>"
//             ));

//             var expected = new Structure("elem",
//                 new Structure("name",
//                     new Structure("ns",
//                         TextHelper.CreateStructure(""),
//                     new Structure("local",
//                         TextHelper.CreateStructure("foo"))),
//                 new Structure("attrs",
//                     null,
//                 new Structure("value",
//                     TextHelper.CreateStructure("Hello, world!"))
//             )));

//             Assert.Equal(expected, structure);
//         }
        
//         [Fact]
//         public void WhitespaceChild()
//         {
//             var structure = StructureHelper.CreateStructure(XElement.Parse(
//                 @"<foo>    </foo>", LoadOptions.PreserveWhitespace
//             ));

//             var expected = new Structure("elem",
//                 new Structure("name",
//                     new Structure("ns",
//                         TextHelper.CreateStructure(""),
//                     new Structure("local",
//                         TextHelper.CreateStructure("foo"))),
//                 new Structure("attrs",
//                     null,
//                 new Structure("value",
//                     TextHelper.CreateStructure("    "))
//             )));

//             Assert.Equal(expected, structure);
//         }
        
//         [Fact]
//         public void ElementChild()
//         {
//             var structure = StructureHelper.CreateStructure(XElement.Parse(
//                 @"<foo>
//                     <bar/>
//                 </foo>"
//             ));

//             var expected = new Structure("elem",
//                 new Structure("name",
//                     new Structure("ns",
//                         TextHelper.CreateStructure(""),
//                     new Structure("local",
//                         TextHelper.CreateStructure("foo"))),
//                 new Structure("attrs",
//                     null,
//                 new Structure("value",
//                     new Structure("elem",
//                         new Structure("name",
//                             new Structure("ns",
//                                 TextHelper.CreateStructure(""),
//                             new Structure("local",
//                                 TextHelper.CreateStructure("bar"))),
//                         new Structure("attrs",
//                             null,
//                         new Structure("value")
//                     ))))
//             )));

//             Assert.Equal(expected, structure);
//         }
        
//         [Fact]
//         public void TwoElementChildren()
//         {
//             var structure = StructureHelper.CreateStructure(XElement.Parse(
//                 @"<foo>
//                     <bar/>
//                     <baz/>
//                 </foo>"
//             ));

//             var expected = new Structure("elem",
//                 new Structure("name",
//                     new Structure("ns",
//                         TextHelper.CreateStructure(""),
//                     new Structure("local",
//                         TextHelper.CreateStructure("foo"))),
//                 new Structure("attrs",
//                     null,
//                 new Structure("value",
//                     new Structure("elem",
//                         new Structure("name",
//                             new Structure("ns",
//                                 TextHelper.CreateStructure(""),
//                             new Structure("local",
//                                 TextHelper.CreateStructure("bar"))),
//                         new Structure("attrs",
//                             null,
//                         new Structure("value"))),
//                     new Structure("elem",
//                         new Structure("name",
//                             new Structure("ns",
//                                 TextHelper.CreateStructure(""),
//                             new Structure("local",
//                                 TextHelper.CreateStructure("baz"))),
//                         new Structure("attrs",
//                             null,
//                         new Structure("value"))))))
//             )));

//             Assert.Equal(expected, structure);
//         }
        
//         [Fact]
//         public void TextAndElementChildren()
//         {
//             var structure = StructureHelper.CreateStructure(XElement.Parse(
//                 @"<foo><bar/>baz</foo>"
//             ));

//             var expected = new Structure("elem",
//                 new Structure("name",
//                     new Structure("ns",
//                         TextHelper.CreateStructure(""),
//                     new Structure("local",
//                         TextHelper.CreateStructure("foo"))),
//                 new Structure("attrs",
//                     null,
//                 new Structure("value",
//                     new Structure("elem",
//                         new Structure("name",
//                             new Structure("ns",
//                                 TextHelper.CreateStructure(""),
//                             new Structure("local",
//                                 TextHelper.CreateStructure("bar"))),
//                         new Structure("attrs",
//                             null,
//                         new Structure("value"))),
//                    TextHelper.CreateStructure("baz")))
//             )));

//             Assert.Equal(expected, structure);
//         }
//     }
// }
