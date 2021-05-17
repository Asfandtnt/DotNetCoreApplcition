using System;
using System.Collections.Generic;
using System.Dynamic;
using Azure.Storage.Blobs;
using Azure;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs.Specialized;
using System.IO;
using System.Threading;
using IBM.Data.DB2.Core;
//using IBM.Data.Db2;
using System.Data;

namespace DotNetCoreApplcition
{

    public static class Program
    {
        public static BlobServiceClient blobServiceClient;
        public static byte[] bytefile;
        public static string DBConnectionString = "Host=devprimarydb;Service=1536;Server=informixdb; User ID=jaguar; password=Ziplock2;Database=tirs; Protocol=onsoctcp;";

        public static void Main(string[] args)
        {
            string connectionString = "DefaultEndpointsProtocol=https;AccountName=tirswebdmsdev;AccountKey=nsCVPhuJfVvD2MMSF5eR1uJZFTrTJyhgM+quOiPq0XBRPSdc4H5eMOgLUEC9IY0nhcL8Bfy0xTI8POt1ZhCYFg==;EndpointSuffix=core.windows.net";
            blobServiceClient = new BlobServiceClient(connectionString);

            //var docToProcess = GetFirstDmsCopyOrUploadFileDetails();


            var mySql = "";

            mySql = GetQueryForFirstDmsDocument();
            // var response = DataAccessWrapper.GetListDataUtil<DmsDocument>(mySql);
            var response = GetListDataUtil<DmsDocument>(mySql);

            var docToProcess = response;






            // if (docToProcess != null)
            for (int i = 0; i < docToProcess.Count; i++)
            {

                testfunc(docToProcess[i].doc_location);


                string query = "update tirsdms set archive_status = 10 where doc_no = " + docToProcess[i].doc_no.ToString();
                DataAccessWrapper.ExecuteQuery(query, null);
                Console.WriteLine("Archive status is set to 10 too against Doc_no :" + docToProcess[i].doc_no.ToString());
                //change Archive status to -1 for now 
            }


        }
        public static string GetQueryForFirstDmsDocument()
        {
            //return "Select  First 1 " +
            //"A.dms_doc_status_key, A.doc_no, NVL(B.doc_location, '\\NoUNC\blob')  doc_location, A.doc_status_cd, A.computer_name, A.touch_dt, A.tirsdms_sysrowid, NVL(A.no_of_attempts, 0) no_of_attempts,  " +
            //"NVL(B.archive_status, 1) archive_status, B.Employee_Key, NVL(A.Elm_flg, 'N') elm_flg, (Date(current) - Date(B.Create_dt)) Waiting_Days, B.clm_key as claim_key,B.trty_key, B.submis_key,B.trans_key,B.cash_rd_key,B.prod_br_key,B.xml_key,B.principal_cd,B.cash_bank_detail_key,B.cash_allocation_key,B.view_access_flg,B.sysindexstate, B.doc_type, B.doc_name, B.user_dept, B.doc_key, B.rpt_ofc_cd,'' rpt_ofc_nm,B.trty_no,B.und_yr,B.doc_dt " +
            //"from   dms_doc_status A, tirsdms B " +
            //" Where  A.doc_no = B.doc_no  " +
            //"and B.archive_status = 1 and b.doc_location like '%https://tirswebdmsdev.blob.core.windows.net%' ";

            return "select xhdoc, sysrowid, doc_no, doc_name, doc_location, doc_dt, create_dt, doc_type, employee_key, rpt_ofc_cd, " +
            "user_dept, sysindexstate, repository, archive_status from tirsdms where archive_status = 1 " +
            "and doc_location like 'https://tirswebdmsdev.blob.core.windows.net%'";
        }

        public static List<DmsDocument> GetFirstDmsCopyOrUploadFileDetails()
        {

            var mySql = "";
            try
            {
                mySql = GetQueryForFirstDmsDocument();

                // var response = DataAccessWrapper.GetListDataUtil<DmsDocument>(mySql);
                var response = GetListDataUtil<DmsDocument>(mySql);

                if (response != null) return response;
            }
            catch (Exception ex)
            {

            }
            return null;
        }

        public static void testfunc(string doc_location)
        {
            List<ExpandoObject> test = null;
            var temp = doc_location.Split('/');
            if (temp != null)
            {
                test = ListAllFilesAsync(temp[temp.Length - 1].Trim());
            }
        }
        public static List<ExpandoObject> ListAllFilesAsync(string containerName)
        {
            try
            {

                BlobContainerClient containerClient = GetContainer(containerName);

                Pageable<BlobItem> blobs = containerClient.GetBlobs();
                dynamic responseObject = new ExpandoObject();
                responseObject.fileDownloadUrl = string.Empty;
                foreach (BlobItem blob in blobs)
                {

                    BlockBlobClient blobClient = containerClient.GetBlockBlobClient(blob.Name);

                    // Download the blob's contents and save it to a file
                    BlobDownloadInfo download = blobClient.Download();

                    bytefile = ReadToEnd(download.Content);
                    UploadToSharePoint("dmsqa", "DMSWebJobOutPut", blob.Name);
                    //Console.WriteLine(blob.Name + " is Uploaded on sharepoint online");
                }

                return null;
            }
            catch (Exception ex)
            {

            }
            return null;
        }
        public static byte[] ReadToEnd(System.IO.Stream stream)
        {
            long originalPosition = 0;

            if (stream.CanSeek)
            {
                originalPosition = stream.Position;
                stream.Position = 0;
            }

            try
            {
                byte[] readBuffer = new byte[4096];

                int totalBytesRead = 0;
                int bytesRead;

                while ((bytesRead = stream.Read(readBuffer, totalBytesRead, readBuffer.Length - totalBytesRead)) > 0)
                {
                    totalBytesRead += bytesRead;

                    if (totalBytesRead == readBuffer.Length)
                    {
                        int nextByte = stream.ReadByte();
                        if (nextByte != -1)
                        {
                            byte[] temp = new byte[readBuffer.Length * 2];
                            Buffer.BlockCopy(readBuffer, 0, temp, 0, readBuffer.Length);
                            Buffer.SetByte(temp, totalBytesRead, (byte)nextByte);
                            readBuffer = temp;
                            totalBytesRead++;
                        }
                    }
                }

                byte[] buffer = readBuffer;
                if (readBuffer.Length != totalBytesRead)
                {
                    buffer = new byte[totalBytesRead];
                    Buffer.BlockCopy(readBuffer, 0, buffer, 0, totalBytesRead);
                }
                return buffer;
            }
            finally
            {
                if (stream.CanSeek)
                {
                    stream.Position = originalPosition;
                }
            }
        }
        public class Data
        {
            public string Title { get; set; }
        }
        public static void UploadToSharePoint(string targetSiteName, string libraryName, string FileName)
        {
            const string DataAPIAllData = "{0}/_api/web/GetFolderByServerRelativeUrl('DMSWebJobOutPut')/Files/add(url='{1}',overwrite=true)";


            var results = new List<Data>();

            string sharepointSiteUrl = Convert.ToString("https://transre.sharepoint.com/sites/dmsqa");
            if (!string.IsNullOrEmpty(sharepointSiteUrl))
            {
                string listname = "Shared Documents";
                string api = string.Format(DataAPIAllData, sharepointSiteUrl, FileName);
                if (!string.IsNullOrEmpty(listname))
                {
                    //Invoke REST Call  
                    string response = GetAPIResponse(api, bytefile);
                }
            }
        }
        public static string GetAPIResponse(string url, byte[] bytefile)
        {
            string response = String.Empty;
            try
            {
                //Call to get AccessToken  
                string accessToken = GetSharePointAccessToken();
                //Call to get the REST API response from Sharepoint  
                System.Net.HttpWebRequest endpointRequest = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(url);
                endpointRequest.Method = "POST";
                endpointRequest.Headers.Add("binaryStringRequestBody", "true");
                endpointRequest.GetRequestStream().Write(bytefile, 0, bytefile.Length);
                //endpointRequest.Accept = "application/json;odata=verbose";
                endpointRequest.Timeout = 1000000;
                endpointRequest.Headers.Add("Authorization", "Bearer " + accessToken);
                System.Net.WebResponse webResponse = endpointRequest.GetResponse();

                Stream webStream = webResponse.GetResponseStream();
                StreamReader responseReader = new StreamReader(webStream);
                response = responseReader.ReadToEnd();
                return response;
            }
            catch (Exception ex)
            {
                throw;
            }

        }
        public static string GetSharePointAccessToken()
        {
            Uri site = new Uri("https://transre.sharepoint.com/sites/dmsqa");
            string user = "Svc_devspadmin@transre.com";
            string pwd = "TRCtrc0920!";

            //string user = "usman@yetanotherdev.onmicrosoft.com";
            //string pwd = "wcVvkqV4HKjLRGL";
            string result;
            using (var authenticationManager = new AuthManager())
            {
                string accessTokenSP = authenticationManager.AcquireTokenAsync(site, user, pwd).Result;
                result = accessTokenSP;
            }
            return result;
        }



        private static BlobContainerClient GetContainer(string containerName)
        {
            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerName);
            return containerClient;
        }
        public static List<T> GetListDataUtil<T>(string query, params string[] paramsList)
        {

            try
            {
                Console.Out.WriteLine("1");
                var respone = new List<T>();
                Console.Out.WriteLine("2");
                string InformixConnectionString = "Server=10.90.18.232:1540;UID=ttester;PWD=Trc2019!;Database=TIRS;TimeOut=300";
                                                  

                Console.Out.WriteLine("3");
                using (var informixConn = new DB2Connection(InformixConnectionString))
                {
                    Console.Out.WriteLine("4");
                    informixConn.Open();
                    Console.Out.WriteLine("5");
                    var selectCommand = new DB2Command(query) { Connection = informixConn };

                    if (paramsList != null)
                    {
                        foreach (var param in paramsList)
                        {
                            selectCommand.Parameters.Add(param);
                        }
                    }

                    using (var reader = selectCommand.ExecuteReader(CommandBehavior.Default))
                    {
                        while (reader.Read())
                        {
                            var val = (IDataObject)Activator.CreateInstance<T>();
                            val.FillData(reader);
                            respone.Add((T)val);
                            Console.Out.WriteLine("Filling data----");
                        }
                        reader.Close();
                        reader.Dispose();
                    }
                    informixConn.Close(); informixConn.Dispose();
                }

                Console.Out.WriteLine("Return response----22");
                return respone;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exceptin Asfand");
                Console.Out.WriteLine(ex);
                return new List<T>();
            }

        }
    }

}
