// <copyright file="AppointmentType.cs" company="Engage Software">
// Engage: Booking - http://www.engagemodules.com
// Copyright (c) 2004-2008
// by Engage Software ( http://www.engagesoftware.com )
// </copyright>
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED 
// TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL 
// THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF 
// CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
// DEALINGS IN THE SOFTWARE.

namespace Engage.Dnn.Booking
{
    using System.Data;

    /// <summary>
    /// Instance of an AppointmentType
    /// </summary>
    public class AppointmentType
    {

        /// <summary>
        /// Gets the id of the type
        /// </summary>
        /// <value>The id of the type.</value>
        public int Id
        {
            get;
            private set;
        }
        
        /// <summary>
        /// Gets the name of the type
        /// </summary>
        /// <value>The name of the type.</value>
        public string Name
        {
            get;
            private set;
        }

        private AppointmentType(int id, string name)
        {
            Id = id;
            Name = name;
        }

        internal static AppointmentType Fill(IDataRecord record)
        {
            return new AppointmentType((int) record["AppointmentTypeId"], record["Name"].ToString());
        }

    }
}
