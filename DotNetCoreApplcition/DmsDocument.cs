using IBM.Data.DB2.Core;
//using IBM.Data.Db2;
using System;

namespace DotNetCoreApplcition
{
    public class DmsDocument : IDataObject 
    {   
        public void FillData(DB2DataReader reader)
        {
            var values = new Object[reader.FieldCount];
            reader.GetValues(values);

            FillData(values);
        }
        public string dms_doc_status_key { get; set; }
        public int doc_no { get; set; }
        public string doc_location { get; set; }
        public string doc_status_cd { get; set; }
        public string computer_name { get; set; }
        public string touch_dt { get; set; }
        public string tirsdms_sysrowid { get; set; }
        public string no_of_attempts { get; set; }
        public string archive_status { get; set; }
        public string Employee_Key { get; set; }
        public string elm_flg { get; set; }
        public string Waiting_Days { get; set; }
        public string claim_key { get; set; }
        public string trty_key { get; set; }
        public string submis_key { get; set; }
        public string trans_key { get; set; }
        public string cash_rd_key { get; set; }
        public string prod_br_key { get; set; }
        public string xml_key { get; set; }
        public string principal_cd { get; set; }
        public string cash_bank_detail_key { get; set; }
        public string cash_allocation_key { get; set; }
        public string view_access_flg { get; set; }
        public int sysindexstate { get; set; }
        public string doc_type { get; set; }
        public string doc_name { get; set; }
        public string user_dept { get; set; }
        public string doc_key { get; set; }
        public string rpt_ofc_cd { get; set; }
        public string rpt_ofc_name { get; set; }
        public string trty_no { get; set; }
        public string und_yr { get; set; }
        public string doc_dt { get; set; }

        public void FillData(object[] values)
        {
            
            //dms_doc_status_key = values[0].ParseValue();
            doc_no = Convert.ToInt32(values[1].ParseValue()==""?0:values[1]);
            
            doc_location = values[4].ParseValue();
            
            //doc_status_cd = values[3].ParseValue();
            //computer_name = values[4].ParseValue();
            //touch_dt = values[5].ParseValue();
            //tirsdms_sysrowid = values[6].ParseValue();
            //no_of_attempts = values[7].ParseValue();
            //archive_status = values[8].ParseValue();
            //Employee_Key = values[9].ParseValue();
            //elm_flg = values[10].ParseValue();
            //Waiting_Days = values[11].ParseValue();
            //claim_key = values[12].ParseValue();
            //trty_key = values[13].ParseValue();
            //submis_key = values[14].ParseValue();
            //trans_key = values[15].ParseValue();
            //cash_rd_key = values[16].ParseValue();
            //prod_br_key = values[17].ParseValue();
            //xml_key = values[18].ParseValue();
            //principal_cd = values[19].ParseValue();
            //cash_bank_detail_key = values[20].ParseValue();
            //cash_allocation_key = values[21].ParseValue();
            //view_access_flg = values[22].ParseValue();
            //sysindexstate = Convert.ToInt32(values[23]);
            //doc_type = values[24].ParseValue();
            //doc_name = values[25].ParseValue();
            //user_dept = values[26].ParseValue();
            //doc_key = values[27].ParseValue();
            //rpt_ofc_cd = values[28].ParseValue();
            //rpt_ofc_name = values[29].ParseValue();
        }
    }
}