using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEBSNotificationConsole
{
    class Staff
    {
        public string TUID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Status { get; set; }
        public string Email { get; set; }

        public Staff() { }//Empty Constructor

        public Staff(string tuid, string fname, string lname, int status, string email)
        {
            this.TUID = tuid;
            this.FirstName = fname;
            this.LastName = lname;
            this.Status = status;
            this.Email = email;
        }//end constructor

        //Utils
        public static List<Staff> GetAllStaff()
        {
            //local var
            List<Staff> SList = new List<Staff>();
            DataSet ds = new DataSet();

            //get dataset of invoice by id
            ds = StaffDB.GetAllStaff();

            //check for null DataSet
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        //localvar
                        Staff staff = new Staff
                            (dr["TUID"].ToString(), dr["FirstName"].ToString(), dr["LastName"].ToString(), 
                            int.Parse(dr["Status"].ToString()), dr["Email"].ToString());

                        //add to list
                        SList.Add(staff);
                    }//end foreach

                    //return list
                    return SList;

                }//end if inner
            }//end if outter
            return null;
        }//end GetAllStaff

        public static Staff GetStaffByTUID(string TUID)
        {
            //local var
            List<Staff> SList = new List<Staff>();
            DataSet ds = new DataSet();

            //get dataset of invoice by id
            ds = StaffDB.GetStaffByTUID(TUID);

            //check for null DataSet
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        //localvar
                        Staff staff = new Staff
                            (dr["TUID"].ToString(), dr["FirstName"].ToString(), dr["LastName"].ToString(),
                            int.Parse(dr["Status"].ToString()), dr["Email"].ToString());

                        //add to list
                        return staff;
                    }//end foreach
                }//end if inner
            }//end if outter
            return null;
        }//end GetStaffByTUID

    }//end Staff Class
}
