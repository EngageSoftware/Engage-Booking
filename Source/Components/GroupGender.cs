// <copyright file="GroupGender.cs" company="Engage Software">
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
    /// The gender of a group of people
    /// </summary>
    public enum GroupGender
    {
        /// <summary>
        /// A group with both males and females
        /// </summary>
        MixedGroup = 0,

        /// <summary>
        /// A group of solely females
        /// </summary>
        Female,

        /// <summary>
        /// A group of solely males
        /// </summary>
        Male
    }
}