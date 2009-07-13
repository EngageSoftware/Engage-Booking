// <copyright file="EventDetail.ascx.cs" company="Engage Software">
// Engage: Booking - http://www.EngageSoftware.com
// Copyright (c) 2004-2009
// by Engage Software ( http://www.engagesoftware.com )
// </copyright>
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED 
// TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL 
// THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF 
// CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
// DEALINGS IN THE SOFTWARE.

namespace Engage.Dnn.Booking.Display
{
    using System;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using DotNetNuke.Common;
    using Framework.Templating;
    using Templating;

    /// <summary>
    /// Event Detail view.
    /// </summary>
    public partial class EventDetail : ModuleBase
    {
        /// <summary>
        /// Processes a tag for a template
        /// </summary>
        /// <param name="container">The container into which created controls should be added</param>
        /// <param name="tag">The tag to process</param>
        /// <param name="templateItem">The object to query for data to implement the given tag</param>
        /// <param name="resourceFile">The resource file to use to find get localized text.</param>
        /// <returns>Whether to process the tag's ChildTags collection</returns>
        internal static bool ProcessTag(Control container, Tag tag, ITemplateable templateItem, string resourceFile)
        {
            if (tag.TagType == TagType.Open && tag.LocalName.Equals("BACKHYPERLINK", StringComparison.OrdinalIgnoreCase))
            {
                HyperLink backHyperlink = new HyperLink();
                backHyperlink.NavigateUrl = Globals.NavigateURL();
                backHyperlink.CssClass = TemplateEngine.GetAttributeValue(tag, templateItem, resourceFile, "CssClass", "class");
                backHyperlink.Text = TemplateEngine.GetAttributeValue(tag, templateItem, resourceFile, "Text");
                
                container.Controls.Add(backHyperlink);
            }

            return true;
        }

        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.Init"/> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> object that contains the event data.</param>
        protected override void OnInit(EventArgs e)
        {
            this.SetupTemplateProvider();
            base.OnInit(e);
        }

        /// <summary>
        /// Sets up the <see cref="Dnn.Framework.ModuleBase.TemplateProvider"/> for this control.
        /// </summary>
        private void SetupTemplateProvider()
        {
            int? eventId = this.AppointmentId;
            if (eventId.HasValue)
            {
                Appointment appointment = Appointment.Load(eventId.Value);

                this.TemplateProvider = new SingleItemTemplateProvider(
                        this.GetTemplate(Utility.GetStringSetting(this.Settings, "SingleItemTemplate")),
                        this,
                        ProcessTag,
                        null,
                        appointment);
            }
        }
    }
}