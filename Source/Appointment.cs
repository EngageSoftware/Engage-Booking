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
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Diagnostics;
    using System.Linq;
    using System.Xml.Serialization;
    using Data;
    using DotNetNuke.Common;
    using DotNetNuke.Common.Lists;
    using DotNetNuke.Common.Utilities;

    /// <summary>
    /// An event, with a title, description, location, and start and end date.
    /// </summary>
    [XmlRoot(ElementName = "appointment", IsNullable = false)]
    public class Appointment : IEditableObject
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
        /// <param name="city">The city of the appointment.</param>
        /// <param name="regionId">The region id.</param>
        /// <param name="postalCode">The postal code.</param>
        /// <param name="phone">The phone number.</param>
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
        /// <param name="isPresenterSpecial">The participant flag.</param>
        /// <param name="participantInstructions">The participant instructions.</param>
        /// <param name="numberOfSpecialParticipants">The number of special participants.</param>
        /// <param name="custom1">The first custom field.</param>
        /// <param name="custom2">The second custom field.</param>
        /// <param name="custom3">The third custom field.</param>
        /// <param name="custom4">The fourth custom field.</param>
        /// <param name="custom5">The fifth custom field.</param>
        /// <param name="custom6">The sixth custom field.</param>
        /// <param name="custom7">The seventh custom field.</param>
        /// <param name="custom8">The eighth custom field.</param>
        /// <param name="custom9">The ninth custom field.</param>
        /// <param name="custom10">The tenth custom field.</param>
        /// <param name="isAccepted"><c>null</c>if the <see cref="Appointment"/> has not bee accepted or declined yet,
        /// otherwise <c>true</c> for an accepted appointment, or <c>false</c> for a declined appointment.</param>
        private Appointment(
                int moduleId,
                int appointmentTypeId,
                string title,
                string description,
                string notes,
                string address1,
                string address2,
                string city,
                int? regionId,
                string postalCode,
                string phone,
                string additionalAddressInfo,
                string contactStreet,
                string contactPhone,
                string requestorName,
                PhoneType requestorPhoneType,
                string requestorPhone,
                string requestorEmail,
                PhoneType requestorAltPhoneType,
                string requestorAltPhone,
                DateTime start,
                DateTime end,
                TimeSpan timeZoneOffset,
                int numberOfParticipants,
                GroupGender participantGender,
                bool isPresenterSpecial,
                string participantInstructions,
                int numberOfSpecialParticipants,
                string custom1,
                string custom2,
                string custom3,
                string custom4,
                string custom5,
                string custom6,
                string custom7,
                string custom8,
                string custom9,
                string custom10,
                bool? isAccepted)
            : this()
        {
            this.ModuleId = moduleId;
            this.AppointmentTypeId = appointmentTypeId;
            this.Title = title;
            this.Description = description;
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
            this.IsPresenterSpecial = isPresenterSpecial;
            this.TimeZoneOffset = timeZoneOffset;
            this.ParticipantInstructions = participantInstructions;
            this.NumberOfSpecialParticipants = numberOfSpecialParticipants;
            this.Custom1 = custom1;
            this.Custom2 = custom2;
            this.Custom3 = custom3;
            this.Custom4 = custom4;
            this.Custom5 = custom5;
            this.Custom6 = custom6;
            this.Custom7 = custom7;
            this.Custom8 = custom8;
            this.Custom9 = custom9;
            this.Custom10 = custom10;
            this.IsAccepted = isAccepted;
        }

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
        /// Gets the name of the region in which this appointment is to take place.
        /// </summary>
        /// <value>The name of the region in which this appointment takes place.</value>
        [XmlIgnore]
        public string RegionName
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
        /// Gets the type of this appointment.
        /// </summary>
        /// <value>The type of this appointment.</value>
        [XmlIgnore]
        public AppointmentType AppointmentType
        {
            get
            {
                return AppointmentType.Load(this.AppointmentTypeId, this.ModuleId);
            }
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
        public PhoneType RequestorPhoneType
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
        public PhoneType RequestorAltPhoneType
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
        public GroupGender ParticipantGender
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
        /// Gets or sets the first custom field.
        /// </summary>
        /// <value>The first custom field.</value>
        [XmlElement(Order = 26)]
        public string Custom1
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the second custom field.
        /// </summary>
        /// <value>The second custom field.</value>
        [XmlElement(Order = 27)]
        public string Custom2
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the third custom field.
        /// </summary>
        /// <value>The third custom field.</value>
        [XmlElement(Order = 28)]
        public string Custom3
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the fourth custom field.
        /// </summary>
        /// <value>The fourth custom field.</value>
        [XmlElement(Order = 29)]
        public string Custom4
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the fifth custom field.
        /// </summary>
        /// <value>The fifth custom field.</value>
        [XmlElement(Order = 30)]
        public string Custom5
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the sixth custom field.
        /// </summary>
        /// <value>The sixth custom field.</value>
        [XmlElement(Order = 31)]
        public string Custom6
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the seventh custom field.
        /// </summary>
        /// <value>The seventh custom field.</value>
        [XmlElement(Order = 32)]
        public string Custom7
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the eighth custom field.
        /// </summary>
        /// <value>The eighth custom field.</value>
        [XmlElement(Order = 33)]
        public string Custom8
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the ninth custom field.
        /// </summary>
        /// <value>The ninth custom field.</value>
        [XmlElement(Order = 34)]
        public string Custom9
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the tenth custom field.
        /// </summary>
        /// <value>The tenth custom field.</value>
        [XmlElement(Order = 35)]
        public string Custom10
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the number of special participants.
        /// </summary>
        /// <value>The number of special participants.</value>
        [XmlElement(Order = 36)]
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
        [XmlElement(Order = 37)]
        public bool? IsAccepted
        {
            get; 
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the presenter is "special."
        /// </summary>
        /// <value>
        /// <c>true</c> if the presenter is "special;" otherwise, <c>false</c>.
        /// </value>
        [XmlElement(Order = 38)]
        public bool IsPresenterSpecial
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the offset from UTC (in minutes) of the time zone in which the appointment takes place.
        /// </summary>
        /// <value>The offset from UTC (in minutes).</value>
        [XmlElement(Order = 39)]
        public TimeSpan TimeZoneOffset
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the full address.
        /// </summary>
        /// <value>The full address.</value>
        [XmlIgnore]
        public string FullAddress
        {
            get
            {
                return Globals.FormatAddress(this.Title, this.Address1 + ", " + this.Address2, this.City, this.RegionName, null, this.PostalCode);
            }
        }

        /// <summary>
        /// Gets the key to use to auto-approve this instance through email.
        /// </summary>
        /// <value>The approve key.</value>
        [XmlIgnore]
        public Guid AcceptKey
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the key to use to auto-decline this instance through email.
        /// </summary>
        /// <value>The decline key.</value>
        [XmlIgnore]
        public Guid DeclineKey
        {
            get;
            private set;
        }

        /// <summary>
        /// Accepts or declines an <see cref="Appointment"/> via the given <paramref name="actionKey"/>.
        /// </summary>
        /// <param name="actionKey">The key the corresponds to accepting or declining a specific <see cref="Appointment"/>.</param>
        /// <returns><c>true</c> if the appointment was accepted, otherwise <c>false</c></returns>
        public static Appointment ApproveByKey(Guid actionKey)
        {
            using (IDataReader reader = AppointmentSqlDataProvider.ApproveByKey(actionKey))
            {
                if (reader.Read())
                {
                    return Fill(reader);
                }

                return null;
            }
        }

        /// <summary>
        /// Determines whether an <see cref="Appointment"/> can be created at the specified <paramref name="start"/> time until the specified <paramref name="end"/> time.
        /// </summary>
        /// <param name="moduleId">The ID of the module in which the appointment is to be created.</param>
        /// <param name="start">The start of the new <see cref="Appointment"/>.</param>
        /// <param name="end">The end of the new <see cref="Appointment"/>.</param>
        /// <param name="max">The maximum appointments allowed for the specified time range</param>
        /// <returns>
        /// <c>true</c> if an <see cref="Appointment"/> can be created at the specified <paramref name="start"/> time until the specified <paramref name="end"/> time; otherwise, <c>false</c>.
        /// </returns>
        public static bool CanCreateAt(int moduleId, DateTime start, DateTime end, int? max)
        {
            var appointments = AppointmentSqlDataProvider.GetConcurrentAppointments(moduleId, start, end);
            var appointmentsInRange = new List<Appointment>(max ?? 10);

            while (appointments.Read())
            {
                appointmentsInRange.Add(Fill(appointments));
            }

            var uniqueStartTimes = appointmentsInRange.Select(apt => apt.StartDateTime).Distinct();
            return uniqueStartTimes.All(time => max > appointmentsInRange.Count(apt => time >= apt.StartDateTime && time < apt.EndDateTime));
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
        /// <param name="city">The city of the appointment.</param>
        /// <param name="regionId">The region id.</param>
        /// <param name="postalCode">The postal code.</param>
        /// <param name="phone">The phone number.</param>
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
        /// <param name="isPresenterSpecial">The participant flag.</param>
        /// <param name="participantInstructions">The participant instructions.</param>
        /// <param name="numberOfSpecialParticipants">The number of special participants.</param>
        /// <param name="custom1">The first custom field.</param>
        /// <param name="custom2">The second custom field.</param>
        /// <param name="custom3">The third custom field.</param>
        /// <param name="custom4">The fourth custom field.</param>
        /// <param name="custom5">The fifth custom field.</param>
        /// <param name="custom6">The sixth custom field.</param>
        /// <param name="custom7">The seventh custom field.</param>
        /// <param name="custom8">The eighth custom field.</param>
        /// <param name="custom9">The ninth custom field.</param>
        /// <param name="custom10">The tenth custom field.</param>
        /// <param name="isAccepted"><c>null</c>if the <see cref="Appointment"/> has not bee accepted or declined yet,
        /// otherwise <c>true</c> for an accepted appointment, or <c>false</c> for a declined appointment.</param>
        /// <returns>
        /// A new <see cref="Appointment"/> instance.
        /// </returns>
        public static Appointment Create(
                int moduleId,
                int appointmentTypeId,
                string title,
                string description,
                string notes,
                string address1,
                string address2,
                string city,
                int? regionId,
                string postalCode,
                string phone,
                string additionalAddressInfo,
                string contactStreet,
                string contactPhone,
                string requestorName,
                PhoneType requestorPhoneType,
                string requestorPhone,
                string requestorEmail,
                PhoneType requestorAltPhoneType,
                string requestorAltPhone,
                DateTime start,
                DateTime end,
                TimeSpan timeZoneOffset,
                int numberOfParticipants,
                GroupGender participantGender,
                bool isPresenterSpecial,
                string participantInstructions,
                int numberOfSpecialParticipants,
                string custom1,
                string custom2,
                string custom3,
                string custom4,
                string custom5,
                string custom6,
                string custom7,
                string custom8,
                string custom9,
                string custom10,
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
                    isPresenterSpecial,
                    participantInstructions,
                    numberOfSpecialParticipants,
                    custom1,
                    custom2,
                    custom3,
                    custom4,
                    custom5,
                    custom6,
                    custom7,
                    custom8,
                    custom9,
                    custom10,
                    isAccepted);
        }

        /// <summary>
        /// Deletes the specified appointment id.
        /// </summary>
        /// <param name="appointmentId">The appointment id.</param>
        public static void Delete(int appointmentId)
        {
            AppointmentSqlDataProvider.DeleteAppointment(appointmentId);
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
        /// Accepts this <see cref="Appointment"/>.
        /// </summary>
        /// <param name="revisingUserId">The ID of the user accepting the <see cref="Appointment"/>.</param>
        public void Accept(int revisingUserId)
        {
            AppointmentSqlDataProvider.AcceptAppointment(this.AppointmentId, revisingUserId);
        }

        /// <summary>
        /// Declines this <see cref="Appointment"/>.
        /// </summary>
        /// <param name="revisingUserId">The ID of the user declining the <see cref="Appointment"/>.</param>
        public void Decline(int revisingUserId)
        {
            AppointmentSqlDataProvider.DeclineAppointment(this.AppointmentId, revisingUserId);
        }

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
        /// Creates an iCal representation of this appointment.
        /// </summary>
        /// <returns>An iCal representation of this appointment</returns>
        public string ToICal()
        {
            return ICalUtil.Export(this.Description, this.FullAddress, this, this.TimeZoneOffset);
        }

        /// <summary>
        /// Fills an Appointment with the data in the specified <paramref name="appointmentRecord"/>.
        /// </summary>
        /// <param name="appointmentRecord">A pre-initialized data record that represents an <see cref="Appointment"/> instance.</param>
        /// <returns>An instantiated Appointment object.</returns>
        internal static Appointment Fill(IDataRecord appointmentRecord)
        {
            return new Appointment
                       {
                               AppointmentId = (int)appointmentRecord["AppointmentId"],
                               AppointmentTypeId = (int)appointmentRecord["AppointmentTypeId"],
                               ModuleId = (int)appointmentRecord["ModuleId"],
                               Title = appointmentRecord["Title"].ToString(),
                               Description = appointmentRecord["Description"].ToString(),
                               Notes = appointmentRecord["Notes"].ToString(),
                               Address1 = appointmentRecord["Address1"].ToString(),
                               Address2 = appointmentRecord["Address2"].ToString(),
                               City = appointmentRecord["City"].ToString(),
                               RegionId = appointmentRecord["RegionId"] as int?,
                               PostalCode = appointmentRecord["PostalCode"].ToString(),
                               Phone = appointmentRecord["Phone"].ToString(),
                               AdditionalAddressInfo = appointmentRecord["AdditionalAddressInfo"].ToString(),
                               ContactStreet = appointmentRecord["ContactStreet"].ToString(),
                               ContactPhone = appointmentRecord["ContactPhone"].ToString(),
                               RequestorName = appointmentRecord["RequestorName"].ToString(),
                               RequestorPhoneType = Utility.ConvertToEnum(appointmentRecord["RequestorPhoneType"].ToString(), PhoneType.None),
                               RequestorPhone = appointmentRecord["RequestorPhone"].ToString(),
                               RequestorEmail = appointmentRecord["RequestorEmail"].ToString(),
                               RequestorAltPhoneType = Utility.ConvertToEnum(appointmentRecord["RequestorAltPhoneType"].ToString(), PhoneType.None),
                               RequestorAltPhone = appointmentRecord["RequestorAltPhone"].ToString(),
                               StartDateTime = (DateTime)appointmentRecord["StartDateTime"],
                               EndDateTime = (DateTime)appointmentRecord["EndDateTime"],
                               TimeZoneOffset = new TimeSpan(0, (int)appointmentRecord["TimeZoneOffset"], 0),
                               NumberOfParticipants = (int)appointmentRecord["NumberOfParticipants"],
                               NumberOfSpecialParticipants = (int)appointmentRecord["NumberOfSpecialParticipants"],
                               ParticipantInstructions = appointmentRecord["ParticipantInstructions"].ToString(),
                               Custom1 = appointmentRecord["Custom1"].ToString(),
                               Custom2 = appointmentRecord["Custom2"].ToString(),
                               Custom3 = appointmentRecord["Custom3"].ToString(),
                               Custom4 = appointmentRecord["Custom4"].ToString(),
                               Custom5 = appointmentRecord["Custom5"].ToString(),
                               Custom6 = appointmentRecord["Custom6"].ToString(),
                               Custom7 = appointmentRecord["Custom7"].ToString(),
                               Custom8 = appointmentRecord["Custom8"].ToString(),
                               Custom9 = appointmentRecord["Custom9"].ToString(),
                               Custom10 = appointmentRecord["Custom10"].ToString(),
                               ParticipantGender = Utility.ConvertToEnum(appointmentRecord["ParticipantGender"].ToString(), GroupGender.MixedGroup),
                               IsPresenterSpecial = appointmentRecord["ParticipantFlag"].ToString()[0] == 'Y',
                               IsAccepted = appointmentRecord["IsAccepted"] as bool?,
                               AcceptKey = (Guid)appointmentRecord["AcceptKey"],
                               DeclineKey = (Guid)appointmentRecord["DeclineKey"]
                       };
        }

        /// <summary>
        /// Inserts this event.
        /// </summary>
        /// <param name="revisingUserId">The user who is inserting this event.</param>
        /// <exception cref="DBException">If an error occurs while going to the database to insert the event</exception>
        private void Insert(int revisingUserId)
        {
            using (var appointmentReader = AppointmentSqlDataProvider.InsertAppointment(this, revisingUserId))
            {
                if (appointmentReader.Read())
                {
                    this.AppointmentId = (int)appointmentReader["AppointmentId"];
                    this.AcceptKey = (Guid)appointmentReader["AcceptKey"];
                    this.DeclineKey = (Guid)appointmentReader["DeclineKey"];
                }
                else
                {
                    throw new DBException("Result set was expected", "InsertAppointment");
                }
            }
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