<%@ Assembly Name="Integrations.DocumentLibraryExtensions, Version=1.0.0.0, Culture=neutral, PublicKeyToken=07673538cb4c8315" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CreatorOwnerSaveSettingsDialog.ascx.cs" Inherits="Integrations.DocumentLibraryExtensions.CONTROLTEMPLATES.DocumentLibraryExtensions.CreatorOwnerSaveSettingsDialog" %>

 <asp:Label ID="lblInstuctions" runat="server" Text="Bitte wählen sie die Bibliotheken an welchen Creator-Owner-Permissions aktiviert werden sollen"></asp:Label>
    <p />
    <asp:CheckBoxList ID="chklbLibs" runat="server" />
    <br />
    <asp:Button ID="btnSave" runat="server" OnClick="SaveSettings"  Text="Anwenden" />
    <p />
    <asp:Label ID="lblStatus" runat="server" Text=""></asp:Label>
