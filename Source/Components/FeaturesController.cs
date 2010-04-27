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
#if TRIAL
    using System;
#endif
    using System.Diagnostics.CodeAnalysis;

    using DotNetNuke.Entities.Modules;
    using DotNetNuke.Services.Scheduling;
    
    /// <summary>
    /// Controls which DNN features are available for this module.
    /// </summary>
    [SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses", Justification = "Instantiated through reflection by DNN")]
    internal class FeaturesController : IUpgradeable
    {
#if TRIAL
        /// <summary>
        /// The license key for this module
        /// </summary>
        public static readonly Guid ModuleLicenseKey = new Guid("6F0F38B4-65CB-469D-B2FF-02C640CA5580");        
#endif

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
                    var scheduleItem = new ScheduleItem
                                           {
                                                   TypeFullName = "Engage.Dnn.Booking.EmailScheduler, EngageBooking",
                                                   TimeLapse = 5,
                                                   TimeLapseMeasurement = "m",
                                                   RetryTimeLapse = 10,
                                                   RetryTimeLapseMeasurement = "m",
                                                   RetainHistoryNum = 50,
                                                   AttachToEvent = "None",
                                                   CatchUpEnabled = true,
                                                   Enabled = true,
                                                   ObjectDependencies = string.Empty,
                                                   Servers = string.Empty
                                           };

                    SchedulingProvider.Instance().AddSchedule(scheduleItem);

                    return "Added EmailScheduler task.";
                default:
                    return "No tasks to perform for upgrade to version " + version;
            }
        }
    }
}