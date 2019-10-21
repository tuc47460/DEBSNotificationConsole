﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace DEBSNotificationConsole
{
    class ProjectDB
    {
        public static DataSet GetProjectByID(int ProjectID)
        {
            //local var
            Connection conn = new Connection();
            SqlCommand sqlcomm = new SqlCommand("Project_by_id");
            DataSet TheDataSet = new DataSet();

            //set stored procedure command type
            sqlcomm.CommandType = CommandType.StoredProcedure;

            //Params
            sqlcomm.Parameters.AddWithValue("@ProjectID", ProjectID); //ID of target Project

            //try connection
            try
            {
                //get data set
                TheDataSet = conn.GetDataSetUsingCmdObj(sqlcomm);

                //If dataset !=Empty, return it
                if (TheDataSet.Tables[0].Rows.Count > 0)
                {
                    return TheDataSet;
                }//end if
                else
                {
                    return null;
                }//end else
            }
            catch (SqlException sqlex)
            {
                return null;
            }//end catch sql ex
            catch (Exception ex)
            {
                return null;
            }//end catch
        }//end GetProjectByID
    }//end ProjectDB
}
