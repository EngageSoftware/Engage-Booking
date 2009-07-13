// <copyright file="ActionControlBase.cs" company="Engage Software">
// Engage: Booking - http://www.EngageSoftware.com
// Copyright (c) 2004-2009
// by Engage Software ( http://www.engagesoftware.com )
// </copyright>
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED 
// TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL 
// THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF 
// CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
// DEALINGS IN THE SOFTWARE.

namespace Engage.Dnn.Booking.Actions
{
    using System;
    using System.Globalization;

    /// <summary>
    /// The base class for all controls in the Engage: Booking module. Since this module is licensed it 
    /// inherits from LicenseModuleBase and requires a unique GUID be defined. DO NOT CHANGE THIS!
    /// </summary>
    public abstract class ActionControlBase : ModuleBase
    {
        /// <summary>
        /// Backing field for <see cref="CurrentAppointment"/>
        /// </summary>
        private Appointment currentAppointment;

        /// <summary>
        /// Gets or sets the current event that this control is displaying actions for.
        /// </summary>
        /// <value>The current event that this control is displaying actions for.</value>
        internal Appointment CurrentAppointment
        {
            get
            {
                return this.currentAppointment ?? Appointment.Load(this.CurrentEventId);
            }

            set
            {
                this.currentAppointment = value;
                this.CurrentEventId = this.currentAppointment.Id;
                this.BindData();
            }
        }

        /// <summary>
        /// Gets or sets the current event id.
        /// </summary>
        /// <value>The current event id.</value>
        internal int CurrentEventId
        {
            get { return Convert.ToInt32(this.ViewState["id"], CultureInfo.InvariantCulture); }

            set { this.ViewState["id"] = value.ToString(CultureInfo.InvariantCulture); }
        }

        /// <summary>
        /// Performs all necessary operations to display the control's data correctly.
        /// </summary>
        protected abstract void BindData();
    }
}