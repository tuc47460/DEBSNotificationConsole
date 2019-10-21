using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEBSNotificationConsole
{
    class NotificationTimer
    {
        public int NotificationTimerID { get; set; }
        public int Amount { get; set; }
        private string unit;

        //Dictionary
        readonly Dictionary<string, int> UDict = new Dictionary<string, int>()
                                            {
                                                {"days", 1},
                                                {"weeks", 7},
                                                {"months", 30},
                                                {"years", 365}
                                            };//end UDict

        public int Unit
        { get { return UDict[unit]; } }

        public NotificationTimer() { }//Empty Constructor

        public NotificationTimer(int notificationTimerID, int amount, string unit)
        {
            this.NotificationTimerID = notificationTimerID;
            this.Amount = amount;
            this.unit = unit;
        }//end constructor

        //Utils
        public static NotificationTimer GetNotificationTimer()
        {
            //local var
            DataSet ds = new DataSet();

            //get dataset of invoice by id
            ds = NotificationTimerDB.GetAllNotificationTimer();

            //check for null DataSet
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        return new NotificationTimer
                            (int.Parse(dr["NotificationTimerID"].ToString()), int.Parse(dr["Amount"].ToString()),dr["Unit"].ToString());
                    }//end foreach

                }//end if inner
            }//end if outter
            return null;
        }//end GetNotificationTimer
    }//end NotificationTimer Class
}
