using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEBSNotificationConsole
{
    class NotificationUsers
    {
        public int NotificationUserID { get; set; }
        public int BillSent { get; set; }
        public int BillTimer { get; set; }
        public string EmailAddress { get; set; }

        public NotificationUsers() { }//Empty Constructor

        public NotificationUsers(int notificationUserID, int billSent, int billTimer, string emailAddress)
        {
            this.NotificationUserID = notificationUserID;
            this.BillSent = billSent;
            this.BillTimer = billTimer;
            this.EmailAddress = emailAddress;
        }//end constructor

        //Utils
        public static List<NotificationUsers> GetAllNotificationUsers()
        {
            //local var
            List<NotificationUsers> NList = new List<NotificationUsers>();
            DataSet ds = new DataSet();

            //get dataset of invoice by id
            ds = NotificationUsersDB.GetAllNotificationUsers();

            //check for null DataSet
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        //localvar
                        NotificationUsers NU = new NotificationUsers
                            (int.Parse(dr["NotificationUserID"].ToString()), int.Parse(dr["BillSent"].ToString()), 
                                int.Parse(dr["BillTimer"].ToString()), dr["EmailAddress"].ToString());

                        //add to list
                        NList.Add(NU);
                    }//end foreach

                    //return list
                    return NList;

                }//end if inner
            }//end if outter
            return null;
        }//end GetAllNotificationUsers

        public static List<NotificationUsers> GetUsersWithTimeOutNotificationChecked(List<NotificationUsers> theList)
        {
            //local var
            List<NotificationUsers> NList = new List<NotificationUsers>();

            foreach (NotificationUsers u in theList)
            {
                //if timer is checked
                if (u.BillTimer == 1)
                {
                    //add to list
                    NList.Add(u);
                }//end if
            }//end foreach
            return NList;
        }//end GetUsersWithTimeOutNotificationChecked
    }//end NotificationUsers
}
