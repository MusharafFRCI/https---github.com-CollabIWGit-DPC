using System;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using NCB_DPC_FRONT.Utils;
using System.Web;
using System.Web.UI;
using NCB_DPC_FRONT.DAL;
using Microsoft.SharePoint;
using System.Collections.Generic;

namespace NCB_DPC_FRONT.WP_DPCAllCourses
{
    [ToolboxItemAttribute(false)]
    public partial class WP_DPCAllCourses : WebPart
    {

        private SqlConnection conn = new SqlConnection(GlobalVar.ConnectionString);
        private DBDataAccess dbDataAccess = new DBDataAccess();


        // Uncomment the following SecurityPermission attribute only when doing Performance Profiling on a farm solution
        // using the Instrumentation method, and then remove the SecurityPermission attribute when the code is ready
        // for production. Because the SecurityPermission attribute bypasses the security check for callers of
        // your constructor, it's not recommended for production purposes.
        // [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Assert, UnmanagedCode = true)]
        public WP_DPCAllCourses()
        {
        }

        //private DBDataAccess dbDataAccess = new DBDataAccess();


        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            InitializeControl();
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                float price = dbDataAccess.GetCoursePrice();

//                List<string> phrases = new List<string>() {
//  "Registration for course to be made via this platform ONLY.",
//  "Course Fee: Rs "+price,
//  "Payment: By Bank Card or Cash",
//  "In case of cash payment, the fee of Rs "+price+" to be paid at MDPA Office (Ebene) prior to registration on this platform."
//};

                

//                HtmlGenericControl ul = Page.FindControl("ul_fee") as HtmlGenericControl;

//                foreach (string phrase in phrases)
//                {
//                    HtmlGenericControl li = new HtmlGenericControl("li");
//                    li.InnerHtml = phrase;

//                    ul.Controls.Add(li);
//                }






                //PopulateTrainingCentre("Online");


          
            }

            else
            {

            }

                
            // PopulateCourses();
        }

        public void loadNotes()
        {

        }

        public void PopulateTrainingCentre(string mode)
        {
            // Assuming you have a SQL query to fetch the data
            string query = "SELECT * FROM DPC_TrainingCentre WHERE Mode = @mode AND Active_Inactive_flag = 'Active'";


            try
            {
                using (SqlConnection conn = new SqlConnection(GlobalVar.ConnectionString))

                {
                    SPSecurity.RunWithElevatedPrivileges(delegate ()
                    {
                        conn.Open();

                        SqlDataAdapter sqlData = new SqlDataAdapter();
                        SqlCommand command = new SqlCommand(query, conn);
                        command.Parameters.AddWithValue("@mode", mode);
                        sqlData.SelectCommand = command;
                        DataTable dtbl = new DataTable();
                        sqlData.Fill(dtbl);
                        tblTrainingCentre.DataSource = dtbl;
                        tblTrainingCentre.DataBind();
                    });
                }
            }

            catch (Exception e)
            {
                Label1.Text = "An error occurred.";
                Label1.Visible = true;
            }

        }

        public void PopulateCourses(string location)
        {

          
            // Assuming you have a SQL query to fetch the data
            // string query = "SELECT * FROM DPC_Batch_Training WHERE Training_centre = @Location";

            string query = @"SELECT No_students_allocated, Batch_Reference, Start_Date, End_Date, Duration, Days, No_students_allowed_per_batch, Publish_Date, ID, BatchNumber, Time, Max_students_allowed
                            FROM DPC_Batch_Training
                            INNER JOIN DPC_SLots_Management ON DPC_Batch_Training.Batch_Reference = DPC_SLots_Management.Batch_Ref
                            WHERE Status = 'Active'
                            AND CONVERT(DATE, Expiry_Date, 103) > GETDATE()
                            AND No_students_allocated < Max_students_allowed
                            AND CONVERT(DATE, Publish_Date, 103) <= GETDATE()
                            AND Training_centre = @Location";


            using (SqlConnection conn = new SqlConnection(GlobalVar.ConnectionString))
            
                {
                SPSecurity.RunWithElevatedPrivileges(delegate ()
                {
                    conn.Open();

                    SqlDataAdapter sqlData = new SqlDataAdapter();
                    SqlCommand command = new SqlCommand(query, conn);
                    command.Parameters.AddWithValue("@Location", location);
                    sqlData.SelectCommand = command;
                    DataTable dtbl = new DataTable();
                    sqlData.Fill(dtbl);

                    //if (dtbl.Rows.Count > 0)
                    //{
                        tblCourse.DataSource = dtbl;
                        tblCourse.DataBind();
                    //}
                    //else
                    //{
                        // Display a message or perform any other action
                        //tblCourse.Visible = false; // Hide the table if there are no records
                        //Label1.Text = "No data available.";
                        //Label1.Visible = true;
                    //}
                });
            }
        }

        public static int GetNumberOfCourses(string location)
        {
            int rowCount = 0;

            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                using (SqlConnection conn = new SqlConnection(GlobalVar.ConnectionString))
                {
                    try
                    {
                        conn.Open();

                        string query = "SELECT COUNT(ID) FROM DPC_Batch_Training where Training_centre = @location";

                        using (SqlCommand command = new SqlCommand(query, conn))
                        {
                            command.Parameters.AddWithValue("@location", location);
                            rowCount = (int)command.ExecuteScalar();
                        }
                    }
                    catch (Exception ex)
                    {
                        // Handle any exceptions here
                        Console.WriteLine("An error occurred: " + ex.Message);
                    }
                }

            });

            return rowCount;
        }

        protected void tblTrainingCentre_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = tblTrainingCentre.Rows[rowIndex];
                string location = row.Cells[0].Text;

                trainingCentreLable.Text = location;
                trainingCentreLable.Visible = true;
                trainingCentreLable.ForeColor = System.Drawing.Color.Green;

                Console.WriteLine(location);

                int NoOfCourses = GetNumberOfCourses(location);

                if (NoOfCourses > 0)
                {
                    tblCourse.Visible = true;
                    Label1.Visible = false;
                    PopulateCourses(location);

                }

                else
                {
                    tblCourse.Visible = false; // Hide the table if there are no records
                    Label1.Text = "No data available.";
                    Label1.Visible = true;
                }
                // Assuming Location is in the first column

                // Pass the location value to populate the second table or perform any other desired action
            }
        }
        //static void tblTrainingCentre()
        //{
        //{
        //    // Write static logic for setTextboxText.  
        //    // This may require a static singleton instance of Form1.
        //}

        protected void tblTrainingCentre_SelectedIndexChanged(object sender, EventArgs e)
        { 
        
        }

        protected void ddlLocation_SelectedIndexChanged(object sender, EventArgs e)
            {
                string mode = ddlLocation.SelectedValue.ToString();

                if (mode == "1")
                {
                    PopulateTrainingCentre("Online");
                   // conn.Close();
                }

                else if (mode == "2")
                {
                    PopulateTrainingCentre("FaceToFace");
                }

                else
                {

                }
      
            }

        protected void tblCourseDetails_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                GridViewRow selectedRow = tblCourse.Rows[rowIndex];

                string _ref = selectedRow.Cells[2].Text; // Assuming BatchNumber is in the first column

                string url = GlobalVar.DPCUrl + "CourseApplication.aspx?ref=" + _ref;

                string script = "if(confirm('Are you sure you want to proceed?')) { window.location.href = '" + url + "'; }";
                ScriptManager.RegisterStartupScript(this, GetType(), "Confirmation", script, true);
            }
        }

        protected void tblCourseDetails_SelectedIndexChanged(object sender, EventArgs e)
        {

                        
        }



    }
}
