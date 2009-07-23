// <copyright file="AppointmentTypeCollection.cs" company="Engage Software">
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
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;

    /// <summary>
    /// A strongly-typed collection of <see cref="Appointment"/> objects.
    /// </summary>
    /// <remarks>
    /// This class inherits from <see cref="BindingList{AppointmentType}"/> for future support.
    /// </remarks>
    public sealed class AppointmentTypeCollection : BindingList<AppointmentType>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AppointmentTypeCollection"/> class with the specified list.
        /// </summary>
        /// <param name="list">An <see cref="IList{AppointmentType}"/> of items to be contained in the <see cref="AppointmentCollection"/>.</param>
        private AppointmentTypeCollection(IList<AppointmentType> list)
            : base(list)
        {
        }

        /// <summary>
        /// Loads all appointment types
        /// </summary>
        /// <returns>
        /// A collection of appointment types.
        /// </returns>
        internal static AppointmentTypeCollection Load()
        {
            using (IDataReader reader = AppointmentSqlDataProvider.GetAppointmentTypes())
            {
                return FillAppointmentTypes(reader);
            }
        }

        /// <summary>
        /// Fills a collection of appointments.
        /// </summary>
        /// <param name="reader">An un-initialized data reader whose result set is a list of appointment types.</param>
        /// <returns>
        /// A collection of instantiated <see cref="AppointmentType"/> objects, as represented in <paramref name="reader"/>.
        /// </returns>
        private static AppointmentTypeCollection FillAppointmentTypes(IDataReader reader)
        {
            List<AppointmentType> appointmentTypes = new List<AppointmentType>();
            while (reader.Read())
            {
                appointmentTypes.Add(AppointmentType.Fill(reader));
            }

            return new AppointmentTypeCollection(appointmentTypes);
        }
    }
}
