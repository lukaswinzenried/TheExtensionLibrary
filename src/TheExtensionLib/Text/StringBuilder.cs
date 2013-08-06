using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Text
{
    public static class StringBuilderExtension
    {
        public static void AppendIfNotNullOrEmpty(this StringBuilder sb, string value)
        {
            if (!string.IsNullOrEmpty(value))
                sb.Append(value);
        }
        public static void AppendLineIfNotNullOrEmpty(this StringBuilder sb, string value)
        {
            if (!string.IsNullOrEmpty(value))
                sb.AppendLine(value);
        }
    }
}
