// using System;
// using Xunit;
// using Kleene;
// using System.Linq;
// using System.Collections.Generic;

// namespace KleeneTests
// {
//     public class AnyExpressionTests
//     {
//         [Fact]
//         public void Any_ReturnsInput() {
//             // Given
//             var expression = new AnyExpression<Runnable<int>>();
//             var input = TextHelper.CreateStructure(new string(new [] { 'x' }));

//             // When
//             var results = expression.Run(input).SelectMany(x => x?.Collapse() ?? EnumerableExt.Yield<Structure?>(null));

//             // Then
//             Assert.Collection(results,
//                 result => {
//                     Assert.Equal(input, result);
//                 });
//         }
//     }
// }