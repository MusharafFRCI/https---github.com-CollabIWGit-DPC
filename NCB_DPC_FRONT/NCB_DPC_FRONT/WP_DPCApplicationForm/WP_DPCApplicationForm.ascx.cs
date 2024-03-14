
using NCB_DPC_FRONT.DAL;
using NCB_DPC_FRONT.Models;
using NCB_DPC_FRONT.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
//using MaupassIntegration;
using System.Text;
using System.Net.Http;
using System.Net.Http.Headers;
using NCB_DPC_FRONT.Services;
using System.Text.Json;
using System.Text.RegularExpressions;
using RestSharp;
using Newtonsoft.Json;
using static NCB_DPC_FRONT.DAL.DBDataAccess;

namespace NCB_DPC_FRONT.WP_DPCApplicationForm
{
    [ToolboxItemAttribute(false)]
    public partial class WP_DPCApplicationForm : WebPart
    {
        // Uncomment the following SecurityPermission attribute only when doing Performance Profiling on a farm solution
        // using the Instrumentation method, and then remove the SecurityPermission attribute when the code is ready
        // for production. Because the SecurityPermission attribute bypasses the security check for callers of
        // your constructor, it's not recommended for production purposes.
        // [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Assert, UnmanagedCode = true)]
        //private SqlConnection conn = new SqlConnection(GlobalVar.ConnectionString);

        private SPListDataAccess spListsDataAccess = new SPListDataAccess();
        private Application applicationDetails = new Application();
        private SendEmail sendEmail = new SendEmail();

        private DBDataAccess dbDataAccess = new DBDataAccess();

        private countries getAllCountries = new countries();
        private static Dictionary<float, string> scratchCard = new Dictionary<float, string>();
        private List<string> titles;
        private string dpcURL;

        public class Root
        {
            public string checkoutMode { get; set; }
            public string merchant { get; set; }
            public string result { get; set; }
            public Session session { get; set; }
            public string successIndicator { get; set; }
        }

        public class Session
        {
            public string id { get; set; }
            public string updateStatus { get; set; }
            public string version { get; set; }
        }

        public WP_DPCApplicationForm()
        {
            dpcURL = GlobalVar.DPCUrl;
         
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            InitializeControl();
            //drp_title.Items.Clear();
            //drp_nationality.Items.Clear();

            //var countries = spListsDataAccess.GetCountries();
            var titles = spListsDataAccess.GetTitles();
           // Page.ClientScript.RegisterStartupScript(GetType(), "key", "getUserDetails();", true);


            //foreach (string country in countries)
            //{
            //drp_nationality.Items.Add("Mauritius");
            //}

            //List<string> countries = getAllCountries.getCountries();
            //foreach (string country in countries)
            //{
            //    drp_nationality.Items.Add(country);
            //}

            string[] country_list = new string[] { "Afghanistan", "Albania", "Algeria", "Andorra", "Angola", "Anguilla", "Antigua & Barbuda", "Argentina", "Armenia", "Aruba", "Australia", "Austria", "Azerbaijan", "Bahamas", "Bahrain", "Bangladesh", "Barbados", "Belarus", "Belgium", "Belize", "Benin", "Bermuda", "Bhutan", "Bolivia", "Bosnia & Herzegovina", "Botswana", "Brazil", "British Virgin Islands", "Brunei", "Bulgaria", "Burkina Faso", "Burundi", "Cambodia", "Cameroon", "Cape Verde", "Cayman Islands", "Chad", "Chile", "China", "Colombia", "Congo", "Cook Islands", "Costa Rica", "Cote D Ivoire", "Croatia", "Cruise Ship", "Cuba", "Cyprus", "Czech Republic", "Denmark", "Djibouti", "Dominica", "Dominican Republic", "Ecuador", "Egypt", "El Salvador", "Equatorial Guinea", "Estonia", "Ethiopia", "Falkland Islands", "Faroe Islands", "Fiji", "Finland", "France", "French Polynesia", "French West Indies", "Gabon", "Gambia", "Georgia", "Germany", "Ghana", "Gibraltar", "Greece", "Greenland", "Grenada", "Guam", "Guatemala", "Guernsey", "Guinea", "Guinea Bissau", "Guyana", "Haiti", "Honduras", "Hong Kong", "Hungary", "Iceland", "India", "Indonesia", "Iran", "Iraq", "Ireland", "Isle of Man", "Israel", "Italy", "Jamaica", "Japan", "Jersey", "Jordan", "Kazakhstan", "Kenya", "Kuwait", "Kyrgyz Republic", "Laos", "Latvia", "Lebanon", "Lesotho", "Liberia", "Libya", "Liechtenstein", "Lithuania", "Luxembourg", "Macau", "Macedonia", "Madagascar", "Malawi", "Malaysia", "Maldives", "Mali", "Malta", "Mauritania", "Mauritius", "Mexico", "Moldova", "Monaco", "Mongolia", "Montenegro", "Montserrat", "Morocco", "Mozambique", "Namibia", "Nepal", "Netherlands", "Netherlands Antilles", "New Caledonia", "New Zealand", "Nicaragua", "Niger", "Nigeria", "Norway", "Oman", "Pakistan", "Palestine", "Panama", "Papua New Guinea", "Paraguay", "Peru", "Philippines", "Poland", "Portugal", "Puerto Rico", "Qatar", "Reunion", "Romania", "Russia", "Rwanda", "Saint Pierre & Miquelon", "Samoa", "San Marino", "Satellite", "Saudi Arabia", "Senegal", "Serbia", "Seychelles", "Sierra Leone", "Singapore", "Slovakia", "Slovenia", "South Africa", "South Korea", "Spain", "Sri Lanka", "St Kitts & Nevis", "St Lucia", "St Vincent", "St. Lucia", "Sudan", "Suriname", "Swaziland", "Sweden", "Switzerland", "Syria", "Taiwan", "Tajikistan", "Tanzania", "Thailand", "Timor L'Este", "Togo", "Tonga", "Trinidad & Tobago", "Tunisia", "Turkey", "Turkmenistan", "Turks & Caicos", "Uganda", "Ukraine", "United Arab Emirates", "United Kingdom", "Uruguay", "Uzbekistan", "Venezuela", "Vietnam", "Virgin Islands (US)", "Yemen", "Zambia", "Zimbabwe" };

            foreach ( string country in country_list)
            {
                drp_nationality.Items.Add(country);
            }

            drp_nationality.SelectedValue = "Mauritius";
                
            //foreach (string title in titles)
            //{
            drp_title.Items.Add("Mr");
            drp_title.Items.Add("Mrs");
            drp_title.Items.Add("Ms");
            drp_title.Items.Add("Miss");

            txt_date.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
            //}

            //titles = spListsDataAccess.GetTitles();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //if (ViewState["GenerateApp"] == null)
            //{
            //    ViewState["GenerateApp"] = Guid.NewGuid().ToString();
            //}
            scratchCard = dbDataAccess.GetAllScratchcardNumbers();
            //loadUserDetails();

            //Get course details and populate read-only fields
            if (!Page.IsPostBack)
            {

                //ph_occupation_student.Visible = false;
              //  ph_sector_private.Visible = false;

                drp_occupation.DataBind();


                homePage.Attributes["href"] = dpcURL + "ListOfCourse.aspx";

                //ViewState["GenerateApp"] = Guid.NewGuid().ToString();

                // Save personal details and request header in Database meanwhile generating new Request ID

                cal_ext_date_birth.EndDate = DateTime.Now;
                string _ref = HttpContext.Current.Request.QueryString["ref"];

                setCourseDetails(_ref);

                //Page.ClientScript.RegisterStartupScript(GetType(), "key", "GetLSInfo();", true);


                div_submit_modal_confirmation.Attributes.Add("style", "display:none");
            }
        }

        protected void PopulateUserData_Click(object sender, EventArgs e)
        {

           
        }


        //private void loadUserDetails()
        //{

        //   // Page.ClientScript.RegisterStartupScript(GetType(), "key", "getUserDetails();", true);


        //    if (!string.IsNullOrEmpty(hdn_username.Value))
        //    {
        //        MaupassAuthManager _authManager = new MaupassAuthManager();
        //        MaupassAuthenticationResponse authenticationResponse = _authManager.ValidateUser(MaupassIntegration.Constants.MaupassAdminUsername,
        //                                                                                        MaupassIntegration.Constants.MaupassAdminPassword);
        //        string accessTokenNAF = authenticationResponse.result.accessToken;
        //        MaupassGetUserResponse getUserResponse = MaupassUtil.GetUser(Crypto.Decrypt(hdn_username.Value), accessTokenNAF);

        //        MaupassGetUserResult userDetails;
        //        if (getUserResponse.success)
        //        {
        //            userDetails = getUserResponse.result;
        //            foreach (string title in titles)
        //            {
        //                drp_title.Items.Add(title);
        //            }
        //            if (userDetails.gender.ToLowerInvariant() == "male")
        //            {
        //                drp_title.SelectedIndex = (int)Enums.Titles.Mr;
        //                ph_maiden_name.Visible = false;
        //            }
        //            else
        //            {
        //                if (!string.IsNullOrEmpty(userDetails.surnameAtBirth))
        //                {
        //                    drp_title.SelectedIndex = (int)Enums.Titles.Mrs;
        //                    ph_maiden_name.Visible = !string.IsNullOrEmpty(userDetails.surnameAtBirth);
        //                }
        //            }

        //            txt_surname.Text = userDetails.surname;
        //            txt_other_name.Text = userDetails.name;
        //            txt_maiden_name.Text = userDetails.surnameAtBirth;
        //            drp_nationality.SelectedValue = (string)userDetails.nationality;

        //            if (userDetails.isCitizen)
        //            {
        //                drp_mauritius_citizenship.SelectedIndex = userDetails.isCitizen ? 0 : 1;
        //                drp_mauritius_citizenship.Attributes.Add("readonly", "readonly");

        //                txt_nic.Text = userDetails.nic;
        //                txt_nic.Attributes.Add("readonly", "readonly");

        //                txt_passport_no.Text = userDetails.passportNumber.ToString();
        //                txt_passport_no.Attributes.Add("readonly", "readonly");
        //            }

        //            DateTime dateBirth;
        //            DateTime.TryParseExact(userDetails.dateOfBirth, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateBirth);
        //            //cal_ext_date_birth.SelectedDate = dateBirth;
        //            //cal_ext_date_birth.Format = "dd/MM/yyyy";
        //            TimeSpan age = DateTime.Today - dateBirth;
        //            txt_age.Text = $"{ age.Days / 365 }";

        //            drp_gender.SelectedValue = userDetails.gender;

        //            txt_phoneNum_mobile.Text = userDetails.phoneNumber;
        //            txt_phoneNum_office.Text = userDetails.fixedLineNumber;

        //            txt_email_address.Text = userDetails.emailAddress;

        //            drp_mauritius_citizenship_SelectedIndexChanged(this, null);

        //        }
        //    }


        //}




        //protected void PopulateUserData_Click(object sender, EventArgs e)
        //{
        //    //Course Details
        //    //string paramValue = HttpContext.Current.Request.QueryString["paramName"];
        //    //CourseDetailsModel getCourseDetails = new CourseDetailsModel();
        //    //getCourseDetails = dbDataAccess.GetCourseDetails(paramValue);
        //    //setCourseDetails(getCourseDetails);

        //    //registered user
        //    if (!string.IsNullOrEmpty(hdn_username.Value))
        //    {
        //    MaupassAuthManager _authManager = new MaupassAuthManager();
        //    MaupassAuthenticationResponse authenticationResponse = _authManager.ValidateUser(MaupassIntegration.Constants.MaupassAdminUsername,
        //                                                                                    MaupassIntegration.Constants.MaupassAdminPassword);
        //    string accessTokenNAF = authenticationResponse.result.accessToken;
        //    MaupassGetUserResponse getUserResponse = MaupassUtil.GetUser(Crypto.Decrypt(hdn_username.Value), accessTokenNAF);

        //    MaupassGetUserResult userDetails;
        //    if (getUserResponse.success)
        //    {
        //        userDetails = getUserResponse.result;
        //        foreach (string title in titles)
        //        {
        //            drp_title.Items.Add(title);
        //        }
        //        if (userDetails.gender.ToLowerInvariant() == "male")
        //        {
        //            drp_title.SelectedIndex = (int)Enums.Titles.Mr;
        //            ph_maiden_name.Visible = false;
        //        }
        //        else
        //        {
        //            if (!string.IsNullOrEmpty(userDetails.surnameAtBirth))
        //            {
        //                drp_title.SelectedIndex = (int)Enums.Titles.Mrs;
        //                ph_maiden_name.Visible = !string.IsNullOrEmpty(userDetails.surnameAtBirth);
        //            }
        //        }

        //        txt_surname.Text = userDetails.surname;
        //        txt_other_name.Text = userDetails.name;
        //        txt_maiden_name.Text = userDetails.surnameAtBirth;
        //        drp_nationality.SelectedValue = (string)userDetails.nationality;

        //        if (userDetails.isCitizen)
        //        {
        //            drp_mauritius_citizenship.SelectedIndex = userDetails.isCitizen ? 0 : 1;
        //                drp_mauritius_citizenship.Attributes.Add("readonly", "readonly");

        //                txt_nic.Text = userDetails.nic;
        //                txt_nic.Attributes.Add("readonly", "readonly");

        //                txt_passport_no.Text = userDetails.passportNumber.ToString();
        //                txt_passport_no.Attributes.Add("readonly", "readonly");
        //            }

        //            DateTime dateBirth;
        //        DateTime.TryParseExact(userDetails.dateOfBirth, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateBirth);
        //        //cal_ext_date_birth.SelectedDate = dateBirth;
        //        //cal_ext_date_birth.Format = "dd/MM/yyyy";
        //        TimeSpan age = DateTime.Today - dateBirth;
        //        txt_age.Text = $"{ age.Days / 365 }";

        //        drp_gender.SelectedValue = userDetails.gender;

        //        txt_phoneNum_mobile.Text = userDetails.phoneNumber;
        //        txt_phoneNum_office.Text = userDetails.fixedLineNumber;

        //        txt_email_address.Text = userDetails.emailAddress;

        //        drp_mauritius_citizenship_SelectedIndexChanged(this, null);

        //        }
        //    }
        //}

        private void setCourseDetails1(CourseDetailsModel courseDetails)
        {
            txt_dcpBatchCourse.Text = courseDetails.courseName;
            txt_location.Text = courseDetails.courseLocation;

            string numDays = courseDetails.courseDays;
            string[] arrNumDays = numDays.Split(',');
            int countDays = arrNumDays.Length * Int32.Parse(courseDetails.courseDuration);
            txt_numberOfDays.Text = countDays.ToString();

            txt_startEndTime.Text = courseDetails.courseTime;
            txt_duration.Text = courseDetails.courseDuration;
            txt_batchDetails.Text = courseDetails.courseDescription;
            txt_startDate.Text = courseDetails.courseStartDate.ToString();
            txt_endDate.Text = courseDetails.courseEndDate.ToString();
        }

        protected void drp_mauritius_citizenship_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void drp_title_SelectedIndexChanged(object sender, EventArgs e)
        {
            ph_maiden_name.Visible = ((DropDownList)sender).SelectedValue == "Mrs";
        }

        protected void txt_date_birth_TextChanged(object sender, EventArgs e)
        {
            //DateTime dateBirth;
            //if (DateTime.TryParse(txt_date_birth.Text, out dateBirth))
            //{
            //    var age = DateTime.Now.Year - dateBirth.Year;
            //    if (dateBirth.AddYears(age).CompareTo(DateTime.Now.Date) > 0)
            //    {
            //        age--;
            //    }

            //    txt_age.Text = age.ToString();
            //    up_date_birth.Update();
            //}
        }

        public static int GetNumberOfApplications()
        {
            int rowCount = 0;

            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                using (SqlConnection conn = new SqlConnection(GlobalVar.ConnectionString))
                {
                    try
                    {
                        conn.Open();

                        string query = "SELECT COUNT(ApplicationID) FROM DPC_Application";

                        using (SqlCommand command = new SqlCommand(query, conn))
                        {
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


        protected void drp_payment_SelectedIndexChanged(object sender, EventArgs e)
        {


            if (((DropDownList)sender).SelectedValue == "Cash Payment")
            {
                ph_payment.Visible = true;
                //req_serial_number.Enabled = true;
                //req_pin.Enabled = true;
                //up_payment.Update();

            }

            else
            {
                ph_payment.Visible = false;
                //req_serial_number.Enabled = false;
                //req_pin.Enabled = false;
                //up_payment.Update();

            }

        }


        protected void btn_submit_close_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "closePopup2", "closePopup2()", true);
        }

        protected void btn_submit_close_modal_Click(object sender, EventArgs e)
        {
            div_submit_modal_confirmation.Attributes.Add("style", "display:none");
        }

        protected void LoadPage(object sender, EventArgs e)
        {

        }

        private void SetLabelInConfirmModal(string requestid, string applicantName, string batchApplied, string startDate, string endDate, string duration, string location)
        {
            lbl_confirmation_requestid.Text = "Application ID: <span style='color: red;'>" + requestid + "</span>";
            lbl_confirmation_nameofapplicant.Text = "<span class='modal-body-labelhighlight'>Applicant Name:</span> " + applicantName;
            lbl_confirmation_batchApplied.Text = "<span class='modal-body-labelhighlight'>Batch Applied For:</span> " + batchApplied;
            lbl_confirmation_startDate.Text = "<span class='modal-body-labelhighlight'>Start Date of Course:</span> " + startDate;
            lbl_confirmation_endDate.Text = "<span class='modal-body-labelhighlight'>End Date of Course:</span> " + endDate;
            lbl_confirmation_duration.Text = "<span class='modal-body-labelhighlight'>Duration of Course:</span> <span style='color:red'>(45 Hrs)</span> :" + duration + "<span style='color:red'> Weeks</span>";
            lbl_confirmation_location.Text = "<span class='modal-body-labelhighlight'>Location/Training centre:</span> " + location;
            lbl_confirmation_applicationDateTime.Text = "<span class='modal-body-labelhighlight'>Date and Time of Application:</span> " + DateTime.Now.ToString("dd/MM/yyyy") + " " + DateTime.Now.ToString("HH:mm");
        }


        private void setCourseDetails(string _ref)
        {

            // Assuming you have a SQL query to fetch the data
            string query = "SELECT * FROM DPC_Batch_Training WHERE Batch_Reference = @ref";

            using (SqlConnection conn = new SqlConnection(GlobalVar.ConnectionString))
            {
                SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                try
                {
                    conn.Open();
                    SqlCommand command = new SqlCommand(query, conn);
                    command.Parameters.AddWithValue("@ref", _ref);
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        txt_dcpBatchCourse.Text = reader["Batch_Reference"].ToString();
                        txt_batchNumber.Text = reader["BatchNumber"].ToString();
                        txt_numberOfDays.Text = reader["Days"].ToString();
                        txt_startDate.Text = reader["Start_Date"].ToString();
                        txt_endDate.Text = reader["End_Date"].ToString();
                        txt_startEndTime.Text = reader["Time"].ToString();
                        txt_duration.Text = reader["Duration"].ToString();
                        txt_location.Text = reader["Training_centre"].ToString();
                        txt_batchDetails.Text = reader["NumberOfDays"].ToString();
                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

            });
            }
        }


        //public static (string sessionId, string successIndicator) createSession(string id, float amount, string currency, string description, string bearerToken, string returnUrl)
        //{

        //    string successIndicator = "";
        //    string sessionId = "";

        //    try
        //    {
        //        var client = new RestClient("http://gvp-stg-app19:13005/api/FRCI/Transport/CreateSessionAsync");
        //        client.Timeout = -1;
        //        var request = new RestRequest(Method.POST);

        //        // Add bearer token to the request headers
        //        //string bearerToken = "PtU21noovH27dywbqShrozA17LQFR0fcneG-o-82Fiw6CJE8JtkfBSrY7gT9M74gVTwojcIOJ0wxUbhoDlFmgLPMumXoTc6nYjfhiL8LBP8BqyHJ9tvX-ar3jgDz6wcW5Ebms3anO2tbb7eK5zy-diydVyfY27Z2_VDN7kLGejGuiPBrrSXSUy0bq9RA3XglsbYl0mMymQk5jbX34b96uu7uZSV1_YwjCgyl3hvGp8L4Wmxr9jaKWHYDgjktvEhHS6UeM0B00Sg3DL7FQYvM7OvE98MKKFuUA3iHHnTrkX6Z5mBfUBvzPsdcQuhVUNCirrte3txzpgpdCL0t47tNR-uEygBAELNqlS51N0EUcsSrRDmJMntC9SnzP1_yIw86Y9yqNVrQwEEDUiRC-iq43Q";
        //        request.AddHeader("Authorization", "Bearer " + bearerToken);
        //        request.AddHeader("Content-Type", "application/json");

        //        // Set the request body
        //        var requestBody = new
        //        {


        //            //apiOperation = "INITIATE_CHECKOUT",
        //            apiOperation = "INITIATE_CHECKOUT",

        //            order = new
        //            {
        //                id,
        //                amount,
        //                currency,
        //                description
        //            },

        //            interaction = new
        //            {
        //                operation = "PURCHASE",
        //                returnUrl,
        //                cancelUrl = "https://www.youtube.com/",
        //                merchant = new
        //                {
        //                    email = "san@sbmgroup.mu",
        //                    logo = "https://www.shutterstock.com/image-photo/montreal-canada-july-30-2017-260nw-706518910.jpg",
        //                    name = "Amazon Shopping",
        //                    phone = "4055555",
        //                    url = "https://www.amazon.com/"
        //                },
        //                displayControl = new
        //                {
        //                    billingAddress = "HIDE",
        //                    customerEmail = "HIDE"
        //                }
        //            }
        //        };

        //        request.AddJsonBody(requestBody);

        //        // Execute the request asynchronously
        //        //var response =  client.Execute(request);
        //        //var content = response.Content;

        //        IRestResponse response = client.Execute(request);
        //        MyResponse authenticationResponse = JsonConvert.DeserializeObject<MyResponse>(response.Content);

        //        successIndicator = authenticationResponse.MastercardSessionResponse.SuccessIndicator;
        //        //  HttpContext.Current.Session["successIndicator"] = successIndicator;

        //        System.Web.HttpCookie cookie = new System.Web.HttpCookie("successIndicator", successIndicator);
        //        HttpContext.Current.Response.Cookies.Add(cookie);


        //        //if (authenticationResponse.MastercardSessionResponse.Result == "SUCCESS")
        //        //{
        //        sessionId = authenticationResponse.MastercardSessionResponse.Session.Id;


        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Exception: {ex.Message}");
        //    }

        //    return (sessionId, successIndicator);
        //}

        //public static (string sessionId, string successIndicator) createSession(string id, float amount, string currency, string description, string returnUrl)
        //{

        //    string successIndicator = "";
        //    string sessionId = "";

        //    try
        //    {
        //        var client = new RestClient("https://test-gateway.mastercard.com/api/rest/version/78/merchant/TEST000509399390/session");
        //        //  client.Time = -1
        //        var request = new RestRequest();

        //        request.Method = Method.POST;

        //        string username = "merchant.TEST000509399390";
        //        string password = "9ed22b01d2cb2c23c93b723d0d7858c5";

        //        string auth = Convert.ToBase64String(Encoding.UTF8.GetBytes(username + ":" + password));

        //        // Add bearer token to the request headers
        //        //string bearerToken = "PtU21noovH27dywbqShrozA17LQFR0fcneG-o-82Fiw6CJE8JtkfBSrY7gT9M74gVTwojcIOJ0wxUbhoDlFmgLPMumXoTc6nYjfhiL8LBP8BqyHJ9tvX-ar3jgDz6wcW5Ebms3anO2tbb7eK5zy-diydVyfY27Z2_VDN7kLGejGuiPBrrSXSUy0bq9RA3XglsbYl0mMymQk5jbX34b96uu7uZSV1_YwjCgyl3hvGp8L4Wmxr9jaKWHYDgjktvEhHS6UeM0B00Sg3DL7FQYvM7OvE98MKKFuUA3iHHnTrkX6Z5mBfUBvzPsdcQuhVUNCirrte3txzpgpdCL0t47tNR-uEygBAELNqlS51N0EUcsSrRDmJMntC9SnzP1_yIw86Y9yqNVrQwEEDUiRC-iq43Q";
        //        request.AddHeader("Authorization", "Basic " + auth);

        //        request.AddHeader("Content-Type", "application/json");

        //        // Set the request body
        //        var requestBody = new
        //        {

        //            //apiOperation = "INITIATE_CHECKOUT",
        //            apiOperation = "INITIATE_CHECKOUT",

        //            order = new
        //            {
        //                id,
        //                amount,
        //                currency,
        //                description
        //            },

        //            interaction = new
        //            {
        //                operation = "PURCHASE",
        //                returnUrl,
        //                cancelUrl = "https://www.youtube.com/",
        //                merchant = new
        //                {
        //                    email = "san@sbmgroup.mu",
        //                    logo = "https://www.shutterstock.com/image-photo/montreal-canada-july-30-2017-260nw-706518910.jpg",
        //                    name = "Amazon Shopping",
        //                    phone = "4055555",
        //                    url = "https://www.amazon.com/"
        //                },
        //                displayControl = new
        //                {
        //                    billingAddress = "HIDE",
        //                    customerEmail = "HIDE"
        //                }
        //            }
        //        };

        //        request.AddJsonBody(requestBody);
        //        IRestResponse response = client.Execute(request);

        //        var responseString = response.Content;

        //        var responseModel = JsonConvert.DeserializeObject<SessionResponse>(responseString);
        //        sessionId = responseModel.Session.Id;
        //        successIndicator = responseModel.SuccessIndicator;

        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Exception: {ex.Message}");
        //    }

        //    return (sessionId, successIndicator);
        //}



        public static (string sessionId, string successIndicator) createSession(string id, float amount, string currency, string description, string returnUrl)
        {

           string dpcMerchant = GlobalVar.DPCMerchant;
           string dpcMerchantKey = GlobalVar.DPCMerchantKey;

            string successIndicator = "";
            string sessionId = "";

            try
            {
                var client = new RestClient("https://test-gateway.mastercard.com/api/rest/version/78/merchant/TEST000509399390/session");
                //  client.Time = -1
                var request = new RestRequest();

                request.Method = Method.POST;

                //string username = "merchant.TEST000509399390";
                //string password = "9ed22b01d2cb2c23c93b723d0d7858c5";

                string username = dpcMerchant;
                string password = dpcMerchantKey;


                string auth = Convert.ToBase64String(Encoding.UTF8.GetBytes(username + ":" + password));

                // Add bearer token to the request headers
                //string bearerToken = "PtU21noovH27dywbqShrozA17LQFR0fcneG-o-82Fiw6CJE8JtkfBSrY7gT9M74gVTwojcIOJ0wxUbhoDlFmgLPMumXoTc6nYjfhiL8LBP8BqyHJ9tvX-ar3jgDz6wcW5Ebms3anO2tbb7eK5zy-diydVyfY27Z2_VDN7kLGejGuiPBrrSXSUy0bq9RA3XglsbYl0mMymQk5jbX34b96uu7uZSV1_YwjCgyl3hvGp8L4Wmxr9jaKWHYDgjktvEhHS6UeM0B00Sg3DL7FQYvM7OvE98MKKFuUA3iHHnTrkX6Z5mBfUBvzPsdcQuhVUNCirrte3txzpgpdCL0t47tNR-uEygBAELNqlS51N0EUcsSrRDmJMntC9SnzP1_yIw86Y9yqNVrQwEEDUiRC-iq43Q";
                request.AddHeader("Authorization", "Basic " + auth);

                request.AddHeader("Content-Type", "application/json");

                // Set the request body
                var requestBody = new
                {

                    //apiOperation = "INITIATE_CHECKOUT",
                    apiOperation = "INITIATE_CHECKOUT",

                    order = new
                    {
                        id,
                        amount,
                        currency,
                        description
                    },

                    interaction = new
                    {
                        operation = "PURCHASE",
                        returnUrl,
                        cancelUrl = "http://gvp-stg-app19:9005/dpc/Pages/ListOfCourse.aspx",
                        timeout = 600,
                        merchant = new
                        {
                            email = "san@sbmgroup.mu",
                            logo = "https://www.shutterstock.com/image-photo/montreal-canada-july-30-2017-260nw-706518910.jpg",
                            name = "Amazon Shopping",
                            phone = "4055555",
                            url = "https://www.amazon.com/"
                        },
                        displayControl = new
                        {
                            billingAddress = "HIDE",
                            customerEmail = "HIDE"
                        }
                    }
                };

                request.AddJsonBody(requestBody);
                IRestResponse response = client.Execute(request);

                var responseString = response.Content;

                var responseModel = JsonConvert.DeserializeObject<Root>(responseString);


                sessionId = responseModel.session.id;
                successIndicator = responseModel.successIndicator;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
            }

            return (sessionId, successIndicator);
        }



        protected void btn_submit_Click(object sender, EventArgs e)
        {

            SlotInfo slotInfo = dbDataAccess.GetBatchSlots(txt_dcpBatchCourse.Text);

            float price = dbDataAccess.GetCoursePrice();


            //string userInput = txt_captcha_code.Text.Trim();
            //bool isHuman = captcha.Validate(userInput);

            string serialNumber = txt_serial_number.Text;
            string pin = txt_pin.Text;
            string passportNo = txt_passport_no.Text;
            string nicNo = txt_nic.Text;
            string OTPstatus = dbDataAccess.CheckIfCardIsUsed(txt_serial_number.Text);
            int age;

            if (HttpContext.Current.Session["Age"] != null)
            {
                txt_age.Text = HttpContext.Current.Session["Age"].ToString();
            }


            string paymentType = drp_payment.SelectedValue;

            if (paymentType == "Cash Payment" && (string.IsNullOrEmpty(serialNumber) || string.IsNullOrEmpty(pin)))
            {

                //if (string.IsNullOrEmpty(serialNumber) || string.IsNullOrEmpty(pin))
                //{
                LabelMessage.Text = "Please enter your pin and code.";
                LabelMessage.ForeColor = System.Drawing.Color.Red;
                //}
                //else if (scratchCard.TryGetValue(serialNumber, out string storedPin) && storedPin != pin)
                //{
                //    LabelMessage.Text = "Please make sure to enter a valid serial number and its pin.";
                //    LabelMessage.ForeColor = System.Drawing.Color.Red;

                //}

            }

            else if (drp_mauritius_citizenship.SelectedValue == "No" && string.IsNullOrEmpty(passportNo))
            {
                LabelMessage.Text = "Please make sure to enter your passport number.";
                LabelMessage.ForeColor = System.Drawing.Color.Red;
            }

            else if (drp_mauritius_citizenship.SelectedValue == "Yes" && string.IsNullOrEmpty(nicNo))
            {
                LabelMessage.Text = "Please make sure to enter your NIC number.";
                LabelMessage.ForeColor = System.Drawing.Color.Red;
            }

            else if (paymentType == "Cash Payment" && (!scratchCard.ContainsKey(float.Parse(serialNumber)) || scratchCard[float.Parse(serialNumber)] != pin))
            {
                LabelMessage.Text = "Please make sure to enter a valid serial number and its pin.";
                LabelMessage.ForeColor = System.Drawing.Color.Red;
           

            }

            //            else if ((scratchCard.TryGetValue(float.Parse(serialNumber), out string storedPin) && storedPin != pin)
            //)
            //            {
            //                LabelMessage.Text = "Please make sure to enter a valid serial number and its pin.";
            //                LabelMessage.ForeColor = System.Drawing.Color.Red;
            //            }


            else if (drp_occupation.SelectedValue == "Employed" && (string.IsNullOrEmpty(txt_job_title.Text)))
            {

                if (radioBtn_private.Checked && string.IsNullOrEmpty(txt_employer2.Text))
                {
                    LabelMessage.Text = "Please fill in your employer details.";
                    LabelMessage.ForeColor = System.Drawing.Color.Red;

                }

            }

            else if (drp_occupation.SelectedValue == "Student" && (string.IsNullOrEmpty(txt_grade.Text)))
            {

                LabelMessage.Text = "Please fill in missing details.";
                LabelMessage.ForeColor = System.Drawing.Color.Red;

            }

            else if (drp_occupation.SelectedValue == "Other" && (string.IsNullOrEmpty(txt_other.Text)))
            {
                //if(string.IsNullOrEmpty(txt_grade.Text))
                //{
                LabelMessage.Text = "Please fill in missing details.";
                LabelMessage.ForeColor = System.Drawing.Color.Red;
                //} 
            }


            //else if (!isHuman)
            //{
            //    LabelMessage.Text = captcha_validator.ErrorMessage;
            //    // LabelMessage.Text = "Please re input captcha.";
            //    LabelMessage.ForeColor = System.Drawing.Color.Red;
            //}

            else if (!cb_declaration.Checked)
            {
                LabelMessage.Text = "Please make sure to acknowledge the above declaration .";
                LabelMessage.ForeColor = System.Drawing.Color.Red;
            }


            else if (!(slotInfo.Allowed - slotInfo.Allocated > 0)) {

                LabelMessage.Text = "Cannot apply to this course as it reached number of participants allowed.";
                LabelMessage.ForeColor = System.Drawing.Color.Red;
            }


            //else if (!int.TryParse(txt_age.Text, out age))
            //{
            //    LabelMessage.Text = "Please enter a valid age.";
            //    LabelMessage.ForeColor = System.Drawing.Color.Red;
            //}
            //else if (age <= 10)
            //{
            //    LabelMessage.Text = "Age must be greater than 10.";
            //    LabelMessage.ForeColor = System.Drawing.Color.Red;
            //}



            else if (!string.IsNullOrEmpty(txt_surname.Text) &&
               !string.IsNullOrEmpty(txt_other_name.Text) &&
               !string.IsNullOrEmpty(txt_date_birth.Text) &&
               //!string.IsNullOrEmpty(txt_gender.Text) &&
               drp_gender.SelectedValue != "select" &&
               //!string.IsNullOrEmpty(txt_address_postalCode.Text) &&
               !string.IsNullOrEmpty(txt_address_line1.Text) &&
               !string.IsNullOrEmpty(txt_phoneNum_mobile.Text) &&
               !string.IsNullOrEmpty(txt_email_address.Text))

            {

                try


                {

                    //Page.Validate("vg_ApplicationForm");

                    //if (Page.IsValid)
                    //{



                        string successIndicator = "";
                        string sessionId = "";

                        //if (!Page.IsPostBack)
                        //{

                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ShowLoader", "javascript: $(function(){ toggleLoader(true) });", true);

                        int numberOfApplications = GetNumberOfApplications();
                        int sequence;
                        int year = DateTime.Today.Year;

                        if (numberOfApplications == 0)
                        {
                            sequence = 1;
                        }

                        else
                        {
                            sequence = numberOfApplications + 1;
                        }

                        //Application ID


                        string appID = "MDPA" + txt_dcpBatchCourse.Text + year + "/" + sequence;


                        string notEmployed = "";
                        string employedSector = "";
                        string sectorIn = "";
                        string passportNumber = "";
                        string NIC = ""; 


                 
                   
                        if (drp_occupation.SelectedValue != "Employed")
                        {

                            txt_job_title.Text = notEmployed;
                            drp_employer1.SelectedValue = notEmployed;
                            txt_employer2.Text = notEmployed;
                        }

                        if (drp_occupation.SelectedValue == "Employed" && radioBtn_private.Checked)
                        {

                            employedSector = txt_employer2.Text;
                            sectorIn = "Private";

                        }

                        if (drp_occupation.SelectedValue == "Employed" && radioBtn_public.Checked)
                        {

                            employedSector = drp_employer1.SelectedValue;
                            sectorIn = "Private";

                        }

                    if (drp_mauritius_citizenship.SelectedValue == "Yes")
                    {

                        NIC = txt_nic.Text;
                        sectorIn = "Private";

                    }









                    if (drp_payment.SelectedValue == "Bank Card")
                        {


                       (sessionId, successIndicator) = createSession(appID.Replace("/", "_"), price, "MUR", "Registration for Digital Proficiency Course (DPC)", "http://gvp-stg-app19:9005/dpc/Pages/ApplicationSuccess.aspx?appId=" + appID);



                            if (successIndicator != null)
                            {


                                if (dbDataAccess.InsertDataUsingStoredProcedure(
                   txt_dcpBatchCourse.Text, txt_location.Text, txt_numberOfDays.Text, txt_startEndTime.Text, txt_duration.Text, txt_batchDetails.Text, txt_startDate.Text,
                   txt_endDate.Text, drp_mauritius_citizenship.SelectedValue, drp_nationality.SelectedValue, txt_nic.Text, txt_passport_no.Text, drp_title.SelectedValue,
                   txt_surname.Text, txt_other_name.Text, txt_maiden_name.Text, txt_date_birth.Text, txt_age.Text, drp_gender.SelectedValue, txt_address_postalCode.Text, txt_address_line1.Text,
                   txt_address_line2.Text, txt_address_line3.Text, txt_phoneNum_home.Text, txt_phoneNum_office.Text, txt_phoneNum_mobile.Text, txt_email_address.Text, drp_occupation.SelectedValue,
                   txt_job_title.Text, sectorIn, txt_grade.Text, txt_other.Text, employedSector, txt_date.Text, txt_signature.Text,
                   drp_payment.SelectedValue, txt_serial_number.Text, txt_pin.Text, appID, "Applicant", txt_signature.Text, successIndicator))
                                {


                                    Page.ClientScript.RegisterStartupScript(
                                                  GetType(),
                                                  "passSessionId",
                                                  "pay('" + sessionId + "');",
                                                  true
                                                );


                                }
                            }

                        }

                        else
                        {

                            if (dbDataAccess.InsertDataUsingStoredProcedure(
                    txt_dcpBatchCourse.Text, txt_location.Text, txt_numberOfDays.Text, txt_startEndTime.Text, txt_duration.Text, txt_batchDetails.Text, txt_startDate.Text,
                    txt_endDate.Text, drp_mauritius_citizenship.SelectedValue, drp_nationality.SelectedValue, txt_nic.Text, txt_passport_no.Text, drp_title.SelectedValue,
                    txt_surname.Text, txt_other_name.Text, txt_maiden_name.Text, txt_date_birth.Text, txt_age.Text, drp_gender.SelectedValue, txt_address_postalCode.Text, txt_address_line1.Text,
                    txt_address_line2.Text, txt_address_line3.Text, txt_phoneNum_home.Text, txt_phoneNum_office.Text, txt_phoneNum_mobile.Text, txt_email_address.Text, drp_occupation.SelectedValue,
                    txt_job_title.Text, sectorIn, txt_grade.Text, txt_other.Text, employedSector, txt_date.Text, txt_signature.Text,
                    drp_payment.SelectedValue, txt_serial_number.Text, txt_pin.Text, appID, "Applicant", txt_signature.Text, "") && dbDataAccess.UpdateScratchCard(float.Parse(txt_serial_number.Text), txt_pin.Text)
                                && dbDataAccess.UpdateBatchSlotUsingStoredProcedure(txt_dcpBatchCourse.Text) && dbDataAccess.UpdateApplicationPaymentStatus(appID))

                            {

                                Application applicationDetails = new Application();

                                applicationDetails.ApplicationID = appID;
                                applicationDetails.eserviceName = "Digital Proficiency Course";
                                applicationDetails.applicantTitle = drp_title.SelectedValue;
                                applicationDetails.applicantName = txt_other_name.Text + " " + txt_surname.Text;
                                applicationDetails.batchApplied = txt_dcpBatchCourse.Text;
                                applicationDetails.startDate = txt_startDate.Text;
                                applicationDetails.endDate = txt_endDate.Text;
                                applicationDetails.duration = txt_duration.Text;
                                applicationDetails.location = txt_location.Text;
                                applicationDetails.dateOfApplication = txt_date.Text;
                                applicationDetails.applicantEmail = txt_email_address.Text;

                                //)

                                // Data successfully inserted
                                LabelMessage.Text = "Application submitted successfully.";
                                LabelMessage.ForeColor = System.Drawing.Color.Green;

                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ShowLoader", "javascript: $(function(){ toggleLoader(false) });", true);

                                dpcFormApp.Attributes.Add("style", "display:none");


                                //Show modal
                                SetLabelInConfirmModal(
                                        appID,
                                        txt_other_name.Text + " " + txt_surname.Text,
                                        txt_dcpBatchCourse.Text,
                                        txt_startDate.Text,
                                        txt_endDate.Text,
                                        txt_duration.Text,
                                        txt_location.Text);

                                div_submit_modal_confirmation.Attributes.Add("style", "display:block");

                                sendEmail.sendEmail(applicationDetails);


                            }
                            else
                            {
                                // Insertion failed
                                LabelMessage.Text = "Error occurred while submitting application.";
                                LabelMessage.ForeColor = System.Drawing.Color.Red;
                            }

                        }
                    //}
                }
                catch (Exception ex)
                {
                    LabelMessage.Text = ex.Message + ex.StackTrace;
                    LabelMessage.ForeColor = System.Drawing.Color.Red;
                }
            }
        
            else
            {
                LabelMessage.Text = "Please correct any mistake before submitting application.";
                LabelMessage.ForeColor = System.Drawing.Color.Red;
            }

        }



        protected void radioBtn_public_CheckedChanged(object sender, EventArgs e)
        {
      
                //public_dept.Style["display"] = "block";
                //private_employer.Style["display"] = "none";
                //ph_sector_public.Visible = true;
                //ph_sector_private.Visible = false;
                //req_employer2.Enabled = false;
                //up_occupation_sector.Update();

        }

        protected void radioBtn_private_CheckedChanged(object sender, EventArgs e)
        {


                //public_dept.Style["display"] = "none";
                //private_employer.Style["display"] = "block";
                //ph_sector_public.Visible = false;
                //ph_sector_private.Visible = true;
                //req_employer2.Enabled = true;
                //up_occupation_sector.Update();

        }

        protected void drp_mauritius_citizenship_SelectedIndexChanged1(object sender, EventArgs e)
        {

        }

        //protected void drp_occupation_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    string selectedValue = drp_occupation.SelectedItem.Text;

        //    if (selectedValue == "Employed (see below*)")
        //    {
        //        ph_occupation_employed_1.Visible = true;
        //        ph_occupation_employed_2.Visible = true;

        //        ph_occupation_student.Visible = false;
        //        req_job_title.Enabled = true;
        //        drp_employer1.Visible = true;
        //        req_grade.Enabled = false;
        //        up_occupation.Update();
        //        up_occupation_sector.Update();

        //    }
        //    else if (selectedValue == "Student (see below**)")
        //    {
        //        ph_occupation_employed_1.Visible = false;
        //        ph_occupation_employed_2.Visible = false;
        //        ph_occupation_student.Visible = true;
        //        req_job_title.Enabled = false;
        //        req_grade.Enabled = true;
        //        ph_sector_public.Visible = false;
        //        req_employer2.Enabled = false;
        //        ph_sector_private.Visible = false;
        //        up_occupation_sector.Visible = false;
        //        drp_employer1.Visible = false;
        //        up_occupation.Update();
        //        up_occupation_sector.Update();

        //    }
        //    else
        //    {
        //        ph_occupation_employed_1.Visible = false;
        //        ph_occupation_employed_2.Visible = false;
        //        ph_occupation_student.Visible = false;
        //        ph_sector_public.Visible = false;
        //        up_occupation_sector.Visible = false;
        //        req_job_title.Enabled = false;
        //        req_grade.Enabled = false;
        //        req_employer2.Enabled = false;
        //        drp_employer1.Visible = false;
        //        ph_sector_private.Visible = false;
        //        up_occupation.Update();
        //        up_occupation_sector.Update();

        //    }
        //}
    }
}
    