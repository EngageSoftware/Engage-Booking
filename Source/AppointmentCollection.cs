﻿// <copyright file="AppointmentCollection.cs" company="Engage Software">
// Engage: Events - http://www.engagemodules.com
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
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Diagnostics;
    using System.Web.UI.WebControls;
    using Data;
    using Framework.Templating;

    /// <summary>
    /// A strongly-typed collection of <see cref="BindingList{T}"/> objects.
    /// </summary>
    /// <remarks>
    /// This class inherits from <see cref="Appointment"/> for future support.
    /// </remarks>
    public class AppointmentCollection : BindingList<Appointment>, IEnumerable<ITemplateable>
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
        /// <param name="totalRecords">The total number of events in this collection.</param>
        private AppointmentCollection(IList<Appointment> list, int totalRecords)
                : base(list)
        {
            this.totalRecords = totalRecords;
        }

        /// <summary>
        /// Gets the total number of events in this collection.
        /// </summary>
        /// <value>The total number of events in this collection.</value>
        public int TotalRecords
        {
            [DebuggerStepThrough]
            get { return this.totalRecords; }
        }

        /// <summary>
        /// Loads a page of events based on the given <paramref name="listingMode"/>.
        /// </summary>
        /// <param name="portalId">The ID of the portal that the events are for.</param>
        /// <param name="listingMode">The listing mode.</param>
        /// <param name="showAll">if set to <c>true</c> included canceled events.</param>
        /// <param name="featuredOnly">if set to <c>true</c> only include events that are featured.</param>
        /// <returns>
        /// A page of events based on the given <paramref name="listingMode"/>.
        /// </returns>
        /// <exception cref="DBException">if there's an error while going to the database to retrieve the events</exception>
        public static AppointmentCollection Load(int portalId, ListingMode listingMode, bool showAll, bool featuredOnly)
        {
            return Load(portalId, listingMode, null, null, null, showAll, featuredOnly, false);
        }

        /// <summary>
        /// Loads a page of events based on the given <paramref name="listingMode"/>.
        /// </summary>
        /// <param name="portalId">The ID of the portal that the events are for.</param>
        /// <param name="listingMode">The listing mode.</param>
        /// <param name="sortExpression">The property by which the events should be sorted.</param>
        /// <param name="pageIndex">The index of the page of events.</param>
        /// <param name="pageSize">Size of the page of events.</param>
        /// <param name="showAll">if set to <c>true</c> included canceled events.</param>
        /// <param name="featuredOnly">if set to <c>true</c> only include events that are featured.</param>
        /// <returns>
        /// A page of events based on the given <paramref name="listingMode"/>.
        /// </returns>
        /// <exception cref="DBException">if there's an error while going to the database to retrieve the events</exception>
        public static AppointmentCollection Load(int portalId, ListingMode listingMode, string sortExpression, int pageIndex, int pageSize, bool showAll, bool featuredOnly)
        {
            return Load(portalId, listingMode, sortExpression, pageIndex, pageSize, showAll, featuredOnly, true);
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.IEnumerator`1"/> that can be used to iterate through the collection.
        /// </returns>
        IEnumerator<ITemplateable> IEnumerable<ITemplateable>.GetEnumerator()
        {
            foreach (Appointment @event in this)
            {
                yield return @event;
            }
        }

        /// <summary>
        /// Loads a page of events based on the given <paramref name="listingMode"/>.
        /// </summary>
        /// <param name="portalId">The ID of the portal that the events are for.</param>
        /// <param name="listingMode">The listing mode.</param>
        /// <param name="sortExpression">The property by which the events should be sorted.</param>
        /// <param name="pageIndex">The index of the page of events.</param>
        /// <param name="pageSize">Size of the page of events.</param>
        /// <param name="showAll">if set to <c>true</c> included canceled events.</param>
        /// <param name="featuredOnly">if set to <c>true</c> only include events that are featured.</param>
        /// <param name="processCollection">if set to <c>true</c> the collection should be sorted and paged, and each recurring event should be replaced by its earliest occurrence.</param>
        /// <returns>
        /// A page of events based on the given <paramref name="listingMode"/>.
        /// </returns>
        /// <exception cref="DBException">if there's an error while going to the database to retrieve the events</exception>
        private static AppointmentCollection Load(int portalId, ListingMode listingMode, string sortExpression, int? pageIndex, int? pageSize, bool showAll, bool featuredOnly, bool processCollection)
        {
            IDataProvider dp = DataProvider.Instance;
            try
            {
                DateTime? startDate;
                DateTime? endDate;

                switch (listingMode)
                {
                    case ListingMode.CurrentMonth:
                        startDate = DateTime.Today;
                        endDate = GetLastDayOfThisMonth();
                        break;
                    case ListingMode.Future:
                        startDate = GetFirstDayOfNextMonth();
                        endDate = null;
                        break;
                    case ListingMode.Past:
                        startDate = null;
                        endDate = DateTime.Today;
                        break;
                        ////case ListingMode.All:
                    default:
                        startDate = endDate = null;
                        break;
                }

                using (
                        IDataReader reader = dp.ExecuteReader(
                                CommandType.StoredProcedure,
                                dp.NamePrefix + "spGetAppointments",
                                Engage.Utility.CreateIntegerParam("@portalId", portalId)))
                {
                    return FillAppointments(reader, processCollection, pageIndex, pageSize, sortExpression, startDate, endDate);
                }
            }
            catch (Exception exc)
            {
                throw new DBException("spGetAppointments", exc);
            }
        }

        /// <summary>
        /// Gets the first day of next month.
        /// </summary>
        /// <returns>The first day of next month</returns>
        private static DateTime GetFirstDayOfNextMonth()
        {
            return new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddMonths(1);
        }

        /// <summary>
        /// Gets the end of the last day of this month.
        /// </summary>
        /// <returns>The just before midnight of the last day of this month</returns>
        private static DateTime GetLastDayOfThisMonth()
        {
            return GetFirstDayOfNextMonth().AddTicks(-1);
        }

        /// <summary>
        /// Fills a collection of events from a <see cref="DataSet"/>.
        /// </summary>
        /// <param name="reader">An un-initialized data reader with two records.
        /// The first should be a collection of records representing the events requested.
        /// The second should be a single integer, representing the total number of events (non-paged) for the requested query.</param>
        /// <param name="processCollection">if set to <c>true</c> the collection should be sorted and paged, and each recurring event should be replaced by its earliest occurrence.</param>
        /// <param name="pageIndex">Index of the page of events being retrieved.</param>
        /// <param name="pageSize">Size of the page (number of events) being retrieved.</param>
        /// <param name="sortExpression">The property by which we should sort.</param>
        /// <param name="startDate">The beginning date of the range of dates being retrieved.</param>
        /// <param name="endDate">The ending date of the range of dates being retrieved.</param>
        /// <returns>
        /// A collection of instantiated <see cref="Appointment"/> object, as represented in <paramref name="reader"/>.
        /// </returns>
        /// <exception cref="DBException">Data reader did not have the expected structure.  An error must have occurred in the query.</exception>
        private static AppointmentCollection FillAppointments(IDataReader reader, bool processCollection, int? pageIndex, int? pageSize, string sortExpression, DateTime? startDate, DateTime? endDate)
        {
            int? beginIndex = processCollection && pageIndex.HasValue && pageSize.HasValue ? pageIndex * pageSize : null;
            int? endIndex = beginIndex.HasValue ? (pageIndex + 1) * pageSize : null;
            List<Appointment> appointments = new List<Appointment>(pageSize ?? 0);

            while (reader.Read())
            {
                Appointment appointment = Appointment.Fill(reader);
                appointments.Add(appointment);
            }

            // After all events have been added (and recurring events outside of the date range have been removed),
            // we need to get the total count before we remove events from the list for paging
            int totalRecords = appointments.Count;

            // We don't need to sort or page if we are never ending, they should be sorted from the database in that case
            if (processCollection && endIndex.HasValue)
            {
                ProcessCollection(beginIndex, endIndex, sortExpression, appointments);
            }

            return new AppointmentCollection(appointments, totalRecords);
        }

        /// <summary>
        /// Adds an occurrence of <paramref name="masterAppointment"/> that fits within the given time span to <paramref name="events"/>.
        /// </summary>
        /// <param name="masterAppointment">The master event.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <param name="events">The list of events to which the occurrence will be added.</param>
        private static void AddEventOccurrence(Appointment masterAppointment, DateTime? startDate, DateTime? endDate, ICollection<Appointment> appointments)
        {
            //masterAppointment.RecurrenceRule.SetEffectiveRange(startDate ?? DateTime.MinValue, endDate ?? DateTime.MaxValue);

            //// Does anyone remember why we were doing this?  It caused bug EVT-536.
            //////int originalMaxOccurrences = masterAppointment.RecurrenceRule.Range.MaxOccurrences;
            //////masterAppointment.RecurrenceRule.Range.MaxOccurrences = int.MaxValue;

            //foreach (DateTime eventStart in masterAppointment.RecurrenceRule.Occurrences)
            //{
            //    appointments.Add(masterAppointment.CreateOccurrence(eventStart));
            //    break;
            //}

            ////masterAppointment.RecurrenceRule.Range.MaxOccurrences = originalMaxOccurrences;
        }

        /// <summary>
        /// Sorts and pages the collection.
        /// </summary>
        /// <param name="beginIndex">The index of the event which should begin the list.</param>
        /// <param name="endIndex">The index of the event which should end the list.</param>
        /// <param name="sortExpression">The name of the property on which the appointments should be sorted.</param>
        /// <param name="appointments">The events collection to sort and page.</param>
        private static void ProcessCollection(int? beginIndex, int? endIndex, string sortExpression, List<Appointment> appointments)
        {
            // TODO: we may need to remove this GenericComparer if performance becomes an issue
            GenericComparer<Appointment> eventComparer = new GenericComparer<Appointment>(sortExpression, SortDirection.Ascending);
            appointments.Sort(eventComparer);

            int endCount = appointments.Count - endIndex.Value;
            if (endCount > 0)
            {
                appointments.RemoveRange(endIndex.Value, endCount);
            }

            if (beginIndex > 0)
            {
                appointments.RemoveRange(0, Math.Min(beginIndex.Value, appointments.Count));
            }
        }
    }
}