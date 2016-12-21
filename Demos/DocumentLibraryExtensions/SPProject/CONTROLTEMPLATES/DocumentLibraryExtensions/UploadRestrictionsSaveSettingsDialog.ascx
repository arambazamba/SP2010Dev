<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UploadRestrictionsSaveSettingsDialog.ascx.cs" Inherits="Integrations.DocumentLibraryExtensions.CONTROLTEMPLATES.DocumentLibraryExtensions.UploadRestrictionsSaveSettingsDialog" %>


  <asp:Label ID="lblList" runat="server" ></asp:Label> 
    <asp:CheckBox ID="chkFiletypes" AutoPostBack="True" OnCheckedChanged="EvalRestriction" runat="server" />

<p></p>
    <asp:Panel ID="pEdit" runat="server" Visible="False">
    
    <asp:Label ID="Label1" runat="server" Text="Please enter allowed File Types seperated by commas"></asp:Label><br />
    <asp:TextBox ID="txtFiletypes" runat="server"></asp:TextBox>
    <p></p>
    <asp:Button ID="btnSave" runat="server" Text="Apply" OnClick="SaveSettings"/></asp:Panel>