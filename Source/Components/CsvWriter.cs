// <copyright file="CsvWriter.cs" company="Engage Software">
// Engage: Booking
// Copyright (c) 2004-2009
// by Engage Software ( http://www.engagesoftware.com )
// </copyright>
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED 
// TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL 
// THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF 
// CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
// DEALINGS IN THE SOFTWARE.

namespace Engage.Dnn.Booking
{
    using System;
    using System.Data;
    using System.Globalization;
    using System.IO;

    /// <summary>
    /// Writes the contents of a <see cref="DataTable"/> as comma-separated values 
    /// </summary>
    /// <remarks>
    /// from http://knab.ws/blog/index.php?/archives/3-CSV-file-parser-and-writer-in-C-Part-1.html
    /// </remarks>
    public static class CsvWriter
    {
        /// <summary>
        /// Writes a <see cref="DataTable"/> to string.
        /// </summary>
        /// <param name="table">The table of data to write as a CSV.</param>
        /// <param name="header">if set to <c>true</c>, include a header row.</param>
        /// <param name="quoteAll">if set to <c>true</c> enclose all cells/fields in quotes.</param>
        /// <returns>The CSV-formatted output generated from a specified <see cref="DataTable"/>.</returns>
        public static string WriteToString(DataTable table, bool header, bool quoteAll)
        {
            using (var writer = new StringWriter(CultureInfo.InvariantCulture))
            {
                WriteToStream(writer, table, header, quoteAll);
                return writer.ToString();
            }
        }

        /// <summary>
        /// Writes a <see cref="DataTable"/> to a <see cref="TextWriter"/> stream.
        /// </summary>
        /// <param name="stream">The stream to which the CSV should be written.</param>
        /// <param name="table">The table of data to write as a CSV.</param>
        /// <param name="header">if set to <c>true</c>, include a header row.</param>
        /// <param name="quoteAll">if set to <c>true</c> enclose all cells/fields in quotes.</param>
        public static void WriteToStream(TextWriter stream, DataTable table, bool header, bool quoteAll)
        {
            if (stream == null)
            {
                throw new ArgumentNullException("stream", "stream must not be null");
            }

            if (table == null)
            {
                throw new ArgumentNullException("table", "table must not be null");
            }

            if (header)
            {
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    WriteItem(stream, table.Columns[i].Caption, quoteAll);
                    if (i < table.Columns.Count - 1)
                    {
                        stream.Write(',');
                    }
                    else
                    {
                        stream.Write('\n');
                    }
                }
            }

            foreach (DataRow row in table.Rows)
            {
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    WriteItem(stream, row[i], quoteAll);
                    if (i < table.Columns.Count - 1)
                    {
                        stream.Write(',');
                    }
                    else
                    {
                        stream.Write('\n');
                    }
                }
            }
        }

        /// <summary>
        /// Writes a single <see cref="DataTable"/> row to a <see cref="TextWriter"/> stream.
        /// </summary>
        /// <param name="stream">The stream to which the CSV should be written.</param>
        /// <param name="item">The value of a field within a row in the <see cref="DataTable"/> being written.</param>
        /// <param name="quoteAll">if set to <c>true</c>, enclose all cells/fields in quotes.</param>
        private static void WriteItem(TextWriter stream, object item, bool quoteAll)
        {
            if (item == null)
            {
                return;
            }

            string s = item.ToString();
            if (quoteAll || s.IndexOfAny("\",\x0A\x0D".ToCharArray()) > -1)
            {
                stream.Write("\"" + s.Replace("\"", "\"\"") + "\"");
            }
            else
            {
                stream.Write(s);
            }
        }
    }
}
