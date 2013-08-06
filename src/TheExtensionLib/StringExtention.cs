using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml;
using System.Xml.Linq;
using Sgml;

namespace System
{
    public static class StringExtention
    {
        /// <summary>
        /// ReplaceDiacritics borrowed from http://code.commongroove.com/2011/04/29/c-string-extension-to-replace-accented-characters/
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns> 
        public static string ReplaceDiacritics(this string source)
        {
            string sourceInFormD = source.Normalize(NormalizationForm.FormD);
            var output = new StringBuilder();
            foreach (char c in sourceInFormD)
            {
                UnicodeCategory uc = CharUnicodeInfo.GetUnicodeCategory(c);
                if (uc != UnicodeCategory.NonSpacingMark)
                    output.Append(c);
            }
            return (output.ToString().Normalize(NormalizationForm.FormC));
        }

        /// <summary>
        /// Get plain text from from HTML
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string GetPlainTextFromHtml(this string source)
        {
            if (source.Trim() == string.Empty) 
                return string.Empty;

            using (var reader = new StringReader(source))
            using (var sgmlReader = new SgmlReader())
            {
                // setup SgmlReader
                sgmlReader.DocType = "HTML";
                sgmlReader.WhitespaceHandling = WhitespaceHandling.All;
                sgmlReader.CaseFolding = CaseFolding.ToLower;
                sgmlReader.InputStream = reader;

                string cleanReadString = sgmlReader.ReadOuterXml();
                if (string.IsNullOrEmpty(cleanReadString)) return string.Empty;

                ////this is a hotfix
                //cleanReadString = RemoveUnsupportedControlChars(cleanReadString);

                var xDoc = XDocument.Parse(cleanReadString);
                var root = xDoc.Root;
                return root != null
                           ? root.DescendantNodes()
                                 .Where(element => element.GetType() == typeof (XText))
                                 .Cast<XText>()
                                 .Where(element => !string.IsNullOrEmpty(element.Value.Trim()))
                                 .Select(element => element.Value.Trim()).Aggregate((s1, s2) => s1 + " " + s2)
                           : string.Empty;
            }
            
        }
    }
}
