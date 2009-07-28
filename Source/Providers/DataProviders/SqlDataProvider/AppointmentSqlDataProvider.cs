// <copyright file="AppointmentSqlDataProvider.cs" company="Engage Software">
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
    using System.Data;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// A SQL implementation of data access for the <see cref="Appointment"/> and related types
    /// </summary>
    public static class AppointmentSqlDataProvider
    {
        /// <summary>
        /// Accepts or declines the <see cref="Appointment"/> with the given <paramref name="appointmentId"/>.
        /// </summary>
        /// <param name="appointmentId">The ID of the <see cref="Appointment"/> to accept or decline.</param>
        /// <param name="revisingUserId">The ID of the user setting the acceptance the <see cref="Appointment"/>.</param>
        /// <returns>Whether the appointment could be successfully accepted</returns>
        public static bool AcceptAppointment(int appointmentId, int revisingUserId)
        {
            return (bool)SqlDataProvider.Instance.ExecuteScalar(
                "AcceptAppointment",
                Engage.Utility.CreateIntegerParam("@appointmentId", appointmentId),
                Engage.Utility.CreateIntegerParam("@revisingUser", revisingUserId));
        }

        /// <summary>
        /// Accepts or declines an <see cref="Appointment"/> via the given <paramref name="actionKey"/>.
        /// </summary>
        /// <param name="actionKey">The key the corresponds to accepting or declining a specific <see cref="Appointment"/>.</param>
        /// <returns>An <see cref="IDataReader"/> with the appointment record of the appointment that was accepted or declined</returns>
        public static IDataReader ApproveByKey(Guid actionKey)
        {
            return SqlDataProvider.Instance.ExecuteReader("ApproveByKey", Engage.Utility.CreateGuidParam("@actionKey", actionKey));
        }

        /// <summary>
        /// Determines whether an <see cref="Appointment"/> can be created at the specified <paramref name="start"/> time until the specified <paramref name="end"/> time.
        /// </summary>
        /// <param name="moduleId">The ID of the module in which the appointment is to be created.</param>
        /// <param name="start">The start of the new <see cref="Appointment"/>.</param>
        /// <param name="end">The end of the new <see cref="Appointment"/>.</param>
        /// <returns>
        /// <c>true</c> if an <see cref="Appointment"/> can be created at the specified <paramref name="start"/> time until the specified <paramref name="end"/> time; otherwise, <c>false</c>.
        /// </returns>
        public static bool CanCreateAppointmentAt(int? moduleId, DateTime start, DateTime end)
        {
            return (bool)SqlDataProvider.Instance.ExecuteScalar(
                "CanCreateAppointmentAt",
                Engage.Utility.CreateIntegerParam("@moduleId", moduleId),
                Engage.Utility.CreateDateTimeParam("@start", start),
                Engage.Utility.CreateDateTimeParam("@end", end));
        }

        /// <summary>
        /// Accepts or declines the <see cref="Appointment"/> with the given <paramref name="appointmentId"/>.
        /// </summary>
        /// <param name="appointmentId">The ID of the <see cref="Appointment"/> to accept or decline.</param>
        /// <param name="revisingUserId">The ID of the user setting the acceptance the <see cref="Appointment"/>.</param>
        public static void DeclineAppointment(int appointmentId, int revisingUserId)
        {
            SqlDataProvider.Instance.ExecuteNonQuery(
                    "DeclineAppointment",
                    Engage.Utility.CreateIntegerParam("@appointmentId", appointmentId),
                    Engage.Utility.CreateIntegerParam("@revisingUser", revisingUserId));
        }

        /// <summary>
        /// Deletes the appointment with the given <paramref name="appointmentId"/>.
        /// </summary>
        /// <param name="appointmentId">The ID of the appointment to delete.</param>
        public static void DeleteAppointment(int appointmentId)
        {
            SqlDataProvider.Instance.ExecuteNonQuery(
                "DeleteAppointment", 
                Engage.Utility.CreateIntegerParam("@Appointment", appointmentId));
        }

        /// <summary>
        /// Gets the appointment with the given <paramref name="appointmentId"/>.
        /// </summary>
        /// <param name="appointmentId">The ID of the appointment to retrieve.</param>
        /// <returns>An <see cref="IDataReader"/> with the appointment record</returns>
        public static IDataReader GetAppointment(int appointmentId)
        {
            return SqlDataProvider.Instance.ExecuteReader(
                "GetAppointment", 
                Engage.Utility.CreateIntegerParam("@appointmentId", appointmentId));
        }

        /// <summary>
        /// Gets a page of the appointments for a given <paramref name="moduleId"/>.
        /// </summary>
        /// <param name="moduleId">The ID of the module to which the appointments belong.</param>
        /// <param name="sortExpression">A comma-delimited list of the columns by which to sort.</param>
        /// <param name="pageSize">Size of the page, or <c>null</c> to retrieve all appointments.</param>
        /// <param name="pageIndex">Index of the page, or <c>null</c> to retrieve all appointments.</param>
        /// <returns>
        /// An <see cref="IDataReader"/> with two results; 
        /// the first being the total number of appointments for the module (as a single integer), 
        /// the second being the appointments.
        /// </returns>
        public static IDataReader GetAppointments(int moduleId, string sortExpression, int? pageSize, int? pageIndex)
        {
            return SqlDataProvider.Instance.ExecuteReader(
                    "GetAllAppointments",
                    Engage.Utility.CreateIntegerParam("@moduleId", moduleId),
                    sortExpression != null ? Engage.Utility.CreateVarcharParam("@sortExpression", sortExpression) : null,
                    Engage.Utility.CreateIntegerParam("@pageSize", pageSize),
                    Engage.Utility.CreateIntegerParam("@pageIndex", pageIndex));
        }

        /// <summary>
        /// Gets a page of the appointments for a given <paramref name="moduleId"/>.
        /// </summary>
        /// <param name="moduleId">The ID of the module to which the appointments belong.</param>
        /// <param name="isAccepted">
        /// <c>true</c> to retrieve only accepted appointments, 
        /// <c>false</c> to retrieve only declines appointments, 
        /// or <c>null</c> to retrieve only those appointments which have been neither accepted nor declined.
        /// Use <see cref="GetAppointments(int,string,System.Nullable{int},System.Nullable{int})"/> to retrieve appointments without regard to the IsAccepted field.
        /// </param>
        /// <param name="sortExpression">A comma-delimited list of the columns by which to sort.</param>
        /// <param name="pageSize">Size of the page, or <c>null</c> to retrieve all appointments.</param>
        /// <param name="pageIndex">Index of the page, or <c>null</c> to retrieve all appointments.</param>
        /// <returns>
        /// An <see cref="IDataReader"/> with two results;
        /// the first being the total number of appointments with the given acceptance status (as a single integer),
        /// the second being the appointments.
        /// </returns>
        public static IDataReader GetAppointments(int moduleId, bool? isAccepted, string sortExpression, int? pageSize, int? pageIndex)
        {
            return SqlDataProvider.Instance.ExecuteReader(
                    "GetAppointments",
                    Engage.Utility.CreateIntegerParam("@moduleId", moduleId),
                    Engage.Utility.CreateBitParam("@isAccepted", isAccepted),
                    sortExpression != null ? Engage.Utility.CreateVarcharParam("@sortExpression", sortExpression) : null,
                    Engage.Utility.CreateIntegerParam("@pageSize", pageSize),
                    Engage.Utility.CreateIntegerParam("@pageIndex", pageIndex));
        }

        /// <summary>
        /// Gets all appointments within a given date range.
        /// </summary>
        /// <param name="moduleId">The ID of the module to which the appointments belong.</param>
        /// <param name="startDateTime">The beginning datetime of the range.</param>
        /// <param name="endDateTime">The ending datetime of the range.</param>
        /// <returns>
        /// An <see cref="IDataReader"/> with a list of appointments.
        /// </returns>
        public static IDataReader GetAppointmentsByDateRange(int moduleId, DateTime? startDateTime, DateTime? endDateTime)
        {
            return SqlDataProvider.Instance.ExecuteReader(
                    "GetAppointmentsByDateRange",
                    Engage.Utility.CreateIntegerParam("@moduleId", moduleId),
                    Engage.Utility.CreateDateTimeParam("@startDateTime", startDateTime),
                    Engage.Utility.CreateDateTimeParam("@endDateTime", endDateTime));
        }

        /// <summary>
        /// Gets the appointment types.
        /// </summary>
        /// <returns>
        /// An <see cref="IDataReader"/> with the list of appointment types for this module.
        /// </returns>
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "Does not represent state")]
        public static IDataReader GetAppointmentTypes()
        {
            return SqlDataProvider.Instance.ExecuteReader("GetAppointmentTypes");
        }

        /// <summary>
        /// Inserts the given <paramref name="appointment"/> into the ol' database.
        /// </summary>
        /// <param name="appointment">The appointment to insert.</param>
        /// <param name="revisingUserId">The ID of the user inserting.</param>
        /// <returns>An <see cref="IDataReader"/> with a result set containing the fields set by the insert (AppointmentId, AcceptKey, and DeclineKey)</returns>
        public static IDataReader InsertAppointment(Appointment appointment, int revisingUserId)
        {
            return SqlDataProvider.Instance.ExecuteReader(
                    "InsertAppointment",
                    Engage.Utility.CreateIntegerParam("@appointmentTypeId", appointment.AppointmentTypeId),
                    Engage.Utility.CreateIntegerParam("@moduleId", appointment.ModuleId),
                    Engage.Utility.CreateVarcharParam("@title", appointment.Title),
                    Engage.Utility.CreateTextParam("@description", appointment.Description),
                    Engage.Utility.CreateTextParam("@notes", appointment.Notes),
                    Engage.Utility.CreateVarcharParam("@address1", appointment.Address1),
                    Engage.Utility.CreateVarcharParam("@address2", appointment.Address2),
                    Engage.Utility.CreateVarcharParam("@city", appointment.City),
                    Engage.Utility.CreateIntegerParam("@regionId", appointment.RegionId),
                    Engage.Utility.CreateVarcharParam("@postalCode", appointment.PostalCode),
                    Engage.Utility.CreateVarcharParam("@phone", appointment.Phone),
                    Engage.Utility.CreateVarcharParam("@additionalAddressInfo", appointment.AdditionalAddressInfo),
                    Engage.Utility.CreateVarcharParam("@contactStreet", appointment.ContactStreet),
                    Engage.Utility.CreateVarcharParam("@contactPhone", appointment.ContactPhone),
                    Engage.Utility.CreateVarcharParam("@requestorName", appointment.RequestorName),
                    Engage.Utility.CreateVarcharParam("@requestorPhoneType", appointment.RequestorPhoneType),
                    Engage.Utility.CreateVarcharParam("@requestorPhone", appointment.RequestorPhone),
                    Engage.Utility.CreateVarcharParam("@requestorEmail", appointment.RequestorEmail),
                    Engage.Utility.CreateVarcharParam("@requestorAltPhoneType", appointment.RequestorAltPhoneType),
                    Engage.Utility.CreateVarcharParam("@requestorAltPhone", appointment.RequestorAltPhone),
                    Engage.Utility.CreateDateTimeParam("@startDateTime", appointment.StartDateTime),
                    Engage.Utility.CreateDateTimeParam("@endDateTime", appointment.EndDateTime),
                    Engage.Utility.CreateIntegerParam("@numberOfParticipants", appointment.NumberOfParticipants),
                    Engage.Utility.CreateVarcharParam("@participantGender", appointment.ParticipantGender),
                    Engage.Utility.CreateCharParam("@participantFlag", appointment.IsPresenterSpecialYOrN.ToString()),
                    Engage.Utility.CreateTextParam("@participantInstructions", appointment.ParticipantInstructions),
                    Engage.Utility.CreateIntegerParam("@numberOfSpecialParticipants", appointment.NumberOfSpecialParticipants),
                    Engage.Utility.CreateTextParam("@custom1", appointment.Custom1),
                    Engage.Utility.CreateTextParam("@custom2", appointment.Custom2),
                    Engage.Utility.CreateTextParam("@custom3", appointment.Custom3),
                    Engage.Utility.CreateTextParam("@custom4", appointment.Custom4),
                    Engage.Utility.CreateTextParam("@custom5", appointment.Custom5),
                    Engage.Utility.CreateTextParam("@custom6", appointment.Custom6),
                    Engage.Utility.CreateTextParam("@custom7", appointment.Custom7),
                    Engage.Utility.CreateTextParam("@custom8", appointment.Custom8),
                    Engage.Utility.CreateTextParam("@custom9", appointment.Custom9),
                    Engage.Utility.CreateTextParam("@custom10", appointment.Custom10),
                    Engage.Utility.CreateBitParam("@isAccepted", appointment.IsAccepted),
                    Engage.Utility.CreateIntegerParam("@revisingUser", revisingUserId));
        }

        /// <summary>
        /// Updates the given <paramref name="appointment"/>'s record.
        /// </summary>
        /// <param name="appointment">The appointment to update.</param>
        /// <param name="revisingUserId">The ID of the user making this update.</param>
        public static void UpdateAppointment(Appointment appointment, int revisingUserId)
        {
            SqlDataProvider.Instance.ExecuteNonQuery(
                    "UpdateAppointment",
                    Engage.Utility.CreateIntegerParam("@appointmentId", appointment.AppointmentId),
                    Engage.Utility.CreateIntegerParam("@appointmentTypeId", appointment.AppointmentTypeId),
                    Engage.Utility.CreateVarcharParam("@title", appointment.Title),
                    Engage.Utility.CreateTextParam("@description", appointment.Description),
                    Engage.Utility.CreateTextParam("@notes", appointment.Notes),
                    Engage.Utility.CreateVarcharParam("@address1", appointment.Address1),
                    Engage.Utility.CreateVarcharParam("@address2", appointment.Address2),
                    Engage.Utility.CreateVarcharParam("@city", appointment.City),
                    Engage.Utility.CreateIntegerParam("@regionId", appointment.RegionId),
                    Engage.Utility.CreateVarcharParam("@postalCode", appointment.PostalCode),
                    Engage.Utility.CreateVarcharParam("@phone", appointment.Phone),
                    Engage.Utility.CreateVarcharParam("@additionalAddressInfo", appointment.AdditionalAddressInfo),
                    Engage.Utility.CreateVarcharParam("@contactStreet", appointment.ContactStreet),
                    Engage.Utility.CreateVarcharParam("@contactPhone", appointment.ContactPhone),
                    Engage.Utility.CreateVarcharParam("@requestorName", appointment.RequestorName),
                    Engage.Utility.CreateVarcharParam("@requestorPhoneType", appointment.RequestorPhoneType),
                    Engage.Utility.CreateVarcharParam("@requestorPhone", appointment.RequestorPhone),
                    Engage.Utility.CreateVarcharParam("@requestorEmail", appointment.RequestorEmail),
                    Engage.Utility.CreateVarcharParam("@requestorAltPhoneType", appointment.RequestorAltPhoneType),
                    Engage.Utility.CreateVarcharParam("@requestorAltPhone", appointment.RequestorAltPhone),
                    Engage.Utility.CreateDateTimeParam("@startDateTime", appointment.StartDateTime),
                    Engage.Utility.CreateDateTimeParam("@endDateTime", appointment.EndDateTime),
                    Engage.Utility.CreateIntegerParam("@numberOfParticipants", appointment.NumberOfParticipants),
                    Engage.Utility.CreateVarcharParam("@participantGender", appointment.ParticipantGender),
                    Engage.Utility.CreateCharParam("@participantFlag", appointment.IsPresenterSpecialYOrN.ToString()),
                    Engage.Utility.CreateTextParam("@participantInstructions", appointment.ParticipantInstructions),
                    Engage.Utility.CreateTextParam("@custom1", appointment.Custom1),
                    Engage.Utility.CreateTextParam("@custom2", appointment.Custom2),
                    Engage.Utility.CreateTextParam("@custom3", appointment.Custom3),
                    Engage.Utility.CreateTextParam("@custom4", appointment.Custom4),
                    Engage.Utility.CreateTextParam("@custom5", appointment.Custom5),
                    Engage.Utility.CreateTextParam("@custom6", appointment.Custom6),
                    Engage.Utility.CreateTextParam("@custom7", appointment.Custom7),
                    Engage.Utility.CreateTextParam("@custom8", appointment.Custom8),
                    Engage.Utility.CreateTextParam("@custom9", appointment.Custom9),
                    Engage.Utility.CreateTextParam("@custom10", appointment.Custom10),
                    Engage.Utility.CreateIntegerParam("@numberOfSpecialParticipants", appointment.NumberOfSpecialParticipants),
                    Engage.Utility.CreateBitParam("@isAccepted", appointment.IsAccepted),
                    Engage.Utility.CreateIntegerParam("@revisingUser", revisingUserId));
        }
    }
}