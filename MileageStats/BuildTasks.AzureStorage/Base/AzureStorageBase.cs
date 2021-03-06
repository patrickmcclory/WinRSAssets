﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

namespace BuildTasks.AzureStorage.Base
{
    /// <summary>
    /// Base class for encapsulating common processes/workflows for accessing Windows Azure Storage
    /// </summary>
    public abstract class AzureStorageBase : Task
    {
        #region Build task properties

        /// <summary>
        /// Defines the default protocol to be used for accessing the Blob Endpoint
        /// </summary>
        public string DefaultEndpointsProtocol { get; set; }

        /// <summary>
        /// Endpoint URL for custom endpoint names.  This input is not required when the standard endpoint format is sufficient.
        /// </summary>
        public string BlobEndpoint { get; set; }

        /// <summary>
        /// Account name to be used when accessing Windows Azure Storage
        /// </summary>
        [Required]
        public string AccountName { get; set; }

        /// <summary>
        /// Account key to be used when accessing Windows Azure Storage
        /// </summary>
        [Required]
        public string AccountKey { get; set; }

        /// <summary>
        /// Container name within the storage account being accessed 
        /// </summary>
        [Required]
        public string ContainerName { get; set; }

        /// <summary>
        /// Name of the Blob being acted upon in Windows Azure Storage - this can either be the name of a Blob being uploaded, downloaded, removed, etc.
        /// </summary>
        [Required]
        public string BlobName { get; set; }

        /// <summary>
        /// Flag indicates whether the local Windows Azure Emulator will be used for storage
        /// </summary>
        public bool UseDevelopmentStorage { get; set; }

        /// <summary>
        /// Flag indicates whether the task is in debug mode and is used to output security-sensitive information such as connection strings and credentials to the console for debug work
        /// </summary>
        public bool DebugMode { get; set; }

        #endregion

        /// <summary>
        /// Constructor initializes base object's optional properties
        /// </summary>
        public AzureStorageBase()
        {
            this.DefaultEndpointsProtocol = "https";
            this.UseDevelopmentStorage = false;
            this.DebugMode = false;
        }

        /// <summary>
        /// Method builds Windows Azure Storage connection string from the properties input at runtime for the task
        /// </summary>
        /// <returns>Properly formatted Windows Azure Storage Connection String</returns>
        protected string GetStorageConnectionString()
        {
            string retVal = string.Empty;
            if (this.UseDevelopmentStorage)
            {
                retVal += "UseDevelopmentStorage=" + this.UseDevelopmentStorage.ToString() + ";";
            }

            retVal += "DefaultEndpointsProtocol=" + this.DefaultEndpointsProtocol + ";";
            retVal += "AccountName=" + this.AccountName + ";";
            retVal += "AccountKey=" + this.AccountKey + ";";

            if (!string.IsNullOrWhiteSpace(this.BlobEndpoint))
            {
                retVal += "BlobEndpoint=" + this.BlobEndpoint + ";";
            }

            retVal = retVal.TrimEnd(';');

            Log.LogMessage("    AzureStorageBase.GetStorageConnectionString - done building connection string");
            return retVal;
        }

    }
}
