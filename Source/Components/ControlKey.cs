// <copyright file="ControlKey.cs" company="Engage Software">
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
    /// <summary>
    /// The keys to the controls of this module
    /// </summary>
    public enum ControlKey
    {
        /// <summary>
        /// The main calendar view
        /// </summary>
        Home = 0,

        /// <summary>
        /// The form to make a request for an appointment
        /// </summary>
        AppointmentRequest,

        /// <summary>
        /// The main calendar view with login enforced
        /// </summary>
        Approval,

        /// <summary>
        /// The control to directly accept or decline an appointment, via a key from an email
        /// </summary>
        DirectApproval,

        /// <summary>
        /// Provides a form to export the data to a CSV
        /// </summary>
        ExportData,

        /// <summary>
        /// Displays the details of a specific appointment
        /// </summary>
        AppointmentDetails
    }
}