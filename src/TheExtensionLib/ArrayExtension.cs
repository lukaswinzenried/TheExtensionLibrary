using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System
{
    public static class ArrayExtensions
    {
        public static T[] Rotate<T>(this T[] source)
        {
            var rotated = new T[source.Length];
            rotated[source.Length - 1] = source[0];
            Array.Copy(source, 1, rotated, 0, source.Length - 1);
            return rotated;
        }
    }
}
