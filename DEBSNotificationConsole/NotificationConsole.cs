using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Console application for Digital Education Billing System Email Notification System
//Written by Robert Convery
//10/21/2019

namespace DEBSNotificationConsole
{
    class NotificationConsole
    {
        static void Main(string[] args)
        {

            //local var
            NotificationTimer Timer = new NotificationTimer();
            List<Receivable> RList = new List<Receivable>();
            List<NotificationUsers> NList = new List<NotificationUsers>();
            List<Staff> SList = new List<Staff>();

            //get timer
            Timer = GetNotificationTimer();

            //get staff list
            SList = GetStaffList();

            //get user notification list
            NList = GetUserNotificationList();

            //reduce List to checked users
            NList = GetCheckedUsersList(NList);

            //get receivable list
            RList = GetReceivableList();
            RList = GetTimedOutReceivablesList(Timer, RList);

            //send emails
            SendEmails(RList, NList, SList);

            Console.ReadKey();

        }//end main

        private static List<Staff> GetStaffList()
        {
            List<Staff> SList;
            Console.WriteLine("Attempting to get Staff List...");
            SList = Staff.GetAllStaff();
            if (SList.Count > 0)
            {
                Console.WriteLine("Staff List = SUCCESS");
            }//end if
            else
            {
                Console.WriteLine("Staff List = FAILED");
            }//end else

            return SList;
        }

        private static void SendEmails(List<Receivable> RList, List<NotificationUsers> NList, List<Staff> SList)
        {
            Console.WriteLine("Starting to send emails...");
            foreach (Receivable r in RList)
            {
                //Get Project info for Receivable
                Project p = GetProjectInfo(r);
                string emailTitle = p.Name + " has an unsent invoice";
                string emailBody = p.Name + " has unsent invoice BillNo - " + r.BillNo;

                foreach (NotificationUsers NU in NList)
                {
                    //local var
                    Staff staff = new Staff();

                    //get staff info
                    staff = Staff.GetStaffByTUID(NU.TUID);

                    if (Email.SendEmail("Notifications@Temple.edu", "DO NOT REPLY", emailTitle, emailBody, staff.Email))
                    {
                        Console.WriteLine("Email Sent successfully to " + staff.Email + " for Project " + p.ProjectID);
                    }//end if 
                }//end foreach inner
            }//end foreach outter
        }//end SendEmails

        private static Project GetProjectInfo(Receivable r)
        {
            Console.WriteLine("Attempting to get Project info for Project " + r.ProjectID);
            Project p = Project.GetProjectByID(r.ProjectID);
            if (p != null)
            {
                Console.WriteLine("Project info = SUCCESS");
            }//end if
            else
            {
                Console.WriteLine("Project info = FAILED");
            }//end else

            return p;
        }

        private static List<Receivable> GetTimedOutReceivablesList(NotificationTimer Timer, List<Receivable> RList)
        {
            Console.WriteLine("Reducing Receivable List...");
            RList = new List<Receivable>(GetTimedOutReceivables(Timer, RList)); //Reduce RList to Receivables that are timed out
            Console.WriteLine("List Reduced");
            return RList;
        }

        private static List<Receivable> GetReceivableList()
        {
            List<Receivable> RList;
            Console.WriteLine("Attempting to get Receivable List...");
            RList = Receivable.GetAllReceivables();
            if (RList.Count > 0)
            {
                Console.WriteLine("Receivable List = SUCCESS");
            }//end if
            else
            {
                Console.WriteLine("Receivable List = FAILED");
            }//end else

            return RList;
        }

        private static List<NotificationUsers> GetCheckedUsersList(List<NotificationUsers> NList)
        {
            Console.WriteLine("Reducing Notification List...");
            NList = NotificationUsers.GetUsersWithTimeOutNotificationChecked(NList); //Reduce NList to Users that have timed out notifications checked
            Console.WriteLine("List Reduced");
            return NList;
        }

        private static List<NotificationUsers> GetUserNotificationList()
        {
            List<NotificationUsers> NList;
            Console.WriteLine("Attempting to get User Notification Settings...");
            NList = NotificationUsers.GetAllNotificationUsers();
            if (NList != null)
            {
                Console.WriteLine("User Notification Settings = SUCCESS");
            }//end if
            else
            {
                Console.WriteLine("User Notification Settings = FAILED");
            }//end else

            return NList;
        }

        private static NotificationTimer GetNotificationTimer()
        {
            NotificationTimer Timer;
            Console.WriteLine("Attempting to get Notification Timer...");
            Timer = NotificationTimer.GetNotificationTimer();
            if (Timer != null)
            {
                Console.WriteLine("Notification Timer = SUCCESS");
            }//end if
            else
            {
                Console.WriteLine("Notification Timer = FAILED");
            }//end else

            return Timer;
        }

        //Utils
        public static List<Receivable> GetTimedOutReceivables(NotificationTimer NotificationTimer, List<Receivable> ReceivableList)
        {
            //local var
            List<Receivable> RList = new List<Receivable>();
            int timeoutDays = NotificationTimer.Amount * NotificationTimer.Unit; //get total number of days before being timed out

            foreach (Receivable R in ReceivableList)
            {
                if (R.DateSent != default(DateTime))
                {
                    //local var
                    double totalsdays = -1;

                    //get total number of days
                    totalsdays = (DateTime.Now - R.DateCreated).TotalDays;

                    if (totalsdays >= timeoutDays)
                    {
                        RList.Add(R);
                    }//end if inner
                }//end if outter
            }//end foreach
            return RList;
        }//end GetTimedOutReceivables

    }
}
