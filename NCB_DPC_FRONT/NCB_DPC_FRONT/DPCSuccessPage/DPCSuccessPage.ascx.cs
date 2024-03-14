using Microsoft.SharePoint;
using NCB_DPC_FRONT.Utils;
using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI.WebControls.WebParts;
using NCB_DPC_FRONT.DAL;
using NCB_DPC_FRONT.Services;
using NCB_DPC_FRONT.Models;

namespace NCB_DPC_FRONT.DPCSuccessPage
{
    [ToolboxItemAttribute(false)]
    public partial class DPCSuccessPage : WebPart
    {

        private DBDataAccess dbDataAccess = new DBDataAccess();
        private SendEmail sendEmail = new SendEmail();
        private string dpcURL;
        //private SqlConnection conn = new SqlConnection(GlobalVar.ConnectionString);


        // Uncomment the following SecurityPermission attribute only when doing Performance Profiling on a farm solution
        // using the Instrumentation method, and then remove the SecurityPermission attribute when the code is ready
        // for production. Because the SecurityPermission attribute bypasses the security check for callers of
        // your constructor, it's not recommended for production purposes.
        // [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Assert, UnmanagedCode = true)]
        public DPCSuccessPage()
        {

            dpcURL = GlobalVar.DPCUrl;
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            InitializeControl();
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {

                homePage.Attributes["href"] = dpcURL + "ListOfCourse.aspx";
                string resultIndicator = HttpContext.Current.Request.QueryString["resultIndicator"];
                string appId = HttpContext.Current.Request.QueryString["appId"];
                string batchRef = GetBatchRef(appId);
                string successIndicator = GetSuccessIndicator(appId);
                string paymentStatus = GetAppPaymentStatus(appId);
                //  string storedValue = HttpContext.Current.Session["successIndicator"] as string;

                if (successIndicator != null && successIndicator.Trim() == resultIndicator && paymentStatus != "Approved")
                {
                    if(dbDataAccess.UpdateApplicationPaymentStatus(appId) && dbDataAccess.UpdateBatchSlotUsingStoredProcedure(batchRef) && dbDataAccess.InsertIntoDPCSBMPayment(appId, successIndicator, DateTime.Now.ToString(), "Success"))
                    {
                        Label1.Text = "APPLICATION SUCCESSFULLY SUBMITTED WITH SBM PAYMENT";
                        SetApplicationDetails(appId);

                    }

                    // Use the successIndicator value as needed

                }

                else if(paymentStatus == "Approved")
                {
                    div_submit_modal_confirmation.Attributes.Add("style", "display:none");
                    Label1.Text = "THIS APPLICATION HAS ALREADY SUBMITTED WITH SBM PAYMENT.";

                }

                else
                {
                    div_submit_modal_confirmation.Attributes.Add("style", "display:none");
                    Label1.Text = "THERE IS AN ISSUE WITH THIS APPLICATION.";

                }


            }
        }


        private string GetBatchRef(string appId)
        {
            string batchRef = "";
            // Assuming you have a SQL query to fetch the data
            string query = "SELECT Batch FROM DPC_Application WHERE ApplicationID = @appID";

            using (SqlConnection conn = new SqlConnection(GlobalVar.ConnectionString))
            {
                SPSecurity.RunWithElevatedPrivileges(delegate ()
                {
                    try
                    {
                        conn.Open();
                        SqlCommand command = new SqlCommand(query, conn);
                        command.Parameters.AddWithValue("@appID", appId);
                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.Read())
                        {
                            batchRef = reader["Batch"].ToString();
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                });
            }
            return batchRef;
            // Set Attachments
        }

        private string GetSuccessIndicator(string appId)
        {
            string successIndicator = "";
            // Assuming you have a SQL query to fetch the data
            string query = "SELECT SuccessIndicator FROM DPC_Application WHERE ApplicationID = @appID";

            using (SqlConnection conn = new SqlConnection(GlobalVar.ConnectionString))
            {
                SPSecurity.RunWithElevatedPrivileges(delegate ()
                {
                    try
                    {
                        conn.Open();
                        SqlCommand command = new SqlCommand(query, conn);
                        command.Parameters.AddWithValue("@appID", appId);
                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.Read())
                        {
                            successIndicator = reader["SuccessIndicator"].ToString();
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                });
            }
            return successIndicator;
            // Set Attachments
        }

        private string GetAppPaymentStatus(string appId)
        {
           string paymentStatus = "";
            // Assuming you have a SQL query to fetch the data
            string query = "SELECT PaymentStatus FROM DPC_Application WHERE ApplicationID = @appID";

            using (SqlConnection conn = new SqlConnection(GlobalVar.ConnectionString))
            {
                SPSecurity.RunWithElevatedPrivileges(delegate ()
                {
                    try
                    {
                        conn.Open();
                        SqlCommand command = new SqlCommand(query, conn);
                        command.Parameters.AddWithValue("@appID", appId);
                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.Read())
                        {
                            paymentStatus = reader["PaymentStatus"].ToString();
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                });
            }
            return paymentStatus.Trim();
            // Set Attachments
        }

        private void SetApplicationDetails(string appId)
        {
            // Assuming you have a SQL query to fetch the data
            string query = "SELECT * FROM DPC_Application WHERE ApplicationID = @appID";

            using (SqlConnection conn = new SqlConnection(GlobalVar.ConnectionString))
            {
                SPSecurity.RunWithElevatedPrivileges(delegate ()
                {
                    try
                    {
                        conn.Open();
                        SqlCommand command = new SqlCommand(query, conn);
                        command.Parameters.AddWithValue("@appID", appId);
                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.Read())
                        {
                            SetLabelInConfirmModal(
                                reader["ApplicationID"].ToString(),
                                reader["Surname"].ToString() + " " + reader["OtherNames"].ToString(),
                                reader["Batch"].ToString(),
                                reader["StartDate"].ToString(),
                                reader["EndDate"].ToString(),
                                reader["Duration"].ToString(),
                                reader["TrainingCentre"].ToString());
                            div_submit_modal_confirmation.Attributes.Add("style", "display:block");

                            Application applicationDetails = new Application();

                            applicationDetails.ApplicationID = reader["ApplicationID"].ToString();
                            applicationDetails.eserviceName = "Digital Proficiency Course";
                            applicationDetails.applicantTitle = reader["Title"].ToString();
                            applicationDetails.applicantName = reader["Surname"].ToString() + " " + reader["OtherNames"].ToString();
                            applicationDetails.batchApplied = reader["Batch"].ToString();
                            applicationDetails.startDate = reader["StartDate"].ToString();
                            applicationDetails.endDate = reader["EndDate"].ToString();
                            applicationDetails.duration = reader["Duration"].ToString();
                            applicationDetails.location = reader["TrainingCentre"].ToString();
                            applicationDetails.dateOfApplication = reader["DateSubmitted"].ToString();
                            applicationDetails.applicantEmail = reader["Email"].ToString();

                            sendEmail.sendEmail(applicationDetails);
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                });
            }
            // Set Attachments
        }

        private void SetLabelInConfirmModal(string requestid, string applicantName, string batchApplied, string startDate, string endDate, string duration, string location)
        {
            lbl_confirmation_requestid.Text = "Application ID : <span style='color: red;'>" + requestid + "</span>";
            lbl_confirmation_nameofapplicant.Text = "<span class='modal-body-labelhighlight'>Applicant Name :</span> " + applicantName;
            lbl_confirmation_batchApplied.Text = "<span class='modal-body-labelhighlight'>Batch Applied For :</span> " + batchApplied;
            lbl_confirmation_startDate.Text = "<span class='modal-body-labelhighlight'>Start Date of Course :</span> " + startDate;
            lbl_confirmation_endDate.Text = "<span class='modal-body-labelhighlight'>End Date of Course :</span> " + endDate;
            lbl_confirmation_duration.Text = "<span class='modal-body-labelhighlight'>Duration of Course : </span>  <span style='color:red'>(45 Hrs)</span> :" + duration + "<span style='color:red'> Weeks</span>";
            lbl_confirmation_location.Text = "<span class='modal-body-labelhighlight'>Location/Training centre:</span> " + location;
            lbl_confirmation_applicationDateTime.Text = "<span class='modal-body-labelhighlight'>Date and Time of Application :</span> " + DateTime.Now.ToString("dd/MM/yyyy") + " " + DateTime.Now.ToString("HH:mm");
        }

        protected void btn_submit_close_modal_Click(object sender, EventArgs e)
        {

        }
    }
}
