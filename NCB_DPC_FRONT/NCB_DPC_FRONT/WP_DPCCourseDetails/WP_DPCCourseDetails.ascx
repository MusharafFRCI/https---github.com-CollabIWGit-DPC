<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=16.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=16.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=16.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=16.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WP_DPCCourseDetails.ascx.cs" Inherits="NCB_DPC_FRONT.WP_DPCCourseDetails.WP_DPCCourseDetails" %>

<link href="/dpc/Site%20Assets/style.css" rel="stylesheet" type="text/css" />
<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css" />

<div id="content-container">
    <div class="cnWrapper">
        <div class="DPCApplication">
            <form>
                <div class="form-row new-title-block">
                    <h3 class="inner-sub-title">Course Details</h3>
                </div>
                <div class="form-row">
                    <div class="form-fields w30">
                        <asp:Label ID="lbl_courseName" AssociatedControlID="txt_courseName" runat="server" Text="Name of Course"></asp:Label>
                        <asp:TextBox ID="txt_courseName" runat="server" ReadOnly="true"></asp:TextBox>
                    </div>
                </div>
                <div class="form-row">
                    <div class="form-fields w50">
                        <asp:Label ID="lbl_description" AssociatedControlID="txt_description" runat="server" Text="Description"></asp:Label>
                        <asp:TextBox ID="txt_description" TextMode="MultiLine" Rows="4" runat="server" ReadOnly="true"></asp:TextBox>
                    </div>
                </div>
                <div class="form-row">
                    <div class="form-fields w50">
                        <asp:Label ID="lbl_startDateOfDPCCourse" AssociatedControlID="txt_startDateOfDPCCourse" runat="server" Text="Start Date"></asp:Label>
                        <asp:TextBox ID="txt_startDateOfDPCCourse" runat="server" ReadOnly="true"></asp:TextBox>
                    </div>
                    <div class="form-fields w50">
                        <asp:Label ID="lbl_endDateOfDPCCourse" AssociatedControlID="txt_endDateOfDPCCourse" runat="server" Text="End Date Of DPC Course"></asp:Label>
                        <asp:TextBox ID="txt_endDateOfDPCCourse" runat="server" ReadOnly="true"></asp:TextBox>
                    </div>
                </div>
                <div class="form-row">
                    <div class="form-fields w30">
                        <asp:Label ID="lbl_daysOfCourse" AssociatedControlID="txt_daysOfCourse" runat="server" Text="Days"></asp:Label>
                        <asp:TextBox ID="txt_daysOfCourse" runat="server" ReadOnly="true"></asp:TextBox>
                    </div>
                    <div class="form-fields w30">
                        <asp:Label ID="lbl_timeOfCourse" AssociatedControlID="txt_timeOfCourse" runat="server" Text="Time"></asp:Label>
                        <asp:TextBox ID="txt_timeOfCourse" runat="server" ReadOnly="true"></asp:TextBox>
                    </div>
                    <div class="form-fields w30">
                        <asp:Label ID="lbl_duration" AssociatedControlID="txt_duration" runat="server" Text="Duration (weeks)"></asp:Label>
                        <asp:TextBox ID="txt_duration" runat="server" ReadOnly="true"></asp:TextBox>
                    </div>
                </div>
                <div class="form-row">
                    <div class="form-fields w50">
                        <asp:Label ID="lbl_closingDate" AssociatedControlID="txt_closingDate" runat="server" Text="Closing Date"></asp:Label>
                        <asp:TextBox ID="txt_closingDate" runat="server" ReadOnly="true"></asp:TextBox>
                    </div>
                </div>
                <div class="form-row">
                    <div class="form-fields w50">
                        <asp:Label ID="lbl_location" AssociatedControlID="txt_location" runat="server" Text="Location"></asp:Label>
                        <asp:TextBox ID="txt_location" runat="server" ReadOnly="true"></asp:TextBox>
                    </div>
                    <div class="form-fields w50">
                        <asp:Label ID="lbl_attachments" runat="server" Text="Attachment (if any)"></asp:Label>
                    </div>
                </div>
                <div class="form-row" id="btnRow">
                    <div class="form-fields submit">
                        <a href="PageName.aspx?paramName=paramValue" id="btn_submit" runat="server">Apply</a>
                    </div>
                </div>
            </form>
        </div>
    </div>
</div>