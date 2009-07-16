// <copyright file="AppointmentCollection.cs" company="Engage Software">
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
    using System.ComponentModel;
    using System.Data;
    using System.Diagnostics;
    using Data;

    /// <summary>
    /// A strongly-typed collection of <see cref="BindingList{T}"/> objects.
    /// </summary>
    /// <remarks>
    /// This class inherits from <see cref="Appointment"/> for future support.
    /// </remarks>
    public class AppointmentCollection : BindingList<Appointment>
    {
        /// <summary>
        /// Backing field for <see cref="TotalRecords"/>.
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly int totalRecords;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppointmentCollection"/> class with the specified list.
        /// </summary>
        /// <param name="list">An <see cref="T:System.Collections.Generic.IList`1" /> of items to be contained in the <see cref="AppointmentCollection"/>.</param>
        /// <param name="totalRecords">The total number of appointments in this collection.</param>
        private AppointmentCollection(IList<Appointment> list, int totalRecords)
                : base(list)
        {
            this.totalRecords = totalRecords;
        }

        /// <summary>
        /// Gets the total number of appointments in this collection.
        /// </summary>
        /// <value>The total number of appointments in this collection.</value>
        public int TotalRecords
        {
            [DebuggerStepThrough]
            get { return this.totalRecords; }
        }

        /// <summary>
        /// Loads all appointments for a given module.
        /// </summary>
        /// <param name="moduleId">The ID of the module to which the appointments belong.</param>
        /// <returns>
        /// A collection of appointments.
        /// </returns>
        /// <exception cref="DBException">if there's an error while going to the database to retrieve the appointments</exception>
        public static AppointmentCollection Load(int moduleId)
        {
            return Load(moduleId, null, null, null);
        }

        /// <summary>
        /// Loads a page of appointments.
        /// </summary>
        /// <param name="moduleId">The ID of the module to which the appointments belong.</param>
        /// <param name="sortExpression">The property by which the appointments should be sorted.</param>
        /// <param name="pageIndex">The index of the page of appointments.</param>
        /// <param name="pageSize">Size of the page of appointments.</param>
        /// <returns>
        /// A page of appointments.
        /// </returns>
        /// <exception cref="DBException">if there's an error while going to the database to retrieve the appointments</exception>
        public static AppointmentCollection Load(int moduleId, string sortExpression, int pageIndex, int pageSize)
        {
            return Load(moduleId, sortExpression, (int?)pageIndex, pageSize);
        }

        /// <summary>
        /// Loads a page of appointments.
        /// </summary>
        /// <param name="moduleId">The ID of the module to which the appointments belong.</param>
        /// <param name="sortExpression">The property by which the appointments should be sorted.</param>
        /// <param name="pageIndex">The index of the page of appointments.</param>
        /// <param name="pageSize">Size of the page of appointments.</param>
        /// <returns>
        /// A page of appointments.
        /// </returns>
        /// <exception cref="DBException">if there's an error while going to the database to retrieve the appointments</exception>
        private static AppointmentCollection Load(int moduleId, string sortExpression, int? pageIndex, int? pageSize)
        {
            IDataProvider dp = DataProvider.Instance;
            try
            {
                using (
                        IDataReader reader = dp.ExecuteReader(
                                CommandType.StoredProcedure,
                                dp.NamePrefix + "spGetAppointments",
                                Engage.Utility.CreateIntegerParam("@moduleId", moduleId),
                                sortExpression == null ? null : Engage.Utility.CreateVarcharParam("@sortExpression", sortExpression),
                                Engage.Utility.CreateIntegerParam("@pageSize", pageSize),
                                Engage.Utility.CreateIntegerParam("@pageIndex", pageIndex)))
                {
                    return FillAppointments(reader, pageSize);
                }
            }
            catch (Exception exc)
            {
                throw new DBException("spGetAppointments", exc);
            }
        }

        /// <summary>
        /// Fills a collection of appointments from a <see cref="DataSet"/>.
        /// </summary>
        /// <param name="reader">An un-initialized data reader with two records.
        /// The first should be a single integer, representing the total number of appointments (non-paged) for the requested query.</param>
        /// The second should be a collection of records representing the appointments requested.
        /// <param name="pageSize">Size of the page.</param>
        /// <returns>
        /// A collection of instantiated <see cref="Appointment"/> object, as represented in <paramref name="reader"/>.
        /// </returns>
        /// <exception cref="DBException">Data reader did not have the expected structure.  An error must have occurred in the query.</exception>
        private static AppointmentCollection FillAppointments(IDataReader reader, int? pageSize)
        {
            if (!reader.Read())
            {
                throw new DBException("IDataReader had no results, expected two");
            }

            int totalCount = reader.GetInt32(0);
            List<Appointment> appointments = new List<Appointment>(pageSize ?? totalCount);

            if (!reader.NextResult())
            {
                throw new DBException("Expected a second result which was not there");
            }

            while (reader.Read())
            {
                appointments.Add(Appointment.Fill(reader));
            }

            return new AppointmentCollection(appointments, totalCount);
        }
    }
}