using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEBSNotificationConsole
{
    public class Project
    {
        public int ProjectID { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
        public int Status { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime LastEdited { get; set; }
        public string LastEditTUID { get; set; }
        public string CreatedByTUID { get; set; }
        public string ProjectNotes { get; set; }

        public static Project GetProjectByID(int ProjectID)
        {
            //local var
            Project theProject = new Project();
            DataSet ds = new DataSet();

            //get dataset of invoice by id
            ds = ProjectDB.GetProjectByID(ProjectID);

            //check for null DataSet
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        //pass values
                        theProject.ProjectID = int.Parse(dr["ProjectID"].ToString());
                        theProject.Name = dr["Name"].ToString();
                        theProject.Desc = dr["Desc"].ToString();
                        theProject.Status = int.Parse(dr["Status"].ToString());
                        theProject.StartDate = DateTime.Parse(dr["StartDate"].ToString());
                        theProject.EndDate = DateTime.Parse(dr["EndDate"].ToString());
                        theProject.LastEdited = DateTime.Parse(dr["LastEdited"].ToString());
                        theProject.LastEditTUID = dr["LastEditTUID"].ToString();
                        theProject.ProjectNotes = dr["ProjectNotes"].ToString();

                    }//end foreach

                    return theProject;

                }//end if inner
            }//end if outter
            return null;
        }
        
    }
}
