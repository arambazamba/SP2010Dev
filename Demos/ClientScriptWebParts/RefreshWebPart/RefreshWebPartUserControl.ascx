<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RefreshWebPartUserControl.ascx.cs" Inherits="JQueryWebparts.RefreshWebPart.RefreshWebPartUserControl" %>
<asp:Timer ID="tTimer" runat="server" Interval="3000" ontick="TimerTicked">
</asp:Timer>
<asp:UpdatePanel ID="upContent" runat="server">
    <Triggers>
    <asp:AsyncPostBackTrigger ControlID="tTimer" EventName="Tick" />
</Triggers>
    <ContentTemplate>
        <asp:Label ID="lblStatus" runat="server" Text="Initial Value"></asp:Label>
    </ContentTemplate>
</asp:UpdatePanel>

