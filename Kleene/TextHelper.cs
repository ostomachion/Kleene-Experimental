using System;
using System.Linq;

namespace Kleene
{
    public static class TextHelper
    {
        public static ObjectSequence<Runnable<char>> ToRunnable(this string text)
        {
            return new ObjectSequence<Runnable<char>>(text.Select(c => new Runnable<char>(c)));
        }
    }
}