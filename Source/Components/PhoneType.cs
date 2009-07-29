// <copyright file="PhoneType.cs" company="Engage Software">
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
    /// The type of a phone number
    /// </summary>
    public enum PhoneType
    {
        /// <summary>
        /// Not a phone number
        /// </summary>
        None = 0,

        /// <summary>
        /// A regular voice telephone
        /// </summary>
        Voice,

        /// <summary>
        /// A teletypewriter (a.k.a. TTD or text display device)
        /// </summary>
        Tty,

        /// <summary>
        /// A fax machine
        /// </summary>
        Fax,

        /// <summary>
        /// An internet-enabled video telephone
        /// </summary>
        Webcam
    }
}