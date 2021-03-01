using System;
using System.Linq;

namespace Kleene
{
    // TODO: This should be heavily optimized in the future.
    // TODO: Figure out namespaces.
    public static class TextHelper
    {
        public static Structure CreateStructure(char c, Structure? next = null)
        {
            return new Structure($"x{(int)c:X4}", null, next);
        }

        public static Structure? CreateStructure(string? text, Structure? next = null)
        {
            return text is null ? null : text.Any() ? CreateStructure(text[0], CreateStructure(text[1..], next) ?? next) : null;
        }
    }
}