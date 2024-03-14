<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=16.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=16.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=16.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=16.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DPCSuccessPage.ascx.cs" Inherits="NCB_DPC_FRONT.DPCSuccessPage.DPCSuccessPage" %>

<%--<link href="/dpc/Site%20Assets/style.css" rel="stylesheet" type="text/css" />
<link href="/dpc/Site%20Assets/loader.css" rel="stylesheet" type="text/css" />--%>

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
    text-transform: capitalize;
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
    text-transform: capitalize;
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

/**********LOADER*********/
.cd-popup3 {
    position: fixed;
    left: 0;
    top: 0;
    height: 100%;
    width: 100%;
    background-color: rgba(94, 110, 141, 0.9);
    opacity: 0;
    visibility: hidden;
    -webkit-transition: opacity 0.3s 0s, visibility 0s 0.3s;
    -moz-transition: opacity 0.3s 0s, visibility 0s 0.3s;
    transition: opacity 0.3s 0s, visibility 0s 0.3s;
    margin: auto;
}

    .cd-popup3.is-visible {
        opacity: 1;
        visibility: visible;
        -webkit-transition: opacity 0.3s 0s, visibility 0s 0s;
        -moz-transition: opacity 0.3s 0s, visibility 0s 0s;
        transition: opacity 0.3s 0s, visibility 0s 0s;
    }


@media only screen and (min-width: 1170px) {
    .cd-popup-container2 {
        margin: 8em auto;
    }
}

.cd-popup-container2 {
    position: relative;
    width: 90%;
    max-width: 400px;
    margin: 4em auto;
    border-radius: .25em .25em .4em .4em;
    text-align: center;
    -webkit-transform: translateY(-40px);
    -moz-transform: translateY(-40px);
    -ms-transform: translateY(-40px);
    -o-transform: translateY(-40px);
    transform: translateY(-40px);
    /* Force Hardware Acceleration in WebKit */
    -webkit-backface-visibility: hidden;
    -webkit-transition-property: -webkit-transform;
    -moz-transition-property: -moz-transform;
    transition-property: transform;
    -webkit-transition-duration: 0.3s;
    -moz-transition-duration: 0.3s;
    transition-duration: 0.3s;
}

cd-popup-container2 {
    margin: 0;
    padding: 0;
    border: 0;
    font: inherit;
    vertical-align: baseline;
}

.is-visible .cd-popup-container2 {
    -webkit-transform: translateY(0);
    -moz-transform: translateY(0);
    -ms-transform: translateY(0);
    -o-transform: translateY(0);
    transform: translateY(0);
}


.loader {
    border: 16px solid #f3f3f3;
    border-radius: 50%;
    border-top: 16px solid #3498db;
    width: 120px;
    height: 120px;
    -webkit-animation: spin 2s linear infinite; /* Safari */
    animation: spin 2s linear infinite;
    margin: auto;
    margin-top: 60%;
  
}

    .labelSuccess {
        padding-left: 38%;
        color: green;
    }

</style>

<section>
    <nav class="stroke">
        <ul>
            <li><a id="homePage" runat="server">Return To List Of Courses</a></li>
        </ul>
    </nav>
</section>


<asp:Label ID="Label1" runat="server" Text="Label" class="labelSuccess"></asp:Label>

<asp:UpdatePanel ID="up_modal_popup" runat="server">
    <Triggers>
        <%--<asp:AsyncPostBackTrigger ControlID="btn_submit"/>--%>
    </Triggers>
    <ContentTemplate>
        <!-- The Modal -->
        <div id="div_submit_modal_confirmation" class="subject_modal" runat="server">
         
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
                    </div>
                    <div class="modal-body-eservice">
                        <asp:Label runat="server" Text="<span class='modal-body-labelhighlight'>E-Service Name:</span> Digital Proficiency Course"></asp:Label>
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
                        <asp:Label runat="server" Text="Please note down your reference Id for future reference"></asp:Label>
                    </div>
                </div>
            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>