using NCB_DPC_FRONT.DAL;
using NCB_DPC_FRONT.Models;
using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI.WebControls.WebParts;

namespace NCB_DPC_FRONT.WP_DPCCourseDetails
{
    [ToolboxItemAttribute(false)]
    public partial class WP_DPCCourseDetails : WebPart
    {
        // Uncomment the following SecurityPermission attribute only when doing Performance Profiling on a farm solution
        // using the Instrumentation method, and then remove the SecurityPermission attribute when the code is ready
        // for production. Because the SecurityPermission attribute bypasses the security check for callers of
        // your constructor, it's not recommended for production purposes.
        // [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Assert, UnmanagedCode = true)]

        //private DBDataAccess dbDataAccess = new DBDataAccess();
        public WP_DPCCourseDetails()
        {
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            InitializeControl();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //Get course details from URL parameter value
            string batchRef = HttpContext.Current.Request.QueryString["batchRef"];
            setCourseDetails(batchRef);

            //CourseDetailsModel getCourseDetails = new CourseDetailsModel();
            //getCourseDetails = dbDataAccess.GetCourseDetails(paramValue);
            //setCourseDetails(getCourseDetails);
        }

        private void setCourseDetails(string batchReference)
        {
            string batchRef = HttpContext.Current.Request.QueryString["batchRef"];
            string connString = "Data Source=GVP-STG-DB19;Initial Catalog=Mauritius_Eservices;Integrated Security=True";

            // Assuming you have a SQL query to fetch the data
            string query = "SELECT * FROM DPC_Batch_Training WHERE Batch_Reference = @BatchRef";

            using (SqlConnection sqlCon = new SqlConnection(connString))
            {
                sqlCon.Open();
                SqlDataAdapter sqlData = new SqlDataAdapter();
                SqlCommand command = new SqlCommand(query, sqlCon);
                command.Parameters.AddWithValue("@BatchRef", batchReference);
                sqlData.SelectCommand = command;
                DataTable dtbl = new DataTable();
                //sqlData.Fill(dtbl);

                if (dtbl.Rows.Count > 0)
                {
                    DataRow row = dtbl.Rows[0];
                    //txt_courseName.Text = row["courseName"].ToString();
                    //txt_description.Text = row["courseDescription"].ToString();
                    txt_startDateOfDPCCourse.Text = row["Start_Date"].ToString();
                    txt_endDateOfDPCCourse.Text = row["End_date"].ToString();
                    txt_daysOfCourse.Text = row["Days"].ToString();
                    txt_timeOfCourse.Text = row["Time"].ToString();
                    txt_duration.Text = row["Duration"].ToString();
                    txt_closingDate.Text = row["Expiry_date"].ToString();
                    txt_location.Text = row["Training_centre"].ToString();
                }


            }

            // Assuming dtbl is populated with the course details from the database

            //Set Attachments
        }

        protected void btn_submit_Click(object sender, EventArgs e)
        {

        }
    }
}
