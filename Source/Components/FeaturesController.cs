// <copyright file="FeaturesController.cs" company="Engage Software">
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
    using System.Collections;
    using DotNetNuke.Entities.Modules;
    using DotNetNuke.Services.Scheduling;
    
    /// <summary>
    /// Controls which DNN features are available for this module.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses", Justification = "Instantiated through reflection by DNN")]
    internal class FeaturesController : IUpgradeable
    {
        /// <summary>
        /// Upgrades the module.
        /// </summary>
        /// <param name="version">The version.</param>
        /// <returns>A success or failure message.</returns>
        public string UpgradeModule(string version)
        {
            switch (version)
            {
                case "1.0.0":
                    var scheduleItem = new ScheduleItem();
                    scheduleItem.TypeFullName = "Engage.Dnn.Booking.EmailScheduler, EngageBooking";
                    scheduleItem.TimeLapse = 30;
                    scheduleItem.TimeLapseMeasurement = "m";
                    scheduleItem.RetryTimeLapse = 10;
                    scheduleItem.RetryTimeLapseMeasurement = "m";
                    scheduleItem.RetainHistoryNum = 50;
                    scheduleItem.AttachToEvent = "None";
                    scheduleItem.CatchUpEnabled = true;
                    scheduleItem.Enabled = true;
                    scheduleItem.ObjectDependencies = string.Empty;
                    scheduleItem.Servers = string.Empty;

                    SchedulingProvider.Instance().AddSchedule(scheduleItem);

                    return "Added EmailScheduler task.";
                default:
                    return "No tasks to perform for upgrade to version " + version;
            }
        }
    }
}