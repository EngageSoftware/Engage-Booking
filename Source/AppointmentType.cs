// <copyright file="AppointmentType.cs" company="Engage Software">
// Engage: Booking - http://www.engagemodules.com
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
    using System.Data;

    /// <summary>
    /// The type of an <see cref="Appointment"/>
    /// </summary>
    public class AppointmentType
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AppointmentType"/> class.
        /// </summary>
        /// <param name="id">The ID of this instance.</param>
        /// <param name="name">The name of this type.</param>
        private AppointmentType(int id, string name)
        {
            this.Id = id;
            this.Name = name;
        }

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

        /// <summary>
        /// Fills an <see cref="AppointmentType"/> with the data in the specified <paramref name="appointmentTypeRecord"/>.
        /// </summary>
        /// <param name="appointmentTypeRecord">A pre-initialized data record that represents an <see cref="AppointmentType"/> instance.</param>
        /// <returns>An instantiated <see cref="AppointmentType"/> object.</returns>
        internal static AppointmentType Fill(IDataRecord appointmentTypeRecord)
        {
            return new AppointmentType((int)appointmentTypeRecord["AppointmentTypeId"], (string)appointmentTypeRecord["Name"]);
        }
    }
}
