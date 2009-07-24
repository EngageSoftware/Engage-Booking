// <copyright file="Appointment.cs" company="Engage Software">
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
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Diagnostics;
    using System.Xml.Serialization;
    using Data;
    using DotNetNuke.Common.Lists;
    using DotNetNuke.Common.Utilities;

    /// <summary>
    /// An event, with a title, description, location, and start and end date.
    /// </summary>
    [XmlRoot(ElementName = "appointment", IsNullable = false)]
    public class Appointment : IEditableObject, INotifyPropertyChanged
    {
        /// <summary>
        /// Prevents a default instance of the <see cref="Appointment"/> class from being created.
        /// </summary>
        private Appointment()
        {
            this.ModuleId = Null.NullInteger;
            this.Description = string.Empty;
            this.Title = string.Empty;
            this.AppointmentId = Null.NullInteger;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Appointment"/> class.
        /// </summary>
        /// <param name="moduleId">The module id.</param>
        /// <param name="appointmentTypeId">The appointment type id.</param>
        /// <param name="title">The title of the event.</param>
        /// <param name="description">The event description.</param>
        /// <param name="notes">The notes.</param>
        /// <param name="address1">The address1.</param>
        /// <param name="address2">The address2.</param>
        /// <param name="city">The city.</param>
        /// <param name="regionId">The region id.</param>
        /// <param name="postalCode">The postal code.</param>
        /// <param name="phone">The phone.</param>
        /// <param name="additionalAddressInfo">The additional address info.</param>
        /// <param name="contactStreet">The contact street.</param>
        /// <param name="contactPhone">The contact phone.</param>
        /// <param name="requestorName">Name of the requestor.</param>
        /// <param name="requestorPhoneType">Type of the requestor phone.</param>
        /// <param name="requestorPhone">The requestor phone.</param>
        /// <param name="requestorEmail">The requestor email.</param>
        /// <param name="requestorAltPhoneType">Type of the requestor alt phone.</param>
        /// <param name="requestorAltPhone">The requestor alt phone.</param>
        /// <param name="start">The start of the appointment.</param>
        /// <param name="end">The end of the appointment.</param>
        /// <param name="timeZoneOffset">The time zone offset.</param>
        /// <param name="numberOfParticipants">The number of participants.</param>
        /// <param name="participantGender">The participant gender.</param>
        /// <param name="participantFlag">The participant flag.</param>
        /// <param name="participantInstructions">The participant instructions.</param>
        /// <param name="numberOfSpecialParticipants">The number of special participants.</param>
        /// <param name="isAccepted">
        /// <c>null</c>if the <see cref="Appointment"/> has not bee accepted or declined yet, 
        /// otherwise <c>true</c> for an accepted appointment, or <c>false</c> for a declined appointment.
        /// </param>
        private Appointment(
                int moduleId,
                int appointmentTypeId,
                string title,
                string description,
                string notes,
                string address1,
                string address2,
                string city,
                int regionId,
                string postalCode,
                string phone,
                string additionalAddressInfo,
                string contactStreet,
                string contactPhone,
                string requestorName,
                string requestorPhoneType,
                string requestorPhone,
                string requestorEmail,
                string requestorAltPhoneType,
                string requestorAltPhone,
                DateTime start,
                DateTime end,
                TimeSpan timeZoneOffset,
                int numberOfParticipants,
                string participantGender,
                char participantFlag,
                string participantInstructions,
                int numberOfSpecialParticipants,
                bool? isAccepted)
            : this()
        {
            this.ModuleId = moduleId;
            this.AppointmentTypeId = appointmentTypeId;
            this.Title = title;
            this.Description = description;
            this.TimeZoneOffset = timeZoneOffset;
            this.Notes = notes;
            this.Address1 = address1;
            this.Address2 = address2;
            this.City = city;
            this.RegionId = regionId;
            this.PostalCode = postalCode;
            this.Phone = phone;
            this.AdditionalAddressInfo = additionalAddressInfo;
            this.ContactStreet = contactStreet;
            this.ContactPhone = contactPhone;
            this.RequestorName = requestorName;
            this.RequestorPhoneType = requestorPhoneType;
            this.RequestorPhone = requestorPhone;
            this.RequestorEmail = requestorEmail;
            this.RequestorAltPhoneType = requestorAltPhoneType;
            this.RequestorAltPhone = requestorAltPhone;
            this.StartDateTime = start;
            this.EndDateTime = end;
            this.NumberOfParticipants = numberOfParticipants;
            this.ParticipantGender = participantGender;
            this.IsPresenterSpecialYOrN = participantFlag;
            this.ParticipantInstructions = participantInstructions;
            this.NumberOfSpecialParticipants = numberOfSpecialParticipants;
            this.IsAccepted = isAccepted;
        }

        #region INotifyPropertyChanged Members

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        /// <summary>
        /// Gets the id of this Appointment.
        /// </summary>
        /// <value>This <see cref="Appointment"/>'s id.</value>
        [XmlElement(Order = 1)]
        public int AppointmentId
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title of this appointment.</value>
        [XmlElement(Order = 2)]
        public string Title
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        [XmlElement(Order = 3)]
        public string Description
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the Notes.
        /// </summary>
        /// <value>The Notes.</value>
        [XmlElement(Order = 4)]
        public string Notes
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the Address1.
        /// </summary>
        /// <value>The Address1.</value>
        [XmlElement(Order = 5)]
        public string Address1
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the Address2.
        /// </summary>
        /// <value>The Address2.</value>
        [XmlElement(Order = 6)]
        public string Address2
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the City.
        /// </summary>
        /// <value>The city in which this appointment takes place.</value>
        [XmlElement(Order = 7)]
        public string City
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the Region Id.
        /// </summary>
        /// <value>The Region Id.</value>
        [XmlElement(Order = 8)]
        public int? RegionId
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the Region.
        /// </summary>
        /// <value>The name of the region in which this appointment takes place.</value>
        [XmlIgnore]
        public string Region
        {
            [DebuggerStepThrough]
            get
            {
                if (this.RegionId.HasValue)
                {
                    var listController = new ListController();
                    var regionEntry = listController.GetListEntryInfo(this.RegionId.Value);
                    if (regionEntry != null)
                    {
                        return regionEntry.Text;
                    }
                }

                return string.Empty;
            }
        }

        /// <summary>
        /// Gets or sets the Postal Code.
        /// </summary>
        /// <value>The Postal Code.</value>
        [XmlElement(Order = 9)]
        public string PostalCode
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the Phone.
        /// </summary>
        /// <value>The Phone.</value>
        [XmlElement(Order = 10)]
        public string Phone
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the module ID.
        /// </summary>
        /// <value>The module ID.</value>
        public int ModuleId
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets when the appointment starts.
        /// </summary>
        /// <value>The appointment's start date and time.</value>
        [XmlElement(Order = 11)]
        public DateTime StartDateTime
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets when this appointment ends.
        /// </summary>
        /// <value>The appointment's end date and time.</value>
        [XmlElement(Order = 12)]
        public DateTime EndDateTime
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the ID of the appointment type.
        /// </summary>
        /// <value>The appointment type ID of this appointment.</value>
        [XmlElement(Order = 13)]
        public int AppointmentTypeId
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the additional address info.
        /// </summary>
        /// <value>The additional address info.</value>
        [XmlElement(Order = 14)]
        public string AdditionalAddressInfo
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the contact's street address.
        /// </summary>
        /// <value>The contact's street address.</value>
        [XmlElement(Order = 15)]
        public string ContactStreet
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the contact's phone number.
        /// </summary>
        /// <value>The contact's phone number.</value>
        [XmlElement(Order = 16)]
        public string ContactPhone
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the name of the requestor.
        /// </summary>
        /// <value>The name of the requestor.</value>
        [XmlElement(Order = 17)]
        public string RequestorName
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the type of <see cref="RequestorPhone"/>.
        /// </summary>
        /// <value>The type of <see cref="RequestorPhone"/>.</value>
        [XmlElement(Order = 18)]
        public string RequestorPhoneType
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the requestor's phone number.
        /// </summary>
        /// <value>The requestor's phone number.</value>
        [XmlElement(Order = 19)]
        public string RequestorPhone
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the requestor's email address.
        /// </summary>
        /// <value>The requestor's email address.</value>
        [XmlElement(Order = 20)]
        public string RequestorEmail
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the type of <see cref="RequestorAltPhone"/>.
        /// </summary>
        /// <value>The type of <see cref="RequestorAltPhone"/>.</value>
        [XmlElement(Order = 21)]
        public string RequestorAltPhoneType
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the requestor's alternate phone number.
        /// </summary>
        /// <value>The requestor's alternate phone number.</value>
        [XmlElement(Order = 22)]
        public string RequestorAltPhone
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the number of participants.
        /// </summary>
        /// <value>The number of participants.</value>
        [XmlElement(Order = 23)]
        public int NumberOfParticipants
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the gender of the group of participants (Male, Female, or Mixed).
        /// </summary>
        /// <value>The gender of the participant group.</value>
        [XmlElement(Order = 24)]
        public string ParticipantGender
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets any special instructions from the participants.
        /// </summary>
        /// <value>The participants' special instructions.</value>
        [XmlElement(Order = 25)]
        public string ParticipantInstructions
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the number of special participants.
        /// </summary>
        /// <value>The number of special participants.</value>
        [XmlElement(Order = 26)]
        public int NumberOfSpecialParticipants
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance has been accepted.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is accepted; otherwise, <c>false</c>.
        /// </value>
        [XmlElement(Order = 27)]
        public bool? IsAccepted
        {
            get; 
            set;
        }

        /// <summary>
        /// Gets or sets whether the presenter is "special" ('Y' if so, 'N' if not).
        /// </summary>
        /// <value>'Y' if the presenter is "special," otherwise 'N'.</value>
        [XmlElement(Order = 28)]
        public char IsPresenterSpecialYOrN
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the TimeZoneOffset.
        /// </summary>
        /// <value>The TimeZoneOffset of this appointment.</value>
        [XmlElement(Order = 29)]
        public TimeSpan TimeZoneOffset
        {
            get;
            set;
        }

        /// <summary>
        /// Loads the <see cref="Appointment"/> with the specified <paramref name="appointmentId"/>.
        /// </summary>
        /// <param name="appointmentId">The ID of the <see cref="Appointment"/> to load.</param>
        /// <returns>The <see cref="Appointment"/> instance with the given <paramref name="appointmentId"/>, or <c>null</c> if no <see cref="Appointment"/> exists with that ID</returns>
        public static Appointment Load(int appointmentId)
        {
            using (IDataReader reader = AppointmentSqlDataProvider.GetAppointment(appointmentId))
            {
                if (reader.Read())
                {
                    return Fill(reader);
                }

                return null;
            }
        }

        /// <summary>
        /// Creates the specified <see cref="Appointment"/>.
        /// </summary>
        /// <param name="moduleId">The module ID.</param>
        /// <param name="appointmentTypeId">The appointment type ID of this appointment.</param>
        /// <param name="title">The title of this appointment.</param>
        /// <param name="description">The description.</param>
        /// <param name="notes">The notes.</param>
        /// <param name="address1">The address1.</param>
        /// <param name="address2">The address2.</param>
        /// <param name="city">The city.</param>
        /// <param name="regionId">The region id.</param>
        /// <param name="postalCode">The postal code.</param>
        /// <param name="phone">The phone.</param>
        /// <param name="additionalAddressInfo">The additional address info.</param>
        /// <param name="contactStreet">The contact street.</param>
        /// <param name="contactPhone">The contact phone.</param>
        /// <param name="requestorName">Name of the requestor.</param>
        /// <param name="requestorPhoneType">Type of the requestor phone.</param>
        /// <param name="requestorPhone">The requestor phone.</param>
        /// <param name="requestorEmail">The requestor email.</param>
        /// <param name="requestorAltPhoneType">Type of the requestor alt phone.</param>
        /// <param name="requestorAltPhone">The requestor alt phone.</param>
        /// <param name="start">The start of the appointment.</param>
        /// <param name="end">The end of the appointment.</param>
        /// <param name="timeZoneOffset">The time zone offset.</param>
        /// <param name="numberOfParticipants">The number of participants.</param>
        /// <param name="participantGender">The participant gender.</param>
        /// <param name="isPresenterSpecialYOrN">The participant flag.</param>
        /// <param name="participantInstructions">The participant instructions.</param>
        /// <param name="numberOfSpecialParticipants">The number of special participants.</param>
        /// <param name="isAccepted">
        /// <c>null</c>if the <see cref="Appointment"/> has not bee accepted or declined yet, 
        /// otherwise <c>true</c> for an accepted appointment, or <c>false</c> for a declined appointment.
        /// </param>
        /// <returns>A new <see cref="Appointment"/> instance.</returns>
        public static Appointment Create(
            int moduleId,
            int appointmentTypeId,
            string title,
            string description,
            string notes,
            string address1,
            string address2,
            string city,
            int regionId,
            string postalCode,
            string phone,
            string additionalAddressInfo,
            string contactStreet,
            string contactPhone,
            string requestorName,
            string requestorPhoneType,
            string requestorPhone,
            string requestorEmail,
            string requestorAltPhoneType,
            string requestorAltPhone,
            DateTime start,
            DateTime end,
            TimeSpan timeZoneOffset,
            int numberOfParticipants,
            string participantGender,
            char isPresenterSpecialYOrN,
            string participantInstructions,
            int numberOfSpecialParticipants,
            bool? isAccepted)
        {
            return new Appointment(
                    moduleId,
                    appointmentTypeId,
                    title,
                    description,
                    notes,
                    address1,
                    address2,
                    city,
                    regionId,
                    postalCode,
                    phone,
                    additionalAddressInfo,
                    contactStreet,
                    contactPhone,
                    requestorName,
                    requestorPhoneType,
                    requestorPhone,
                    requestorEmail,
                    requestorAltPhoneType,
                    requestorAltPhone,
                    start,
                    end,
                    timeZoneOffset,
                    numberOfParticipants,
                    participantGender,
                    isPresenterSpecialYOrN,
                    participantInstructions,
                    numberOfSpecialParticipants,
                    isAccepted);
        }

        /// <summary>
        /// Deletes the specified appointment id.
        /// </summary>
        /// <param name="id">The appointment id.</param>
        public static void Delete(int id)
        {
            AppointmentSqlDataProvider.DeleteAppointment(id);
        }

        /// <summary>
        /// Accepts the <see cref="Appointment"/> with the given <paramref name="appointmentId"/>.
        /// </summary>
        /// <param name="appointmentId">The ID of the <see cref="Appointment"/> to accept.</param>
        /// <param name="revisingUserId">The ID of the user accepting the <see cref="Appointment"/>.</param>
        public static void Accept(int appointmentId, int revisingUserId)
        {
            var appointment = Load(appointmentId);
            if (appointment != null)
            {
                AppointmentSqlDataProvider.SetAppointmentAcceptance(appointment.AppointmentId, true, revisingUserId);
                EmailService.SendAcceptanceEmail(appointment);
            }
        }

        /// <summary>
        /// Declines the <see cref="Appointment"/> with the given <paramref name="appointmentId"/>.
        /// </summary>
        /// <param name="appointmentId">The ID of the <see cref="Appointment"/> to decline.</param>
        /// <param name="declineReason">The reason for declining the appointment request.</param>
        /// <param name="revisingUserId">The ID of the user declining the <see cref="Appointment"/>.</param>
        public static void Decline(int appointmentId, string declineReason, int revisingUserId)
        {
            var appointment = Load(appointmentId);
            if (appointment != null)
            {
                AppointmentSqlDataProvider.SetAppointmentAcceptance(appointment.AppointmentId, false, revisingUserId);
                EmailService.SendDeclineEmail(appointment, declineReason);
            }
        }

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
        /// Saves this event.
        /// </summary>
        /// <param name="revisingUser">The user who is saving this event.</param>
        public void Save(int revisingUser)
        {
            if (this.AppointmentId == Null.NullInteger)
            {
                this.Insert(revisingUser);
            }
            else
            {
                this.Update(revisingUser);
            }
        }

        /// <summary>
        /// Fills an Appointment with the data in the specified <paramref name="appointmentRecord"/>.
        /// </summary>
        /// <param name="appointmentRecord">A pre-initialized data record that represents an <see cref="Appointment"/> instance.</param>
        /// <returns>An instantiated Appointment object.</returns>
        internal static Appointment Fill(IDataRecord appointmentRecord)
        {
            Appointment appointment = new Appointment();

            appointment.AppointmentId = (int)appointmentRecord["AppointmentId"];
            appointment.AppointmentTypeId = (int)appointmentRecord["AppointmentTypeId"];
            appointment.ModuleId = (int)appointmentRecord["ModuleId"];
            appointment.Title = appointmentRecord["Title"].ToString();
            appointment.Description = appointmentRecord["Description"].ToString();
            appointment.Notes = appointmentRecord["Notes"].ToString();
            appointment.Address1 = appointmentRecord["Address1"].ToString();
            appointment.Address2 = appointmentRecord["Address2"].ToString();
            appointment.City = appointmentRecord["City"].ToString();
            appointment.RegionId = appointmentRecord["RegionId"] as int?;
            appointment.PostalCode = appointmentRecord["PostalCode"].ToString();
            appointment.Phone = appointmentRecord["Phone"].ToString();
            appointment.AdditionalAddressInfo = appointmentRecord["AdditionalAddressInfo"].ToString();
            appointment.ContactStreet = appointmentRecord["ContactStreet"].ToString();
            appointment.ContactPhone = appointmentRecord["ContactPhone"].ToString();
            appointment.RequestorName = appointmentRecord["RequestorName"].ToString();
            appointment.RequestorPhoneType = appointmentRecord["RequestorPhoneType"].ToString();
            appointment.RequestorPhone = appointmentRecord["RequestorPhone"].ToString();
            appointment.RequestorEmail = appointmentRecord["RequestorEmail"].ToString();
            appointment.RequestorAltPhoneType = appointmentRecord["RequestorAltPhoneType"].ToString();
            appointment.RequestorAltPhone = appointmentRecord["RequestorAltPhone"].ToString();
            appointment.StartDateTime = (DateTime)appointmentRecord["StartDateTime"];
            appointment.EndDateTime = (DateTime)appointmentRecord["EndDateTime"];
            appointment.NumberOfParticipants = (int)appointmentRecord["NumberOfParticipants"];
            appointment.NumberOfSpecialParticipants = (int)appointmentRecord["NumberOfSpecialParticipants"];
            appointment.ParticipantGender = appointmentRecord["ParticipantGender"].ToString();
            appointment.IsPresenterSpecialYOrN = appointmentRecord["ParticipantFlag"].ToString()[0];
            appointment.IsAccepted = appointmentRecord["IsAccepted"] as bool?;

            return appointment;
        }

        /// <summary>
        /// Inserts this event.
        /// </summary>
        /// <param name="revisingUserId">The user who is inserting this event.</param>
        /// <exception cref="DBException">If an error occurs while going to the database to insert the event</exception>
        private void Insert(int revisingUserId)
        {
            this.AppointmentId = AppointmentSqlDataProvider.InsertAppointment(this, revisingUserId);
        }

        /// <summary>
        /// Updates this event.
        /// </summary>
        /// <param name="revisingUser">The user responsible for updating this event.</param>
        /// <exception cref="DBException">If an error occurs while going to the database to update the event</exception>
        private void Update(int revisingUser)
        {
            AppointmentSqlDataProvider.UpdateAppointment(this, revisingUser);
        }
    }
}