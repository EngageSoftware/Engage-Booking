// <copyright file="SqlDataProvider.cs" company="Engage Software">
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
    using System.Configuration;
    using System.Data;
    using System.Data.SqlClient;
    using Data;
    using DotNetNuke.Framework.Providers;
    using Microsoft.ApplicationBlocks.Data;

    /// <summary>
    /// Base functionality for SQL Server data access
    /// </summary>
    public class SqlDataProvider
    {
        /// <summary>
        /// Singleton reference to <see cref="SqlDataProvider"/> instance
        /// </summary>
        private static readonly SqlDataProvider ProviderInstance = new SqlDataProvider();

        /// <summary>
        /// The prefix used for database objects belonging to this module
        /// </summary>
        private const string ModuleQualifier = "EngageBooking_";

        /// <summary>
        /// The connection string to access the database
        /// </summary>
        private readonly string connectionString;

        /// <summary>
        /// The prefix for all DNN database objects
        /// </summary>
        private readonly string objectQualifier;

        /// <summary>
        /// The owner or schema name to prefix object with
        /// </summary>
        private readonly string databaseOwner;

        /// <summary>
        /// Prevents a default instance of the <see cref="SqlDataProvider"/> class from being created.
        /// </summary>
        private SqlDataProvider()
        {
            // Read the configuration specific information for this provider
            ProviderConfiguration providerConfiguration = ProviderConfiguration.GetProviderConfiguration("data");
            Provider sqlDataProviderConfigSection = (Provider)providerConfiguration.Providers[providerConfiguration.DefaultProvider];
            if (!string.IsNullOrEmpty(sqlDataProviderConfigSection.Attributes["connectionStringName"]) && !string.IsNullOrEmpty(ConfigurationManager.AppSettings[sqlDataProviderConfigSection.Attributes["connectionStringName"]]))
            {
                this.connectionString = ConfigurationManager.AppSettings[sqlDataProviderConfigSection.Attributes["connectionStringName"]];
            }
            else
            {
                this.connectionString = sqlDataProviderConfigSection.Attributes["connectionString"];
            }

            this.objectQualifier = sqlDataProviderConfigSection.Attributes["objectQualifier"];
            if (!string.IsNullOrEmpty(this.objectQualifier) && !this.objectQualifier.EndsWith("_", StringComparison.OrdinalIgnoreCase))
            {
                this.objectQualifier += "_";
            }

            this.databaseOwner = sqlDataProviderConfigSection.Attributes["databaseOwner"];
            if (!string.IsNullOrEmpty(this.databaseOwner) && !this.databaseOwner.EndsWith(".", StringComparison.OrdinalIgnoreCase))
            {
                this.databaseOwner += ".";
            }
        }

        /// <summary>
        /// Gets the single instance of the <see cref="SqlDataProvider"/> type.
        /// </summary>
        /// <value>The <see cref="SqlDataProvider"/> instance.</value>
        public static SqlDataProvider Instance
        {
            get
            {
                return ProviderInstance;
            }
        }

        /// <summary>
        /// Gets the name prefix for Engage: Booking database objects.
        /// </summary>
        /// <value>The name prefix for Engage: Booking database objects.</value>
        public string NamePrefix
        {
            get
            {
                return this.databaseOwner + this.objectQualifier + ModuleQualifier;
            }
        }

        /// <summary>
        /// Executes a SQL stored procedure without returning any value.
        /// </summary>
        /// <param name="storedProcedureName">Name of the stored procedure.  Does not include any prefix, for example "InsertAppointment" is translated to "dnn_EngageBooking_spInsertAppointment."</param>
        /// <param name="parameters">The parameters for this query.</param>
        /// <exception cref="DBException">if there's an error while going to the database to retrieve the appointments</exception>
        public void ExecuteNonQuery(string storedProcedureName, params SqlParameter[] parameters)
        {
            try
            {
                SqlHelper.ExecuteNonQuery(
                    this.connectionString,
                    CommandType.StoredProcedure,
                    this.NamePrefix + "sp" + storedProcedureName,
                    parameters);
            }
            catch (SqlException exc)
            {
                throw new DBException(exc);
            }
        }

        /// <summary>
        /// Executes a SQL stored procedure, returning the results as a <see cref="SqlDataReader"/>.
        /// </summary>
        /// <param name="storedProcedureName">Name of the stored procedure.  Does not include any prefix, for example "GetAppointment" is translated to "dnn_EngageBooking_spGetAppointment."</param>
        /// <param name="parameters">The parameters for this query.</param>
        /// <returns>A <see cref="SqlDataReader"/> with the results of the stored procedure call</returns>
        /// <exception cref="DBException">if there's an error while going to the database to retrieve the appointments</exception>
        public SqlDataReader ExecuteReader(string storedProcedureName, params SqlParameter[] parameters)
        {
            try
            {
                return SqlHelper.ExecuteReader(
                        this.connectionString,
                        CommandType.StoredProcedure,
                        this.NamePrefix + "sp" + storedProcedureName,
                        parameters);
            }
            catch (SqlException exc)
            {
                throw new DBException(exc);
            }
        }

        /// <summary>
        /// Executes a SQL stored procedure, returning a single value.
        /// </summary>
        /// <param name="storedProcedureName">Name of the stored procedure.  Does not include any prefix, for example "InsertAppointment" is translated to "dnn_EngageBooking_spInsertAppointment."</param>
        /// <param name="parameters">The parameters for this query.</param>
        /// <returns>The single value returned from the stored procedure call</returns>
        /// <exception cref="DBException">if there's an error while going to the database to retrieve the appointments</exception>
        public object ExecuteScalar(string storedProcedureName, params SqlParameter[] parameters)
        {
            try
            {
                return SqlHelper.ExecuteScalar(
                        this.connectionString,
                        CommandType.StoredProcedure,
                        this.NamePrefix + "sp" + storedProcedureName,
                        parameters);
            }
            catch (SqlException exc)
            {
                throw new DBException(exc);
            }
        }
    }
}