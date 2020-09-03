using System;
using System.Xml.Linq;
using Xunit;

namespace XmlTests
{
    public class UnitTest1
    {

        [Fact]
        public void Test()
        {
            var el = XElement.Parse("<root name='value'>foo<div class='a b c'>bar</div></root>");
            var structure = Kleene.Xml.Helper.CreateStructure(el);
            ;
        }
    }
}
