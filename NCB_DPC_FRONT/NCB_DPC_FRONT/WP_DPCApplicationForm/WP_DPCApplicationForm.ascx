<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=16.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=16.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=16.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=16.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control   Language="C#" AutoEventWireup="true" CodeBehind="WP_DPCApplicationForm.ascx.cs" Inherits="NCB_DPC_FRONT.WP_DPCApplicationForm.WP_DPCApplicationForm" %>
<%@ Register Assembly="AjaxControlToolkit, Version=20.1.0.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%--<%@ Register Assembly="BotDetect, Version=4.4.2.0, Culture=neutral, PublicKeyToken=74616036388b765f" Namespace="BotDetect.Web.UI" TagPrefix="BotDetect" %>--%>

<%--<link href="/dpc/Site%20Assets/style.css" rel="stylesheet" type="text/css" />--%>
<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css" />

<script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>

<script src="https://test-gateway.mastercard.com/static/checkout/checkout.min.js" data-error="errorCallback" data-cancel="cancelCallback" data-timeout="http://gvp-stg-app19:9005/dpc/Pages/ListOfCourse.aspx"></script>



<script>
    function GetLSInfo() {
        $("#<%= hdn_username.ClientID%> ").val(localStorage.getItem('nusr'));

        $("#<%= btn_populateUserData.ClientID %>").click();
    }

    function getRegisteredUserInfo() {

    }

    function toggleLoader(show) {
        if (show) {
            $('.cd-popup3').addClass('is-visible');
        }
        else {
            $('.cd-popup3').removeClass('is-visible');
        }
    }

    function closePopup2() {
        $('.cd-popup2').removeClass('is-visible');
        redirectToAllCourses();
    }

    function redirectToAllCourses() {
        window.location.href = 'http://sp2019:8005/dpc/Pages/ListOfCourse.aspx';
    }

   
        function errorCallback(error) {
            console.log(JSON.stringify(error));
        }

        function cancelCallback() {
            console.log('Payment cancelled');
        }

        function pay(sessionId) {
            Checkout.configure({
                session: {
                    id: sessionId
                }
            })

                .then(() => {
                    Checkout.showPaymentPage();
                }); 

    }


    function getUserDetails() {
        $('#<%= hdn_username.ClientID%>').val(localStorage.getItem('nusr'));
        console.log("user id", localStorage.getItem('nusr'));
    }
// Usage 



<%--    var select = document.getElementById('<%=drp_occupation.ClientID%>');  --%>

    //select.addEventListener("change", toggleInputs);  

 

    $(document).ready(function () {


        var ph_occupation_employed_1 = document.getElementById('job_title');
        var ph_occupation_employed_2 = document.getElementById('sector');
        var ph_occupation_student = document.getElementById('grade');
        var gov_employer = document.getElementById('employer_gov');
        var private_employer = document.getElementById('private_employer');
        var other_employed = document.getElementById('other');
        var nic_number = document.getElementById('txt_nic_div');

        var passport_no = document.getElementById('passport_no');

        //
                var date_birth = document.getElementById('txt_date_birth');

                var txt_age = document.getElementById('txt_age');




        $('#<%= hdn_username.ClientID%>').val(localStorage.getItem('nusr'));
        console.log(localStorage.getItem('nusr'));

        // Hide ASP controls initially
        ph_occupation_student.style.display = 'none';
        private_employer.style.display = 'none';
        other_employed.style.display = 'none';
        passport_no.style.display = 'none';
        nic_number.style.display = 'block';
       

       // document.getElementById('req_grade').enabled = false;

<%--        ValidatorEnable($('#<%=req_grade.ClientID %>')[0], false);--%>

        var selectedValue = sessionStorage.getItem('selectedOption');

        // If no saved selection, default to first option
        if (!selectedValue) {
            selectedValue = $('#<%=drp_occupation.ClientID%> option:first').val();
        }

        // Set initial fields display based on saved selection
        handleFieldDisplay(selectedValue);


        $('#<%=drp_occupation.ClientID%>').change(function () {

            var selectedValue = $(this).val();

            // Save selection to sessionStorage
            sessionStorage.setItem('selectedOption', selectedValue);

            // Handle field display
            handleFieldDisplay(selectedValue);

        });


        $('#<%= radioBtn_public.ClientID %>').change(function () {
            // Perform actions when the 'Public' radio button is selected
            gov_employer.style.display = "block";
            private_employer.style.display = "none";
            console.log('Public radio button selected');
            // Add your code here
        });

        $('#<%= radioBtn_private.ClientID %>').change(function () {
            // Perform actions when the 'Private' radio button is selected
            private_employer.style.display = "block";
            gov_employer.style.display = "none";
            console.log('Private radio button selected');
            // Add your code here
        });



        var selectedValueCitizen = sessionStorage.getItem('selectedOptionCitizen');

        handlePassPortField(selectedValueCitizen);

        // If no saved selection, default to first option
        if (!selectedValueCitizen) {
            selectedValueCitizen = $('#<%=drp_mauritius_citizenship.ClientID%> option:first').val();
            passport_no.style.display = 'none';
            nic_number.style.display = 'block';
        }


        $('#<%= drp_mauritius_citizenship.ClientID %>').change(function () {

            var selectedValueCitizen = $(this).val();

            sessionStorage.setItem('selectedOptionCitizen', selectedValueCitizen);
            handlePassPortField(selectedValueCitizen);

            // Add your code here
        });


         $('#<%=txt_date_birth.ClientID%>').change(function () {

            var dateBirth = new Date($(this).val());

            if (dateBirth) {

            
                var age = new Date().getFullYear() - dateBirth.getFullYear();

                if (dateBirth.setFullYear(age) > new Date()) {
                    age--;
                }

                $('#<%=txt_age.ClientID%>').val(age);

                Session["Age"] = age;

            }

        });


    });

    
    function handlePassPortField(selectedValue) {
        var passport_no = document.getElementById('passport_no');
        var nic_number = document.getElementById('txt_nic_div');

        if (selectedValue === 'Yes') {
            // Show NIC number
            if (passport_no.style.display !== 'none') {
                passport_no.style.display = "none";
            }
            nic_number.style.display = "block";

        } else {
            // Show passport number
            if (nic_number.style.display !== 'none') {
                nic_number.style.display = "none";
            }
            passport_no.style.display = "block";
        }
    }

    function handleFieldDisplay(selectedValue) {

        var ph_occupation_employed_1 = document.getElementById('job_title');
        var ph_occupation_employed_2 = document.getElementById('sector');
        var ph_occupation_student = document.getElementById('grade');
        var gov_employer = document.getElementById('employer_gov');
        var private_employer = document.getElementById('private_employer');
        var other_employed = document.getElementById('other');
        var passport_no = document.getElementById('passport_no');

        if (selectedValue === 'Employed') {
            ph_occupation_employed_1.style.display = "block";
            ph_occupation_employed_2.style.display = "block";
            ph_occupation_student.style.display = "none";
            gov_employer.style.display = "block";
            other_employed.style.display = "none";



        }
        else if (selectedValue === 'Student') {
            ph_occupation_employed_1.style.display = "none";
            ph_occupation_employed_2.style.display = "none";
            ph_occupation_student.style.display = "block";
            private_employer.style.display = "none";
            gov_employer.style.display = "none";
            other_employed.style.display = "none";




        }
        else if (selectedValue === 'Other') {
            ph_occupation_employed_1.style.display = "none";
            ph_occupation_employed_2.style.display = "none";
            ph_occupation_student.style.display = "none";
            private_employer.style.display = "none";
            gov_employer.style.display = "none";
            other_employed.style.display = "block";
        }


        else {
            ph_occupation_employed_1.style.display = "none";
            ph_occupation_employed_2.style.display = "none";
            ph_occupation_student.style.display = "none";
            private_employer.style.display = "none";
            gov_employer.style.display = "none";
            other_employed.style.display = "none";


        }

    }


    
      
        
</script>



<style type="text/css">
    .auto-style1 {
        height: 26px;
    }

    body {
        font-family: Arial, Helvetica, sans-serif;
    }

#s4-ribbonrow, .bottom-nav-container, .ms-cui-topBar2, .s4-notdlg, .s4-pr s4-ribbonrowhidetitle, .s4-notdlg noindex, #ms-cui-ribbonTopBars, #s4-titlerow, #s4-pr s4-notdlg s4-titlerowhidetitle, #s4-leftpanel-content {
    display: none !important;
 }

#sector {
  margin-left: 20px;
}

#sector label {
  font-weight: bold;
  margin-bottom: 5px;
}

.modal {
  position: fixed;
  top: 50%;
  left: 50%;
  transform: translate(-50%, -50%);
  z-index: 1000;
}

.modal-content {
  background-color: white;
  border: 1px solid #ccc;
  padding: 20px;
  width: 80%;
  box-shadow: 0 4px 8px 0 rgba(0,0,0,0.2);
}

#sector input[type="radio"] {
  margin-bottom: 5px;
}

#sector input[type="radio"] + label {
  margin-left: 10px;
}

#sector input[type="radio"]:checked + label {
  font-weight: bold;
}



/* .s4-ca { */
    /* margin-left: 0px !important; */
    /* margin-right: 0px !important; */
/* } */

.ms-core-navigation { 
     display: none; 
 }

/* #contentBox { */
    /* margin-left: 0; */
/* } */

.ms-webpart-titleText {
    display: none;
}

.removeCaps {
    text-transform: none !important;
}

/* The Modal (background) */
.modal {
    display: none; /* Hidden by default */
    position: fixed; /* Stay in place */
    z-index: 1; /* Sit on top */
    padding-top: 100px; /* Location of the box */
    left: 0;
    top: 0;
    width: 100%; /* Full width */
    height: 100%; /* Full height */
    overflow: auto; /* Enable scroll if needed */
    background-color: rgb(0,0,0); /* Fallback color */
    background-color: rgba(0,0,0,0.4); /* Black w/ opacity */
}

/* Modal Content */
.modal-content {
    background-color: #fefefe;
    margin: auto;
    padding: 20px;
    border: 1px solid #888;
    width: 80%;
}

/* The Close Button */
.close {
    color: #aaaaaa;
    float: right;
    font-size: 28px;
    font-weight: bold;
}

    .close:hover,
    .close:focus {
        color: #000;
        text-decoration: none;
        cursor: pointer;
    }

.DPCApplication {
    max-width: 100%;
}

    .DPCApplication .form-row:not(#btnRow) {
        display: flex;
        flex-wrap: wrap;
        max-width: 80%;
        margin: 0 auto;
        float: none;
    }

#btnRow {
    max-width: 85%;
    margin: 0 auto;
    float: none;
    text-align: center;
}

.DPCApplication .form-fields.submit input {
    width: 200px;
    padding: 12px 30px;
    margin: 0 auto;
    border: none;
    background-color: #799951;
    color: #fff;
    font-size: 20;
    font-weight: 500;
}

.DPCApplication .form-fields.submit a {
    text-decoration: none;
    display: block;
    width: 140px;
    padding: 12px 30px;
    margin: 0 auto;
    border: none;
    background-color: #799951;
    color: #fff;
    font-size: 20;
    font-weight: 500;
}

    .DPCApplication .form-fields.submit input:hover, .DPCApplication .form-fields.submit a:hover {
        background-color: #02AFEF;
        cursor: pointer;
    }

.DPCApplication .form-fields.submitTC input {
    width: 250px;
    padding: 12px 30px;
    margin: 0 auto;
    border: none;
    background-color: #799951;
    color: #fff;
    font-size: 20;
    font-weight: 500;
}

    .DPCApplication .form-fields.submitTC input:hover {
        background-color: #02AFEF;
        cursor: pointer;
    }

#txt_trainingCentreCode {
    margin-top: 4px;
}

.new-title-block {
    width: 77%;
    position: relative;
    float: left;
    margin-top: 30px;
    margin-bottom: 15px;
}

.inner-sub-title {
    font-size: 52px;
    font-weight: 700 !important;
    color: #799951;
}

.sub-title {
    font-size: 25px;
    font-weight: 600 !important;
    color: #799951;
}

span.tooltip {
    color: #ff0000;
    font-size: 22px;
    line-height: 26px;
    cursor: pointer;
}

span.mandatory {
    background: #465159;
    color: #fff;
    font-size: 12px;
    line-height: 18px;
    display: inline-block;
    margin-left: 10px;
    opacity: 0;
    visibility: hidden;
    position: absolute;
    max-width: 0;
    padding: 5px 8px;
    border-radius: 3px;
    transition: all 0.5s ease-out 0s;
}

span.tooltip:hover + span.mandatory {
    opacity: 1;
    visibility: visible;
    max-width: 100%;
}

.form-fields {
    position: relative;
    margin: 2% 2% 0;
}

    .form-fields.w100 {
        width: 100%;
    }

        .form-fields.w100.field-med input, .form-fields.w100.field-med select {
            width: 46%;
        }

        .form-fields.w100.field-sm input, .form-fields.w100.field-sm select {
            width: 29.3%;
        }

    .form-fields.w50 {
        width: 46%;
    }

    .form-fields.w30 {
        width: 29.3%;
    }

    .form-fields.w70 {
        width: 65.7%;
    }

@media screen and (max-width: 900px) {
    .form-fields.w100.field-sm input, .form-fields.w100.field-sm select {
        width: 100%;
    }

    .form-fields.w30 {
        width: 100%;
    }

    .form-fields.w70 {
        width: 100%;
    }

    .form-fields.w50 {
        width: 100%;
    }

    .form-fields.w100.field-med input, .form-fields.w100.field-med select {
        width: 100%;
    }

    .form-fields-sm-tit {
        margin: 15px 0 0;
    }

    .form-fields {
        position: relative;
        margin: 5% 0 0;
    }

        .form-fields input[type='text'], .form-fields select {
            height: 40px;
        }
}

.form-row .form-fields label {
    font-size: 14px;
    line-height: 22px;
    font-weight: bold;
    color: #000000a3;
}

.form-fields input[type='text'], .form-fields select {
    width: 100%;
    height: 35px;
    border: 1px solid #c5c5c5;
    font-size: 14px;
    line-height: 22px;
    color: #47515a;
    border-radius: 0;
    background-color: transparent;
    /*    /*text-transform: capitalize;*/*/
    padding: 0 15px;
}

.form-fields textarea {
    width: 100%;
    min-height: 100px;
    border: 1px solid #c5c5c5;
    font-size: 14px;
    line-height: 22px;
    color: #47515a;
    border-radius: 0;
    background-color: transparent;
    /*    /*text-transform: capitalize;*/*/
    padding: 15px;
    margin-top: 4px;
    font-family: 'Roboto', sans-serif;
}

.form-fields input[type='checkbox'] {
    margin-right: 3.5%;
    height: 17px;
    width: 17px;
}

label.lbl_days_text {
    vertical-align: text-bottom;
    font-size: 16px !important;
    font-weight: 500 !important;
}

#tblCourses {
    border-collapse: collapse;
    width: 100%;
}

    #tblCourses td, #tblCourses th {
        border: 1px solid #ddd;
        padding: 8px;
    }

    #tblCourses tr:nth-child(even) {
        background-color: #f2f2f2;
    }

    #tblCourses tr:hover {
        background-color: #ddd;
    }

    #tblCourses thead {
        padding-top: 12px;
        padding-bottom: 12px;
        text-align: left;
        background-color: #799951;
        color: white;
        font-weight: 600;
    }

.not-mandatory {
    margin-top: 4px;
}

.alert {
    padding: 15px;
    margin-bottom: 20px;
    border: 1px solid transparent;
    border-radius: 4px;
    display: flex;
    align-items: center;
}

    .alert:before {
        content: '';
        position: absolute;
        width: 0;
        height: calc(100% - 44px);
        border-left: 1px solid;
        border-right: 2px solid;
        border-bottom-right-radius: 3px;
        border-top-right-radius: 3px;
        left: 0;
        top: 50%;
        transform: translate(0,-50%);
        height: 20px;
    }

    .alert > .start-icon {
        margin-right: 0;
        min-width: 20px;
        text-align: center;
        margin-right: 5px;
        float: left;
    }

.alert-simple.alert-info {
    border: 1px solid rgba(6, 44, 241, 0.46);
    background-color: rgba(7, 73, 149, 0.12156862745098039);
    box-shadow: 0px 0px 2px #0396ff;
    color: #0396ff;
    transition: 0.5s;
}

.alert-info:hover {
    background-color: rgba(7, 73, 149, 0.35);
    transition: 0.5s;
}

#radioBtn_public {
    margin-top: 10px;
}

#radioBtn_private {
    margin-left: 10%;
}


.custom-gridview td, .custom-gridview th {
    border: 1px solid #ddd;
    padding: 8px;
}

.custom-gridview tr:nth-child(even) {
    background-color: #f2f2f2;
}

.custom-gridview tr:hover {
    background-color: #ddd;
}

.custom-gridview th {
    padding-top: 12px;
    padding-bottom: 12px;
    text-align: left;
    background-color: #799951;
    color: white;
}

.custom-gridview tbody tr td a {
    text-decoration: none;
}

.declaration {
    color: red;
}

.declarationText {
    display: inline-flex;
    margin-top: 10px;
    margin-left: 2%;
    font-size: 14px;
    font-weight: 500;
}

.inline {
    display: inline-flex;
    margin-top: 10px;
    font-size: 15px;
}

    .inline input {
        margin-right: 8px;
        margin-top: 3.5px;
        font-size: 15px;
    }

    .inline label {
        cursor: pointer;
        margin-right: 25px;
        font-size: 15px;
    }

    body {
    font-family: Arial, Helvetica, sans-serif;
    padding: 0;
    margin: 0;
}

h2 {
    text-align: center;
    font-size: 40px;
    margin: 0;
    font-weight: 300;
    color: inherit;
    padding: 50px;
}

.center {
    text-align: center;
}

section {
    height: 5%;
}

/* NAVIGATION */
nav {
    width: 80%;
    margin: 0 auto;
    background: #fff;
    padding: 5px 0;
}

nav ul {
    list-style: none;
    text-align: left;
}

nav ul li {
    display: inline-block;
}

nav ul li a {
    display: block;
    padding: 15px;
    text-decoration: none;
    color: #aaa;
    font-weight: 600;
    text-transform: uppercase;
    margin: 0 10px;
}

nav ul li a,
nav ul li a:after,
nav ul li a:before {
    transition: all .5s;
}

nav ul li a:hover {
    color: #555;
}

/* stroke */
nav.stroke ul li a,
nav.fill ul li a {
    position: relative;
}

nav.stroke ul li a:after,
nav.fill ul li a:after {
    position: absolute;
    bottom: 0;
    left: 0;
    right: 0;
    margin: auto;
    width: 0%;
    content: '.';
    color: transparent;
    background: #333;
    height: 1px;
}

nav.stroke ul li a:hover:after {
    width: 100%;
}

nav.fill ul li a {
    transition: all 2s;
}

nav.fill ul li a:after {
    text-align: left;
    content: '.';
    margin: 0;
    opacity: 0;
}

nav.fill ul li a:hover {
    color: #023f1c;
    z-index: 1;
}

nav.fill ul li a:hover:after {
    z-index: -10;
    animation: fill 1s forwards;
    -webkit-animation: fill 1s forwards;
    -moz-animation: fill 1s forwards;
    opacity: 1;
}


</style>





<div id="content-container">

    
    <div class="cnWrapper">
        <div class="DPCApplication" >
            <form class="application_form">
         
                <section>
                    <nav class="stroke">
                        <ul>
                            <li><a id="homePage" runat="server">Return To List Of Courses</a></li>
                        </ul>
                    </nav>
                </section>

                <asp:HiddenField ID="hdn_username" runat="server" />

                <div><asp:Button ID="btn_populateUserData" OnClick="PopulateUserData_Click" runat="server" /></div>


                <div id="dpcFormApp" runat="server">

                <div class="form-row new-title-block">
                    <h3 class="inner-sub-title"> DPC Application Form</h3>
                </div>
                <div class="form-row">

                    <div class="form-fields w30">
                        <asp:Label ID="lbl_dcpBatchCourse" AssociatedControlID="txt_dcpBatchCourse" runat="server" Text="Batch"></asp:Label>
                        <asp:TextBox ID="txt_dcpBatchCourse" runat="server" ReadOnly="true"></asp:TextBox>
                    </div>

                    <div class="form-fields w30">
                        <asp:Label ID="lbl_batchNumber" AssociatedControlID="txt_batchNumber" runat="server" Text="Batch Number"></asp:Label>
                        <asp:TextBox ID="txt_batchNumber" runat="server" ReadOnly="true"></asp:TextBox>
                    </div>

                    <div class="form-fields w30">
                        <asp:Label ID="lbl_location" AssociatedControlID="txt_location" runat="server" Text="Location"></asp:Label>
                        <asp:TextBox ID="txt_location" runat="server" ReadOnly="true"></asp:TextBox>
                    </div>


                </div>


                <div class="form-row">
                    <div class="form-fields w30">
                        <asp:Label ID="lbl_numberOfDays" AssociatedControlID="txt_numberOfDays" runat="server" Text="Number of Days"></asp:Label>
                        <asp:TextBox ID="txt_numberOfDays" runat="server" ReadOnly="true"></asp:TextBox>
                    </div>
                    <div class="form-fields w30">
                        <asp:Label ID="lbl_startEndTime" AssociatedControlID="txt_startEndTime" runat="server" Text="Start and End Time"></asp:Label>
                        <asp:TextBox ID="txt_startEndTime" runat="server" ReadOnly="true"></asp:TextBox>
                    </div>
                    <div class="form-fields w30">
                        <asp:Label ID="lbl_duration" AssociatedControlID="txt_duration" runat="server" Text="Duration (weeks)"></asp:Label>
                        <asp:TextBox ID="txt_duration" runat="server" ReadOnly="true"></asp:TextBox>
                    </div>
                </div>
                <div class="form-row">
                    <div class="form-fields w100">
                        <asp:Label ID="lbl_batchDetails" AssociatedControlID="txt_batchDetails" runat="server" Text="Batch Details"></asp:Label>
                        <asp:TextBox ID="txt_batchDetails" TextMode="MultiLine" Rows="4" runat="server" ReadOnly="true"></asp:TextBox>
                    </div>
                </div>
                <div class="form-row">
                    <div class="form-fields w50">
                        <asp:Label ID="lbl_startDate" AssociatedControlID="txt_startDate" runat="server" Text="Start Date"></asp:Label>
                        <asp:TextBox ID="txt_startDate" runat="server" ReadOnly="true"></asp:TextBox>
                    </div>
                    <div class="form-fields w50">
                        <asp:Label ID="lbl_endDate" AssociatedControlID="txt_endDate" runat="server" Text="End Date"></asp:Label>
                        <asp:TextBox ID="txt_endDate" runat="server" ReadOnly="true"></asp:TextBox>
                    </div>
                </div>


                <div class="form-row">
                    <div class="form-fields w100">
                        <h3 class="sub-title">Personal Details</h3>
                    </div>
                    <div class="form-fields w50">
                        <asp:Label ID="lbl_mauritius_citizenship" AssociatedControlID="drp_mauritius_citizenship" runat="server" Text="Are you a citizen of Mauritius?"></asp:Label>
                        <asp:DropDownList ID="drp_mauritius_citizenship" runat="server">
                            <asp:ListItem Value="Yes">Yes</asp:ListItem>
                            <asp:ListItem Value="No">No</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="form-fields w50">
                        <asp:Label ID="lbl_nationality" AssociatedControlID="drp_nationality" runat="server" Text="Nationality"></asp:Label>
                        <asp:DropDownList ID="drp_nationality" runat="server"></asp:DropDownList>
                    </div>
                </div>
                <div class="form-row">
                    <div class="form-fields w50" id="txt_nic_div">
                        <asp:Label ID="lbl_nic" AssociatedControlID="txt_nic" runat="server" Text="National Identity Card No. (NIC) <span class='tooltip'>*</span> <span class='mandatory'>Mandatory field</span>"></asp:Label>
        <%--                <asp:RequiredFieldValidator ID="req_nic" ControlToValidate="txt_nic" ErrorMessage="Required" EnableClientScript="true" ForeColor="Red" ValidationGroup="vg_ApplicationForm" SetFocusOnError="true" Display="Dynamic" runat="server" />--%>
                        <asp:TextBox ID="txt_nic" runat="server"></asp:TextBox>
                    </div>
                    <div class="form-fields w50" id="passport_no">
                        <asp:Label ID="lbl_passport_no" AssociatedControlID="txt_passport_no" runat="server"
                            Text="Passport Number <span class='tooltip'>*</span> <span class='mandatory'>Mandatory field</span>"></asp:Label>
                        <asp:TextBox ID="txt_passport_no" runat="server"></asp:TextBox>
                  <%--      <asp:RequiredFieldValidator ID="req_passport_no" ControlToValidate="txt_passport_no" ErrorMessage="Required" ForeColor="Red" EnableClientScript="true" ValidationGroup="vg_ApplicationForm" SetFocusOnError="true" Display="Dynamic" runat="server" />
                        <asp:RegularExpressionValidator ID="re_passport_no" ControlToValidate="txt_passport_no" ErrorMessage="Passport number must consist of alphanumeric characters."
                            ValidationExpression="^[a-zA-Z][a-zA-Z0-9]*$" ForeColor="Red" Enabled="false" SetFocusOnError="true" Display="Dynamic" runat="server" />--%>
                    </div>
                </div>
                <div class="form-row">
                    <div class="form-fields w50">
                        <asp:Label ID="lbl_title" AssociatedControlID="drp_title" runat="server" Text="Title"></asp:Label>
                      <%--  <asp:RequiredFieldValidator ID="req_title" ControlToValidate="drp_title" ErrorMessage="Required" ForeColor="Red"
                            SetFocusOnError="true" Display="Dynamic" runat="server" ValidationGroup="vg_ApplicationForm" EnableClientScript="true"/>--%>
                        <asp:DropDownList ID="drp_title" runat="server">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="form-row">
                    <div class="form-fields w50">
                        <asp:Label ID="lbl_surname" AssociatedControlID="txt_surname" runat="server" Text="Surname <span class='tooltip'>*</span> <span class='mandatory'>Mandatory field</span>"></asp:Label>
                     <%--   <asp:RequiredFieldValidator ID="req_surname" ControlToValidate="txt_surname" ErrorMessage="Required" ForeColor="Red"
                            SetFocusOnError="true" Display="Dynamic" runat="server" ValidationGroup="vg_ApplicationForm" EnableClientScript="true"/>--%>
                        <asp:TextBox ID="txt_surname" runat="server"></asp:TextBox>
                    </div>
                    <asp:UpdatePanel ID="up_maiden_name" class="form-fields w50" runat="server" style="margin-top: 1.3%;">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="drp_title"/>
                        </Triggers>
                        <ContentTemplate>
                            <asp:PlaceHolder ID="ph_maiden_name" runat="server">
                                <div class="form-fields w50">
                                    <asp:Label ID="lbl_maiden_name" AssociatedControlID="txt_maiden_name" runat="server" Text="Surname at Birth (If applicable) "></asp:Label>
                                    <asp:TextBox ID="txt_maiden_name" runat="server"></asp:TextBox>
                                </div>
                            </asp:PlaceHolder>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="form-row">
                    <div class="form-fields w50">
                        <asp:Label ID="lbl_other_name" AssociatedControlID="txt_other_name" runat="server" Text="Other Name(s)  <span class='tooltip'>*</span> <span class='mandatory'>Mandatory field</span>"></asp:Label>
              <%--          <asp:RequiredFieldValidator ID="req_other_name" ControlToValidate="txt_other_name" ErrorMessage="Required" ForeColor="Red"
                            SetFocusOnError="true" Display="Dynamic" runat="server" ValidationGroup="vg_ApplicationForm" EnableClientScript="true"/>--%>
                        <asp:TextBox ID="txt_other_name" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="form-row">
                    <div class="form-fields w30">
                        <asp:Label ID="lbl_date_birth" AssociatedControlID="txt_date_birth" runat="server" Text="Date of Birth (DOB) <span class='tooltip'>*</span> <span class='mandatory'>Mandatory field</span>"></asp:Label>
                        <asp:TextBox ID="txt_date_birth" runat="server" AutoPostBack="false"></asp:TextBox>
                        <asp:CalendarExtender ID="cal_ext_date_birth" TargetControlID="txt_date_birth" runat="server" Format="dd/MM/yyyy"></asp:CalendarExtender>
                        <asp:MaskedEditExtender ID="mee_date_birth" TargetControlID="txt_date_birth" Mask="99/99/9999" MaskType="Date" UserDateFormat="DayMonthYear" runat="server" />
                        <asp:MaskedEditValidator ID="mev_date_birth" ControlExtender="mee_date_birth" ControlToValidate="txt_date_birth"
                            InvalidValueMessage="Invalid date" ForeColor="Red" Display="Dynamic" runat="server" ValidationGroup="vg_ApplicationForm" EnableClientScript="true"></asp:MaskedEditValidator>
                    </div>

               <%--    <asp:UpdatePanel ID="up_date_birth" class="form-fields w50" UpdateMode="Conditional" runat="server">
            
                        <ContentTemplate>--%>
                            <div class="form-fields w30">
                                <asp:Label ID="lbl_age" AssociatedControlID="txt_age" runat="server" Text="Age"></asp:Label>
                                <asp:TextBox ID="txt_age" runat="server" ReadOnly="true"></asp:TextBox>
                            </div>
           <%--          </ContentTemplate>
                    </asp:UpdatePanel>--%>


                    <div class="form-fields w30" style="margin-top: 16px;">
                        <asp:Label ID="lbl_gender" AssociatedControlID="drp_gender" runat="server" Text="Gender <span class='tooltip'>*</span> <span class='mandatory'>Mandatory field</span>"></asp:Label>
       
<%--                        <asp:TextBox ID="txt_gender" runat="server"></asp:TextBox>--%>

                        <asp:DropDownList ID="drp_gender" runat="server">
                            <asp:ListItem Text="Please select.." Value="select"></asp:ListItem>
                            <asp:ListItem Text="Male" Value="Male"></asp:ListItem>
                            <asp:ListItem Text="Female" Value="Female"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>


                <div class="form-row">
                    <div class="form-fields w100">
                        <h3 class="sub-title">Residential Address</h3>
                    </div>
                    <div class="form-fields w30">
                        <asp:Label ID="lbl_address_postalCode" AssociatedControlID="txt_address_postalCode" runat="server" Text="Postal Code"></asp:Label>

                        <asp:TextBox ID="txt_address_postalCode" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="form-row">
                    <div class="form-fields w50">
                        <asp:Label ID="lbl_address_line1" AssociatedControlID="txt_address_line1" runat="server" Text="Address Line 1 <span class='tooltip'>*</span> <span class='mandatory'>Mandatory field</span>"></asp:Label>
                  <%--      <asp:RequiredFieldValidator ID="req_address_line1" ControlToValidate="txt_address_line1" ErrorMessage="Required" ForeColor="Red"
                            SetFocusOnError="true" Display="Dynamic" runat="server" EnableClientScript="true" />--%>
                        <asp:TextBox ID="txt_address_line1" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="form-row">
                    <div class="form-fields w50">
                        <asp:Label ID="lbl_address_line2" AssociatedControlID="txt_address_line2" runat="server" Text="Address Line 2"></asp:Label>
                        <asp:TextBox ID="txt_address_line2" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="form-row">
                    <div class="form-fields w50">
                        <asp:Label ID="lbl_address_line3" AssociatedControlID="txt_address_line3" runat="server" Text="Locality / Town / Village "></asp:Label>
                        <asp:TextBox ID="txt_address_line3" runat="server"></asp:TextBox>
                    </div>
                </div>


                <div class="form-row">

                    <div class="form-fields w100">
                        <h3 class="sub-title">Contact Details</h3>
                    </div>


                    <div class="form-fields w30">
                        <asp:Label ID="lbl_lbl_phoneNum_home" AssociatedControlID="txt_phoneNum_home" runat="server" Text="Home"></asp:Label>
                        <asp:TextBox ID="txt_phoneNum_home" runat="server"></asp:TextBox>

                    </div>
                    <div class="form-fields w30">
                        <asp:Label ID="lbl_phoneNum_office" AssociatedControlID="txt_phoneNum_office" runat="server" Text="Office"></asp:Label>
                        <asp:TextBox ID="txt_phoneNum_office" runat="server"></asp:TextBox>

                    </div>
                    <div class="form-fields w30" style="margin-top: 1.7%;">
                        <asp:Label ID="lbl_phoneNum_mobile" AssociatedControlID="txt_phoneNum_mobile" runat="server" Text="Mobile <span class='tooltip'>*</span> <span class='mandatory'>Mandatory field</span>"></asp:Label>
                        <asp:TextBox ID="txt_phoneNum_mobile" runat="server"></asp:TextBox>
             <%--           <asp:RequiredFieldValidator ID="req_phoneNum_mobile" ControlToValidate="txt_phoneNum_mobile" ErrorMessage="Required" ForeColor="Red"
                            SetFocusOnError="true" Display="Dynamic" runat="server" ValidationGroup="vg_ApplicationForm" EnableClientScript="true"/>--%>
                    </div>
                </div>
                <div class="form-row">
                    <div class="form-fields w50">
                        <asp:Label ID="lbl_email_address" AssociatedControlID="txt_email_address" runat="server" Text="Email <span class='tooltip'>*</span> <span class='mandatory'>Mandatory field</span>"></asp:Label>
                        <asp:TextBox ID="txt_email_address" runat="server" CssClass="removeCaps"></asp:TextBox>
    <%--                    <asp:RequiredFieldValidator ID="req_email_address" ControlToValidate="txt_email_address" ErrorMessage="Required" ForeColor="Red"
                            SetFocusOnError="true" Display="Dynamic" runat="server" ValidationGroup="vg_ApplicationForm" EnableClientScript="true"/>--%>
                    </div>
                </div>


                <div class="form-row">

                    <div class="form-fields w100">
                        <h3 class="sub-title">Employment Details</h3>
                    </div>

                    <div class="form-fields w30">
                        <asp:Label ID="lbl_occupation" AssociatedControlID="drp_occupation" runat="server" Text="Occupation <span class='tooltip'>*</span> <span class='mandatory'>Mandatory field</span>"></asp:Label>
                        <asp:DropDownList ID="drp_occupation" runat="server">
                            <asp:ListItem Text="Employed" Value="Employed"></asp:ListItem>
                            <asp:ListItem Text="Unemployed" Value="Unemployed"></asp:ListItem>
                            <asp:ListItem Text="Self-Employed" Value="Self-Employed"></asp:ListItem>
                            <asp:ListItem Text="Student" Value="Student"></asp:ListItem>
                            <asp:ListItem Text="University Student" Value="University Student"></asp:ListItem>
                            <asp:ListItem Text="Senior Citizen" Value="Senior Citizen"></asp:ListItem>
                            <asp:ListItem Text="Housewife" Value="Housewife"></asp:ListItem>
                            <asp:ListItem Text="Retired" Value="Retired"></asp:ListItem>
                            <asp:ListItem Text="None" Value="None"></asp:ListItem>
                            <asp:ListItem Text="Other" Value="Other"></asp:ListItem>
                        </asp:DropDownList>
                    </div>


<%--                   <asp:UpdatePanel ID="up_occupation" class="form-fields w50" UpdateMode="Conditional" runat="server" ChildrenAsTriggers="true">
                        <Triggers>
<%--                            <asp:AsyncPostBackTrigger ControlID="drp_occupation" EventName="SelectedIndexChanged"/>--%>
<%--                        </Triggers>
                        <ContentTemplate>--%>
<%--                            <asp:PlaceHolder ID="ph_occupation_employed_1" runat="server">--%>

                                <div class="form-fields w30" id="job_title" style="margin-top: 2.7%;">
                                    <asp:Label ID="lbl_job_title" AssociatedControlID="txt_job_title" runat="server" Text="Job Title <span class='tooltip'>*</span> <span class='mandatory'>Mandatory field</span>"></asp:Label>
                                    <asp:TextBox ID="txt_job_title" runat="server"></asp:TextBox>
                            <%--        <asp:RequiredFieldValidator ID="req_job_title" ControlToValidate="txt_job_title" ErrorMessage="Required" ForeColor="Red"
                                        SetFocusOnError="true" Display="Dynamic" runat="server" ValidationGroup="vg_ApplicationForm" EnableClientScript="true"/>--%>
                                </div>

                            <%--    </asp:PlaceHolder>--%>

                         <%--   <asp:PlaceHolder ID="ph_occupation_employed_2" runat="server">--%>

<%--                                <div class="form-fields w30" id="sector">
                                    <asp:Label ID="label_sector" runat="server" Text="Sector:"></asp:Label>
                                    <asp:RadioButton ID="radioBtn_public" runat="server"  Text="Public" GroupName="sectorBtns" Checked="true"   />
                                    <asp:RadioButton ID="radioBtn_private" runat="server"  Text="Private" GroupName="sectorBtns"   />
                                </div>--%>

                    <div class="form-fields w30" id="sector" style="margin-top: 3%;">
                        <asp:Label ID="label_sector" runat="server" Text="Sector:" Font-Bold="True" Font-Size="Large"></asp:Label>

<%--                        <asp:RadioButton ID="radioBtn_public" runat="server" GroupName="sectorBtns" Checked="true" />
                        <label for="radioBtn_public">Public</label> 
                        <br/>


                        <asp:RadioButton ID="radioBtn_private" runat="server" GroupName="sectorBtns" />
                        <label for="radioBtn_private">Private</label> --%>


                        <div class="btn-group" data-toggle="buttons">
                            <label class="btn btn-default">
                                <asp:RadioButton runat="server" ID="radioBtn_public" GroupName="sectorBtns" Checked="true" />
                                Public
                            </label>
                            <label class="btn btn-default">
                                <asp:RadioButton runat="server" ID="radioBtn_private" GroupName="sectorBtns" />
                                Private
                            </label>
                        </div>
                    </div>




<%--                            </asp:PlaceHolder>--%>


<%--                            <asp:PlaceHolder ID="ph_occupation_student" Visible="false" runat="server">--%>
                                <div class="form-fields w30" id="grade">
                                    <asp:Label ID="lbl_grade" AssociatedControlID="txt_grade" runat="server" Text="Grade <span class='tooltip'>*</span> <span class='mandatory'>Mandatory field</span>"></asp:Label>
                                    <asp:TextBox ID="txt_grade" runat="server"></asp:TextBox>
                       <%--             <asp:RequiredFieldValidator ID="req_grade" ControlToValidate="txt_grade" ErrorMessage="Required" ForeColor="Red"
                                        SetFocusOnError="true" Display="Dynamic" runat="server" ValidationGroup="vg_ApplicationForm" EnableClientScript="true"/>--%>
                                </div>
<%--                            </asp:PlaceHolder>--%>


<%--                        </ContentTemplate>
                    </asp:UpdatePanel>--%>
                </div>

                <div class="form-row">
                    <div class="form-fields w50" id="other">
                        <asp:Label ID="lbl_other" AssociatedControlID="txt_other" runat="server" Text="Other (please specify)"></asp:Label>
                        <asp:TextBox ID="txt_other" runat="server"></asp:TextBox>
                 <%--       <asp:RequiredFieldValidator ID="req_other" ControlToValidate="txt_other" ErrorMessage="Required" ForeColor="Red"
                            SetFocusOnError="true" Display="Dynamic" runat="server" ValidationGroup="vg_ApplicationForm" EnableClientScript="true"/>--%>
                    </div>
                </div>

                <div class="form-row">
<%--                    <asp:UpdatePanel ID="up_occupation_sector" class="form-fields w50" UpdateMode="Conditional" runat="server" ChildrenAsTriggers="true">--%>

<%--                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="radioBtn_public" EventName="CheckedChanged" />
                            <asp:AsyncPostBackTrigger ControlID="radioBtn_private" EventName="CheckedChanged" />
                            <asp:AsyncPostBackTrigger ControlID="drp_occupation" EventName="SelectedIndexChanged"/>
                        </Triggers>--%>

                       <%-- <ContentTemplate>--%>

<%--                            <asp:PlaceHolder ID="ph_sector_public" runat="server">--%>
                                <div class="form-fields w30" id="employer_gov">
                                    <asp:Label ID="lbl_employer1" AssociatedControlID="drp_employer1" runat="server" Text="Name of Employer <span class='tooltip'>*</span> <span class='mandatory'>Mandatory field</span>"></asp:Label>
                                    <asp:DropDownList ID="drp_employer1" runat="server" AutoPostBack="true">
                                        <asp:ListItem>NDU</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                       <%--     </asp:PlaceHolder>--%>


                            <%--<asp:PlaceHolder ID="ph_sector_private" Visible="false" runat="server">--%>
                                <div class="form-fields w30" id="private_employer">
                                    <asp:Label ID="lbl_employer2" AssociatedControlID="txt_employer2" runat="server" Text="Name of Employer <span class='tooltip'>*</span> <span class='mandatory'>Mandatory field</span>"></asp:Label>
                                    <asp:TextBox ID="txt_employer2" runat="server"></asp:TextBox>
                           <%--         <asp:RequiredFieldValidator ID="req_employer2" ControlToValidate="txt_employer2" ErrorMessage="Required" ForeColor="Red"
                                        SetFocusOnError="true" Display="Dynamic" runat="server" ValidationGroup="vg_ApplicationForm" EnableClientScript="true" />--%>
                                </div>

                    
                          <%--  </asp:PlaceHolder>--%>

                  <%--      </ContentTemplate>
                    </asp:UpdatePanel>--%>

<%--                    <div class="form-fields w30">
                        <asp:Label ID="lbl_institution" AssociatedControlID="txt_institution" runat="server" Text="Institution"></asp:Label>
                        <asp:TextBox ID="txt_institution" runat="server"></asp:TextBox>
                    </div>--%>

                </div>

                  <div class="form-row">
                    <!-- DECLARATION MSG -->
                    <div class="form-fields w100">
                        <asp:CheckBox ID="cb_declaration" runat="server" CssClass="inline" Text="I declare that the particulars in this application are true and accurate and that I have not wilfully suppressed any material fact." Font-Size="Large" />
                    </div>

                    <div class="form-fields w100">
                        <asp:UpdatePanel runat="server" UpdateMode="Always" ID="up_declaration" ChildrenAsTriggers="true">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="cb_declaration" />
                            </Triggers>
                            <ContentTemplate>
                                <asp:PlaceHolder ID="ph_declaration" Visible="false" runat="server">
                                    <div class="declaration" >
                                        <asp:Literal ID="declarationMsg" runat="server"></asp:Literal>
                                    </div>
                                </asp:PlaceHolder>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>

                <div class="form-row">
                    <div class="form-fields w30">
                        <asp:Label ID="lbl_date" AssociatedControlID="txt_date" runat="server" Text="Date"></asp:Label>
                        <asp:TextBox ID="txt_date" runat="server" ReadOnly="true"></asp:TextBox>
                    </div>
                    <div class="form-fields w30">
                        <asp:Label ID="lbl_signature" AssociatedControlID="txt_signature" runat="server" Text="Signature (Insert Full Name)"></asp:Label>
                        <asp:TextBox ID="txt_signature" runat="server" ></asp:TextBox>
                    </div>
                </div>
                  <div class="form-row">
                    <div class="form-fields w70">
                        <div class="alert alert-simple alert-info">
                            <p><i class="fa fa-info-circle"></i>    You should receive an Application id confirming that the application has gone through.</p>
                        </div>
                    </div>
                </div>

                <%--<div class="form-row">
                    <div class="form-fields w100 field-med">
                        <asp:Label ID="lbl_captcha" AssociatedControlID="captcha" Text="Captcha <span class='tooltip'>*</span> <span class='mandatory'>Mandatory field</span>" runat="server"></asp:Label>

                        <botdetect:captchavalidator ID="captcha_validator" runat="server"
                            ControlToValidate="txt_captcha_code" CaptchaControl="captcha"
                            ErrorMessage="Retype the characters exactly as they appear in the picture"
                            EnableClientScript="true" ForeColor="Red" SetFocusOnError="true" ValidationGroup="vg_ApplicationForm">
                            Incorrect CAPTCHA code
                        </botdetect:captchavalidator>
                        <botdetect:webformscaptcha ID="captcha" UserInputID="txt_captcha_code" runat="server" />            
                    </div>

                    <div class="form-fields w100 field-sm">
                        <asp:TextBox ID="txt_captcha_code" runat="server"></asp:TextBox>  
                    </div>
                </div>--%>
     

                <div class="form-row">
                    <div class="form-fields w50">
                        <asp:Label ID="lbl_payment" AssociatedControlID="drp_payment" runat="server" Text="Payment Method"></asp:Label>
                        <asp:DropDownList ID="drp_payment" runat="server" OnSelectedIndexChanged="drp_payment_SelectedIndexChanged" AutoPostBack="true">
                      

                                <asp:ListItem>Bank Card</asp:ListItem>
                            <asp:ListItem>Cash Payment</asp:ListItem>

                             

                        </asp:DropDownList>
                    </div>
                </div>
                <div class="form-row">

                    <asp:UpdatePanel ID="up_payment" class="form-fields w50" UpdateMode="Conditional" runat="server" ChildrenAsTriggers="true">

                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="drp_payment" EventName="SelectedIndexChanged" />
                        </Triggers>

                        <ContentTemplate>
                            <asp:PlaceHolder ID="ph_payment" Visible="false" runat="server">

                                <div class="form-fields w50">
                                    <asp:Label ID="lbl_serial_number" AssociatedControlID="txt_serial_number" runat="server" Text="Serial Number <span class='tooltip'>*</span> <span class='mandatory'>Mandatory field</span>"></asp:Label>
                                    <asp:TextBox ID="txt_serial_number" runat="server" AutoPostBack="true" onkeypress="return (event.charCode !=8 && event.charCode ==0 || (event.charCode >= 48 && event.charCode <= 57))"></asp:TextBox>
                         <%--           <asp:RequiredFieldValidator ID="req_serial_number" ControlToValidate="txt_serial_number" ErrorMessage="Required" ForeColor="Red"
                                        SetFocusOnError="true" Display="Dynamic" runat="server" ValidationGroup="vg_ApplicationForm" EnableClientScript="true" Enabled="false" />--%>
                                </div>

                                <div class="form-fields w50">
                                    <asp:Label ID="lbl_pin" AssociatedControlID="txt_pin" runat="server" Text="PIN <span class='tooltip'>*</span> <span class='mandatory'>Mandatory field</span>"></asp:Label>
                                    <asp:TextBox ID="txt_pin" runat="server" AutoPostBack="true" OnText="txt_pin_TextChanged"></asp:TextBox>
                                <%--    <asp:RequiredFieldValidator ID="req_pin" ControlToValidate="txt_pin" ErrorMessage="Required" ForeColor="Red"
                                        SetFocusOnError="true" Display="Dynamic" runat="server" ValidationGroup="vg_ApplicationForm" EnableClientScript="true" Enabled="false" />--%>
                                </div>

                            </asp:PlaceHolder>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>

                <div class="form-row">
                    <div class="form-fields w50">
                        <asp:Label ID="LabelMessage" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="form-row" id="btnRow">
                     <asp:Button ID="btn_submit" Text="Submit Application" OnClick="btn_submit_Click" runat="server" CausesValidation="false" /> 
                </div>

                  </div>
        
            </form>
        </div>
    </div>
    </div>


<%--</asp:Content>--%>



<asp:UpdatePanel ID="up_modal_popup" runat="server" ChildrenAsTriggers="true">

    <Triggers>
        <%--<asp:AsyncPostBackTrigger ControlID="btn_submit"/>--%>
    </Triggers>
    <ContentTemplate>
        <!-- The Modal -->
        <div id="div_submit_modal_confirmation" class="subject_modal" runat="server">
            <!-- Modal content -->
            <div class="modal-content">
                <div class="modal-header">
                    <asp:LinkButton runat="server" ID="btn_submit_close_modal" Text="&times;" CssClass="close" OnClick="btn_submit_close_modal_Click" />
                </div>
                <div class="modal-body">
                    <div class="modal-body-header">
                        <asp:Label runat="server" Text="Thank you for your application!"></asp:Label><br />
                        <br />
                        <asp:Label runat="server" Text="Your Application is hereby acknowledged"></asp:Label><br />
                        <br />
                        <asp:Label runat="server" ID="lbl_confirmation_requestid" Text="Application ID: "></asp:Label>
                        <br />
                    </div>
                    <div class="modal-body-eservice">
                        <asp:Label runat="server" Text="<span class='modal-body-labelhighlight'>E-Service Name:</span> Digital Proficiency Course"></asp:Label>
                         <br />
                    </div>
                    <div class="modal-body-appdetails">
                        <asp:Label ID="lbl_confirmation_nameofapplicant" runat="server" Text="Applicant Name: "></asp:Label><br />
                        <br />
                        <asp:Label ID="lbl_confirmation_batchApplied" runat="server" Text="Batch Applied For: "></asp:Label><br />
                        <br />
                        <asp:Label ID="lbl_confirmation_startDate" runat="server" Text="Start Date of Course: "></asp:Label><br />
                        <br />
                        <asp:Label ID="lbl_confirmation_endDate" runat="server" Text="End Date of Course: "></asp:Label><br />
                        <br />
                        <asp:Label ID="lbl_confirmation_duration" runat="server" Text="Duration of Course: "></asp:Label><br />
                        <br />
                        <asp:Label ID="lbl_confirmation_location" runat="server" Text="Location/Training centre: "></asp:Label><br />
                        <br />
                        <asp:Label ID="lbl_confirmation_applicationDateTime" runat="server" Text="Date and Time of Application: "></asp:Label><br />
                        <br />
                    </div>
                    <div class="modal-body-note">
                        <asp:Label runat="server" Text="Note that an email has been sent to the email address as provided in the application form"></asp:Label>
                    </div>
                    <div class="modal-body-referencenote">
                        <asp:Label runat="server" Text="Please note down your Application Id for future reference"></asp:Label>
                    </div>
                </div>
            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>

<asp:UpdatePanel ID="up_popup3" class="form-row" UpdateMode="Conditional" runat="server" ChildrenAsTriggers="true">
    <ContentTemplate>
        <div class="cd-popup3" role="alert" runat="server" id="popup3">
            <div class="cd-popup-container2">
                <div class="loader"></div>
            </div>
            <!-- cd-popup-container -->
        </div>
        <!-- cd-popup -->
    </ContentTemplate>
</asp:UpdatePanel>