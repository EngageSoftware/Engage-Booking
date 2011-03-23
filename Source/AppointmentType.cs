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
        /// Prevents a default instance of the <see cref="AppointmentType"/> class from being created. 
        /// </summary>
        private AppointmentType()
        {
            this.Id = -1;
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
        /// Gets or sets the name of this type.
        /// </summary>
        /// <value>The appointment type name.</value>
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the appointment type with the given <paramref name="appointmentTypeId"/>.
        /// </summary>
        /// <param name="appointmentTypeId">The ID of the appointment type.</param>
        /// <param name="moduleId">The module id.</param>
        /// <returns>An <see cref="AppointmentType"/> instance</returns>
        public static AppointmentType Load(int appointmentTypeId, int moduleId)
        {
            using (var appointmentTypeReader = AppointmentSqlDataProvider.GetAppointmentType(appointmentTypeId, moduleId))
            {
                if (appointmentTypeReader.Read())
                {
                    return Fill(appointmentTypeReader);
                }
            }

            return null;
        }

        /// <summary>
        /// Creates a new appointment type.
        /// </summary>
        /// <param name="name">The name of the appointment type.</param>
        /// <returns>A new <see cref="AppointmentType"/>.</returns>
        public static AppointmentType Create(string name)
        {
            var appointmentType = new AppointmentType 
            {
                Name = name
            };

            return appointmentType;
        }

        /// <summary>
        /// Deletes the specified appointment type id.
        /// </summary>
        /// <param name="appointmentTypeId">The appointment type id.</param>
        public static void Delete(int appointmentTypeId)
        {
            AppointmentSqlDataProvider.DeleteAppointmentType(appointmentTypeId);
        }

        /// <summary>
        /// Saves the appointment type.
        /// </summary>
        /// <param name="revisingUser">The revising user.</param>
        /// <param name="moduleId">The module Id.</param>
        public void Save(int revisingUser, int moduleId)
        {
            if (this.Id < 0)
            {
                this.Insert(revisingUser, moduleId);
            }
            else
            {
                this.Update(revisingUser);
            }
        }

        /// <summary>
        /// Fills an <see cref="AppointmentType"/> with the data in the specified <paramref name="appointmentTypeRecord"/>.
        /// </summary>
        /// <param name="appointmentTypeRecord">A pre-initialized data record that represents an <see cref="AppointmentType"/> instance.</param>
        /// <returns>An instantiated <see cref="AppointmentType"/> object.</returns>
        internal static AppointmentType Fill(IDataRecord appointmentTypeRecord)
        {
            var appointmentType = new AppointmentType 
            {
                Id = (int)appointmentTypeRecord["AppointmentTypeId"],
                Name = appointmentTypeRecord["Name"].ToString()
            };

            return appointmentType;
        }

        /// <summary>
        /// Inserts the appointment type.
        /// </summary>
        /// <param name="revisingUser">The revising user.</param>
        /// <param name="moduleId">The module Id.</param>
        private void Insert(int revisingUser, int moduleId)
        {
            AppointmentSqlDataProvider.InsertAppointmentType(this, revisingUser, moduleId);
        }

        /// <summary>
        /// Updates the appointment type.
        /// </summary>
        /// <param name="revisingUser"> The revising user. </param>
        private void Update(int revisingUser)
        {
            AppointmentSqlDataProvider.UpdateAppointmentType(this, revisingUser);
        }
    }
}
