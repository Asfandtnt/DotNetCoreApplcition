using System;
using System.Configuration;

namespace DotNetCoreApplcition
{
    public static class AppGlobal
    {
        public static long StartDocNumber;
        public static long EndDocNumber;
        public static string DocumentNumber;
        //Modes: ELM, DMS, Both
        public static string ProcessFileMode;
        //Share Point Upload Site Address
        public static string SharePointSiteAddress;
        public static string LogEntries;
        public static string LogDir;
        public static string UserName;
        public static string IsOffice365;
        public static string Password;
        public static string Domain;
        public static string UserNameSP2010;
        public static string PasswordSP2010;
        public static string DomainSP2010;
        public static string ConnectionString;
        public static string Environment;
        public static string BaseSiteUrl;
        public static string BaseSiteUrlSP2010;
        public static string RootSiteCollection;
        public static string Region;
        public static string ELMProcessMigrationFolders;
        public static string ELMProcessModeCutOffDateTime;
        public static string ArchiveFolderPath;
        public static string TempFolderPath;
        public static string ELMFolderPath;
        public static string ElmTempSiteUrl;
        public static string ElmTempLibraryName;
        public static string DmsKeyWord;
        public static string WebTemplate;
        public static int FileSizeToleranceLimitPct;
        public static string TestDocumentsToBeProcessed;
        public static string TestElmDocumentsToBeProcessed;
        public static string FileLocation;
        public static string Migration_UserDept;
        public static string Migration_ReportingOffice;
        public static string Migration_Work_SpDocument;
        public static string Migration_TargetSiteCollection;
        public static string Migration_LogDir;

        public static string CT_DMS_GUID;

        public static string MigrationTargetDocumentLibrary;
        public static string CommonSiteName;

        public static string LibAccounting;
        public static string LibSubmission;
        public static string LibUnderwriting;
        public static string LibClaims;
        public static string LibCash;
        public static string LibCollection;
        public static string LibUSU;
        public static string LibPBuyer;
        public static string LibOther;

        public static string MaxAttemptsForUpload;
        public static long MaxiFileSizeToUploadInMB;
        public static int FileChunkSizeInMB;
        public static int DelayTimeInMinutesForNextAttempt;
        public static int ELMCompletedStatusCode;

        public static int TerminateAllowance;
        public static string TerminateFileDirectory;
        public static string LargeFileUploadTempPath;

        public static string LocalShareBackupDirectory;

        public static int ResetFailedRecordsInDMS;
        public static string NewDMSProgramStartDate;
        public static int ProcessFailedRecordsAfterMinutes;

        //SP 2010 and SP 2013 Code Logic Commented -> Start
        public static string DMSUploadToSharePoint;
        public static string DMSUploadToSharePointLibraryName;
        public static string ContentTypeName;
        //SP 2010 and SP 2013 Code Logic Commented -> End

        /// <summary>
        /// Method to set Application Global Variables
        /// Use App.config to read values
        /// </summary>
        public static void SetGlobalValues()
        {
            DocumentNumber = ConfigurationManager.AppSettings["TestDocumentNumber"];
            var startDocNo = ConfigurationManager.AppSettings["StartingDocumentNumber"];
            var endDocNo = ConfigurationManager.AppSettings["EndingDocumentNumber"];
            var fileSizeToleranceLimitPct = ConfigurationManager.AppSettings["FileSizeToleranceLimitPCT"];
            StartDocNumber = Convert.ToInt64(String.IsNullOrEmpty(startDocNo) ? "0" : startDocNo);
            EndDocNumber = Convert.ToInt64(String.IsNullOrEmpty(endDocNo) ? "0" : endDocNo);
            ProcessFileMode = ProcessFileMode ?? ConfigurationManager.AppSettings["ProcessFileMode"].ToUpper();
            SharePointSiteAddress = ConfigurationManager.AppSettings["SharePointSiteAddress"];
            MigrationTargetDocumentLibrary = ConfigurationManager.AppSettings["Migration_TargetDocumentLibrary"];
            CommonSiteName = ConfigurationManager.AppSettings["CommonSiteName"];
            LogEntries = ConfigurationManager.AppSettings["LogEntries"];
            LogDir = ConfigurationManager.AppSettings["LogDir"];
            UserName = ConfigurationManager.AppSettings["UserName"];
            IsOffice365 = ConfigurationManager.AppSettings["IsOffice365"];
            Region = ConfigurationManager.AppSettings["Region"];
            Password = ConfigurationManager.AppSettings["Password"];
            Domain = ConfigurationManager.AppSettings["Domain"];
            UserNameSP2010 = ConfigurationManager.AppSettings["UserNameSP2010"];
            PasswordSP2010 = ConfigurationManager.AppSettings["PasswordSP2010"];
            DomainSP2010 = ConfigurationManager.AppSettings["DomainSP2010"];
            ConnectionString = ConfigurationManager.AppSettings["DBConnectionString"];
            Environment = ConfigurationManager.AppSettings["Environment"];
            BaseSiteUrl = ConfigurationManager.AppSettings["BaseSiteUrl"];
            BaseSiteUrlSP2010 = ConfigurationManager.AppSettings["BaseSiteUrlSP2010"];
            RootSiteCollection = ConfigurationManager.AppSettings["RootSiteCollection"];
            ELMProcessMigrationFolders = ConfigurationManager.AppSettings["ELMProcessMigrationFolders"];
            ELMProcessModeCutOffDateTime = ConfigurationManager.AppSettings["ELMProcessModeCutOffDateTime"];
            ArchiveFolderPath = ConfigurationManager.AppSettings["ArchiveFolderPath"];
            TempFolderPath = ConfigurationManager.AppSettings["TempFolderPath"];
            ELMFolderPath = ConfigurationManager.AppSettings["ELMFolderPath"];
            ElmTempSiteUrl = ConfigurationManager.AppSettings["ELMSite"];
            ElmTempLibraryName = ConfigurationManager.AppSettings["ELMSourceLibraryName"];
            DmsKeyWord = ConfigurationManager.AppSettings["DmsKeyWord"];
            WebTemplate = ConfigurationManager.AppSettings["WebTemplate"];
            FileSizeToleranceLimitPct = Convert.ToInt32(String.IsNullOrEmpty(fileSizeToleranceLimitPct) ? "0" : fileSizeToleranceLimitPct);
            if (string.IsNullOrEmpty(AppGlobal.TestDocumentsToBeProcessed) || AppGlobal.TestDocumentsToBeProcessed == "0")
            {
                TestDocumentsToBeProcessed = ConfigurationManager.AppSettings["TestDocumentsToBeProcessed"];
            }

            TestElmDocumentsToBeProcessed = ConfigurationManager.AppSettings["TestElmDocumentsToBeProcessed"];
            FileLocation = ConfigurationManager.AppSettings["FileLocation"];
            Migration_UserDept = ConfigurationManager.AppSettings["Migration_UserDept"];
            Migration_ReportingOffice = ConfigurationManager.AppSettings["Migration_ReportingOffice"];
            Migration_Work_SpDocument = ConfigurationManager.AppSettings["Migration_Work_SpDocument"];
            Migration_TargetSiteCollection = ConfigurationManager.AppSettings["Migration_TargetSiteCollection"];
            Migration_LogDir = ConfigurationManager.AppSettings["Migration_LogDir"];

            //Content Type Guid            
            CT_DMS_GUID = ConfigurationManager.AppSettings["CT_DMS_GUID"];

            //Library Names
            LibAccounting = ConfigurationManager.AppSettings["LibAccounting"];
            LibSubmission = ConfigurationManager.AppSettings["LibSubmission"];
            LibUnderwriting = ConfigurationManager.AppSettings["LibUnderwriting"];
            LibClaims = ConfigurationManager.AppSettings["LibClaims"];
            LibCash = ConfigurationManager.AppSettings["LibCash"];
            LibCollection = ConfigurationManager.AppSettings["LibCollection"];
            LibUSU = ConfigurationManager.AppSettings["LibUSU"];
            LibPBuyer = ConfigurationManager.AppSettings["LibPBuyer"];
            LibOther = ConfigurationManager.AppSettings["LibOther"];

            MaxAttemptsForUpload = ConfigurationManager.AppSettings["MaxAttemptsForUpload"];
            MaxiFileSizeToUploadInMB = Convert.ToInt64(ConfigurationManager.AppSettings["MaxiFileSizeToUploadInMB"]);
            FileChunkSizeInMB = Convert.ToInt32(ConfigurationManager.AppSettings["FileChunkSizeInMB"]);
            DelayTimeInMinutesForNextAttempt = Convert.ToInt32(ConfigurationManager.AppSettings["DelayTimeInMinutesForNextAttempt"]);
            ELMCompletedStatusCode = Convert.ToInt32(ConfigurationManager.AppSettings["ELMCompletedStatusCode"]);

            //Terminate File
            TerminateAllowance = Convert.ToInt32(ConfigurationManager.AppSettings["TerminateAllowance"]);
            TerminateFileDirectory = ConfigurationManager.AppSettings["TerminateFileDirectory"].ToString();

            LargeFileUploadTempPath = ConfigurationManager.AppSettings["LargeFileUploadTempPath"].ToString();

            LocalShareBackupDirectory = ConfigurationManager.AppSettings["LocalShareBackupDirectory"].ToString();

            ResetFailedRecordsInDMS = Convert.ToInt32(ConfigurationManager.AppSettings["ResetFailedRecordsInDMS"]);
            NewDMSProgramStartDate = ConfigurationManager.AppSettings["NewDMSProgramStartDate"].ToString();
            ProcessFailedRecordsAfterMinutes = Convert.ToInt32(ConfigurationManager.AppSettings["ProcessFailedRecordsAfterMinutes"]);

            //SP 2010 and SP 2013 Code Logic Commented -> Start
            DMSUploadToSharePoint = ConfigurationManager.AppSettings["DMSUploadToSharePoint"].ToString();
            DMSUploadToSharePointLibraryName = ConfigurationManager.AppSettings["DMSUploadToSharePointLibraryName"].ToString();
            ContentTypeName = ConfigurationManager.AppSettings["ContentTypeName"].ToString();
            //SP 2010 and SP 2013 Code Logic Commented -> End
        }
        /// <summary>
        /// Method to validate Global Variables
        /// </summary>
        /// <returns></returns>
        public static bool ValidateGlobalVariables()
        {
            //Checking if below properties are null or empty then stop program execution and log error
            return !string.IsNullOrEmpty(ProcessFileMode) && !string.IsNullOrEmpty(SharePointSiteAddress);
        }
    }

}


