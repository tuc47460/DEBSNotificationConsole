using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEBSNotificationConsole
{
    class Receivable
    {
        //properties
        public int BillNo { get; set; }
        public int ProjectID { get; set; }
        public int ItemCount { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateSent { get; set; }
        public string SentByTuid { get; set; }
        public double AmountDue { get; set; }

        public Receivable() { }//Empty Constructor

        //Utils
        public static List<Receivable> GetAllReceivables()
        {
            //local var
            List<Receivable> RList = new List<Receivable>();
            DataSet ds = new DataSet();

            //get dataset of invoice by id
            ds = ReceivableDB.GetAllReceivables();

            //check for null DataSet
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        //local var
                        Receivable Rec = new Receivable();
                        //pass values
                        Rec.BillNo = int.Parse(dr["BillNo"].ToString());
                        Rec.ProjectID = int.Parse(dr["ProjectID"].ToString());
                        Rec.ItemCount = int.Parse(dr["InvoiceCount"].ToString());
                        Rec.DateCreated = DateTime.Parse(dr["DateCreated"].ToString());
                        Rec.DateSent = DateTime.Parse(dr["DateSent"].ToString());
                        Rec.SentByTuid = dr["SentByTUID"].ToString();
                        Rec.AmountDue = double.Parse(dr["AmountDue"].ToString());

                        //add to list
                        RList.Add(Rec);
                    }//end foreach

                    //Return List
                    return RList;
                }//end if inner
            }//end if outter
            return null;
        }//end GetReceivableByProjectID

    }//end Receivable Class
}
