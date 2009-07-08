// <copyright file="Setting.cs" company="Engage Software">
// Engage: Booking - http://www.EngageSoftware.com
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
    /// <summary>
    /// This class contains Event Module specific settings. Framework version contains general settings.
    /// As more functionality is moved up, some of these can go with it.
    /// </summary>
    internal class Setting : Framework.Setting
    {
        /// <summary>
        /// The URL to navigate to in order to indicate that an email has been opened
        /// </summary>
        public static readonly Setting OpenLinkUrl = new Setting("openLinkUrl", "Specify the URL for your Open Link to track opens.");

        /// <summary>
        /// The privacy policy URL to use in emails
        /// </summary>
        public static readonly Setting PrivacyPolicyUrl = new Setting("upnlRating", "Specify the URL for your Privacy Policy.");

        /// <summary>
        /// The skin used for the Calendar display
        /// </summary>
        public static readonly Setting SkinSelection = new Setting("SkinSelection", "The skin used for Calendar Display.");

        /// <summary>
        /// The number of events to display on a single day in the calendar's month view
        /// </summary>
        public static readonly Setting EventsPerDay = new Setting("EventsPerDay", "The number of events to display on a single day in the calendar's month view");

        /// <summary>
        /// The unsubscribe URL to use in emails
        /// </summary>
        public static readonly Setting UnsubscribeUrl = new Setting("unsubscribeUrl", "Specify the URL for unsubscribing.");

        /// <summary>
        /// Initializes a new instance of the <see cref="Setting"/> class.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="description">The description.</param>
        protected Setting(string propertyName, string description)
                : base(propertyName, description)
        {
        }
    }
}