// using System;
// using Xunit;
// using Kleene;
// using System.Linq;
// using System.Collections.Generic;

// namespace KleeneTests
// {
//     public class RepExpressionTests
//     {
//         [Fact]
//         public void NullExpression_Throws()
//         {
//             Assert.Throws<ArgumentNullException>(() =>
//             {
//                 new RepExpression(null!);
//             });
//         }

//         [Fact]
//         public void SimpleRepetition_ReturnsChars()
//         {
//             // Given
//             var expression = new RepExpression(
//                 (Expression)TextHelper.CreateStructure('x'));
//             var input = TextHelper.CreateStructure("xxxx");

//             // When
//             var result = expression.Run(input).SelectMany(x => x?.Collapse() ?? EnumerableExt.Yield<Structure?>(null));

//             // Then
//             Assert.Collection(result,
//                 branch =>a
//                 {
//                     Assert.Equal(branch, input);
//                 });
//         }
        
//         [Fact]
//         public void SingleRepetition_ReturnsChars()
//         {
//             // Given
//             var expression = new RepExpression(
//                 (Expression)TextHelper.CreateStructure("x"));
//             var input = TextHelper.CreateStructure("x");

//             // When
//             var result = expression.Run(input).SelectMany(x => x?.Collapse() ?? EnumerableExt.Yield<Structure?>(null));

//             // Then
//             Assert.Collection(result,
//                 branch =>
//                 {
//                     Assert.Equal(branch, input);
//                 });
//         }
        
//         [Fact]
//         public void EmptyInput_ReturnsEmpty()
//         {
//             // Given
//             var expression = new RepExpression(
//                 (Expression)TextHelper.CreateStructure("x"));
//             var input = TextHelper.CreateStructure("");

//             // When
//             var result = expression.Run(input).SelectMany(x => x?.Collapse() ?? EnumerableExt.Yield<Structure?>(null));

//             // Then
//             Assert.Collection(result,
//                 branch =>
//                 {
//                     Assert.Equal(branch, input);
//                 });
//         }
//     }
// }