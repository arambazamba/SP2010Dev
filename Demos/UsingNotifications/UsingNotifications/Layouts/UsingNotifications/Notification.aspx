<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Notification.aspx.cs" Inherits="UsingNotifications.Layouts.UsingNotifications.Notification" DynamicMasterPageFile="~masterurl/default.master" %>

<asp:Content ID="PageHead" ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">

</asp:Content>

<asp:Content ID="Main" ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <SharePoint:ScriptLink ID="ScriptLink1" Name="SP.js" runat="server" OnDemand="true" Localizable="false" />
    <SharePoint:ScriptLink ID="ScriptLink2" Name="/UsingNotifications/Notification.aspx.js" runat="server" OnDemand="false" Localizable="false" />

  <input id="Button1" type="button" value="Add Notification" onclick="AddNotification()" />
    <input id="Button2" type="button" value="Remove Notification" onclick="RemoveNotification()" />
    <p></p>
    <input id="Button3" type="button" value="Add Status" onclick="AddStatus()" />
    <input id="Button4" type="button" value="Remove Last Status" onclick="RemoveLastStatus()" />
    <input id="Button5" type="button" value="Remove All Status" onclick="RemoveAllStatus()" />

</asp:Content>

<asp:Content ID="PageTitle" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
Application Page
</asp:Content>

<asp:Content ID="PageTitleInTitleArea" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea" runat="server" >
My Application Page
</asp:Content>
