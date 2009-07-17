// <copyright file="Utility.cs" company="Engage Software">
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
    using System.Text;
    using DotNetNuke.Common;
    using DotNetNuke.Services.Localization;

    /// <summary>
    /// All common, shared functionality for the Engage: Booking module.
    /// </summary>
    public static class Utility
    {
        /// <summary>
        /// The friendly name of this module.
        /// </summary>
        public const string DesktopModuleName = "Engage: Booking";

        /// <summary>
        /// The friendly name of this module's definition.
        /// </summary>
        public const string ModuleDefinitionFriendlyName = "Engage: Booking";

        /// <summary>
        /// Gets the name of the desktop module folder.
        /// </summary>
        /// <value>The name of the desktop module folder.</value>
        public static string DesktopModuleFolderName
        {
            get
            {
                StringBuilder sb = new StringBuilder(128);
                sb.Append("/DesktopModules/");
                sb.Append(Globals.GetDesktopModuleByName(DesktopModuleName).FolderName);
                sb.Append("/");
                return sb.ToString();
            }
        }

        /// <summary>
        /// Gets the relative path to the resource file holding resources that are shared by multiple controls within the module.
        /// </summary>
        /// <value>The relative path to the resource file holding resources that are shared by multiple controls within the module</value>
        public static string LocalSharedResourceFile
        {
            get { return "~" + DesktopModuleFolderName + Localization.LocalResourceDirectory + "/" + Localization.LocalSharedResourceFile; }
        }
    }
}