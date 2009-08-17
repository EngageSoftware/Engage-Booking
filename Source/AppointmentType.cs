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
    using DotNetNuke.Services.Localization;

    /// <summary>
    /// The type of an <see cref="Appointment"/>
    /// </summary>
    public class AppointmentType
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AppointmentType"/> class.
        /// </summary>
        /// <param name="id">The ID of this instance.</param>
        /// <param name="resourceKeyName">The name of the resource key for the name of this type.</param>
        private AppointmentType(int id, string resourceKeyName)
        {
            this.Id = id;
            this.ResourceKeyName = resourceKeyName;
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
        /// Gets the name of the resource key in which is store the name of this type (i.e. the invariant name of the type that is stored in the database).
        /// </summary>
        /// <value>The name of the resource key for this type.</value>
        public string ResourceKeyName
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the localized name of this type.
        /// </summary>
        /// <value>The localized name.</value>
        public string Name
        {
            get
            {
                return Localization.GetString(this.ResourceKeyName, Utility.LocalSharedResourceFile);
            }
        }

        /// <summary>
        /// Gets the appointment type with the given <paramref name="appointmentTypeId"/>.
        /// </summary>
        /// <param name="appointmentTypeId">The ID of the appointment type.</param>
        /// <returns>An <see cref="AppointmentType"/> instance</returns>
        public static AppointmentType Load(int appointmentTypeId)
        {
            using (IDataReader appointmentTypeReader = AppointmentSqlDataProvider.GetAppointmentType(appointmentTypeId))
            {
                if (appointmentTypeReader.Read())
                {
                    return Fill(appointmentTypeReader);
                }
            }

            return null;
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
