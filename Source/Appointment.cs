// <copyright file="Appointment.cs" company="Engage Software">
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
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Diagnostics;
    using System.Globalization;
    using System.Xml.Serialization;
    using Data;
    using Engage;
    using Framework.Templating;

    /// <summary>
    /// An event, with a title, description, location, and start and end date.
    /// </summary>
    [XmlRoot(ElementName = "appointment", IsNullable = false)]
    public class Appointment : IEditableObject, INotifyPropertyChanged, ITemplateable
    {

        /// <summary>
        /// Backing field for <see cref="PortalId"/>.
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private int portalId = -1;

        /// <summary>
        /// Backing field for <see cref="EndDateTime"/>.
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private DateTime endDateTime;

        /// <summary>
        /// Backing field for <see cref="startDateTime"/>.
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private DateTime startDateTime;

        /// <summary>
        /// Backing field for <see cref="Id"/>.
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private int id = -1;

        /// <summary>
        /// Backing field for <see cref="Description"/>.
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string description = string.Empty;

        /// <summary>
        /// Backing field for <see cref="Title"/>.
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string title = string.Empty;

        /// <summary>
        /// Backing field for <see cref="ModuleId"/>.
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private int moduleId = -1;

        /// <summary>
        /// Backing field for <see cref="AppointmentTypeId"/>.
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private int appointmentTypeId;

        /// <summary>
        /// Backing field for <see cref="Notes"/>.
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string notes;

        /// <summary>
        /// Backing field for <see cref="Address1"/>.
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string address1;

        /// <summary>
        /// Backing field for <see cref="Address2"/>.
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string address2;

        /// <summary>
        /// Backing field for <see cref="City"/>.
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string city;

        /// <summary>
        /// Backing field for <see cref="StateId"/>.
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private int stateId;

        /// <summary>
        /// Backing field for <see cref="Zip"/>.
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string zip;

        /// <summary>
        /// Backing field for <see cref="Phone"/>.
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string phone;

        private Appointment()
        {
        }

        private Appointment(int portalId, int moduleId, string organizerEmail, string title, string overview, string description, DateTime eventStart, DateTime eventEnd, TimeSpan timeZoneOffset, string location, bool isFeatured, bool allowRegistrations, int? capacity, bool inDaylightTime, string capacityMetMessage)
        {
            this.portalId = portalId;
            this.moduleId = moduleId;
            //this.organizerEmail = organizerEmail ?? string.Empty;
            //this.title = title;
            //this.overview = overview;
            //this.description = description;
            //this.eventStart = eventStart;
            //this.eventEnd = eventEnd;
            //this.timeZoneOffset = timeZoneOffset;
            //this.location = location;
            //this.isFeatured = isFeatured;
            //this.allowRegistrations = allowRegistrations;
            //this.recurrenceRule = recurrenceRule;
            //this.canceled = canceled;
            //this.capacity = capacity;
            //this.inDaylightTime = inDaylightTime;
            //this.capacityMetMessage = capacityMetMessage;
        }

        #region INotifyPropertyChanged Members

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region IEditableObject Members

        /// <summary>
        /// Begins an edit on an object.
        /// </summary>
        public void BeginEdit()
        {
        }

        /// <summary>
        /// Discards changes since the last <see cref="M:System.ComponentModel.IEditableObject.BeginEdit"/> call.
        /// </summary>
        public void CancelEdit()
        {
        }

        /// <summary>
        /// Pushes changes since the last <see cref="M:System.ComponentModel.IEditableObject.BeginEdit"/> or <see cref="M:System.ComponentModel.IBindingList.AddNew"/> call into the underlying object.
        /// </summary>
        public void EndEdit()
        {
        }

        #endregion

        /// <summary>
        /// Loads the specified appointment id.
        /// </summary>
        /// <param name="id">The appointment id.</param>
        /// <returns></returns>
        public static Appointment Load(int id)
        {
            IDataProvider dp = DataProvider.Instance;
            Appointment e = null;

            try
            {
                using (IDataReader reader = dp.ExecuteReader(CommandType.StoredProcedure, dp.NamePrefix + "spGetEvent", Engage.Utility.CreateIntegerParam("@EventId", id)))
                {
                    if (reader.Read())
                    {
                        e = Fill(reader);
                    }
                }
            }
            catch (Exception se)
            {
                throw new DBException("spGetAppointment", se);
            }

            return e;
        }

        /// <summary>
        /// Saves this event.
        /// </summary>
        /// <param name="revisingUser">The user who is saving this event.</param>
        public void Save(int revisingUser)
        {
            if (this.id < 0)
            {
                this.Insert(revisingUser);
            }
            else
            {
                this.Update(revisingUser);
            }
        }

        /// <summary>
        /// Gets the value of the property with the given <paramref name="propertyName"/>, or <see cref="string.Empty"/> if a property with that name does not exist on this object or is <c>null</c>.
        /// </summary>
        /// <remarks>
        /// To avoid conflicts with template syntax, avoid using the following symbols in the property name
        /// <list type="bullet">
        ///     <item><description>:</description></item>
        ///     <item><description>%</description></item>
        ///     <item><description>$</description></item>
        ///     <item><description>#</description></item>
        ///     <item><description>&gt;</description></item>
        ///     <item><description>&lt;</description></item>
        ///     <item><description>"</description></item>
        ///     <item><description>'</description></item>
        /// </list>
        /// </remarks>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns>The string representation of the value of this instance.</returns>
        public string GetValue(string propertyName)
        {
            return this.GetValue(propertyName, null);
        }

        /// <summary>
        /// Gets the value of the property with the given <paramref name="propertyName"/>, or <see cref="string.Empty"/> if a property with that name does not exist on this object or is <c>null</c>.
        /// </summary>
        /// <remarks>
        /// To avoid conflicts with template syntax, avoid using the following symbols in the property name
        /// <list type="bullet">
        ///     <item><description>:</description></item>
        ///     <item><description>%</description></item>
        ///     <item><description>$</description></item>
        ///     <item><description>#</description></item>
        ///     <item><description>&gt;</description></item>
        ///     <item><description>&lt;</description></item>
        ///     <item><description>"</description></item>
        ///     <item><description>'</description></item>
        /// </list>
        /// </remarks>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="format">A numeric or DateTime format string, or <c>null</c> or <see cref="string.Empty"/> to apply the default format.</param>
        /// <returns>The string representation of the value of this instance as specified by <paramref name="format"/>.</returns>
        public string GetValue(string propertyName, string format)
        {
            switch (propertyName.ToUpperInvariant())
            {
                case "ID":
                    return this.Id.ToString(format, CultureInfo.CurrentCulture);
                    //case "TITLE":
                    //    return this.Title;
                    //case "OVERVIEW":
                    //    return this.Overview;
                    //case "DESCRIPTION":
                    //    return this.Description;
                    //case "EVENTSTART":
                    //case "EVENT START":
                    //    return this.EventStart.ToString(format, CultureInfo.CurrentCulture);
                    //case "EVENTEND":
                    //case "EVENT END":
                    //    return this.EventEnd.ToString(format, CultureInfo.CurrentCulture);
                    //case "LOCATION":
                    //    return this.Location;
            }

            return string.Empty;
        }

        /// <summary>
        /// Deletes the specified appointment id.
        /// </summary>
        /// <param name="id">The appointment id.</param>
        public static void Delete(int id)
        {
            IDataProvider dp = DataProvider.Instance;

            try
            {
                dp.ExecuteNonQuery(CommandType.StoredProcedure, dp.NamePrefix + "spDeleteAppointment", Engage.Utility.CreateIntegerParam("@Appointment", id));
            }
            catch (Exception se)
            {
                throw new DBException("spDeleteAppointment", se);
            }           
        }

        /// <summary>
        /// Creates the specified event.
        /// </summary>
        /// <param name="portalId">The portal id.</param>
        /// <param name="moduleId">The module id.</param>
        /// <param name="organizerEmail">The organizer email.</param>
        /// <param name="title">The title of the event.</param>
        /// <param name="overview">The overview or description of the event.</param>
        /// <param name="description">The description.</param>
        /// <param name="eventStart">The event's start date and time.</param>
        /// <param name="eventEnd">The event end.</param>
        /// <param name="timeZoneOffset">The time zone offset.</param>
        /// <param name="location">The location of the event.</param>
        /// <param name="isFeatured">if set to <c>true</c> the event should be listed in featured displays.</param>
        /// <param name="allowRegistrations">if set to <c>true</c> this event allows users to register for it.</param>
        /// <param name="recurrenceRule">The recurrence rule.</param>
        /// <param name="capacity">The maximum number of registrants for this event, or <c>null</c> if there is no maximum.</param>
        /// <param name="inDaylightTime">if set to <c>true</c> this event occurs in Daylight Time.</param>
        /// <param name="capacityMetMessage">
        /// <c>null</c> or <see cref="string.Empty"/> to display a generic message.
        /// </param>
        /// <returns>A new event object.</returns>
        public static Appointment Create(int portalId, int moduleId, string organizerEmail, string title, string overview, string description, DateTime eventStart, DateTime eventEnd, TimeSpan timeZoneOffset, string location, bool isFeatured, bool allowRegistrations, int? capacity, bool inDaylightTime, string capacityMetMessage)
        {
            return new Appointment(portalId, moduleId, organizerEmail, title, overview, description, eventStart, eventEnd, timeZoneOffset, location, isFeatured, allowRegistrations, capacity, inDaylightTime, capacityMetMessage);
        }

        /// <summary>
        /// Fills an Appointment with the data in the specified <paramref name="appointmentRecord"/>.
        /// </summary>
        /// <param name="appointmentRecord">A pre-initialized data record that represents an Event instance.</param>
        /// <returns>An instantiated Appointment object.</returns>
        internal static Appointment Fill(IDataRecord appointmentRecord)
        {
            Appointment appointment = new Appointment();

            appointment.id = (int)appointmentRecord["AppointmentId"];
            appointment.moduleId = (int)appointmentRecord["ModuleId"];
            appointment.appointmentTypeId = (int)appointmentRecord["AppointmentTypeId"];
            appointment.portalId = (int)appointmentRecord["PortalId"];
            appointment.title = appointmentRecord["Title"].ToString();
            appointment.description = appointmentRecord["Description"].ToString();
            appointment.notes = appointmentRecord["Notes"].ToString();
            appointment.address1 = appointmentRecord["Address1"].ToString();
            appointment.address2 = appointmentRecord["Address2"].ToString();
            appointment.city = appointmentRecord["City"].ToString();
            appointment.stateId = (int)appointmentRecord["StateId"];
            appointment.zip = appointmentRecord["Zip"].ToString();
            appointment.phone = appointmentRecord["Phone"].ToString();
            //e.additionalAddressInfo = appointmentRecord["AdditionalAddressInfo"].ToString();
            //e.contactStreet = appointmentRecord["contactStreet"].ToString();
            //e.contactPhone = appointmentRecord["contactPhone"].ToString();
            //e.requestorName = appointmentRecord["requestorName"].ToString();
            //e.requestorPhoneType = appointmentRecord["requestorPhoneType"].ToString();
            //e.requestorPhone = appointmentRecord["requestorPhone"].ToString();
            //e.requestorEmail = appointmentRecord["requestorEmail"].ToString();
            //e.requestorAltPhoneType = appointmentRecord["requestorAltPhoneType"].ToString();
            //e.requestorAltPhone = appointmentRecord["requestorAltPhone"].ToString();
            //e.numberOfParticipants = appointmentRecord["numberOfParticipants"].ToString();
            //e.numberOfSpecialParticipants = appointmentRecord["numberOfSpecialParticipants"].ToString();
            //e.participantGender = appointmentRecord["participantGender"].ToString();
            //e.participantFlag = appointmentRecord["AdditionalAddressInfo"].ToString();
            //e.accepted = (bool)appointmentRecord["Accepted"];
            //e.creationDate = (DateTime)appointmentRecord["creationDate"];
            //e.revisionDate = (DateTime)appointmentRecord["RevisionDate"];

            return appointment;
        }

        /// <summary>
        /// Gets the id of this Appointment.
        /// </summary>
        /// <value>This <see cref="Appointment"/>'s id.</value>
        [XmlElement(Order = 1)]
        public int Id
        {
            [DebuggerStepThrough]
            get { return this.id; }
        }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title of this appointment.</value>
        [XmlElement(Order = 2)]
        public string Title
        {
            [DebuggerStepThrough]
            get { return this.title; }
            [DebuggerStepThrough]
            set { this.title = value; }
        }

        /// <summary>
        /// Gets or sets the Description.
        /// </summary>
        /// <value>The Description.</value>
        [XmlElement(Order = 8)]
        public string Description
        {
            [DebuggerStepThrough]
            get { return this.description; }
            [DebuggerStepThrough]
            set { this.description = value; }
        }

        /// <summary>
        /// Gets or sets the Notes.
        /// </summary>
        /// <value>The Notes.</value>
        [XmlElement(Order = 8)]
        public string Notes
        {
            [DebuggerStepThrough]
            get { return this.notes; }
            [DebuggerStepThrough]
            set { this.notes = value; }
        }

        /// <summary>
        /// Gets or sets the Address1.
        /// </summary>
        /// <value>The Address1.</value>
        [XmlElement(Order = 8)]
        public string Address1
        {
            [DebuggerStepThrough]
            get { return this.address1; }
            [DebuggerStepThrough]
            set { this.address1 = value; }
        }

        /// <summary>
        /// Gets or sets the Address2.
        /// </summary>
        /// <value>The Address2.</value>
        [XmlElement(Order = 9)]
        public string Address2
        {
            [DebuggerStepThrough]
            get { return this.address2; }
            [DebuggerStepThrough]
            set { this.address2 = value; }
        }

        /// <summary>
        /// Gets or sets the City.
        /// </summary>
        /// <value>The City.</value>
        [XmlElement(Order = 9)]
        public string City
        {
            [DebuggerStepThrough]
            get { return this.city; }
            [DebuggerStepThrough]
            set { this.city = value; }
        }

        /// <summary>
        /// Gets or sets the StateId.
        /// </summary>
        /// <value>The StateId.</value>
        [XmlElement(Order = 9)]
        public int StateId
        {
            [DebuggerStepThrough]
            get { return this.stateId; }
            [DebuggerStepThrough]
            set { this.stateId = value; }
        }

        /// <summary>
        /// Gets or sets the State.
        /// </summary>
        /// <value>The State.</value>
        [XmlElement(Order = 9)]
        public string State
        {
            [DebuggerStepThrough]
            //// TODO: lookup using DNN mechanisms.
                    get { return string.Empty; }
        }

        /// <summary>
        /// Gets or sets the Zip.
        /// </summary>
        /// <value>The Zip.</value>
        [XmlElement(Order = 9)]
        public string Zip
        {
            [DebuggerStepThrough]
            get { return this.zip; }
            [DebuggerStepThrough]
            set { this.zip = value; }
        }

        /// <summary>
        /// Gets or sets the Phone.
        /// </summary>
        /// <value>The Phone.</value>
        [XmlElement(Order = 9)]
        public string Phone
        {
            [DebuggerStepThrough]
            get { return this.phone; }
            [DebuggerStepThrough]
            set { this.phone = value; }
        }

        /// <summary>
        /// Gets the portal id.
        /// </summary>
        /// <value>The portal id.</value>
        public int PortalId
        {
            [DebuggerStepThrough]
            get { return this.portalId; }
        }

        /// <summary>
        /// Gets the module id.
        /// </summary>
        /// <value>The module id.</value>
        public int ModuleId
        {
            [DebuggerStepThrough]
            get { return this.moduleId; }
        }

        /// <summary>
        /// Gets or sets when the appointment starts.
        /// </summary>
        /// <value>The appointment's start date and time.</value>
        [XmlElement(Order = 9)]
        public DateTime StartDateTime
        {
            [DebuggerStepThrough]
            get { return this.startDateTime; }
            [DebuggerStepThrough]
            set { this.startDateTime = value; }
        }

        /// <summary>
        /// Gets or sets when this appointment ends.
        /// </summary>
        /// <value>The appointment's end date and time.</value>
        [XmlElement(Order = 10)]
        public DateTime EndDateTime
        {
            [DebuggerStepThrough]
            get { return this.endDateTime; }
            [DebuggerStepThrough]
            set { this.endDateTime = value; }
        }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The appointmentType Id of this appointment.</value>
        [XmlElement(Order = 2)]
        public int AppointmentTypeId
        {
            [DebuggerStepThrough]
            get { return this.appointmentTypeId; }
            [DebuggerStepThrough]
            set { this.appointmentTypeId = value; }
        }
        
        /// <summary>
        /// Creates an iCal representation of this appointment.
        /// </summary>
        /// <returns>An iCal representation of this appointment</returns>
        public string ToICal()
        {
            //string rule = this.RecurrenceRule != null ? this.RecurrenceRule.ToString() : null;
            //return Util.ICalUtil.Export(this.overview, this.location, new Appointment(this.Id, this.StartDateTime, this.EventEnd, this.Title, rule), true, this.TimeZoneOffset);
            return string.Empty;
        }

        /// <summary>
        /// Inserts this event.
        /// </summary>
        /// <param name="revisingUser">The user who is inserting this event.</param>
        /// <exception cref="DBException">If an error occurs while going to the database to insert the event</exception>
        private void Insert(int revisingUser)
        {
            IDataProvider dp = DataProvider.Instance;

            try
            {
                this.id = dp.ExecuteNonQuery(
                        CommandType.StoredProcedure,
                        dp.NamePrefix + "spInsertAppointment",
                        Utility.CreateIntegerParam("@PortalId", this.portalId),
                        Utility.CreateIntegerParam("@ModuleId", this.moduleId),
                        Utility.CreateVarcharParam("@Title", this.title),
                        Utility.CreateTextParam("@Description", this.description)
                        //Utility.CreateDateTimeParam("@EventStart", this.eventStart),
                        //Utility.CreateDateTimeParam("@EventEnd", this.eventEnd),
                        //Utility.CreateIntegerParam("@TimeZoneOffset", (int)this.timeZoneOffset.TotalMinutes),
                        //Utility.CreateVarcharParam("@Organizer", this.organizer),
                        //Utility.CreateVarcharParam("@OrganizerEmail", this.organizerEmail),
                        //Utility.CreateVarcharParam("@Location", this.location),
                        //Utility.CreateVarcharParam("@LocationUrl", this.locationUrl),
                        //Utility.CreateVarcharParam("@InvitationUrl", this.invitationUrl),
                        //Utility.CreateVarcharParam("@RecapUrl", this.recapUrl),
                        //Utility.CreateIntegerParam("@RecurrenceParentId", this.recurrenceParentId),
                        //Utility.CreateVarcharParam("@RecurrenceRule", this.recurrenceRule != null ? this.recurrenceRule.ToString() : null),
                        //Utility.CreateBitParam("@AllowRegistrations", this.allowRegistrations),
                        //Utility.CreateBitParam("@isFeatured", this.isFeatured),
                        //Utility.CreateIntegerParam("@CreatedBy", revisingUser),
                        //Utility.CreateDateTimeParam("@FinalRecurringEndDate", this.FinalRecurringEndDate),
                        //Utility.CreateIntegerParam("@Capacity", this.capacity),
                        //Utility.CreateBitParam("@InDaylightTime", this.inDaylightTime),
                        //Utility.CreateTextParam("@CapacityMetMessage", this.capacityMetMessage),
                        //Utility.CreateBitParam("@IsDeleted", this.isDeleted)
                        );
            }
            catch (SystemException de)
            {
                throw new DBException("spInsertEvent", de);
            }
        }

        /// <summary>
        /// Updates this event.
        /// </summary>
        /// <param name="revisingUser">The user responsible for updating this event.</param>
        /// <exception cref="DBException">If an error occurs while going to the database to update the event</exception>
        private void Update(int revisingUser)
        {
            IDataProvider dp = DataProvider.Instance;

            try
            {
                dp.ExecuteNonQuery(
                        CommandType.StoredProcedure,
                        dp.NamePrefix + "spUpdateAppointment",
                        Utility.CreateIntegerParam("@EventId", this.id),
                        Utility.CreateVarcharParam("@Title", this.title),
                        Utility.CreateTextParam("@Description", this.description)
                        ////Utility.CreateDateTimeParam("@EventStart", this.eventStart),
                        ////Utility.CreateDateTimeParam("@EventEnd", this.eventEnd),
                        ////Utility.CreateIntegerParam("@TimeZoneOffset", (int)this.timeZoneOffset.TotalMinutes),
                        ////Utility.CreateVarcharParam("@Organizer", this.organizer),
                        ////Utility.CreateVarcharParam("@OrganizerEmail", this.organizerEmail),
                        ////Utility.CreateVarcharParam("@Location", this.location),
                        ////Utility.CreateVarcharParam("@LocationUrl", this.locationUrl),
                        ////Utility.CreateVarcharParam("@InvitationUrl", this.invitationUrl),
                        ////Utility.CreateVarcharParam("@RecapUrl", this.recapUrl),
                        ////Utility.CreateTextParam("@RecurrenceRule", this.recurrenceRule != null ? this.recurrenceRule.ToString() : null),
                        ////Utility.CreateIntegerParam("@RecurrenceParentId", this.recurrenceParentId),
                        ////Utility.CreateBitParam("@AllowRegistrations", this.allowRegistrations),
                        ////Utility.CreateBitParam("@Canceled", this.canceled),
                        ////Utility.CreateBitParam("@isFeatured", this.isFeatured),
                        ////Utility.CreateIntegerParam("@RevisingUser", revisingUser),
                        ////Utility.CreateDateTimeParam("@FinalRecurringEndDate", this.FinalRecurringEndDate),
                        ////Utility.CreateIntegerParam("@Capacity", this.capacity),
                        ////Utility.CreateBitParam("@InDaylightTime", this.inDaylightTime),
                        ////Utility.CreateTextParam("@CapacityMetMessage", this.capacityMetMessage),
                        ////Utility.CreateBitParam("@IsDeleted", this.isDeleted)
                        );
            }
            catch (SystemException de)
            {
                throw new DBException("spUpdateEvent", de);
            }
        }
    }
}