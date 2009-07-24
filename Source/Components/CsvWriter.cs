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
    using System.Collections.Generic;
    using System.Data;
    using System.IO;
    using System.Web;

    /// <summary>
    /// CsvWriter class
    /// (from http://knab.ws/blog/index.php?/archives/3-CSV-file-parser-and-writer-in-C-Part-1.html)
    /// </summary>
    public class CsvWriter
    {
        /// <summary>
        /// Writes a DataTable to string.
        /// </summary>
        /// <param name="table">A datatable.</param>
        /// <param name="header">if set to <c>true</c>, include a header row.</param>
        /// <param name="quoteall">if set to <c>true</c> enclose all cells/fields in quotes.</param>
        /// <returns>The CSV-formatted output generated from a specified datatable.</returns>
        public static string WriteToString(DataTable table, bool header, bool quoteall)
        {
            StringWriter writer = new StringWriter();
            WriteToStream(writer, table, header, quoteall);
            return writer.ToString();
        }

        /// <summary>
        /// Writes a DataTable to a TextWriter stream.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <param name="table">The table.</param>
        /// <param name="header">if set to <c>true</c> [header].</param>
        /// <param name="quoteall">if set to <c>true</c> [quoteall].</param>
        public static void WriteToStream(TextWriter stream, DataTable table, bool header, bool quoteall)
        {
            if (header)
            {
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    WriteItem(stream, table.Columns[i].Caption, quoteall);
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
                    WriteItem(stream, row[i], quoteall);
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
        /// Writes a single datatable row to a TextWriter stream.
        /// </summary>
        /// <param name="stream">The TextWriter stream.</param>
        /// <param name="item">The datatable row.</param>
        /// <param name="quoteall">if set to <c>true</c>, enclose all cells/fields in quotes.</param>
        private static void WriteItem(TextWriter stream, object item, bool quoteall)
        {
            if (item == null)
            {
                return;
            }

            string s = item.ToString();
            if (quoteall || s.IndexOfAny("\",\x0A\x0D".ToCharArray()) > -1)
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
